using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Haptics;

public class ControllerHaptics : MonoBehaviour
{
    public static ControllerHaptics instance;

    [SerializeField] private bool debug = false;

    [SerializeField] private float debugLowFrequency = .0f;
    [SerializeField] private float debugHighFrequency = .0f;
    [SerializeField] private float debugDuration = .0f;

    void Awake()
    {
        instance = this;
    }

    void OnGUI()
    {
        if (debug)
        {
            GUI.Box(new Rect(500, 0, 300, 200), "Controls Haptics :");

            // GUI.Label(new Rect(500, 0, 200, 25), "Controls Haptics :");
            GUI.Label(new Rect(515, 25, 265, 25), "Low Frequency (Left motor) : " + debugLowFrequency);
            debugLowFrequency = GUI.HorizontalSlider(new Rect(515, 50, 265, 25), debugLowFrequency, .0f, 10f);
            GUI.Label(new Rect(515, 75, 265, 25), "High Frequency (Right motor) : " + debugHighFrequency);
            debugHighFrequency = GUI.HorizontalSlider(new Rect(515, 100, 265, 25), debugHighFrequency, .0f, 10f);
            GUI.Label(new Rect(515, 125, 265, 25), "Duration : " + debugDuration);
            debugDuration = GUI.HorizontalSlider(new Rect(515, 150, 265, 25), debugDuration, .0f, 10f);

            if (GUI.Button(new Rect(525, 175, 250, 25), "Shake All Controller"))
            {
                ShakeAllController(debugLowFrequency, debugHighFrequency, debugDuration);
            }
        }
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
                StartCoroutine(StopHaptics((Gamepad)InputSystem.GetDeviceById(device.deviceId), delay));
        }
    }

    public void ShakeController(int deviceId, float lowFrequency, float highFrequency, float duration)
    {
        Gamepad gamepad = (Gamepad)InputSystem.GetDeviceById(deviceId);

        gamepad.SetMotorSpeeds(lowFrequency, highFrequency);

        Debug.Log("Start Shaking " + deviceId);
        Debug.Log(duration);
        if (duration >= 0)
            StartCoroutine(StopHaptics(gamepad, duration));
    }

    public IEnumerator StopHaptics(Gamepad gamepad, float delay)
    {
        yield return new WaitForSeconds(delay);

        Debug.Log("Stop Shaking " + gamepad.deviceId + " after " + delay + " seconds");
        gamepad.SetMotorSpeeds(.0f, .0f);
    }
}
