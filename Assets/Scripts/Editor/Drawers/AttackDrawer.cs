using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(Attack))]
public class AttackDrawer : PropertyDrawer
{
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUI.BeginProperty(position, label, property);

		position = EditorGUI.PrefixLabel(position, label);
		Rect nameRect = new Rect(position.x, position.y, 75, position.height);
		Rect degatRect = new Rect(position.x + 75, position.y, 25, position.height);
		Rect typeRect = new Rect(position.x + 105, position.y, position.width - 105, position.height);

		EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);
		EditorGUI.PropertyField(degatRect, property.FindPropertyRelative("degat"), GUIContent.none);
		EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("type"), GUIContent.none);

		EditorGUI.EndProperty();
	}
}

