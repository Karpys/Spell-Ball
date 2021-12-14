using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCinematique : MonoBehaviour
{
    // Start is called before the first frame update
    public FloorManager Floor;
    public BossBehavior Boss;

    public void LittleShake()
    {
        CameraShakeManager.CameraShake.Shake(0.1f, 0.5f, 15);
    }

    public void LongShake()
    {
        CameraShakeManager.CameraShake.Shake(1.5f, 1f, 15);
        Floor.start = true;
    }

    public void RunLittleRat()
    {
        Boss.LaunchPhase(0);
    }
}
