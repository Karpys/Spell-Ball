using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Manager_Life : MonoBehaviour
{
    public int maxHealth;
    //public int damages;
    [SerializeField] private float currentLife;

    public int heal;
    float regen = 5f;


    /*public UnityEvent OnDamage;
    public UnityEvent OnHeal;*/

    [SerializeField] private GameObject Visual;

    // Start is called before the first frame update
    void Start()
    {
        //OnDamage.AddListener(DamageHealth);
        //OnHeal.AddListener(HealHealth);
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

    public void DamageHealth(int dmg)
    {
        Debug.Log("je fais chier mon monde");
        ShakerEntity Shake = Visual.AddComponent<ShakerEntity>();
        Shake.SetShakeParameters(0.25f, 0.2f, 10f, new Vector3(1, 0, 1));
        currentLife -= dmg;
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
