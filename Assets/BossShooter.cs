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
    public override void Activate()
    {
        DeadShooter= 0;
        NbrShooter = Instantier.BallThrower.Count;
        //BossBehavior.Boss.HeadRotation.SetTargetRotation(0);
        base.Activate();
        /*Boss.HpManager.SetHpBoss(HpBossState);*/
        Instantier.InstAllBallThrower(this);

    }
    public override void Deactivate()
    {
        Instantier.DeastroyShooter();
        base.Deactivate();
    }

    public void EndShooter()
    {
        DeadShooter++;
        if (NbrShooter == DeadShooter)
        {
            BossBehavior.Boss.NextAction();
        }
    }
}
