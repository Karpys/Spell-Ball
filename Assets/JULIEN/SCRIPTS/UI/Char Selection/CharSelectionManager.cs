using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharSelectionManager : MonoBehaviour
{
    public static CharSelectionManager instance;
    
    [SerializeField] private PlayerInputManager _playerInputManager;

    public GameObject mainMenu;

    [SerializeField] private GameObject _startGameText;
    [SerializeField] private GameObject _pressAXToJoinText;

    [SerializeField] private Manager_MainMenu _managerMainMenu;

    [SerializeField] private List<CharInfoData> _possibleChar;

    public List<CharInfoData> possibleChar
    {
        get { return _possibleChar; }
    }

    private PlayerInput[] _playersInputs;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _playersInputs = new PlayerInput[4];
    }

    public void AddPlayer(PlayerInput playerInput)
    {
        RectTransform playerTransform = (RectTransform) playerInput.transform;
        playerTransform.SetParent(transform);
        playerTransform.localScale = Vector3.one;
        playerTransform.anchoredPosition = new Vector2(250 + (playerTransform.sizeDelta.x * (playerInput.playerIndex)) + (50 * (playerInput.playerIndex)), 100);
        playerInput.gameObject.GetComponentInChildren<TMP_Text>().text = "Player " + (playerInput.playerIndex + 1);

        if (playerInput.currentActionMap != null)
        {
            playerInput.currentActionMap["Launch Game"].performed += LaunchGame;
            
            
            _playersInputs[playerInput.playerIndex] = playerInput;
        }

        if (_playerInputManager.playerCount == 4)
        {
            _startGameText.SetActive(true);
            _pressAXToJoinText.SetActive(false);
        }
    }

    public void GoBack()
    {
        EventSystem.current.SetSelectedGameObject(null);
        // Rajouter les anims
        foreach(PlayerInput input in _playersInputs)
        {
            if(input != null)
                input.gameObject.GetComponent<CharSelection>().RunExit();
        }

        StartCoroutine(WaitEndAnim());
    }
    public void GoBack(InputAction.CallbackContext ctx)
    {
        EventSystem.current.SetSelectedGameObject(null);
        // Rajouter les anims
        StartCoroutine(WaitEndAnim());
    }

    IEnumerator WaitEndAnim()
    {
        yield return new WaitForSecondsRealtime(1);
        foreach (PlayerInput input in _playersInputs)
        {
            if (input != null)
            {
                GameObject obj = input.gameObject;
                input.user.UnpairDevicesAndRemoveUser();
                Destroy(obj);
            }
        }
        EventSystem.current.SetSelectedGameObject(null);
        gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        mainMenu.GetComponent<Animator>().SetBool("ENTER", true);
    }
    
    private void LaunchGame(InputAction.CallbackContext ctx)
    {
        if (_playerInputManager.playerCount == 4)
        {
            _managerMainMenu.LaunchGame(ctx);
        }
    }

    public void RemovePlayer(PlayerInput playerInput)
    {
        print("Remove de joueur");
        if (_playerInputManager.playerCount <= 4)
        {
            _startGameText.SetActive(false);
            _pressAXToJoinText.SetActive(true);
        }
    }
    
}

[Serializable]
public struct CharInfoData
{
    public Sprite fireImage;
    public Sprite iceImage;
    public Sprite poisonImage;
    public Sprite elecImage;

    public GameObject fireChar;
    public GameObject iceChar;
    public GameObject poisonChar;
    public GameObject elecChar;
}