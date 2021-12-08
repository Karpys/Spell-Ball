using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Music_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public AK.Wwise.Event PlayMusicBoss;
    public string RTPCToSet = "";
    public float TransitionDuration;
    private float timer;
    void Start()
    {
        PlayMusicBoss.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= TransitionDuration)
        {
            timer += Time.deltaTime;
        }

        if (RTPCToSet !="")
        {
            AkSoundEngine.SetRTPCValue(RTPCToSet, Mathf.Lerp(0, 50, timer / TransitionDuration));
        }
    }

    public void LaunchLayer(int id)
    {
        string layer = "GP_M_Layer";
        layer += id.ToString();
        timer = 0;
        RTPCToSet = layer;
        
    }

    public void WeakBoss()
    {
        AkSoundEngine.SetRTPCValue("GP_M_BOSS_IsWeak", 100);
    }
}
