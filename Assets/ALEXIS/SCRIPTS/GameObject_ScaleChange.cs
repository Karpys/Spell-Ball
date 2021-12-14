using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObject_ScaleChange : MonoBehaviour
{

    private bool goEnter= false;
    private bool goBack = false;

    [SerializeField] private Vector3 scaleIt = new Vector3(1f,1f,1f);
    private Vector3 plusScale;
    private Vector3 moinsScale;

    [SerializeField] private float timer = 0.5f;
    private float timerRef;

    [Range(0, 1)]
    [SerializeField] private float lerpTime = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        timerRef = timer;
        plusScale = this.transform.localScale + scaleIt;
        moinsScale = this.transform.localScale;

        GrowUp();

    }

    // Update is called once per frame
    void Update()
    {
        if (goEnter == true)
        {

            this.transform.localScale = Vector3.Lerp(this.transform.localScale, plusScale, lerpTime);

            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                timer = timerRef;
                goBack = true;
                goEnter = false;
            }
        }

        if (goBack == true)
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, moinsScale, lerpTime);

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = timerRef;
                goBack = false;
            }
        }
    }

    public void GrowUp()
    {
        goEnter = true;
    }
}
