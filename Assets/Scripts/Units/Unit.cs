using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected Fraction fraction;
    [SerializeField] protected int unitLevel = 1;
    [SerializeField] protected float radius = 0;
    [SerializeField] protected UnitStats unitStats;
    [SerializeField] protected AIPlayer aIPlayer;
    [SerializeField] protected Visual visual;
       
    private float potentialDamage = 0f;

    public event Action OnDie;
    public event Action OnNeedRelease;

    public Vector3 Position => visual.GetAnchor("Position") == null 
        ? transform.position : visual.GetAnchor("Position").position;
    public float Radius => radius;
    public Fraction Fraction => fraction;
    public UnitStats UnitStats => unitStats;
    public AIPlayer AIPlayer => aIPlayer;
    public Visual Visual => visual;
    public Animator Animator => visual.Animator;
    public bool IsAlive => (unitStats?.GetStat(StatType.CurrentHealth).Value ?? 0) > 0;
    public bool IsPotentialDead => ((unitStats?.GetStat(StatType.CurrentHealth).Value ?? 0) - potentialDamage) <= 0;
    public int Level => unitLevel;

    public Vector3 GetClosestPointOnRadius(Vector3 position)
    {
        return Position + (position - Position).normalized * radius;
    }

    protected virtual void Awake()
    {
        SetLevel(unitLevel);
    }

    private void OnEnable()
    {
        SetGlobalUnitsGroup();
    }

    public void SetLevel(int level)
    {
        unitLevel = level;
    }

    public void SetFraction(Fraction fraction)
    {
        this.fraction = fraction;       
    }

    public virtual void ApplyDamage(DamageData damage)
    {
        if (!IsAlive) return;

        unitStats.SetStat(StatType.CurrentHealth, unitStats.GetStat(StatType.CurrentHealth).Value - damage.Damage);
        if (!IsAlive)
        {
            OnDie?.Invoke();
        }

        new FloatingText($"{damage.Damage}")
            .SetSettings("lol")
            .SetTextColor(damage.DamageType == DamageType.Critical ? Color.red : Color.white)
            .SetExtraTextSize(damage.DamageType == DamageType.Critical ? 54 : 42)
            .SetTarget(visual.GetAnchor("DamageText"))
            .Show();
    }

    public void ApplyPotentialDamage(DamageData damage)
    {
        potentialDamage -= damage.Damage;
        ApplyDamage(damage);
    }

    public void AddPotentialDamage(DamageData damage)
    {
        potentialDamage += damage.Damage;
    }

    public void DestroyUnit()
    {
        OnNeedRelease?.Invoke();
    }

    public void OnRelease()
    {
        
    }

    public void OnRevive()
    {
        SetLevel(unitLevel);
    }


    private int globalUnitsGroup = 0;
    public int GlobalUnitsGroup => globalUnitsGroup;
    private static int nextUnitsGroup = 0;
    public const int GlobalUnitsGroupsCount = 4;
    private void SetGlobalUnitsGroup()
    {
        globalUnitsGroup = nextUnitsGroup;
        nextUnitsGroup++;
        if (nextUnitsGroup > 3)
            nextUnitsGroup = 0;
    }
}
