using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Music_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public AK.Wwise.Event PlayMusicBoss;
    void Start()
    {
        PlayMusicBoss.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchLayer(int id)
    {
        string layer = "GP_M_Layer";
        layer += id.ToString();
        
        Debug.Log(layer);
        AkSoundEngine.SetRTPCValue(layer,50);
    }

    public void WeakBoss()
    {
        AkSoundEngine.SetRTPCValue("GP_M_BOSS_IsWeak", 100);
    }
}
