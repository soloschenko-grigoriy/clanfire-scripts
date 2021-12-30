using GS.Hex;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(HexCellCategory))]
public class HexCellTypeDrawer : PropertyDrawer
{
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label)
    {
        var value = (HexCellCategory)property.enumValueIndex;
        EditorGUI.LabelField( position, label );
        position = EditorGUI.PrefixLabel(position, label);
        GUI.Label(position, value.ToString());
    }
}
