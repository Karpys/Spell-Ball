using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Enemy : MonoBehaviour
{
    public GameObject prefabEnemyBasic;
    Manager_NumbPlayers recupValue;
    public int n = 2;
    // Start is called before the first frame update
    void Start()
    {
        recupValue = FindObjectOfType<Manager_NumbPlayers>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WaitAndLaunch()
    {

            StartCoroutine(Wait1S());
    }
    public void InvokeEnemies()
    {
            for (int i = 0; i < n; i++)
            {
                GameObject En = Instantiate(prefabEnemyBasic, new Vector3(Random.Range(-42, 42), 0, Random.Range(22, -50)), Quaternion.identity);
                /*En.GetComponent<IA_BasicEnemy>().playerMov1 = recupValue.player1.transform;
                En.GetComponent<IA_BasicEnemy>().playerMov2 = recupValue.player2.transform;*/
            }
    }

    IEnumerator Wait1S()
    {
        if (recupValue.number == 1)
        {
            yield return new WaitForSeconds(2);
            InvokeEnemies();
        }
    }
}
