using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestructScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float Timer;
    void Start()
    {
        Destroy(gameObject,Timer);
    }

}
