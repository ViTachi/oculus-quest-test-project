public class PauseIntention : Intention
{
    public override bool IsNeedToBeActivated()
    {
        return false;
    }

    public override void Activate()
    {
        base.Activate();
        aIPlayer.Unit.Animator.speed = 0f;
    }

    public override void Stop()
    {
        aIPlayer.Unit.Animator.speed = 1f;
        base.Stop();
    }

    public override void Run()
    {
        base.Run();
        //if (level == null || !level.IsPaused)
        //    Complete();
    }
}
