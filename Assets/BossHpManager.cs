using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpManager : MonoBehaviour
{
    [SerializeField] private GameObject HPDisplay;

    public Transform inFrontOfBoss;
    public Transform bottomOfBoss;
    public Transform leftOfBoss;

    public Transform leftBackCameraSpot;
    public Transform rightBackCameraSpot;
    public Transform leftFrontCameraSpot;
    public Transform rightFrontCameraSpot;

    // Start is called before the first frame update
    /*public int HpBeforeNextState;

    // Update is called once per frame
    void Update()
    {
        *//*if (HpBeforeNextState <= 0 && Boss.BossStarted)
        {
            if (Boss.ActualState <= 0)
            {
                Boss.StartState();
            }
            else
            {
                Boss.NextState();
            }
        }*//*
    }

    public void SetHpBoss(int HpToSet)
    {
        HpBeforeNextState = HpToSet;
    }
    public void BossGetDamage(int Dmg)
    {
        HpBeforeNextState -= Dmg;
        HpBeforeNextState = Mathf.Max(HpBeforeNextState, 0);
    }*/

    private BossHealthDisplay _healthDisplay;
    private int currentHealth;
    private Manager_Life _managerLife;
    
    private void Start()
    {
        BossBehavior bossBehavior = GetComponent<BossBehavior>();
        _managerLife = GetComponent<Manager_Life>();
        _healthDisplay = HPDisplay.GetComponent<BossHealthDisplay>();

        int totalHealth = 0;
        
        foreach (BossBehavior.Phase phase in bossBehavior.Phases)
        {
            totalHealth += phase.HpToSet;
        }

        currentHealth = totalHealth;
        
        _healthDisplay.SetMaxHealth(totalHealth);
        _healthDisplay.SetCurrentHealth(currentHealth);
    }

    public void BossTookDamage()
    {
        int lastDmgAmount = _managerLife.GetLastDamageAmount();
        currentHealth -= lastDmgAmount;
        print(currentHealth);
        if (currentHealth < 0) currentHealth = 0;
        _healthDisplay.SetCurrentHealth(currentHealth);
    }
    
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
}
