using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Options_Audio : MonoBehaviour
{

    [Header("Ref")]
    public GameObject refMenuAudio;
    public GameObject refChooseOptions;

    [Header("BUTTONS")]
    public GameObject master;
    public GameObject vfx;
    public GameObject music;
    public GameObject back;

    [Header("VOLUMES")]
    public Text masterV;
    public Text vfxV;
    public Text musicV;

    [Header("Sliders")]
    public Slider masterS;
    public Slider vfxS;
    public Slider musicS;

    [Header("CheckScene")]
    public string scene1;
    public string scene2;

    [HideInInspector]
    public float volM = 50;
    [HideInInspector]
    public float volV = 50;
    [HideInInspector]
    public float volMu = 50;

    Manager_MainMenu refMenuMain;
    UI_MenuPause refMenuPause;

    public Animator animatorRef;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(master);


        PlayerPrefs.SetInt("SaveExist", 1);
        PlayerPrefs.Save();

        animatorRef.SetBool("ENTER_AU", true);

        //Check Which scene is currently active
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == scene1)
        {
            refMenuMain = FindObjectOfType<Manager_MainMenu>();
            refMenuMain.UIIndex = 3;
        }
        else if (sceneName == scene2)
        {
            refMenuPause = FindObjectOfType<UI_MenuPause>();
            refMenuPause.UIIndex = 3;
        }

        //SAUVEGARDE
        if (PlayerPrefs.HasKey("volumeMaster"))
        {
            volM = PlayerPrefs.GetFloat("volumeMaster");
            masterS.value = volM;
            masterV.text = volM.ToString("0");
        }
        else
        {
            masterS.value = 50;
            masterV.text = "50";
        }

        if (PlayerPrefs.HasKey("volumeVfx"))
        {
            volV = PlayerPrefs.GetFloat("volumeVfx");
            vfxS.value = volV;
            vfxV.text = volV.ToString("0");
        }
        else
        {
            vfxS.value = 50;
            vfxV.text = "50";
        }

        if (PlayerPrefs.HasKey("volumeMusic"))
        {
            volMu = PlayerPrefs.GetFloat("volumeMusic");
            musicS.value = volMu;
            musicV.text = volMu.ToString("0");
        }
        else
        {
            musicS.value = 50;
            musicV.text = "50";
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        animatorRef = gameObject.GetComponent<Animator>();
    }

    public void OnSliderMaster(float volume)
    {

        masterV.text = volume.ToString("0"); 
        PlayerPrefs.SetFloat("volumeMaster", volume);
        PlayerPrefs.Save();
    }

    public void OnSliderVfx(float volume)
    {

        vfxV.text = volume.ToString("0");
        PlayerPrefs.SetFloat("volumeVfx", volume);
        PlayerPrefs.Save();
    }

    public void OnSliderMusic(float volume)
    {

        musicV.text = volume.ToString("0");
        PlayerPrefs.SetFloat("volumeMusic", volume);
        PlayerPrefs.Save();
    }

    public void OnBack()
    {
        back.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        EventSystem.current.SetSelectedGameObject(null);
        animatorRef.SetBool("QUIT_AU", true);
        StartCoroutine("WaitEndAnim");
    }

    public void ENTER()
    {
        animatorRef.SetBool("ENTER_AU", false);
    }

    public void QUIT()
    {
        animatorRef.SetBool("QUIT_AU", false);
    }

    IEnumerator WaitEndAnim()
    {
        yield return new WaitForSecondsRealtime(1f);
        EventSystem.current.SetSelectedGameObject(null);
        refMenuAudio.SetActive(false);
        refChooseOptions.SetActive(true);
    }
}
