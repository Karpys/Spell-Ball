using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossHpManager : MonoBehaviour
{
    [SerializeField] private GameObject HPDisplay;

    [SerializeField] private GameObject UI;
    
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
        originalRotation = HPDisplay.transform.rotation;
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

    public void HideUI(bool hide)
    {
        UI.SetActive(!hide);
    }
    
    public void BossTookDamage()
    {
        int lastDmgAmount = _managerLife.GetLastDamageAmount();
        currentHealth -= lastDmgAmount;
        print(currentHealth);
        if (currentHealth < 0) currentHealth = 0;
        _healthDisplay.SetCurrentHealth(currentHealth);
        ShakePlayerVisual();
    }
    
    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    
    public void ShakePlayerVisual()
    {
        StartCoroutine(ShakeVisual(.1f, 10f));
        Invoke("ReplaceUI", .1f);
    }
    
    public void ReplaceUI()
    {
        HPDisplay.transform.rotation = originalRotation;
    }

    private Quaternion originalRotation;
    
    private IEnumerator ShakeVisual(float _shakeTime, float shakeRange)
    {
        float elapsed = 0.0f;
        
        while (elapsed < _shakeTime)
        {
            elapsed += Time.deltaTime;
            float z = Random.value * shakeRange - (shakeRange /2);
            HPDisplay.transform.eulerAngles = new Vector3(originalRotation.x, originalRotation.y, originalRotation.z + z); 
            yield return null;
        }
 
        HPDisplay.transform.rotation = originalRotation;
    }
}
