using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Enemy : MonoBehaviour
{
    public Transform prefabEnemyBasic;
    Test recupValue;
    public int n = 2;
    // Start is called before the first frame update
    void Start()
    {
        recupValue = FindObjectOfType<Test>();
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
        if(recupValue.number == 2)
        {
            for (int i = 0; i < n; i++)
            {
                Instantiate(prefabEnemyBasic, new Vector3(i, 0, 0), Quaternion.identity);
            }
        }

    }

    IEnumerator Wait1S()
    {

        yield return new WaitForSeconds(2);
        InvokeEnemies();
    }
}
