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


        AkSoundEngine.PostEvent("Play_B_UI_SideSrompt", this.gameObject);

        animatorRef.SetBool("ENTER_AU", true);

        ParamSaved();

        //Check Which scene is currently active
        Scene currentScene = SceneManager.GetActiveScene();
        int sceneName = currentScene.buildIndex;

        if (sceneName == 0)
        {
            refMenuMain = FindObjectOfType<Manager_MainMenu>();
            refMenuMain.UIIndex = 3;
        }
        else if (sceneName == 1)
        {
            refMenuPause = FindObjectOfType<UI_MenuPause>();
            refMenuPause.UIIndex = 3;
        }



    }

    // Start is called before the first frame update
    void Start()
    {
        animatorRef = gameObject.GetComponent<Animator>();


    }
    public void ParamSaved()
    {
        PlayerPrefs.SetInt("SaveExist", 1);
        PlayerPrefs.Save();

        //SAUVEGARDE
        if (PlayerPrefs.HasKey("volumeMaster"))
        {
            volM = PlayerPrefs.GetFloat("volumeMaster");
            masterS.value = volM;
            masterV.text = volM.ToString("0");
            AkSoundEngine.SetRTPCValue("GP_MASTER", masterS.value);
        }
        else
        {
            masterS.value = 50;
            masterV.text = "50";
            AkSoundEngine.SetRTPCValue("GP_MASTER", masterS.value);
        }

        if (PlayerPrefs.HasKey("volumeVfx"))
        {
            volV = PlayerPrefs.GetFloat("volumeVfx");
            vfxS.value = volV;
            vfxV.text = volV.ToString("0");
            AkSoundEngine.SetRTPCValue("GP_SFX", vfxS.value);
        }
        else
        {
            vfxS.value = 50;
            vfxV.text = "50";
            AkSoundEngine.SetRTPCValue("GP_SFX", vfxS.value);
        }

        if (PlayerPrefs.HasKey("volumeMusic"))
        {
            volMu = PlayerPrefs.GetFloat("volumeMusic");
            musicS.value = volMu;
            musicV.text = volMu.ToString("0");
            AkSoundEngine.SetRTPCValue("GP_MUSIC", musicS.value);
        }
        else
        {
            musicS.value = 50;
            musicV.text = "50";
            AkSoundEngine.SetRTPCValue("GP_MUSIC", musicS.value);
        }
    }
    public void OnSliderMaster(float volume)
    {

        masterV.text = volume.ToString("0"); 
        PlayerPrefs.SetFloat("volumeMaster", volume);
        AkSoundEngine.SetRTPCValue("GP_MASTER", masterS.value);
        PlayerPrefs.Save();
    }

    public void OnSliderVfx(float volume)
    {

        vfxV.text = volume.ToString("0");
        PlayerPrefs.SetFloat("volumeVfx", volume);
        AkSoundEngine.SetRTPCValue("GP_SFX", vfxS.value);
        PlayerPrefs.Save();
    }

    public void OnSliderMusic(float volume)
    {

        musicV.text = volume.ToString("0");
        PlayerPrefs.SetFloat("volumeMusic", volume);
        AkSoundEngine.SetRTPCValue("GP_MUSIC", musicS.value);
        PlayerPrefs.Save();
    }

    public void OnBack()
    {
        AkSoundEngine.PostEvent("Play_B_UI_Click", this.gameObject);
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
