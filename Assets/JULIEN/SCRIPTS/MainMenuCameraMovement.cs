using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _centerOfAction;
    [SerializeField] private List<Transform> _cameraPositions;
    [SerializeField] private float _moveSpeed;

    private int targetPosition = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StopTime", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _cameraPositions[targetPosition].position, _moveSpeed);
        transform.LookAt(_centerOfAction);

        if (Vector3.Distance(transform.position, _cameraPositions[targetPosition].position) <= 3) targetPosition++;

        if (targetPosition >= _cameraPositions.Count) targetPosition = 0;
    }

    private void StopTime()
    {
        Time.timeScale = 0;
    }
}
