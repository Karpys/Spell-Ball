using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time>5)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0,1f), Random.Range(0, 1f), Random.Range(0, 1f),0.196f);
            time = 0;
        }

    }
}
