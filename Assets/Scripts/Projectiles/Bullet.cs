using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private float speed = 5f;

    private float moveAmount = 0f;

    private void OnEnable()
    {
        moveAmount = 0f;
    }

    private void Update()
    {
        if (target != null)
        {
            targetPosition = target.Visual.GetAnchor("Center").position;
        }

        moveAmount += speed * Time.deltaTime;

        Vector3 position = Vector3.Lerp(startPosition, targetPosition, moveAmount);

        Quaternion rotation = Quaternion.LookRotation(position - transform.position, Vector3.up);

        transform.position = position;
        transform.rotation = rotation;

        if(moveAmount >= 1f)
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
