using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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

    bool onFullScreen = true;
    public List<ResItem> resolutionDetails;

    int qual = 2;
    int res = 0;
    int saveFullscreen = 1;

    Manager_MainMenu refMenu;
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(quality);

        refMenu = FindObjectOfType<Manager_MainMenu>();
        refMenu.UIIndex = 2;

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
    }
    public void OnBack()
    {
        refMenuGraph.SetActive(false);
        refChooseOptions.SetActive(true);
    }

    
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
