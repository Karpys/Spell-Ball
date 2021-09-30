using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Speed;

    public LayerMask CollisionMask;
    public Vector3 Direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Time.deltaTime * Speed + 0.5f, CollisionMask))
        {
            Vector3 Reflect = Vector3.Reflect(transform.forward, hit.normal);
            float NewRotation = Mathf.Atan2(Reflect.x, Reflect.z) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0,NewRotation,0);
        }

    }
}
