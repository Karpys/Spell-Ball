using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Manager_MainMenu : MonoBehaviour
{
    public AnimationClip earlyMenu;
    public GameObject launch;
    public GameObject fadeRef;

    public GameObject optionsMenu;

    [Header("WHICH ONE IS ACTIVE ?")]
    public int UIIndex;

    [Header("Focus On")]
    public GameObject play;
    public GameObject OnGraphics;
    public GameObject OnQuality;
    public GameObject OnMaster;



    //Faire des booleans en fonction si Options est ouvert ou pas////////

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(play);


    }

    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = true;
        launch.GetComponent<Animation>().clip = earlyMenu;
        StartCoroutine(Wait());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnFocus(InputAction.CallbackContext ctx) => CheckFocus(ctx.ReadValue<Vector2>());

    public void CheckFocus(Vector2 mov)
    {
        switch (UIIndex)
        {
            case 3:
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(null);

                    EventSystem.current.SetSelectedGameObject(OnMaster);

                }
                break;
            case 2:
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(null);

                    EventSystem.current.SetSelectedGameObject(OnQuality);

                }
                break;
            case 1:
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(null);

                    EventSystem.current.SetSelectedGameObject(OnGraphics);

                }
                break;
            case 0:
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    EventSystem.current.SetSelectedGameObject(null);

                    EventSystem.current.SetSelectedGameObject(play);

                }
                break;
        }

    }
    public void PlayButton(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void OptionsButton()
    {
        optionsMenu.gameObject.SetActive(true);
        launch.gameObject.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);
        
        launch.GetComponent<Animation>().Play();
        launch.SetActive(true);
        fadeRef.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(play);
    }
}
