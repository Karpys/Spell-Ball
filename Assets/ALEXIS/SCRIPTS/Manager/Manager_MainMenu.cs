using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager_MainMenu : MonoBehaviour
{
    public AnimationClip earlyMenu;
    public GameObject launch;
    public GameObject fadeRef;
    
    // Start is called before the first frame update
    void Start()
    {
        launch.GetComponent<Animation>().clip = earlyMenu;
        StartCoroutine(Wait1s());

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

    IEnumerator Wait1s()
    {
        yield return new WaitForSeconds(1);
        
        launch.GetComponent<Animation>().Play();
        launch.SetActive(true);
        fadeRef.SetActive(false);
    }
}
