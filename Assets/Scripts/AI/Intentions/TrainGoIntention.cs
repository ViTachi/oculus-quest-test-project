public class TrainGoIntention : Intention
{
    private Train train;

    public override bool IsNeedToBeActivated()
    {
        return aIPlayer.Unit.IsAlive;
    }

    public override void Activate()
    {
        base.Activate();

        train = GetComponentInParent<Train>();

        if (train != null) train.SetTargetSpeed(aIPlayer.Unit.UnitStats.GetStat(StatType.MovementSpeed).Value);
    }

    public override void Run()
    {
        base.Run();

        if (train == null || !aIPlayer.Unit.IsAlive)
        {
            Complete();
            return;
        } 
    }
}
