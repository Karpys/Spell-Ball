using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private List<GameObject> _doorsGameObjects;
    [SerializeField] private bool _activateRandomlyOneDoorOnly;
    [SerializeField] private int _numberOfPlayersToActivate;

    [SerializeField] private float _timeToStayBeforeOpenning;
    [SerializeField] private float _timeBeforeClosing;
    // [SerializeField] private int _numberOfColorCorrectMinimumToActivate;
    // [SerializeField] private List<ColorEnum> _colorNeededToActivate;

    private List<IDoor> _doors;
    private float timer;
    private List<PlayerController> _playersController;

    private List<IDoor> _opennedDoor;
    private float openningTimer;

    private void Awake()
    {
        _doors = new List<IDoor>();
        _opennedDoor = new List<IDoor>();
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
                foreach (IDoor door in _opennedDoor)
                {
                    if (door.getDoorState() == DoorState.OPEN)
                        door.CloseDoor();
                }

                _opennedDoor.Clear();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            PlayerController _player = other.GetComponent<PlayerController>();
            if (!_playersController.Contains(_player)) _playersController.Add(_player);
            
            if (_opennedDoor.Count <= 0)
            {
                if (!_activateRandomlyOneDoorOnly)
                {
                    foreach (IDoor door in _doors)
                    {
                        _opennedDoor.Add(door);
                    }
                }
                else
                {
                    IDoor door = _doors[Random.Range(0, _doors.Count)];
                    _opennedDoor.Add(door);
                }
            }

            if (_playersController.Count >= _numberOfPlayersToActivate /*&& hasAllColorNeeded()*/)
            {
                openningTimer = _timeToStayBeforeOpenning;
            }
        }
    }

    /*private bool hasAllColorNeeded()
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
    }*/

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<CharacterController>() != null)
        {
            if (_playersController.Count >= _numberOfPlayersToActivate /*&& hasAllColorNeeded()*/)
            {

                openningTimer -= Time.deltaTime;
                if (openningTimer <= 0f)
                {
                    foreach (IDoor door in _opennedDoor)
                    {
                        if (door.getDoorState() == DoorState.CLOSE)
                        {
                            door.OpenDoor();
                        }
                    }
                    
                    timer = _timeBeforeClosing;
                }
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