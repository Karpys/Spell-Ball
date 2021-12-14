using System.Collections;
using System.Collections.Generic;
using AK.Wwise;
using UnityEngine;

public class BossVoiceManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static BossVoiceManager inst;
    public static BossVoiceManager Voice { get => inst; }

    [SerializeField] private AK.Wwise.Event[] LaserVoice = new AK.Wwise.Event[2];
    [SerializeField] private AK.Wwise.Event[] HitVoice = new AK.Wwise.Event[2];
    public AK.Wwise.Event SplashVoice;
    public AK.Wwise.Event ChangePhase;

    void Awake()
    {
        if (Voice != null && Voice != this)
            Destroy(gameObject);

        inst = this;
    }

    public void PlayLaser()
    {
        LaserVoice[Random.Range(0, LaserVoice.Length)].Post(gameObject);
    }

    public void PlayHit()
    {
        HitVoice[Random.Range(0, HitVoice.Length)].Post(gameObject);
    }

    public void Play(AK.Wwise.Event _event)
    {
        _event.Post(gameObject);
    }


}
