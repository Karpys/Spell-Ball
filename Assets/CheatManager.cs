using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheatManager : MonoBehaviour
{
    [Header("Panel")]
    public GameObject ballPanel;
    public GameObject ennemyPanel;
    public GameObject lifePanel;

    [Header("Balls")]
    public GameObject ballPrefab;
    public Text nCountBall;
    public Text nBallSpawn;
    public Text nBallLevel;
    private List<GameObject> balls = new List<GameObject>();

    [Header("Ennemy")]
    public GameObject ennemyPrefab;
    public Text nCountEnnemy;
    public Text nEnnemySpawn;
    public Text nEnnemyLife;
    private List<GameObject> ennemys = new List<GameObject>();

    public void OpenBallPanel()
    {
        ballPanel.SetActive(true);
        ennemyPanel.SetActive(false);
        lifePanel.SetActive(false);

        foreach (GameObject ball in GameObject.FindGameObjectsWithTag("Balle"))
            balls.Add(ball);

        nCountBall.text = balls.Count.ToString();
    }
    public void SpawnBalls()
    {
        for(int i = 0; i < System.Convert.ToInt32(nBallSpawn.text); i++)
        {
            GameObject ball = GameObject.Instantiate(ballPrefab,new Vector3(-4.5f, 2f, -2.3f),Quaternion.Euler(0,Random.Range(0f, 360f),0));
            ball.GetComponent<Balle>().combo = System.Convert.ToInt32(nBallLevel.text);
            balls.Add(ball);
        }
        nCountBall.text = balls.Count.ToString();
    }

    public void DestroyBalls()
    {
        foreach (GameObject destroy in balls)
            GameObject.Destroy(destroy);

        balls.Clear();

        nCountBall.text = balls.Count.ToString();
    }

    public void OpenEnemiesPanel()
    {
        ballPanel.SetActive(false);
        ennemyPanel.SetActive(true);
        lifePanel.SetActive(false);

        foreach (GameObject ennemy in GameObject.FindGameObjectsWithTag("Ennemy"))
            ennemys.Add(ennemy);

        nCountEnnemy.text = ennemys.Count.ToString();
    }

    public void DestroyEnnemys()
    {
        foreach (GameObject destroy in ennemys)
            GameObject.Destroy(destroy);

        ennemys.Clear();

        nCountEnnemy.text = ennemys.Count.ToString();
    }

    public void SpawnEnnemy()
    {
        for (int i = 0; i < System.Convert.ToInt32(nEnnemySpawn.text); i++)
        {
            GameObject ennemy = GameObject.Instantiate(ennemyPrefab, new Vector3(Random.Range(-18,5), 2f, Random.Range(-13, 3)), Quaternion.identity);
            ennemy.transform.localScale = Vector3.one;
            ennemy.GetComponent<Manager_Life>().maxHealth = System.Convert.ToInt32(nEnnemyLife.text);
            ennemys.Add(ennemy);
        }
        nCountEnnemy.text = ennemys.Count.ToString();
    }


    public void OpenLifePanel()
    {
        ballPanel.SetActive(false);
        ennemyPanel.SetActive(false);
        lifePanel.SetActive(true);
    }


}
