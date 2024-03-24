using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    protected AIPlayer aIPlayer;
    protected bool isMoving = false;
    protected bool isTargetReached = false;

    public bool IsMoving => isMoving;
    public bool IsTargetReached => isTargetReached;

    public void Initialize(AIPlayer aIPlayer)
    {
        this.aIPlayer = aIPlayer;
    }

    public abstract void MoveToPoint(Vector3 point);
    public abstract void MoveToTarget(Transform target);
    public abstract void MoveToUnit(Unit target);
    public abstract void Stop();
}
