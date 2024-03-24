public class SpawnIntention : Intention
{
    private bool isSpawned = false;

    protected override void OnDisable()
    {
        base.OnDisable();
        isSpawned = false;
    }

    public override bool IsNeedToBeActivated() => !isSpawned;

    public override void Activate()
    {
        base.Activate();

        isSpawned = true;
        Complete();
    }
}
