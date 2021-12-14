using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObject_ScaleChange : MonoBehaviour
{
    public Vector3 scale = new Vector3(1f,1f,1f);
    bool goEnter= false;
    bool goBack = false;
    public float timer = 0.2f;

    public Vector3 scaleIt;
    private float timerV;
    private Vector3 changeScalePlus;
    private Vector3 changeScaleLow;
    // Start is called before the first frame update
    void Start()
    {
        timerV = timer;
        scaleIt = this.transform.localScale;
        changeScalePlus = this.transform.localScale += scale;
        changeScaleLow = this.transform.localScale -= scale;

        GrowUp();

    }

    // Update is called once per frame
    void Update()
    {



        if (goEnter == true)
        {
            timer -= Time.deltaTime;
            scaleIt = Vector3.Lerp(this.transform.localScale, this.transform.localScale += scale, 1f);

            //if (timer <= 0)
            //{
            //    timer = timerV;
            //    scaleIt = this.transform.localScale;
            //    Back();
            //    goEnter = false;
            //}
        }

        //if (goBack == true)
        //{
        //    timer -= Time.deltaTime;
        //    scaleIt = Vector3.Lerp(this.transform.localScale, this.transform.localScale -= scale, 1f);

        //    if (timer <= 0)
        //    {
        //        timer = timerV;
        //        scaleIt = this.transform.localScale;
        //        goBack = false;
        //    }
        //}

    }

    public void GrowUp()
    {

        goEnter = true;

    }

    public void Back()
    {

        goBack = true;
    }
}
