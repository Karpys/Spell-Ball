using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpeedEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float delayBetween;
    [SerializeField] private int NbrEffect;
    [SerializeField] private GameObject Effect;
    float timer;

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
        else if(NbrEffect==0)
        {
            Destroy(gameObject);
        }
    }

    void CreateEffect()
    {
        GameObject Obj = Instantiate(Effect, transform.position, transform.rotation);
        Obj.GetComponent<ParticleManager>().ApplyRotation(new Vector3(0, transform.eulerAngles.y, 0));

    }
}
