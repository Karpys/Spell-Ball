using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_BasicEnemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    public List<GameObject> players;
    public Transform playerMov1;
    public Transform playerMov2;
    public Manager_NumbPlayers ManagePlayer;
    public int lifeBEnemy = 1;
    int r;
    bool playerIsInRange;


    // Start is called before the first frame update
    void Start()
    {
        /*playerMov1 = GameObject.FindWithTag("Player").transform;
        playerMov2 = GameObject.FindWithTag("Player1").transform;*/
        ManagePlayer = FindObjectOfType<Manager_NumbPlayers>();
        playerMov1 = ManagePlayer.player1.transform;
        playerMov2 = ManagePlayer.player2.transform;

        r = Random.Range(0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerIsInRange();
        CheckLifeEnemy();
    }

    public void CheckPlayerIsInRange()
    {
        if(playerIsInRange == false)
        {
            if (r <= 3)
            {
                enemy.SetDestination(playerMov2.position);
            }
            else if (r > 3)
            {
                enemy.SetDestination(playerMov1.position);
            }
        }
    }

    public void CheckLifeEnemy()
    {
        if (lifeBEnemy <= 0)
        {
            Destroy(gameObject);
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsInRange = true;
            enemy.SetDestination(other.transform.position);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsInRange = false;
            r = Random.Range(0, 5);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Balle")
        {
            ComboManager.instance.combo = gameObject.GetComponent<Manager_Life>().damages;
            gameObject.GetComponent<Manager_Life>().OnDamage.Invoke();

            ComboManager.instance.combo = 0;
        }
    }
}

