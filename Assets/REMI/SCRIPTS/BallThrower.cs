using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameUtilities.GameUtilities;

public class BallThrower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Projectile;
    [SerializeField] private float Power;
    [SerializeField] private float TimeBetweenShot;
    [SerializeField] private int NbrShoot = 10;
    float timer;

    // Update is called once per frame
    void Update()
    {
        if (NbrShoot <= 0)
            return;
        timer += Time.deltaTime;
        if (timer >= TimeBetweenShot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject target = GetClosestGameObject(transform.position, ListToListGameObjects(FindObjectsOfType<PlayerController>().ToList()));
        if (target)
        {
            NbrShoot--;
            InstantiateBall(target);
        }
        timer = 0;
    }

    void InstantiateBall(GameObject target)
    {
        GameObject Proj = Instantiate(Projectile, transform.position, transform.rotation);
        Proj.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position).normalized * Power,ForceMode.Impulse);
    }

}
