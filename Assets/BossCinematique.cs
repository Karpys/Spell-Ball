using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCinematique : MonoBehaviour
{
    // Start is called before the first frame update
    public FloorManager Floor;
    public BossBehavior Boss;

    public GameObject Smoke;
    public GameObject LittleSmoke;
    public GameObject LittleSmokeTransform;
    public GameObject SmokeTransform;

    public AK.Wwise.Event BossSound;

    public void LittleShake()
    {
        CameraShakeManager.CameraShake.Shake(0.1f, 0.5f, 15);
        Instantiate(LittleSmoke, LittleSmokeTransform.transform.position, LittleSmokeTransform.transform.rotation);
    }

    public void PlaySoundBoss()
    {
        BossSound.Post(gameObject);
    }

    public void LongShake()
    {
        CameraShakeManager.CameraShake.Shake(1.5f, 1f, 15);
        Instantiate(Smoke, SmokeTransform.transform.position, SmokeTransform.transform.rotation);
        Floor.start = true;
    }

    public void RunLittleRat()
    {
        Boss.LaunchPhase(0);
    }
}
