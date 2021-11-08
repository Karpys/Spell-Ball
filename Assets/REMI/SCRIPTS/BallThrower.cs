using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameUtilities.GameUtilities;

public class BallThrower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Projectile;
    [SerializeField] private List<GameObject> Parent;
    [SerializeField] private float Power;
    [SerializeField] private float TimeBetweenShot;
    [SerializeField] private int NbrShoot = 10;
    [SerializeField] private bool InfiniteProj;
    [SerializeField] private float DelayDmgSelf = 0.5f;
    [SerializeField] private Vector2 DirectionMaxMin;
    [SerializeField] private bool ShootAtClosestPlayer;
    [SerializeField] private bool DestroyOnHitWall;
    [SerializeField] private bool ActAsABurst;
    [SerializeField] private int ProjPerBurst;
    [SerializeField] private bool StartPositionLoop;
    [SerializeField] private int ActualStartPosition;
    [SerializeField] private float DelayBeforeFirstShoot;
    public float Angle;
    float timer;

    void Start()
    {
        timer = TimeBetweenShot - DelayBeforeFirstShoot;
    }

    //SETUPTHOWERSTATS//
    public void SetUpThrower(BossState.BallThrowerStats Stats)
    {
        Projectile = Stats.Projectile;
        Parent = Stats.Parents;
        Power = Stats.Power;
        TimeBetweenShot = Stats.Cadence;
        NbrShoot = Stats.NbrProj;
        InfiniteProj = Stats.InfiniteProj;
        DestroyOnHitWall = Stats.DestroyOnHit;
        DirectionMaxMin = Stats.DirectionMaxMin;
        ShootAtClosestPlayer = Stats.ShootAtPlayer;
        ActAsABurst = Stats.ActAsABurst;
        ProjPerBurst = Stats.ProjPerBurst;
        DelayBeforeFirstShoot = Stats.DelayBeforeFirstShoot;
        StartPositionLoop = Stats.LoopParent;
        ActualStartPosition = 0;
    }

    public GameObject GetStartPosition()
    {
        if (StartPositionLoop)
        {
            GameObject Start = Parent[ActualStartPosition];
            ActualStartPosition += 1;
            if (ActualStartPosition >= Parent.Count)
            {
                ActualStartPosition = 0;
            }

            return Start;
        }
        else
        {
            return Parent[Random.Range(0, Parent.Count)];
        }
    }
    // Update is called once per frame
    void Update()
    {
        //BALLTHROWER NORMAL
        if (NbrShoot <= 0)
            //CAN DESTROY GAMEOBJECT//
            return;
        timer += Time.deltaTime;
        if (timer >= TimeBetweenShot)
        {
            if(!ActAsABurst)
            {
                if (ShootAtClosestPlayer)
                {
                    ShootClosest(GetStartPosition());
                }
                else
                {
                    ShootDirection(Random.Range(transform.eulerAngles.y + DirectionMaxMin.x, transform.eulerAngles.y + DirectionMaxMin.y), GetStartPosition());
                    ReduceShoot();
                }
            }
            else
            {
                if (ShootAtClosestPlayer)
                {
                    int ShootNbr = ProjPerBurst;
                    for (int i = 0; i < ShootNbr; i++)
                    {
                        ShootClosest(GetStartPosition());
                    }
                    ReduceShoot();
                }
                else
                {
                    int ShootNbr = ProjPerBurst;
                    for (int i = 0; i < ShootNbr; i++)
                    {
                        ShootDirection(Random.Range(transform.eulerAngles.y + DirectionMaxMin.x, transform.eulerAngles.y + DirectionMaxMin.y), GetStartPosition());
                    }

                    ReduceShoot();
                }
            }
        }
    }


    public void ReduceShoot()
    {
        if (InfiniteProj)
        {
            return;
        }
        else
        {
            NbrShoot--;
        }
    }
    //OLD
    void ShootClosest()
    {
        GameObject target = GetClosestGameObject(transform.position, ListToListGameObjects(FindObjectsOfType<AddComponentTest>().ToList()));
        if (target)
        {
            ReduceShoot();
            InstantiateBall(target);
        }
        timer = 0;
    }


    //NEW
    void ShootClosest(GameObject StartPosition)
    {
        GameObject target = GetClosestGameObject(StartPosition.transform.position, ListToListGameObjects(FindObjectsOfType<AddComponentTest>().ToList()));
        if (target)
        {
            InstantiateBall(target, StartPosition);
        }
        timer = 0;
    }

    void ShootDirection(float Direction,GameObject StartPosition)
    {
        InstantiateBall(Direction,StartPosition);
        timer = 0;
    }

    //DEPEND ON DIRECTION//

    void InstantiateBall(float Direction,GameObject StartPosition)
    {
        GameObject Proj = Instantiate(Projectile, StartPosition.transform.position, transform.rotation);
        Proj.GetComponent<Ball>().Speed = Power;
        Proj.GetComponent<Ball>().SetDirection(new Vector3(0,Direction,0));
        Proj.GetComponent<Ball>().DelayDamageSelf = DelayDmgSelf;
        Proj.GetComponent<Ball>().DestroyOnHitWall = DestroyOnHitWall;
    }

    //DEPEND ON A GAMEOBJECT//

    void InstantiateBall(GameObject target,GameObject StartPosition)
    {
        GameObject Proj = Instantiate(Projectile, StartPosition.transform.position, transform.rotation);
        Proj.GetComponent<Ball>().Speed = Power;
        Proj.GetComponent<Ball>().LookAtStart(target);
        Proj.GetComponent<Ball>().DelayDamageSelf = DelayDmgSelf;
        Proj.GetComponent<Ball>().DestroyOnHitWall = DestroyOnHitWall;
    }
    

    //OLD VERSION
    void InstantiateBall(GameObject target)
    {
        GameObject Proj = Instantiate(Projectile, transform.position, transform.rotation);
        Proj.GetComponent<Ball>().Speed = Power;
        Proj.GetComponent<Ball>().LookAtStart(target);
        Proj.GetComponent<Ball>().DelayDamageSelf = DelayDmgSelf;
    }


}
