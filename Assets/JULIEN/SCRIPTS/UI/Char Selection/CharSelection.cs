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

    private Animator animatorRef;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    

    private void Start()
    {
        currentSelectedChar = 0;
        if (!_charInfo.playerControllerToId.ContainsKey(_playerInput.playerIndex))
            _charInfo.playerControllerToId.Add(_playerInput.playerIndex, _playerInput.devices[0].deviceId);
        else
        {
            _charInfo.playerControllerToId[_playerInput.playerIndex] = _playerInput.devices[0].deviceId;
        }
        if (!_charInfo.playerPrefabForId.ContainsKey(_playerInput.playerIndex))
            _charInfo.playerPrefabForId.Add(_playerInput.playerIndex, null);
        else
        {
            _charInfo.playerPrefabForId[_playerInput.playerIndex] = null;
        }

        SetPlayerChar(CharSelectionManager.instance.possibleChar[currentSelectedChar]);

        animatorRef = gameObject.GetComponent<Animator>();

        animatorRef.SetBool("Active", true);
    }

    private void OnDestroy()
    {
        _charInfo.playerControllerToId.Remove(_playerInput.playerIndex);
        _charInfo.playerPrefabForId.Remove(_playerInput.playerIndex);
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

    public void CallBackAnimation()
    {
        
        CharSelectionManager.instance.GoBack();
    }

    public void QUIT()
    {
        animatorRef.SetBool("Deactive", false);
    }

    public void ENTER()
    {
        animatorRef.SetBool("Active", false);
    }

    public void RunExit()
    {
        animatorRef.SetBool("Deactive", true);
    }
}
