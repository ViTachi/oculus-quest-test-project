public abstract class DeathAction : UnitAction<DeathAction.Parameters>
{
    public class Parameters
    {
        public Unit Unit { get; set; }
    }
}
