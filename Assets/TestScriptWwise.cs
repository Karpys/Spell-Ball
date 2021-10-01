using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptWwise : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            AkSoundEngine.PostEvent("Play_Test", gameObject);
        }
    }
}
