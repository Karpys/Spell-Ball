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

    private BossBehavior Boss;
    public override void Activate(BossBehavior boss)
    {
        base.Activate(boss);
        Boss = boss;
        DeadRay = 0;
        NbrRay = Instantier.Stats.Count;
        Instantier.InstLaser(this);

        if(Boss.HeadRotation!=null)
            Boss.HeadRotation.SetTargetRotation(1);

        if (Instantier.Stats[0].ThrowHead)
        {
            if (Boss.Head != null)
            {
                Boss.Head.LaunchHead(1);
            }
        }
        /*Boss.HpManager.SetHpBoss(HpBossState);*/
    }
    public override void Deactivate()
    {
        if (Instantier.Stats[0].ThrowHead)
        {
            if(Boss.Head!=null)
                Boss.Head.RetractHead(1);
        }
        Instantier.DestroyLaser();
        base.Deactivate();
    }

    public void EndRay()
    {
        DeadRay++;
        if (NbrRay == DeadRay)
        {
            Boss.NextAction();
        }
    }
}
