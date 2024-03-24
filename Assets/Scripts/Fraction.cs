using UnityEngine;

[CreateAssetMenu(menuName = "Data/Fraction", fileName = "Fraction")]
public class Fraction : ScriptableObject
{
    [SerializeField] private string fractionName;
    [SerializeField] private Color color = Color.white;

    public string FractionName => fractionName;
    public Color Color => color;
}
