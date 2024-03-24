using Sirenix.OdinInspector;
using System;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [HorizontalGroup("Split")]
    [VerticalGroup("Split/Left")]
    [CustomValueDrawer("StatDrawer"), HideLabel]
    [SerializeField] private StatTypeData statTypeData;

    [GUIColor("GetColor"), HideLabel]
    [VerticalGroup("Split/Right")]
    [SerializeField] private float value; 

    public float Value => value;
    public StatTypeData StatTypeData => statTypeData;
    public StatType StatType => statTypeData.StatType;
    public void SetValue(float value) => this.value = value;

    public Stat(StatTypeData statTypeData, float value = 0f)
    {
        this.statTypeData = statTypeData;
        this.value = value;
    }

#if UNITY_EDITOR
    private StatTypeData StatDrawer(StatTypeData value, GUIContent label, Func<GUIContent, bool> callNextDrawer)
    {
        Sirenix.Utilities.Editor.SirenixEditorGUI.BeginBox();
        if (value != null && value.Icon != null)
        {           
            GUIContent content = new GUIContent(value.Icon.texture);
            callNextDrawer(content);
        }
        else
        {
            callNextDrawer(label);
        }
        var result = value;
        Sirenix.Utilities.Editor.SirenixEditorGUI.EndBox();
        return result;
    }

    private Color GetColor()
    {
        return statTypeData == null ? Color.white : statTypeData.Color;
    }
#endif
}
