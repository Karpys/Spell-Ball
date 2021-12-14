using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_ChooseOptions : MonoBehaviour
{

    [Header("BUTTONS")]
    public GameObject buttonGraph;

    Manager_MainMenu refMenuMain;
    UI_MenuPause refMenuPause;

    [Header("CheckScene")]
    public string scene1;
    public string scene2;

    public Animator animref;

    private void Start()
    {
        animref = gameObject.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttonGraph);

        buttonGraph.gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        animref.SetBool("ENTER_OP", true);

        //Check Which scene is currently active
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneName = currentScene.buildIndex;

        if (sceneName == 0)
        {
            refMenuMain = FindObjectOfType<Manager_MainMenu>();
            refMenuMain.UIIndex = 1;
        }
        else if (sceneName == 1)
        {
            refMenuPause = FindObjectOfType<UI_MenuPause>();
            refMenuPause.UIIndex = 1;
        }


    }

    public void ENTER()
    {
        animref.SetBool("ENTER_OP", false);
    }

    public void QUIT()
    {
        animref.SetBool("QUIT_OP", false);
    }
}
