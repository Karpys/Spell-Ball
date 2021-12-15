using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    GameObject [] player = new GameObject [4];
    public GameObject Fade;
    public GameInfo Info;
    //public GameObject Boss;
    //public GameObject pauseMenu;
    //public Animation FadeOut;

    public static GameOver instance = null;
    bool gameOver = false;
    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player[0] = GameObject.Find("Player Rouge(Clone)");
        player[1] = GameObject.Find("Player Vert(Clone)");
        player[2] = GameObject.Find("Player Orange(Clone)");
        player[3] = GameObject.Find("Player Bleu(Clone)");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckGameOver()
    {
        
        bool end = true;
        Debug.Log("check end"+ end);
        for(int i = 0; i<player.Length;i++)
        {
            if (player[i].GetComponent<Manager_Life>().GetCurentLife() > 0 && end)
                end = false;
        }

        if(end && !gameOver)
        {
            StartCoroutine(PlayGameOver());
        }
    }

    public IEnumerator PlayGameOver()
    {
        Debug.Log("GAME OVER");
        gameOver = true;
        Fade.SetActive(true);
        Info.victory = false;
        Fade.GetComponent<Animation>().Play("FadeOut_UI_V001");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);

    }
}
