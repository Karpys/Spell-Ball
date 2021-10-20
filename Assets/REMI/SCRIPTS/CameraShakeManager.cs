using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraShakeManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform Entity;

    private Vector3 OnStopLocation;
    private float _timerPerlin;
    float timerShake;
    float timerReplace;
    bool IsShaking;
    [SerializeField] private float ReplaceTime = 0.5f;
    [SerializeField] private float _duration = 0.25f;
    
    [SerializeField]  private float _frequence = 15.0f;
    [SerializeField] private float _shakeForce = 3.0f;

    float _shakeForceDefault = 3.0f;
    float _durationDefault = 0.25f;
    float _frequenceDefault = 15.0f;
    // Update is called once per frame

    private static CameraShakeManager inst;
    public static CameraShakeManager CameraShake { get => inst; }

    void Awake()
    {
        if (CameraShake != null && CameraShake != this)
            Destroy(gameObject);

        inst = this;
    }
    void Update()
    {
        
        if(timerShake <= _duration && IsShaking)
        {
            timerShake += Time.deltaTime;
            Entity.localPosition = GetVector3() * _shakeForce;
            _timerPerlin += Time.deltaTime * _frequence;
        }
        else if(timerReplace<=ReplaceTime)
        {
            if (IsShaking)
            {
                OnStopLocation = Entity.localPosition;
                IsShaking = false;
            }
            timerReplace += Time.deltaTime;
            Entity.localPosition = Vector3.Lerp(OnStopLocation, Vector3.zero, timerReplace / ReplaceTime);
        }

    }

    void SetShakeParameters()
    {
        timerShake = 0;
        timerReplace = 0;
        IsShaking = true;
    }
    public void Shake()
    {
        SetShakeParameters();
        _shakeForce = _shakeForceDefault;
        _duration = _durationDefault;
        _frequence = _frequenceDefault;
    }
    public void Shake(float force)
    {
        SetShakeParameters();
        _shakeForce = force;
    }
    public void Shake(float force, float duration)
    {
        _shakeForce = force;
        _duration = duration;
        SetShakeParameters();
    }
    public void Shake(float force, float duration,float frequence)
    {
        _shakeForce = force;
        _duration = duration;
        _frequence = frequence;
        SetShakeParameters();
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
