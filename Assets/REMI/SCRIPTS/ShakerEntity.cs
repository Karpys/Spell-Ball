using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class ShakerEntity : MonoBehaviour
{
    // Start is called before the first frame update

    private Vector3 OnStopLocation;
    private float _timerPerlin;
    float timerShake;
    float timerReplace;
    bool IsShaking = true;
    [SerializeField] private float ReplaceTime = 0.2f;
    [SerializeField] private float _duration = 0.25f;

    [SerializeField] private float _frequence = 15.0f;
    [SerializeField] private float _shakeForce = 3.0f;

    float _shakeForceDefault = 3.0f;
    float _durationDefault = 0.25f;
    float _frequenceDefault = 15.0f;
    Vector3 DirectionShake = Vector3.zero;

    Vector3 RandomPerlin;
    // Update is called once per frame

    void Awake()
    {
        RandomPerlin = new Vector3(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100));
    }
    void Update()
    {
        if (timerShake <= _duration && IsShaking)
        {
            timerShake += Time.deltaTime;
            transform.localPosition = GetVector3() * _shakeForce;
            _timerPerlin += Time.deltaTime * _frequence;
        }
        else if (timerReplace <= ReplaceTime)
        {
            if (IsShaking)
            {
                OnStopLocation = transform.localPosition;
                IsShaking = false;
            }
            timerReplace += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(OnStopLocation, Vector3.zero, timerReplace / ReplaceTime);
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetShakeParameters(float duration ,float shakeforce,float frequence,Vector3 LocalDirection)
    {
        _duration = duration;
        _shakeForce = shakeforce;
        _frequence = frequence;
        DirectionShake = LocalDirection;
    }

    public void SetShakeParameters(Vector3 LocalDirection)
    {
        DirectionShake = LocalDirection;
        _shakeForce = _shakeForceDefault;
        _duration = _durationDefault;
        _frequence = _frequenceDefault;
    }

    Vector3 GetVector3()
    {
        Vector3 VecReturn = Vector3.zero;
        if (DirectionShake.x != 0)
        {
            VecReturn.x = GetPerlinFloat(RandomPerlin.x);
        }
        if (DirectionShake.y != 0)
        {
            VecReturn.y = GetPerlinFloat(RandomPerlin.y);
        }
        if (DirectionShake.z != 0)
        {
            VecReturn.z = GetPerlinFloat(RandomPerlin.z);
        }
        return VecReturn;
    }

    float GetPerlinFloat(float seed)
    {
        return (Mathf.PerlinNoise(seed, _timerPerlin) - 0.5f) * 2;
    }


}
