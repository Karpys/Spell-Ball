using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Haptics;
using Unity.EditorCoroutines.Editor;

[CustomEditor(typeof(ControllerHaptics))]
public class ControlHapticsEditor : Editor
{
    private List<bool> shownController;

    void OnEnable()
    {
        shownController = new List<bool>();
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty debugProperty = serializedObject.FindProperty("debug");

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Debug");
        debugProperty.boolValue = EditorGUILayout.Toggle(debugProperty.boolValue);
        EditorGUILayout.EndHorizontal();

        if (debugProperty.boolValue)
        {
            int index = 0;
            foreach (InputDevice device in InputSystem.devices)
            {
                if (!(device is IDualMotorRumble)) continue;
                if (index >= shownController.Count)
                    shownController.Add(false);
                if (shownController[index] == null)
                    shownController[index] = false;

                shownController[index] =
                    EditorGUILayout.BeginFoldoutHeaderGroup(shownController[index], device.displayName);

                if (shownController[index])
                {

                #region DrawOneController

                SerializedProperty debugLowFrequencyProperty = serializedObject.FindProperty("debugLowFrequency");
                SerializedProperty debugHighFrequencyProperty = serializedObject.FindProperty("debugHighFrequency");
                SerializedProperty debugDurationProperty = serializedObject.FindProperty("debugDuration");

                EditorGUILayout.LabelField("Controller Settings");

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                GUILayoutOption[] sliderOptions = new[] { GUILayout.MinWidth(100), GUILayout.MinHeight(100) };

                GUILayout.BeginHorizontal();

                GUILayout.BeginVertical();
                EditorGUILayout.LabelField("Left Motor");
                debugLowFrequencyProperty.floatValue = EditorGUILayout.FloatField(debugLowFrequencyProperty.floatValue, new[] { GUILayout.MaxWidth(50) });

                debugLowFrequencyProperty.floatValue =
                    GUILayout.VerticalSlider(debugLowFrequencyProperty.floatValue, 3f, 0f, sliderOptions);

                GUILayout.EndVertical();

                Texture controller = (Texture)AssetDatabase.LoadAssetAtPath("Assets/JULIEN/VISUALS/controller-1827840__480.png", typeof(Texture));
                GUI.DrawTexture(new Rect(40, 120 * (index + 1) + (index * 200), 143.2f, 96f), controller);

                GUILayout.BeginVertical();
                EditorGUILayout.LabelField("Right Motor");
                debugHighFrequencyProperty.floatValue = EditorGUILayout.FloatField(debugHighFrequencyProperty.floatValue, new[] { GUILayout.MaxWidth(50) });

                debugHighFrequencyProperty.floatValue =
                    GUILayout.VerticalSlider(debugHighFrequencyProperty.floatValue, 3f, 0f, sliderOptions);

                GUILayout.EndVertical();

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Shake duration", new[] { GUILayout.MaxWidth(100) });
                debugDurationProperty.floatValue = GUILayout.HorizontalSlider(debugDurationProperty.floatValue, 0f, 300f);
                GUILayout.EndHorizontal();

                float duration = debugDurationProperty.floatValue;
                int minutes = Mathf.RoundToInt(duration / 60);

                duration %= 60;
                int seconds = Mathf.RoundToInt(duration);

                GUILayout.Label((minutes > 0 ? minutes + " minutes " : "") + seconds + " seconds");

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                if (GUILayout.Button("Shake continuous"))
                {
                    ShakeController(device.deviceId, debugLowFrequencyProperty.floatValue, debugHighFrequencyProperty.floatValue, -1);
                }

                if (GUILayout.Button("Shake for " + (minutes > 0 ? minutes + " minutes " : "") + seconds + " seconds"))
                {
                    ShakeController(device.deviceId, debugLowFrequencyProperty.floatValue, debugHighFrequencyProperty.floatValue, debugDurationProperty.floatValue);
                }

                if (GUILayout.Button("Stop Shaking"))
                {
                    EditorCoroutineUtility.StartCoroutine(StopHaptics((Gamepad)InputSystem.GetDeviceById(device.deviceId), 0), this);
                }

                    #endregion

                }
                EditorGUILayout.EndFoldoutHeaderGroup();
                index++;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }


    public void ShakeAllController(float lowFrequency, float highFrequency, float duration)
    {
        foreach (InputDevice device in InputSystem.devices)
        {
            if (device is IDualMotorRumble)
                ShakeController(device.deviceId, lowFrequency, highFrequency, duration);

        }
    }

    public void StopAllControllers(float delay)
    {
        foreach (InputDevice device in InputSystem.devices)
        {
            if (device is IDualMotorRumble)
                EditorCoroutineUtility.StartCoroutine(StopHaptics((Gamepad)InputSystem.GetDeviceById(device.deviceId), delay), this);
        }
    }

    public void ShakeController(int deviceId, float lowFrequency, float highFrequency, float duration)
    {
        Gamepad gamepad = (Gamepad)InputSystem.GetDeviceById(deviceId);

        gamepad.SetMotorSpeeds(lowFrequency, highFrequency);

        if (duration >= 0)
            EditorCoroutineUtility.StartCoroutine(StopHaptics(gamepad, duration), this);
    }

    public IEnumerator StopHaptics(Gamepad gamepad, float duration)
    {
        yield return new EditorWaitForSeconds(duration);

        gamepad.SetMotorSpeeds(.0f, .0f);
    }

}
