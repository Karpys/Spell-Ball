using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooter : BossAction
{
    // Start is called before the first frame update
    /*public int HpBossState;*/
    public BallThrowerInstantier Instantier;
    [HideInInspector]
    public int NbrShooter;
    [HideInInspector]
    public int DeadShooter;

    private BossBehavior Boss;
    public override void Activate(BossBehavior boss)
    {
        Boss = boss;
        base.Activate(Boss);
        DeadShooter= 0;
        NbrShooter = Instantier.BallThrower.Count;
        /*Boss.HpManager.SetHpBoss(HpBossState);*/
        Instantier.InstAllBallThrower(this);
        if (Instantier.BallThrower[0].ThrowHead)
        {
            if (boss.Head != null)
            {
                Boss.Head.LaunchHead(0);
            }
        }

        if (boss.HeadRotation != null)
        {
            Boss.HeadRotation.SetTargetRotation(0);
        }

    }
    public override void Deactivate()
    {
        if (Instantier.BallThrower[0].ThrowHead)
        {
            if(Boss.Head!=null)
                Boss.Head.RetractHead(0);
        }
        Instantier.DeastroyShooter();
        base.Deactivate();
    }

    public void EndShooter()
    {
        DeadShooter++;
        if (NbrShooter == DeadShooter)
        {
            Boss.NextAction();
        }
    }
}
