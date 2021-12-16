using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UI_Options_Graphics : MonoBehaviour
{

    [Header("Ref")]
    public GameObject refMenuGraph;
    public GameObject refChooseOptions;


    [Header("BUTTONS")]
    public GameObject quality;
    public GameObject resolution;
    public GameObject fullscreen;
    public GameObject back;

    [Header("UI")]
    public Dropdown qualityD;
    public Dropdown resolutionD;
    public Toggle fullscreenT;

    [Header("CheckScene")]
    public string scene1;
    public string scene2;

    bool onFullScreen = true;
    public List<ResItem> resolutionDetails;

    int qual = 2;
    int res = 0;
    int saveFullscreen = 1;

    Manager_MainMenu refMenuMain;
    UI_MenuPause refMenuPause;

    public Animator animatorRef;
    private void Start()
    {
        animatorRef = gameObject.GetComponent<Animator>();
    }
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quality);

        animatorRef.SetBool("ENTER_GRA", true);

        AkSoundEngine.PostEvent("Play_B_UI_SideSrompt", this.gameObject);

        PlayerPrefs.SetInt("SaveExist", 1);
        PlayerPrefs.Save();

        //Check Which scene is currently active
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneName = currentScene.buildIndex;

        if (sceneName == 0)
        {
            refMenuMain = FindObjectOfType<Manager_MainMenu>();
            refMenuMain.UIIndex = 2;
        }
        else if (sceneName == 1)
        {
            refMenuPause = FindObjectOfType<UI_MenuPause>();
            refMenuPause.UIIndex = 2;
        }

        ParamGraphChanged();

    }

    public void ParamGraphChanged()
    {
        //SAUVEGARDE
        if (PlayerPrefs.HasKey("qualityIndex"))
        {
            int qual = PlayerPrefs.GetInt("qualityIndex");
            SetQuality(qual);
            qualityD.value = qual;
        }
        else
        {
            SetQuality(qual);
            qualityD.value = qual;
        }

        if (PlayerPrefs.HasKey("resIndex"))
        {
            int res = PlayerPrefs.GetInt("resIndex");
            SetResolutionG(res);
            resolutionD.value = res;
        }
        else
        {
            SetResolutionG(res);
            resolutionD.value = res;
        }

        if (PlayerPrefs.HasKey("saveFullscreen"))
        {
            saveFullscreen = PlayerPrefs.GetInt("saveFullscreen", saveFullscreen);

            if (saveFullscreen > 0)
            {
                Screen.fullScreen = true;
                fullscreenT.isOn = true;
            }
            else
            {
                Screen.fullScreen = false;
                fullscreenT.isOn = false;
            }
        }
        else
        {
            Screen.fullScreen = true;
            fullscreenT.isOn = true;
            saveFullscreen = 1;
        }


    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        onFullScreen = isFullscreen;

        if (onFullScreen)
        {
            saveFullscreen = 1;
            PlayerPrefs.SetInt("saveFullscreen", saveFullscreen);
        }
        else
        {
            saveFullscreen = 0;
            PlayerPrefs.SetInt("saveFullscreen", saveFullscreen);
        }
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("qualityIndex", qualityIndex);
        PlayerPrefs.Save();
    }

    public void SetResolutionG(int resIndex)
    {
        Screen.SetResolution(resolutionDetails[resIndex].horizontal, resolutionDetails[resIndex].vertical, onFullScreen);
        PlayerPrefs.SetInt("resIndex", resIndex);
        PlayerPrefs.Save();
    }
    public void OnBack()
    {
        AkSoundEngine.PostEvent("Play_B_UI_Click", this.gameObject);
        back.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        EventSystem.current.SetSelectedGameObject(null);
        animatorRef.SetBool("QUIT_GRA", true);
        StartCoroutine("WaitEndAnim");
    }

    IEnumerator WaitEndAnim()
    {
        yield return new WaitForSecondsRealtime(1f);
        EventSystem.current.SetSelectedGameObject(null);
        refMenuGraph.SetActive(false);
        refChooseOptions.SetActive(true);
    }

    public void ENTER()
    {
        animatorRef.SetBool("ENTER_GRA", false);
    }

    public void QUIT()
    {
        animatorRef.SetBool("QUIT_GRA", false);
    }
}



[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
