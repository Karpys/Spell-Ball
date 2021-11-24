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
    public Dropdown typeEnemy;
    private List<GameObject> ennemys = new List<GameObject>();

    [Header("Player Life")]
    public GameObject playerManager;
    [SerializeField] public PlayerLifeCheat[] playerLife = new PlayerLifeCheat[4];

    [Header("Botton")]
    public GameObject botton;

    public GameObject GroupCheat;


    public void ShowHide()
    {
        GroupCheat.SetActive(!GroupCheat.activeSelf);
    }

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
            ennemy.GetComponent<Manager_Life>().maxHealth = System.Convert.ToInt32(nEnnemyLife.text); //10;

            switch (typeEnemy.value)
            {
                case 0:
                    ennemy.GetComponentInChildren<EnemyColor>().random = true;
                    break;

                case 1:
                    ennemy.GetComponentInChildren<EnemyColor>().GetColorCopy().color = Color.white;
                    break;

                case 2:
                    ennemy.GetComponentInChildren<EnemyColor>().GetColorCopy().color = Color.red;
                    break;

                case 3:
                    ennemy.GetComponentInChildren<EnemyColor>().GetColorCopy().color = ColorInfuse.instance.orange;
                    break;

                case 4:
                    ennemy.GetComponentInChildren<EnemyColor>().GetColorCopy().color = Color.blue;
                    break;

                case 5:
                    ennemy.GetComponentInChildren<EnemyColor>().GetColorCopy().color = Color.green;
                    break;
            }
            ennemys.Add(ennemy);
        }
        nCountEnnemy.text = ennemys.Count.ToString();
    }

    public void OpenLifePanel()
    {
        ballPanel.SetActive(false);
        ennemyPanel.SetActive(false);
        lifePanel.SetActive(true);
        
        if(playerManager.GetComponent<Manager_NumbPlayers>().player1 != null)
        {
            playerLife[0].Life.gameObject.SetActive(true);
            playerLife[0].LifeText.gameObject.SetActive(true);
            playerLife[0].nPlayerText.gameObject.SetActive(true);
            playerLife[0].Life.maxValue = playerManager.GetComponent<Manager_NumbPlayers>().player1.GetComponent<Manager_Life>().maxHealth;
            playerLife[0].Life.value =  playerManager.GetComponent<Manager_NumbPlayers>().player1.GetComponent<Manager_Life>().GetCurentLife();
            playerLife[0].LifeText.text = ((int)playerManager.GetComponent<Manager_NumbPlayers>().player1.GetComponent<Manager_Life>().GetCurentLife()).ToString();
        }
        else
        {
            playerLife[0].Life.gameObject.SetActive(false);
            playerLife[0].LifeText.gameObject.SetActive(false);
            playerLife[0].nPlayerText.gameObject.SetActive(false);
        }

        if (playerManager.GetComponent<Manager_NumbPlayers>().player2 != null)
        {
            playerLife[1].Life.gameObject.SetActive(true);
            playerLife[1].LifeText.gameObject.SetActive(true);
            playerLife[1].nPlayerText.gameObject.SetActive(true);
            playerLife[1].Life.maxValue = playerManager.GetComponent<Manager_NumbPlayers>().player2.GetComponent<Manager_Life>().maxHealth;
            playerLife[1].Life.value = playerManager.GetComponent<Manager_NumbPlayers>().player2.GetComponent<Manager_Life>().GetCurentLife();
            playerLife[1].LifeText.text = ((int)playerManager.GetComponent<Manager_NumbPlayers>().player2.GetComponent<Manager_Life>().GetCurentLife()).ToString();
        }
        else
        {
            playerLife[1].Life.gameObject.SetActive(false);
            playerLife[1].LifeText.gameObject.SetActive(false);
            playerLife[1].nPlayerText.gameObject.SetActive(false);
        }

        if (playerManager.GetComponent<Manager_NumbPlayers>().player3 != null)
        {
            playerLife[2].Life.gameObject.SetActive(true);
            playerLife[2].LifeText.gameObject.SetActive(true);
            playerLife[2].nPlayerText.gameObject.SetActive(true);
            playerLife[2].Life.maxValue = playerManager.GetComponent<Manager_NumbPlayers>().player3.GetComponent<Manager_Life>().maxHealth;
            playerLife[2].Life.value = playerManager.GetComponent<Manager_NumbPlayers>().player3.GetComponent<Manager_Life>().GetCurentLife();
            playerLife[2].LifeText.text = ((int)playerManager.GetComponent<Manager_NumbPlayers>().player3.GetComponent<Manager_Life>().GetCurentLife()).ToString();
        }
        else
        {
            playerLife[2].Life.gameObject.SetActive(false);
            playerLife[2].LifeText.gameObject.SetActive(false);
            playerLife[2].nPlayerText.gameObject.SetActive(false);
        }

        if (playerManager.GetComponent<Manager_NumbPlayers>().player4 != null)
        {
            playerLife[3].Life.gameObject.SetActive(true);
            playerLife[3].LifeText.gameObject.SetActive(true);
            playerLife[3].nPlayerText.gameObject.SetActive(true);
            playerLife[3].Life.maxValue = playerManager.GetComponent<Manager_NumbPlayers>().player4.GetComponent<Manager_Life>().maxHealth;
            playerLife[3].Life.value = playerManager.GetComponent<Manager_NumbPlayers>().player4.GetComponent<Manager_Life>().GetCurentLife();
            playerLife[3].LifeText.text = ((int)playerManager.GetComponent<Manager_NumbPlayers>().player4.GetComponent<Manager_Life>().GetCurentLife()).ToString();
        }
        else
        {
            playerLife[3].Life.gameObject.SetActive(false);
            playerLife[3].LifeText.gameObject.SetActive(false);
            playerLife[3].nPlayerText.gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        if(lifePanel.activeSelf)
        {
            if(playerLife[0].Life.gameObject.activeSelf)
            {
                playerManager.GetComponent<Manager_NumbPlayers>().player1.GetComponent<Manager_Life>().SetCurentLife(playerLife[0].Life.value);
                playerLife[0].LifeText.text = ((int)playerManager.GetComponent<Manager_NumbPlayers>().player1.GetComponent<Manager_Life>().GetCurentLife()).ToString();
            }

            if (playerLife[1].Life.gameObject.activeSelf)
            {
                playerManager.GetComponent<Manager_NumbPlayers>().player2.GetComponent<Manager_Life>().SetCurentLife(playerLife[1].Life.value);
                playerLife[1].LifeText.text = ((int)playerManager.GetComponent<Manager_NumbPlayers>().player2.GetComponent<Manager_Life>().GetCurentLife()).ToString();
            }

            if (playerLife[2].Life.gameObject.activeSelf)
            {
                playerManager.GetComponent<Manager_NumbPlayers>().player3.GetComponent<Manager_Life>().SetCurentLife(playerLife[2].Life.value);
                playerLife[2].LifeText.text = ((int)playerManager.GetComponent<Manager_NumbPlayers>().player3.GetComponent<Manager_Life>().GetCurentLife()).ToString();
            }

            if (playerLife[3].Life.gameObject.activeSelf)
            {
                playerManager.GetComponent<Manager_NumbPlayers>().player4.GetComponent<Manager_Life>().SetCurentLife(playerLife[3].Life.value);
                playerLife[3].LifeText.text = ((int)playerManager.GetComponent<Manager_NumbPlayers>().player4.GetComponent<Manager_Life>().GetCurentLife()).ToString();
            }

        }
    }

    public void AddPlayerBotton()
    {
        //Debug.Log("coucou je spawn!");
        GameObject player = GameObject.Instantiate(playerManager.GetComponent<Manager_NumbPlayers>().listPlayers[playerManager.GetComponent<Manager_NumbPlayers>().number], new Vector3(Random.Range(-18, 5), 2f, Random.Range(-13, 3)), Quaternion.identity);
        if (playerManager.GetComponent<Manager_NumbPlayers>().player3)
            botton.SetActive(false);
        //playerManager.GetComponent<Manager_NumbPlayers>().AddPlayer();
    }
}

[System.Serializable]
public struct PlayerLifeCheat
{
    public Slider Life;
    public Text LifeText;
    public Text nPlayerText;
}
