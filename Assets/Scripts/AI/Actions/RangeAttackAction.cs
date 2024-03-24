using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class RangeAttackAction : AttackAction
{
    [SerializeField] private List<Transform> weapons;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private List<ParticleSystem> gunFxs;

    private ObjectPool<Projectile> projectilesPool;

    private Projectile CreateProjectile(Transform parent)
    {
        Projectile projectile = Instantiate(projectilePrefab, parent);
        projectile.OnReachTarget += () => projectilesPool.Release(projectile);
        return projectile;
    }

    private void OnGet(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }

    private void OnRelease(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }

    public override void StartAction(Parameters parameters)
    {
        base.StartAction(parameters);

        parameters.DamageData.SourceUnit.Animator.SetTrigger("Attack");

        StartCoroutine(LaunchProjectile(parameters));    
    }

    private IEnumerator LaunchProjectile(Parameters parameters)
    {
        yield return new WaitForSeconds(parameters.DamageDelay);

        if (projectilesPool == null)
        {
            projectilesPool = new ObjectPool<Projectile>(() => CreateProjectile(parameters.DamageData.SourceUnit.transform.parent),
                OnGet, OnRelease, (projectile) => Destroy(projectile.gameObject), true, 3, 50);
        }

        foreach (var weapon in weapons)
        {
            parameters.Target.AddPotentialDamage(parameters.DamageData);
            Projectile projectile = projectilesPool.Get();
            projectile.transform.position = weapon.position;
            projectile.transform.rotation = Quaternion.LookRotation(parameters.Target.Position - projectile.transform.position, Vector3.up);
            projectile.Launch(parameters.Target, parameters.DamageData);
        }      

        foreach (var fx in gunFxs)
            fx.Play();

        CompleteAction();
    }

    public override void StopAction()
    {
        base.StopAction();
        StopAllCoroutines();
        DOTween.Kill(this);
    }
}
