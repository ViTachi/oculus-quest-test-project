using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovement : Movement
{
    [SerializeField] private float reachTargetStopDistance = 1f;
    [SerializeField] private NavMeshAgent agent;

    private Transform target = null;
    private Vector3 targetPoint;
    private int runAnimationHash = Animator.StringToHash("Run");

    public override void MoveToPoint(Vector3 point)
    {
        if (!agent.isOnNavMesh) return;

        isTargetReached = false;

        targetPoint = point;

        OnStartMove();

        NavMeshPath path = new NavMeshPath();
        NavMesh.CalculatePath(agent.transform.position, point, NavMesh.AllAreas, path);
        if (path.status == NavMeshPathStatus.PathInvalid)
        {
            if (NavMesh.SamplePosition(point, out NavMeshHit hit, float.PositiveInfinity, NavMesh.AllAreas))
                NavMesh.CalculatePath(agent.transform.position, hit.position, NavMesh.AllAreas, path);
        }
        agent.SetPath(path);
    }

    public override void MoveToTarget(Transform target)
    {
        if (!agent.isOnNavMesh) return;

        OnStartMove();
        this.target = target;
    }

    public override void MoveToUnit(Unit target)
    {
        if (!agent.isOnNavMesh) return;

        OnStartMove();
        this.target = target.transform;
    }

    private void OnStartMove()
    {
        isMoving = true;
        agent.isStopped = false;
        agent.speed = aIPlayer.Unit.UnitStats.GetStat(StatType.MovementSpeed).Value;
        if (aIPlayer.Unit.Animator != null)
            aIPlayer.Unit.Animator.SetBool(runAnimationHash, true);
    }

    private void Update()
    {
        if (!agent.isOnNavMesh) return;

        if (isMoving && (aIPlayer.Unit.Position - targetPoint).magnitude <= reachTargetStopDistance)
        {
            OnTargetReached();
        }

        if (target != null)
        {
            if ((aIPlayer.Unit.Position - target.position).magnitude <= reachTargetStopDistance)
            {
                OnTargetReached();
            }
            else
                MoveToPoint(target.position);
        }
    }

    private void OnTargetReached()
    {
        isTargetReached = true;
        Stop();
    }

    public override void Stop()
    {
        if(aIPlayer.Unit.Animator != null)
            aIPlayer.Unit.Animator.SetBool(runAnimationHash, false);
        target = null;

        if (!agent.isOnNavMesh) return;
        agent.isStopped = true;
    }
}
