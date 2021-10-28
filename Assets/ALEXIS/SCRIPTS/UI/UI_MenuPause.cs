using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class UI_MenuPause : MonoBehaviour
{
    public GameObject targetMenu;
    public bool isPause = false;
    public Animator animator;

    public GameObject resumeFocus;

    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.transform.GetChild(0).GetComponent<Animator>();
        EventSystem.current.SetSelectedGameObject(null);

        EventSystem.current.SetSelectedGameObject(resumeFocus);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Resume()
    {
        animator.SetBool("Active", false);
        StartCoroutine(Wait());
    }

    public void Pause()
    {
        if (isPause == true)
        {

            animator.SetBool("Active", false);
            StartCoroutine(Wait());


        }
        else
        {
            targetMenu.gameObject.SetActive(true);
            animator.SetBool("Active", true);
            isPause = true;
            Time.timeScale = 0f;

            EventSystem.current.SetSelectedGameObject(null);

            EventSystem.current.SetSelectedGameObject(resumeFocus);
        }


        
    }
    public void Options()
    {
        Debug.Log("OPTIONS");
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Scene_Alexis");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.5f);
        targetMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;

    }


}
