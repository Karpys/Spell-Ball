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
    float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= TimeBetweenShot)
        {
            GameObject target = GetClosestGameObject(transform.position,ListToListGameObjects(FindObjectsOfType<PlayerController>().ToList()));
            if (target)
            {
                InstantiateBall(target);
            }

            timer = 0;
        }
    }


    void InstantiateBall(GameObject target)
    {
        GameObject Proj = Instantiate(Projectile, transform.position, transform.rotation);
        Proj.GetComponent<Rigidbody>().AddForce((target.transform.position - transform.position) * Power,ForceMode.Impulse);
    }

}
