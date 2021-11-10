using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    float volM = 50;
    float volV = 50;
    float volMu = 50;

    Manager_MainMenu refMenu;
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(master);

        refMenu = FindObjectOfType<Manager_MainMenu>();
        refMenu.UIIndex = 3;

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

    }

    // Update is called once per frame
    void Update()
    {
        
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
        refMenuAudio.SetActive(false);
        refChooseOptions.SetActive(true);
    }
}
