using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class CameraFocus : MonoBehaviour
{
    public static CameraFocus instance;

    [SerializeField] private List<Transform> _followTargets;

    [SerializeField] private float _sizeMin = 2f;
    [SerializeField] private float _sizeMax = 40f;

    [SerializeField] private float _distanceMin = 2f;
    [SerializeField] private float _distanceMax = 40f;

    [SerializeField] private float _offsetMin = 32f;
    [SerializeField] private float _offsetMax = 74f;

    [SerializeField] private float _followSpeed = 5f;

    private Camera _camera;

    private void Awake()
    {
        instance = this;
        _camera = GetComponent<Camera>();
    }

    public void AddTarget(Transform target)
    {
        _followTargets.Add(target);
    }

    public Transform RemoveTarget(Transform target)
    {
        Transform toRemove = _followTargets.Find((x) => x = target);
        _followTargets.Remove(target);
        return toRemove;
    }

    void Update()
    {

        float distance = _GetGreatestDistanceBetweenTargets();
        float ratio = Mathf.InverseLerp(_distanceMin, _distanceMax, distance);
        float size = Mathf.Lerp(_sizeMin, _sizeMax, ratio);
        _camera.orthographicSize = size;

        Vector3 centroid = _CalculateTargetsCentroid();

        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Lerp(transform.position.x, centroid.x, Time.deltaTime * _followSpeed);
        newPosition.y = 29;
        float offset = Mathf.Clamp((_offsetMax * (size / _sizeMax)), _offsetMin, _offsetMax);
        newPosition.z = Mathf.Lerp(transform.position.z, centroid.z - offset, Time.deltaTime * _followSpeed);
        transform.position = newPosition;
    }

    private Vector3 _CalculateTargetsCentroid()
    {
        Vector3 centroid = Vector3.zero;
        foreach (Transform target in _followTargets)
        {
            centroid += target.position;
        }
        centroid /= _followTargets.Count;
        return centroid;
    }

    private float _GetGreatestDistanceBetweenTargets()
    {
        float greatestDistance = 0f;
        foreach (Transform target1 in _followTargets)
        {
            foreach (Transform target2 in _followTargets)
            {
                float distance = (target2.position - target1.position).magnitude;
                greatestDistance = Mathf.Max(distance, greatestDistance);
            }
        }

        return greatestDistance;
    }
}
