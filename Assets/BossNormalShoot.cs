using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNormalShoot : BossState
{
    // Start is called before the first frame update
    public int HpBossState;
    public BallThrowerInstantier Instantier;
    public override void Activate()
    {
        base.Activate();
        Boss.HpManager.SetHpBoss(HpBossState);
        Instantier.InstAllBallThrower();

    }
    public override void Deactivate()
    {
        Instantier.ClearAllThrower();
        base.Deactivate();
    }
}
