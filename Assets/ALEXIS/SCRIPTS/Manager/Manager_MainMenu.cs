using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Manager_MainMenu : MonoBehaviour
{
    //public AnimationClip earlyMenu;
    public GameObject launch;
    public GameObject fadeRef;

    public GameObject optionsMenu;

    public string SceneName;

    [Header("WHICH ONE IS ACTIVE ?")]
    public int UIIndex;

    [Header("Focus On")]
    public GameObject play;
    public GameObject OnGraphics;
    public GameObject OnQuality;
    public GameObject OnMaster;

    [Header("Animator")]
    public Animator animRef;
    public float tempsAnim = 1f;



    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(play);

        if (!PlayerPrefs.HasKey("SaveExist"))
        {
            Screen.fullScreen = true;
            Screen.SetResolution(1920, 1080, true);
            QualitySettings.SetQualityLevel(2);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //Screen.fullScreen = true;
        //launch.GetComponent<Animation>().clip = earlyMenu;
        StartCoroutine(Wait());

        animRef = launch.GetComponent<Animator>();
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
    public void PlayButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        animRef.SetBool("QUIT", true);
        StartCoroutine("WaitPlay");
    }

    public void OptionsButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        animRef.SetBool("QUIT", true);
        StartCoroutine("WaitEndAnim");
        //launch.gameObject.SetActive(false);
        //optionsMenu.gameObject.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    #region Coroutine
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.2f);

        //launch.GetComponent<Animation>().Play();
        launch.SetActive(true);
        fadeRef.SetActive(false);
        animRef.SetBool("ENTER", true);

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(play);
    }

    IEnumerator WaitEndAnim()
    {
        yield return new WaitForSeconds(tempsAnim);
        EventSystem.current.SetSelectedGameObject(null);
        launch.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    IEnumerator WaitPlay()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneName);
    }
    #endregion
}
