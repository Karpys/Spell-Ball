using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_Ball : MonoBehaviour
{
    public GameObject prefabBall;
    GameObject ball;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindWithTag("Balle");
    }

    // Update is called once per frame
    void Update()
    {
          
    }

    void SpawnBall()
    {

        GameObject Ball = Instantiate(prefabBall, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
