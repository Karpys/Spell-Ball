using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scale : MonoBehaviour
{
    Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);



    public void EnterPointer()
    {
        AkSoundEngine.PostEvent("Play_B_UI_Hover_Small", this.gameObject);
        this.transform.localScale += scale;
    }

    public void ExitEnter()
    {
        this.transform.localScale -= scale;

    }


}
