using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Speed;
    private float BaseSpeed;
    public LayerMask CollisionMask;
    public LayerMask CollisionMaskEnnemy;
    public Vector3 Direction;
    public float radius = 1.0f;


    [SerializeField] private GameObject CollisionParticle;

    [SerializeField] private GameObject SpeedEffect;

    // Start is called before the first frame update
    void Start()
    {
        BaseSpeed = Speed;
    }

    // Update is called once per frame
    void Update()
    {

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Vector3 forward = transform.forward * Speed * Time.deltaTime;
        Debug.DrawRay(transform.position,forward, Color.green);
        if (Physics.SphereCast(ray, radius, out hit, Speed * Time.deltaTime + radius,CollisionMask))
        {
            AddEffect();
            Reflect(hit.normal);
            Instantiate(CollisionParticle, transform.position, transform.rotation);
        }

        if (Physics.Raycast(ray, out hit, Time.deltaTime * Speed, CollisionMaskEnnemy))
        {
            Reflect(hit.normal);
            ResetSpeedAndCombo();
            hit.transform.gameObject.GetComponent<IA_BasicEnemy>().GetDamage(gameObject);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * Speed);

    }


    public void Reflect(Vector3 Normal)
    {
        Vector3 Reflect = Vector3.Reflect(transform.forward,Normal);
        float NewRotation = Mathf.Atan2(Reflect.x, Reflect.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, NewRotation, 0);
    }
    public void SetDirection(Vector3 EulerAngles)
    {
        transform.eulerAngles = EulerAngles;
    }

    public void AddEffect()
    {
        Instantiate(SpeedEffect, transform.position, transform.rotation, transform);
    }

    public void SetSpeed(float NewSpeed)
    {
        Speed = NewSpeed;
    }

    public void ResetSpeed()
    {
        Speed = BaseSpeed;
    }

    public void ResetSpeedAndCombo()
    {
        ResetSpeed();
        GetComponent<Balle>().combo = 0;
    }
}
