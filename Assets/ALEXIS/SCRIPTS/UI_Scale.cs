using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scale : MonoBehaviour
{
    Vector3 test = new Vector3(0.1f, 0.1f, 0.1f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnterPointer()
    {
        this.transform.localScale += test;


    }

    public void ExitEnter()
    {
        this.transform.localScale -= test;

    }


}
