using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWithMultipleButton : MonoBehaviour
{
    [SerializeField] private List<GameObject> _buttonsGameObjects;
    [SerializeField] private float _timeToStayBeforeOpenning;

    [SerializeField] private List<float> _delayBeforeClosing;

    private List<PressurePlateMultiple> _buttons;
    private IDoor _door;
    private float timer;
    private List<PlayerController> _playersController;
    private float closingTimer = 0;
    private int doorPhase = 0;

    public List<PressurePlateMultiple> selectedPlate;
    public List<ColorEnum> selectedColor;
    public List<ColorEnum> pressedColor;

    // Start is called before the first frame update
    void Start()
    {
        selectedPlate = new List<PressurePlateMultiple>();
        pressedColor = new List<ColorEnum>();
        _door = GetComponent<IDoor>();
        _buttons = new List<PressurePlateMultiple>();
        _playersController = new List<PlayerController>();
        foreach (GameObject _buttonGO in _buttonsGameObjects)
        {
            _buttons.Add(_buttonGO.GetComponent<PressurePlateMultiple>());
        }

        ReselectRandomlyPlate();
    }

    // Update is called once per frame
    void Update()
    {
        if (closingTimer > 0)
        {
            closingTimer -= Time.deltaTime;
            if (closingTimer <= 0f)
            {
                if (_door.getDoorState() == DoorState.OPEN)
                {
                    _door.CloseDoor();
                    doorPhase++;
                    closingTimer = 0;
                    ReselectRandomlyPlate();
                }
            }
        }
    }

    private void ReselectRandomlyPlate()
    {
        selectedPlate = new List<PressurePlateMultiple>();
        selectedColor = new List<ColorEnum>();
        pressedColor = new List<ColorEnum>();
        PressurePlateMultiple[] availablePlate = _buttons.ToArray();
        List<ColorEnum> availableColors = new List<ColorEnum>();
        availableColors.Add(ColorEnum.RED);
        availableColors.Add(ColorEnum.BLEU);
        availableColors.Add(ColorEnum.GREEN);
        availableColors.Add(ColorEnum.ORANGE);

        foreach (PressurePlateMultiple plate in availablePlate)
        {
            plate.SetUsedRune(false);
        }

        int numOfPlateToActivate = Random.Range(1, _buttons.Count);
        for (int i = 0; i < numOfPlateToActivate; i++)
        {
            int index = Random.Range(0, _buttons.Count);
            int indexColor = Random.Range(0, availableColors.Count);
            if (availablePlate[index] != null)
            {
                selectedPlate.Add(availablePlate[index]);
                selectedColor.Add(availableColors[indexColor]);
                availablePlate[index].playerColorNeeded = availableColors[indexColor];
                availablePlate[index].SetUsedRune(true);
                availableColors.RemoveAt(indexColor);
                availablePlate[index] = null;
            }
        }
    }

    public void OnTriggerEnterEvent(Collider other, PressurePlateMultiple platePressed)
    {
        if (!selectedPlate.Contains(platePressed)) return;

        if (other.GetComponent<CharacterController>() != null)
        {
            PlayerController _player = other.GetComponent<PlayerController>();
            if (!_playersController.Contains(_player)) _playersController.Add(_player);

            if (_player.GetPlayerColor() == platePressed.playerColorNeeded)
                pressedColor.Add(platePressed.playerColorNeeded);

            if (hasAllColorNeeded())
            {
                timer = _timeToStayBeforeOpenning;
            }
        }
    }

    public void OnTriggerStayEvent(Collider other, PressurePlateMultiple platePressed)
    {
        if (!selectedPlate.Contains(platePressed)) return;

        if (other.GetComponent<CharacterController>() != null)
        {
            if (hasAllColorNeeded())
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    if (_door.getDoorState() == DoorState.CLOSE)
                    {
                        _door.OpenDoor();

                        closingTimer = doorPhase < _delayBeforeClosing.Count ? _delayBeforeClosing[doorPhase] : _delayBeforeClosing[_delayBeforeClosing.Count - 1];
                    }
                }
            }
        }
    }

    public void OnTriggerExitEvent(Collider other, PressurePlateMultiple platePressed)
    {
        if (!selectedPlate.Contains(platePressed)) return;

        if (other.GetComponent<CharacterController>() != null)
        {
            PlayerController _player = other.GetComponent<PlayerController>();
            if (_playersController.Contains(_player)) _playersController.Remove(_player);
            if (pressedColor.Contains(platePressed.playerColorNeeded))
                pressedColor.Remove(platePressed.playerColorNeeded);
        }
    }

    private bool hasAllColorNeeded()
    {
        foreach (ColorEnum colorToPress in selectedColor)
        {
            if (!pressedColor.Contains(colorToPress)) return false;
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (GameObject _button in _buttonsGameObjects)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, _button.transform.position);
        }
    }
}