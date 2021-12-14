using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShield : BossAction
{
    /*public int HpBossState;*/
    public ShieldInstantier Instantier;
    public override void Activate()
    {
        base.Activate();
        //BossBehavior.Boss.HeadRotation.SetTargetRotation(2);
        if (BossBehavior.Boss.GetComponentInChildren<SheildManager>())
        {
            //IGNORE SHIELD DEJA EN PLACE//
            BossBehavior.Boss.NextAction();
        }
        else
        {
            Instantier.CreateShield();
        }
        
        //CREER SHIELD AVEC SHIELD STATS//
        /*Boss.HpManager.SetHpBoss(HpBossState);*/

    }
    public override void Deactivate()
    {

        base.Deactivate();
    }
}
