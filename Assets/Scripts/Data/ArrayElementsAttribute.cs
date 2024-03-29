using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class ArrayElementsAttribute : PropertyAttribute
{
    public ArrayElementsAttribute(string titleVariableName) 
    {
        TitleVariableName = titleVariableName;
    }
    public string TitleVariableName { get; }
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(ArrayElementsAttribute))]
public class ArrayElementTitleDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property,
                                    GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
    protected virtual ArrayElementsAttribute Atribute
    {
        get { return (ArrayElementsAttribute)attribute; }
    }
    SerializedProperty TitleNameProp;
    public override void OnGUI(Rect position,
                              SerializedProperty property,
                              GUIContent label)
    {
        string FullPathName = property.propertyPath + "." + Atribute.TitleVariableName;
        TitleNameProp = property.serializedObject.FindProperty(FullPathName);
        
        string newlabel = TitleNameProp.enumNames[ TitleNameProp.enumValueIndex];
        if (string.IsNullOrEmpty(newlabel))
            newlabel = label.text;
        EditorGUI.PropertyField(position, property, new GUIContent(newlabel, label.tooltip), true);
    }   
    
}
#endif