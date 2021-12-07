using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharSelection : MonoBehaviour
{

    [SerializeField] private SO_CharInfo _charInfo;

    [SerializeField] private Image _image;
    
    private int currentSelectedChar;

    private PlayerInput _playerInput;
    
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        currentSelectedChar = 0;

        _charInfo.playerControllerToId.Add(_playerInput.playerIndex, _playerInput.devices[0].deviceId);
        _charInfo.playerPrefabForId.Add(_playerInput.playerIndex, null);
        
        SetPlayerChar(CharSelectionManager.instance.possibleChar[currentSelectedChar]);
    }


    public void NextChar(InputAction.CallbackContext ctx)
    {
        currentSelectedChar++;
        if (currentSelectedChar >= CharSelectionManager.instance.possibleChar.Count)
        {
            currentSelectedChar = 0;
        }
        SetPlayerChar(CharSelectionManager.instance.possibleChar[currentSelectedChar]);
    }

    public void PreviousChar(InputAction.CallbackContext ctx)
    {
        currentSelectedChar--;
        if (currentSelectedChar < 0)
        {
            currentSelectedChar = CharSelectionManager.instance.possibleChar.Count - 1;
        }
        SetPlayerChar(CharSelectionManager.instance.possibleChar[currentSelectedChar]);
    }

    public void SetPlayerChar(CharInfoData charInfoData)
    {
        Sprite sprite;
        GameObject selectedChar;
        switch (_playerInput.playerIndex)
        {
            case 0:
                sprite = charInfoData.fireImage;
                selectedChar = charInfoData.fireChar;
                break;
            case 1:
                sprite = charInfoData.iceImage;
                selectedChar = charInfoData.iceChar;
                break;
            case 2:
                sprite = charInfoData.poisonImage;
                selectedChar = charInfoData.poisonChar;
                break;
            case 3:
                sprite = charInfoData.elecImage;
                selectedChar = charInfoData.elecChar;
                break;
            default:
                sprite = charInfoData.fireImage;
                selectedChar = charInfoData.fireChar;
                break;
        }

        _image.sprite = sprite;
        _charInfo.playerPrefabForId[_playerInput.playerIndex] = selectedChar;
    }
}
