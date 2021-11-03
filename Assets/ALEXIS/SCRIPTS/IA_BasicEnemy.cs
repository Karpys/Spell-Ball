using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static GameUtilities.GameUtilities;

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

    public bool CanMove = true;

    
    [SerializeField] float DelayCheckout = 5.0f;
    /*[SerializeField] private float Range;*/
    private GameObject Target;
    float timerCheckout;


    // Start is called before the first frame update
    void Start()
    {
        /*playerMov1 = GameObject.FindWithTag("Player").transform;
        playerMov2 = GameObject.FindWithTag("Player1").transform;*/
        /*ManagePlayer = FindObjectOfType<Manager_NumbPlayers>();
        playerMov1 = ManagePlayer.player1.transform;
        playerMov2 = ManagePlayer.player2.transform;*/

        r = Random.Range(0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerCheckout >= DelayCheckout)
        {
            timerCheckout = 0;
            CheckOutClosestPlayer();
        }

        if (CanMove)
        {
            CheckPlayerIsInRange();
        }
        CheckLifeEnemy();
    }

    public void UpdateMovement()
    {
        if (!CanMove)
        {
            enemy.SetDestination(transform.position);
        }
    }

    public void CheckPlayerIsInRange()
    {
        /*if(playerIsInRange == false)
        {
            if (r <= 3)
            {
                enemy.SetDestination(playerMov2.position);
            }
            else if (r > 3)
            {
                enemy.SetDestination(playerMov1.position);
            }
        }*/
        if (Target)
        {
            enemy.SetDestination(Target.transform.position);
        }
        else
        {
            CheckOutClosestPlayer();
        }
    }

    public void CheckOutClosestPlayer()
    {
        Target = GetClosestGameObject(transform.position, ListToListGameObjects(FindObjectsOfType<PlayerController>().ToList()));
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
       /* if (other.gameObject.tag == "Player")
        {
            playerIsInRange = true;
            enemy.SetDestination(other.transform.position);
        }*/

       /* if (other.gameObject.tag == "Balle")
        {
            //gameObject.GetComponent<Manager_Life>().damages = collision.gameObject.GetComponent<Balle>().combo;
            

        }*/

    }
    private void OnTriggerExit(Collider other)
    {
        /*if (other.gameObject.tag == "Player")
        {
            playerIsInRange = false;
            r = Random.Range(0, 5);
        }*/

    }

   /* private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Balle")
        {
            //gameObject.GetComponent<Manager_Life>().damages = collision.gameObject.GetComponent<Balle>().combo;
            gameObject.GetComponent<Manager_Life>().DamageHealth(collision.gameObject.GetComponent<Balle>().combo);
            collision.gameObject.GetComponent<Balle>().combo = 0;
            GameObject Parti = Instantiate(ParticleEffectOnHit, collision.transform.position, transform.rotation);
            Parti.GetComponent<ParticleManager>().ApplyColor(collision.gameObject.GetComponent<Balle>().trail.startColor);
            
        }
    }*/
}

