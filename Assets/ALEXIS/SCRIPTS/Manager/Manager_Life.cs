using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Manager_Life : MonoBehaviour
{
    public int maxHealth;
    
    [SerializeField] private float currentLife;

    [Header("Auto regen settings")]
    [SerializeField] private int autoRegenAmount;
    [SerializeField] private float autoRegenTimer = 5f;
    [SerializeField] private bool autoRegen;

    [Header("Shake settings")]
    [SerializeField] private bool shakeOnDamage = true;
    [SerializeField] private GameObject Visual;

    [Header("Particle settings")]
    public GameObject ParticleEffectOnHit;
    public GameObject ParticleEffectOnHeal;

    private float _regenCurrentTimer;
    
    [Header("Events settings")]
    public UnityEvent OnDeath;
    public UnityEvent OnDamage;
    public UnityEvent OnHeal;


    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();

        if (autoRegen)
        {
            if (currentLife < maxHealth)
            {
                _regenCurrentTimer -= Time.deltaTime;

                if (_regenCurrentTimer <= 0)
                {
                    HealHealth(autoRegenAmount);
                    _regenCurrentTimer = autoRegenTimer;
                    if (currentLife > maxHealth)
                    {
                        currentLife = maxHealth;
                    }
                }
            }
        }
    }

    private void CheckDeath()
    {
        if (currentLife <= 0)
        {
            currentLife = 0;
            OnDeath.Invoke();
        }
    }

    public void DamageByColor(Balle ball)
    {
        if(ball.GetComponent<MeshRenderer>().material.color == gameObject.GetComponentInChildren<MeshRenderer>().material.color || gameObject.GetComponentInChildren<MeshRenderer>().material.color == Color.white)
        {
            DamageHealth(ball.combo);
        }
    }

    public void DamageHealth(int dmg)
    {
        Debug.Log("je fais chier mon monde");
        if (shakeOnDamage)
        {
            ShakerEntity Shake = Visual.AddComponent<ShakerEntity>();
            Shake.SetShakeParameters(0.25f, 0.2f, 10f, new Vector3(1, 0, 1));
        }
        currentLife -= dmg;
        OnDamage.Invoke();
    }

    public void SummonHitParticle(Vector3 position, Quaternion rotation, Color? particleColor = null)
    {
        GameObject Parti = Instantiate(ParticleEffectOnHit, position, rotation);
        
        if (particleColor.HasValue)
        {
            Parti.GetComponent<ParticleManager>().ApplyColor(particleColor.Value);
        }
    }

    public void HealHealth(int heal)
    {
        currentLife += heal;
        OnHeal.Invoke();
    }
    
    public void SummonHealParticle(Vector3 position, Quaternion rotation, Color? particleColor = null)
    {
        GameObject Parti = Instantiate(ParticleEffectOnHeal, position, rotation);
        
        if (particleColor.HasValue)
        {
            Parti.GetComponent<ParticleManager>().ApplyColor(particleColor.Value);
        }
    }

    public float GetCurentLife()
    {
        return currentLife;
    }

    public void SetCurentLife(float life)
    {
        currentLife = life;
    }
}