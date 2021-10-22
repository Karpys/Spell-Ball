using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject CollisionParticle;
   /* [SerializeField] private LayerMask Layers;*/


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 9)
        {
            Instantiate(CollisionParticle, transform.position, transform.rotation);
        }
    }
}
