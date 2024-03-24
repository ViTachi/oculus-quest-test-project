using UnityEngine;

[RequireComponent(typeof(UnitStats))]
public class HealthRegeneration : MonoBehaviour
{
    [SerializeField] private float regenSpeed;

    private UnitStats unitStats;

    private void Awake()
    {
        unitStats = GetComponent<UnitStats>();
    }

    private void Update()
    {
        if (unitStats != null)
        {
            float currentHealth = unitStats.GetStat(StatType.CurrentHealth).Value;
            if (currentHealth <= 0f) return;

            unitStats.SetStat(StatType.CurrentHealth,
                Mathf.Clamp(currentHealth + regenSpeed * Time.deltaTime, 
                0, unitStats.GetStat(StatType.MaxHealth).Value));
        }
    }
}
