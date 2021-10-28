using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CanEditMultipleObjects]
[CustomEditor(typeof(CharacterMovement))]
public class CharacterMovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        #region PropertyDef


        var Script = target as CharacterMovement;

        Script.ShowInspector = GUILayout.Toggle(Script.ShowInspector, "Show/Hide");

        if (Script.ShowInspector)
        {
            SerializedProperty rollProperty = serializedObject.FindProperty("Roll_Manager");
            EditorGUILayout.PropertyField(rollProperty);

            SerializedProperty characterValuesProperty = serializedObject.FindProperty("Stats");
            EditorGUILayout.PropertyField(characterValuesProperty);

            SerializedProperty controllerProperty = serializedObject.FindProperty("_controller");
            EditorGUILayout.PropertyField(controllerProperty);

            SerializedProperty visualProperty = serializedObject.FindProperty("CharacterVisual");
            EditorGUILayout.PropertyField(visualProperty);

            SerializedProperty movementDirectionProperty = serializedObject.FindProperty("MovementDir");
            EditorGUILayout.PropertyField(movementDirectionProperty);

            SerializedProperty animProperty = serializedObject.FindProperty("Anim");
            EditorGUILayout.PropertyField(animProperty);
        
        }

        /*SerializedProperty movementInputProperty = serializedObject.FindProperty("MovementInput");
        EditorGUILayout.PropertyField(movementInputProperty);*/
        #endregion

        serializedObject.ApplyModifiedProperties();
    }

    [System.Serializable]
    public class GUIEditor
    {
        public bool Show;
        public string name;
    }
}
