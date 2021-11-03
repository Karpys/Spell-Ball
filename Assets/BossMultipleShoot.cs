using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMultipleShoot : BossState
{
    public int HpBossState;
    public override void Activate()
    {
        Boss.HpManager.SetHpBoss(HpBossState);
        base.Activate();
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
