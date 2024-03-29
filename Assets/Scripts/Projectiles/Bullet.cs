using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private float speed = 5f;

    private float moveAmount = 0f;
    private float distance = 0f;

    private void OnEnable()
    {
        moveAmount = 0f;
    }

    public override void Launch(Unit target, DamageData damage)
    {
        base.Launch(target, damage);

        distance = (startPosition - targetPosition).magnitude;
    }

    private void Update()
    {
        if (target != null)
        {
            targetPosition = target.Visual.GetAnchor("Center").position;
        }

        moveAmount += speed * Time.deltaTime;

        Vector3 position = Vector3.Lerp(startPosition, targetPosition, moveAmount / distance);

        Quaternion rotation = Quaternion.LookRotation(position - transform.position, Vector3.up);

        transform.position = position;
        transform.rotation = rotation;

        if(moveAmount >= distance)
        {
            ReachTarget();
        }
    }

    private void ReachTarget()
    {
        if(target != null)
        {
            target.ApplyPotentialDamage(damage);
        }
        OnReachTarget?.Invoke();
    }
}
