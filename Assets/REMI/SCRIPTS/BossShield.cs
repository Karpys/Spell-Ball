using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : BossAction
{
    /*public int HpBossState;*/
    public ShieldInstantier Instantier;

    private BossBehavior Boss;
    public override void Activate(BossBehavior boss)
    {
        Boss = boss;
        base.Activate(boss);
        //BossBehavior.Boss.HeadRotation.SetTargetRotation(2);
        if (Boss.GetComponentInChildren<SheildManager>())
        {
            //IGNORE SHIELD DEJA EN PLACE//
            Boss.NextAction();
        }
        else
        {
            Instantier.CreateShield(boss);
        }
        
        //CREER SHIELD AVEC SHIELD STATS//
        /*Boss.HpManager.SetHpBoss(HpBossState);*/

    }
    public override void Deactivate()
    {

        base.Deactivate();
    }
}
