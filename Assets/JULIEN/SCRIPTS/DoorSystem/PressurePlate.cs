using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    [SerializeField] private List<GameObject> _doorsGameObjects;
    [SerializeField] private int _numberOfPlayersToActivate;
    [SerializeField] private float _timeBeforeClosing;

    private List<IDoor> _doors;
    private float timer;
    private int _currentNumbOfPlayer;

    private void Awake()
    {
        _doors = new List<IDoor>();
        foreach (GameObject _doorGO in _doorsGameObjects)
        {
            _doors.Add(_doorGO.GetComponent<IDoor>());
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                foreach (IDoor door in _doors)
                {
                    door.CloseDoor();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            _currentNumbOfPlayer++;
            if (_currentNumbOfPlayer >= _numberOfPlayersToActivate)
            {
                foreach (IDoor door in _doors)
                {
                    if (door.getDoorState() == DoorState.CLOSE)
                        door.OpenDoor();
                }
            }
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            if (_currentNumbOfPlayer >= _numberOfPlayersToActivate)
            {
                timer = _timeBeforeClosing;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            _currentNumbOfPlayer--;
        }
    }

    private void OnDrawGizmosSelected()
    {
        foreach (GameObject _doors in _doorsGameObjects)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _doors.transform.position);
        }
    }
}
