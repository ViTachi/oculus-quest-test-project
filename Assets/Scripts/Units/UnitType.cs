using UnityEngine;

[CreateAssetMenu(menuName = "Data/UnitType", fileName = "Unit")]
public class UnitType : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private Sprite icon;
    public string Id => id;
    public Sprite Icon => icon;
}
