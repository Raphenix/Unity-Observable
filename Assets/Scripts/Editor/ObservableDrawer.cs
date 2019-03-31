using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// All custom non-generic Observable classes must be added here
// to be displayed with this custom property drawer
[CustomPropertyDrawer(typeof(OInt))]
[CustomPropertyDrawer(typeof(OUInt))]
[CustomPropertyDrawer(typeof(OBool))]
[CustomPropertyDrawer(typeof(OFloat))]
[CustomPropertyDrawer(typeof(OString))]
[CustomPropertyDrawer(typeof(OTransform))]
[CustomPropertyDrawer(typeof(OGameObject))]
[CustomPropertyDrawer(typeof(OColor))]
public class ObservableDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        float checkboxWidth = 14;
        float strictLabelWidth = 35;
        Rect valueRect = new Rect(position.x, position.y, position.width - checkboxWidth - strictLabelWidth - 10, position.height);
        Rect strictRect = new Rect(position.x + position.width - checkboxWidth - strictLabelWidth - 5, position.y, strictLabelWidth, position.height);
        Rect checkboxRect = new Rect(position.x + position.width - checkboxWidth, position.y, checkboxWidth, position.height);

        SerializedProperty strictProperty = property.FindPropertyRelative("strict");

        EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("value"), GUIContent.none);
        EditorGUI.LabelField(strictRect, new GUIContent("Strict", "If strict checked, the event will be triggered only if the new value set is different than the previous value"));
        strictProperty.boolValue = EditorGUI.Toggle(checkboxRect, strictProperty.boolValue);

        EditorGUI.EndProperty();
    }
}