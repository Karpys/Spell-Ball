using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    UI_Options_Audio audioMenu;
    UI_Options_Graphics graphMenu;

    public Animator animref;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttonGraph);


    }
    // Start is called before the first frame update
    void Start()
    {
        audioMenu = FindObjectOfType<UI_Options_Audio>();
        graphMenu = FindObjectOfType<UI_Options_Graphics>();
        animref = chooseOp.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGraph()
    {
        Graph.gameObject.SetActive(true);
        chooseOp.gameObject.SetActive(false);
        animref.SetBool("QUIT_OP", true);
    }

    public void OnAudio()
    {
        Audio.gameObject.SetActive(true);
        chooseOp.gameObject.SetActive(false);
        animref.SetBool("QUIT_OP", true);
    }

    public void OnResetSettings(string SceneName)
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneName);
    }

    public void OnBack()
    {

        buttonBack.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        BaseOp.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        animref.SetBool("QUIT_OP", true);

        mainMenu.GetComponent<Animator>().SetBool("ENTER", true);
    }
}
