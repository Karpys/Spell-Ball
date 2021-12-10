using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : BossAction
{
    public LaserInstantier Instantier;
    [HideInInspector]
    public int NbrRay;
    [HideInInspector]
    public int DeadRay;
    public override void Activate()
    {
        DeadRay = 0;
        NbrRay = Instantier.Stats.Count;
        BossBehavior.Boss.HeadRotation.SetTargetRotation(1);
        Instantier.InstLaser(this);
        if (Instantier.Stats[0].ThrowHead)
        {
            BossBehavior.Boss.Head.LaunchHead(1);
        }
        base.Activate();
        /*Boss.HpManager.SetHpBoss(HpBossState);*/
    }
    public override void Deactivate()
    {
        if (Instantier.Stats[0].ThrowHead)
        {
            BossBehavior.Boss.Head.RetractHead(1);
        }
        Instantier.DestroyLaser();
        base.Deactivate();
    }

    public void EndRay()
    {
        DeadRay++;
        if (NbrRay == DeadRay)
        {
            BossBehavior.Boss.NextAction();
        }
    }
}
