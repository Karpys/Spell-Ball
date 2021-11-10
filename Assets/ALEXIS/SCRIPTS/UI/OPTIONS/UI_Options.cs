using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UI_Options : MonoBehaviour
{
    [Header("MAIN MENU")]
    public GameObject mainMenu;

    [Header("OPTION MENU")]
    public GameObject Graph;
    public GameObject Audio;
    public GameObject BaseOp;
    public GameObject chooseOp;

    [Header("BUTTONS")]
    public GameObject buttonGraph;
    public GameObject buttonAud;
    public GameObject buttonBack;


    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttonGraph);


    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGraph()
    {
        Graph.gameObject.SetActive(true);
        chooseOp.gameObject.SetActive(false);
    }

    public void OnAudio()
    {
        Audio.gameObject.SetActive(true);
        chooseOp.gameObject.SetActive(false);
    }

    public void OnResetSettings()
    {
        PlayerPrefs.DeleteAll();
    }

    public void OnBack()
    {
        mainMenu.gameObject.SetActive(true);
        BaseOp.gameObject.SetActive(false);
    }
}
