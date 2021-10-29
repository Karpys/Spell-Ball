using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Manager_MainMenu : MonoBehaviour
{
    public AnimationClip earlyMenu;
    public GameObject launch;
    public GameObject fadeRef;

    public GameObject play, options, quit;



    // Start is called before the first frame update
    void Start()
    {
        launch.GetComponent<Animation>().clip = earlyMenu;
        StartCoroutine(Wait());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void OptionsButton()
    {

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
