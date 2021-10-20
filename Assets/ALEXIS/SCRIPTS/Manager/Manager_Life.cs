using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Manager_Life : MonoBehaviour
{
    public int maxHealth;
    public int damages;
    [SerializeField] private float currentLife;

    public int heal;
    float regen = 5f;


    public UnityEvent OnDamage;
    public UnityEvent OnHeal;

    // Start is called before the first frame update
    void Start()
    {
        OnDamage.AddListener(DamageHealth);
        OnHeal.AddListener(HealHealth);
        currentLife = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        DestructionOfThis();
    }

    private void DestructionOfThis()
    {
        if(this.gameObject.tag != "Player")
        {
            if (currentLife <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

    public void DamageHealth()
    {
        currentLife -= damages;
    }

    public void HealHealth()
    {
        currentLife += heal;
    }

    private void RegenAuto()
    {
        if(currentLife <= maxHealth)
        {
            regen -= Time.deltaTime;

            if(regen <= 0)
            {
                HealHealth();
                regen = 5f;
            }
        }
    }



}
