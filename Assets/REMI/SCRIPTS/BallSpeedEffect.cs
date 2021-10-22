using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpeedEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private float delayBetween;
    private int NbrEffect;
    [SerializeField] private GameObject Effect;
    float timer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= delayBetween && NbrEffect!=0)
        {
            NbrEffect--;
            timer = 0;
            CreateEffect();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void CreateEffect()
    {
        Instantiate(Effect, transform.position, transform.rotation);

    }
}
