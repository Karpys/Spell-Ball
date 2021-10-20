using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Life : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] private int damages;
    public int heal;
    float regen = 5f;

    [SerializeField] private float currentLife;



    // Start is called before the first frame update
    void Start()
    {
        currentLife = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        DestructionOfThis();
        RegenAuto();
    }

    private void DestructionOfThis()
    {
        if(currentLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DamageHealth(int dgt)
    {
        currentLife -= dgt;
    }

    public void HealHealth(int soin)
    {
        currentLife += soin;
    }

    private void RegenAuto()
    {
        if(currentLife <= maxHealth)
        {
            regen -= Time.deltaTime;

            if(regen <= 0)
            {
                HealHealth(heal);
                regen = 5f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {


        if(collision.gameObject.tag == "Balle")
        {
            if(this.gameObject.tag != "Player")
            {
                int d = collision.gameObject.GetComponent<Balle>().combo;
                DamageHealth(d);
            }
        }

        if (collision.gameObject.tag == "Boss")
        {
            DamageHealth(damages);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.tag == "Player")
        {
            if (other.gameObject.tag == "Ennemy")
            {
                int d = 1;
                DamageHealth(d);
            }
            else if (other.gameObject.tag == "Boss")
            {
                int d = 3;
                DamageHealth(d);
            }
        }

    }


}
