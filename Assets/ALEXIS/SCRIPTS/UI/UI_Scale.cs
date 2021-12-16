using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scale : MonoBehaviour
{
    Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);


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
        this.transform.localScale += scale;
        AkSoundEngine.PostEvent("Play_B_UI_Hover_Small", this.gameObject);
    }

    public void ExitEnter()
    {
        this.transform.localScale -= scale;

    }


}
