using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int HpBeforeNextState;
    public BossBehavior Boss;

    // Update is called once per frame
    void Update()
    {
        if (HpBeforeNextState <= 0 && Boss.BossStarted)
        {
            if (Boss.ActualState <= 0)
            {
                Boss.StartState();
            }
            else
            {
                Boss.NextState();
            }
        }
    }

    public void SetHpBoss(int HpToSet)
    {
        HpBeforeNextState = HpToSet;
    }
    public void BossGetDamage(int Dmg)
    {
        HpBeforeNextState -= Dmg;
        HpBeforeNextState = Mathf.Max(HpBeforeNextState, 0);
    }
}
