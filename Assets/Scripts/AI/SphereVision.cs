using UnityEngine;

public class SphereVision : Vision
{
    [SerializeField] private LayerMask layerMask;

    public override void UpdateUnitsInVision()
    {
        unitsInVision.Clear();
        foreach (var collider in Physics.OverlapSphere(transform.position, 
            aIPlayer.Unit.UnitStats.GetStat(StatType.VisionRange).Value, layerMask))
        {
            Unit unit = collider.GetComponent<Unit>();
            if (unit != null && unit != aIPlayer.Unit)
                unitsInVision.Add(unit);
        }
    }
}
