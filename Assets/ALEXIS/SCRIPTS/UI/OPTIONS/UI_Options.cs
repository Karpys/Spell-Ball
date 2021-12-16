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
    bool wFocus = true;

    public Animator animref;

    [Header("Music")]
    [SerializeField] private AK.Wwise.Event stopMusicMenu;
    [SerializeField] private GameObject bank;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttonGraph);

        AkSoundEngine.PostEvent("Play_B_UI_SideSrompt", this.gameObject);
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
        AkSoundEngine.PostEvent("Play_B_UI_Click", this.gameObject);
        EventSystem.current.SetSelectedGameObject(null);
        animref.SetBool("QUIT_OP", true);
        StartCoroutine("WaitGraph");
    }

    public void OnAudio()
    {
        AkSoundEngine.PostEvent("Play_B_UI_Click", this.gameObject);
        EventSystem.current.SetSelectedGameObject(null);
        animref.SetBool("QUIT_OP", true);
        StartCoroutine("WaitAudio");
    }

    public void OnResetSettings(string SceneName)
    {
        AkSoundEngine.PostEvent("Play_B_UI_Click", this.gameObject);
        PlayerPrefs.DeleteAll();
        stopMusicMenu.Post(bank);
        SceneManager.LoadScene(0);
    }

    public void OnBack()
    {
        AkSoundEngine.PostEvent("Play_B_UI_Click", this.gameObject);
        EventSystem.current.SetSelectedGameObject(null);
        animref.SetBool("QUIT_OP", true);
        StartCoroutine("WaitEndAnim");

    }

    IEnumerator WaitEndAnim()
    {
        yield return new WaitForSecondsRealtime(1);
        EventSystem.current.SetSelectedGameObject(null);
        buttonBack.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        BaseOp.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(true);
        mainMenu.GetComponent<Animator>().SetBool("ENTER", true);
    }

    IEnumerator WaitGraph()
    {
        yield return new WaitForSecondsRealtime(1);
        EventSystem.current.SetSelectedGameObject(null);
        Graph.gameObject.SetActive(true);
        chooseOp.gameObject.SetActive(false);
    }
    IEnumerator WaitAudio()
    {
        yield return new WaitForSecondsRealtime(1);
        EventSystem.current.SetSelectedGameObject(null);
        Audio.gameObject.SetActive(true);
        chooseOp.gameObject.SetActive(false);
    }
}
