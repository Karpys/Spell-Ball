using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMultipleShoot : BossAction
{
    public int HpBossState;
    public override void Activate()
    {
        /*Boss.HpManager.SetHpBoss(HpBossState);*/
        base.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
