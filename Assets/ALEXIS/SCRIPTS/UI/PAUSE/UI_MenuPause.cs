using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class UI_MenuPause : MonoBehaviour
{
    public GameObject fond;
    public GameObject targetMenu;
    public Animator animator;
    public GameObject fadeRef;
    Animation animFade;

    public float timeTransi = 1f;

    public bool playerDoPause;
    public bool unPause = false;


    private float timer = 1f;

    public GameObject mainM;
    public GameObject optionsM;
    public GameObject buttonOp;

    [Header("OnFocus")]
    public GameObject resumeFocus;
    public GameObject OnGraphics;
    public GameObject OnQuality;
    public GameObject OnMaster;

    public GameObject Audio;
    public GameObject Graph;

    [Header("SceneParam")]
    public string sceneName;
    public int UIIndex = 0;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.transform.GetChild(1).GetComponent<Animator>();
        animFade = fadeRef.GetComponent<Animation>();
        Audio.gameObject.GetComponent<UI_Options_Audio>().ParamSaved();
        Graph.gameObject.GetComponent<UI_Options_Graphics>().ParamGraphChanged();
    }

    // Update is called once per frame
    void Update()
    {

        if (unPause == true)
        {
            timer -= Time.deltaTime;

            if(timer < 0)
            {
                targetMenu.gameObject.SetActive(false);
                fond.gameObject.SetActive(false);
                timer = 1f;
                unPause = false;
                playerDoPause = false;
            }
        }
    }

    public void OnFocusAgain()
    {

        if (Time.timeScale == 0f && EventSystem.current.currentSelectedGameObject == null)
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

                        EventSystem.current.SetSelectedGameObject(resumeFocus);

                    }
                    break;
            }
        }

    }

    public void Resume()
    {
        AkSoundEngine.PostEvent("Play_B_UI_Click", this.gameObject);
        animator.SetBool("Deactive", true);
        unPause = true;
        Time.timeScale = 1f;
        EventSystem.current.SetSelectedGameObject(null);
        AkSoundEngine.SetRTPCValue("GP_Lowpass_Pause", Mathf.Lerp(50, 0, 4 / timeTransi));
    }
    public void Pause()
    {
        AkSoundEngine.PostEvent("Play_B_UI_SideSrompt", this.gameObject);
        playerDoPause = true;
        Time.timeScale = 0f;
        fond.gameObject.SetActive(true);
        targetMenu.gameObject.SetActive(true);
        animator.SetBool("Active", true);

        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(resumeFocus);

        AkSoundEngine.SetRTPCValue("GP_Lowpass_Pause", Mathf.Lerp(0, 50, 4 / timeTransi));
    }
    public void Options()
    {
        AkSoundEngine.PostEvent("Play_B_UI_Click", this.gameObject);
        animator.SetBool("Deactive", true);
        buttonOp.gameObject.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        StartCoroutine("WaitOptions");
    }
    public void MainMenu()
    {
        AkSoundEngine.PostEvent("Play_B_UI_Click", this.gameObject);
        animator.SetBool("Deactive", true);
        animFade.Play("FadeOut_UI_V001");
        Time.timeScale = 1f;
        StartCoroutine("WaitMainMenu");
    }

    IEnumerator WaitOptions()
    {
        yield return new WaitForSecondsRealtime(1);
        EventSystem.current.SetSelectedGameObject(null);
        optionsM.gameObject.SetActive(true);
        mainM.gameObject.SetActive(false);
    }

    IEnumerator WaitMainMenu()
    {
        yield return new WaitForSecondsRealtime(1);
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene(sceneName);
    }

}