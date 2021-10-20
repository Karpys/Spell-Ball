using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    public InputAction ShakeInput;
    
    [SerializeField] private Transform CameraPosition;

    private Vector3 OnStopLocation;
    private float _timerPerlin;
    float timerShake;
    float timerReplace;
    bool IsShaking;
    [SerializeField] private float ReplaceTime;
    [SerializeField] private float _duration;
    
    [SerializeField]  private float _frequence;
    [SerializeField] private float _shakeForce;

    // Update is called once per frame

    void Awake()
    {
        ShakeInput.Enable();
    }
    void Update()
    {
        
        if(timerShake <= _duration && IsShaking)
        {
            timerShake += Time.deltaTime;
            CameraPosition.localPosition = GetVector3() * _shakeForce;
            _timerPerlin += Time.deltaTime * _frequence;
        }
        else if(timerReplace<=ReplaceTime)
        {
            if (IsShaking)
            {
                OnStopLocation = CameraPosition.localPosition;
                IsShaking = false;
            }
            timerReplace += Time.deltaTime;
            CameraPosition.localPosition = Vector3.Lerp(OnStopLocation, Vector3.zero, timerReplace / ReplaceTime);
        }

        if (ShakeInput.triggered)
        {
            Shake();
        }
    }
    
    void Shake()
    {
        timerShake = 0;
        timerReplace = 0;
        IsShaking = true;
    }

    float GetPerlinFloat(float seed)
    {
        return (Mathf.PerlinNoise(seed, _timerPerlin)- 0.5f) * 2;
    }

    Vector3 GetVector3()
    {
        return new Vector3(
            GetPerlinFloat(0),
            GetPerlinFloat(10),
            0
        );
    }


}
