using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject BaseShield;
    public GameObject BaseShooter;
    public GameObject BaseLaser;
    public GameObject BaseGameObject;
    private static GameManager inst;
    public static GameManager gameManager { get => inst; }

    void Awake()
    {
        if (gameManager != null && gameManager != this)
            Destroy(gameObject);

        inst = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
