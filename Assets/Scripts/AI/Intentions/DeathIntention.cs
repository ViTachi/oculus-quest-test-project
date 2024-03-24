using UnityEngine;

public class DeathIntention : Intention
{
    [SerializeField] private DeathAction deathAction;

    private void Awake()
    {
        deathAction.OnActionComplete += (UnitAction<DeathAction.Parameters> t) => { Complete(); };
    }

    public override bool IsNeedToBeActivated()
    {
        return !aIPlayer.Unit.IsAlive;
    }

    public override void Activate()
    {
        base.Activate();
        aIPlayer.Unit.Animator.SetBool("IsAlive", false);
        deathAction.StartAction(new DeathAction.Parameters() { Unit = aIPlayer.Unit });
    }

    public override void Stop()
    {
        base.Stop();
        aIPlayer.Unit.Animator.SetBool("IsAlive", true);
        if (deathAction.IsInProgress)
            deathAction.StopAction();
    }
}
