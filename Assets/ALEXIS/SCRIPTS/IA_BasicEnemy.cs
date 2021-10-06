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
    int r;
    GameObject test;
    bool playerIsInRange;


    Test recupValue;
    // Start is called before the first frame update
    void Start()
    {
        recupValue = FindObjectOfType<Test>();
        playerMov1 = GameObject.FindWithTag("Player").transform;
        playerMov2 = GameObject.FindWithTag("Player1").transform;
        r = Random.Range(0, 5);
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerIsInRange();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsInRange = true;
            enemy.SetDestination(other.transform.position);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsInRange = false;
            r = Random.Range(0, 5);
        }

    }
}
