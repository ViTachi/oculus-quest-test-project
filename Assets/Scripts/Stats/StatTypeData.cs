using UnityEngine;

[CreateAssetMenu(menuName = "Data/Stat")]
public class StatTypeData : ScriptableObject
{
    [SerializeField] private StatType statType;
    public StatType StatType => statType; 
    public string StatName => statType.ToString(); 

#if UNITY_EDITOR
    [Sirenix.OdinInspector.PreviewField(32, Sirenix.OdinInspector.ObjectFieldAlignment.Left)]
    [SerializeField] private Sprite icon;
    public Sprite Icon => icon;
    [SerializeField] private Color color = Color.white;
    public Color Color => color;
#endif
}

public enum StatType
{
    CurrentHealth, 
    MaxHealth,
    Damage,
    AttackSpeed,
    AttackRange,
    VisionRange,
    MovementSpeed,
}
