using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour
{
    public int combo;
    public float comboSpeed;

    public int comboSet;

    private Rigidbody rb;

    public static Balle instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        combo = 0;
        // rb.AddForce(new Vector3(Random.Range(-50, 50), 0, Random.Range(-100, 100)), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //combo = comboSet;
    }

    /*public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            combo = 0;
        }
    }*/

}
