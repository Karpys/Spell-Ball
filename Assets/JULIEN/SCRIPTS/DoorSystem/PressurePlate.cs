using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    [SerializeField] private List<GameObject> _doorsGameObjects;
    [SerializeField] private int _numberOfPlayersToActivate;
    [SerializeField] private float _timeBeforeClosing;
    [SerializeField] private int _numberOfColorCorrectMinimumToActivate;
    [SerializeField] private List<ColorEnum> _colorNeededToActivate;

    private List<IDoor> _doors;
    private float timer;
    private List<PlayerController> _playersController;

    private void Awake()
    {
        _doors = new List<IDoor>();
        _playersController = new List<PlayerController>();
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
                    if (door.getDoorState() == DoorState.OPEN)
                        door.CloseDoor();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            PlayerController _player = other.GetComponent<PlayerController>();
            if (!_playersController.Contains(_player)) _playersController.Add(_player);
            if (_playersController.Count >= _numberOfPlayersToActivate)
            {
                if (hasAllColorNeeded())
                {
                    foreach (IDoor door in _doors)
                    {
                        if (door.getDoorState() == DoorState.CLOSE)
                            door.OpenDoor();
                    }
                }
            }
        }
    }

    private bool hasAllColorNeeded()
    {
        List<ColorEnum> currentOnPlate = new List<ColorEnum>();
        for (int i = 0; i < _playersController.Count; i++)
        {
            currentOnPlate.Add(_playersController[i].GetPlayerColor());
        }

        bool isValid = true;
        int numbOfCorrectColor = _colorNeededToActivate.Count;
        for (int i = 0; i < _colorNeededToActivate.Count; i++)
        {
            if (!currentOnPlate.Contains(_colorNeededToActivate[i]))
            {
                isValid = false;
                numbOfCorrectColor--;
            }
        }
        

        return isValid || numbOfCorrectColor >= _numberOfColorCorrectMinimumToActivate;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            if (_playersController.Count >= _numberOfPlayersToActivate && hasAllColorNeeded())
            {
                timer = _timeBeforeClosing;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            PlayerController _player = other.GetComponent<PlayerController>();
            if (!_playersController.Contains(_player)) _playersController.Remove(_player);
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
