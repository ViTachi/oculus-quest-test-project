using UnityEngine;

public class Bomb : Projectile
{
    [SerializeField] private StatTypeData explosionRadiusStat;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float height = 0.3f;
    [SerializeField] private LayerMask unitsMask;
    [SerializeField] private AnimationCurve heightCurve;
    [SerializeField] private ParticleSystem explosionFx;

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

        float y = (startPosition - targetPosition).magnitude
            * height * heightCurve.Evaluate(moveAmount);

        Vector3 position = Vector3.Lerp(startPosition, targetPosition, moveAmount)
            + Vector3.up * y;

        Quaternion rotation = Quaternion.LookRotation(position - transform.position, Vector3.up);

        transform.position = position;
        transform.rotation = rotation;

        if (moveAmount >= 1f)
        {
            ReachTarget();
        }
    }

    private void ReachTarget()
    {
        if (target != null)
        {
            target.ApplyPotentialDamage(damage);
            ParticleSystem fx = Instantiate(explosionFx, targetPosition, Quaternion.identity);
            fx.gameObject.SetActive(true);
            Collider[] unitColliders = Physics.OverlapSphere(target.Position, explosionRadius, unitsMask);
            foreach (var unitCollider in unitColliders)
            {
                Unit unit = unitCollider.GetComponent<Unit>();
                if (unit != null && unit != target && unit.IsAlive)
                {
                    unit.ApplyDamage(damage);
                }
            }
        }
        OnReachTarget?.Invoke();
    }
}
