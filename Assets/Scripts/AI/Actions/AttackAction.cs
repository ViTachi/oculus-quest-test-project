public abstract class AttackAction : UnitAction<AttackAction.Parameters>
{
    public class Parameters
    {
        public DamageData DamageData { get; set; }
        public Unit Target { get; set; }
        public float DamageDelay { get; set; }
    }
}
