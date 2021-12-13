using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMultipleShoot : BossAction
{
    public int HpBossState;
    public override void Activate(BossBehavior boss)
    {
        /*Boss.HpManager.SetHpBoss(HpBossState);*/
        base.Activate(boss);
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
