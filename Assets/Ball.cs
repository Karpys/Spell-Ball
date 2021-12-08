using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Speed;
    private float BaseSpeed;
    public LayerMask CollisionMask;
    public LayerMask CollisionMaskEnnemy;
    public LayerMask CollisionPlayer;
    public LayerMask CollisionShield;
    public Vector3 Direction;
    public float radius = 1.0f;

    public bool DestroyOnHitWall;

    public bool hitShield = false;

    public float DelayDamageSelf;//Used for prevent self Damage //
    private float DelayDamageSelfPlayer; // Same but for the Player//

    public bool Returnable;

    [SerializeField] private AK.Wwise.Event playBounce;

    float _timerDamageSelf;
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
        hitShield = false;

        if (DelayDamageSelf >= 0)
        {
            DelayDamageSelf -= Time.deltaTime;
        }

        if (DelayDamageSelfPlayer >= 0)
        {
            DelayDamageSelfPlayer -= Time.deltaTime;
        }


        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Vector3 forward = transform.forward * Speed * Time.deltaTime;
        Debug.DrawRay(transform.position,forward, Color.green);
        if (Physics.SphereCast(ray, radius, out hit, Speed * Time.deltaTime + radius,CollisionMask))
        {
            //TEST A SUPPRIMER//
            if (DestroyOnHitWall)
            {
                Destroy(gameObject);
            }
            else
            {
                playBounce.Post(gameObject);
            }
            AddEffect();
            Reflect(hit.normal);
            Instantiate(CollisionParticle, transform.position, transform.rotation);
        }

        if (Physics.Raycast(ray, out hit, Time.deltaTime * Speed, CollisionShield))
        {
            Reflect(hit.normal);
            ResetSpeedAndCombo();
            hitShield = true;
            if (hit.transform.gameObject.GetComponent<Sheild>().color == gameObject.GetComponent<Balle>().color)
            {
                hit.transform.gameObject.GetComponentInParent<SheildManager>().ChangeLastSheild();
            }
            else
                Instantiate(CollisionParticle, transform.position, transform.rotation);

        }

        
        if (Physics.Raycast(ray, out hit, Time.deltaTime * Speed, CollisionMaskEnnemy) && DelayDamageSelf<0 && !hitShield)
        {
            
            Reflect(hit.normal);
            Destroy(gameObject);
            if (hit.transform.gameObject && hit.transform.gameObject.TryGetComponent(out Manager_Life test))
            {
                Balle balle = GetComponent<Balle>();
                Manager_Life managerLife = hit.transform.gameObject.GetComponent<Manager_Life>();
                managerLife.Damage(balle);
                managerLife.SummonHitParticle(transform.position, hit.transform.rotation, Color.red);

                
            }
            ResetSpeedAndCombo();

        }



        if (Physics.SphereCast(ray, radius, out hit, Speed * Time.deltaTime + radius, CollisionPlayer) && DelayDamageSelfPlayer < 0)
        {
            /*Reflect(hit.normal);*/
            /*ResetSpeedAndCombo();*/
            
            Manager_Life Life = hit.collider.gameObject.GetComponentInParent<Manager_Life>();
            if (Life.Timerinvis <= 0)
            {
                //DAMAGE PLAYER//
                Balle balle = GetComponent<Balle>();
                Life.DamageHealth(1);
                Destroy(gameObject);
            }
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

    public void LookAtStart(GameObject Target)
    {
        Vector3 _direction = (new Vector3(Target.transform.position.x,transform.position.y,Target.transform.position.z) - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation,1);
    }
}
