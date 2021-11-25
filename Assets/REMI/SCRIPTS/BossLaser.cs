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
        Instantier.InstLaser(this);
        base.Activate();
        /*Boss.HpManager.SetHpBoss(HpBossState);*/
    }
    public override void Deactivate()
    {
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
