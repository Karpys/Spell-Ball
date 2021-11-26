using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameUtilities.GameUtilities;

public class BallThrower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public BossAction.BallThrowerStats ThrowerStats;
    /*[SerializeField] private GameObject Projectile;
    [SerializeField] private List<GameObject> Parent;
    [SerializeField] private float Power;
    [SerializeField] private float TimeBetweenShot;
    [SerializeField] private int NbrShoot = 10;
    [SerializeField] private bool InfiniteProj;
    [SerializeField] private Vector2 DirectionMaxMin;
    [SerializeField] private bool ShootAtClosestPlayer;
    [SerializeField] private bool DestroyOnHitWall;
    [SerializeField] private bool ActAsSplash;
    [SerializeField] private int ProjPerBurst;
    [SerializeField] private bool StartPositionLoop;
    [SerializeField] private float DelayBeforeFirstShoot;*/

    [SerializeField] private int ActualStartPosition;

    [SerializeField] private float DelayDmgSelf = 0.5f;
    public float Angle;
    public bool Lock = false;
    float timer;

    void Start()
    {
        name = ThrowerStats.Name;
        timer = Random.Range(ThrowerStats.Cadence.x,ThrowerStats.Cadence.y) - ThrowerStats.DelayBeforeFirstShoot;
    }

    //SETUPTHOWERSTATS//
    public void SetUpThrower(BossAction.BallThrowerStats Stats)
    {
        ThrowerStats = Stats;
        ThrowerStats.Repetition.z = Random.Range(ThrowerStats.Repetition.x, ThrowerStats.Repetition.y);
        /*ThrowerStats.Projectile = Stats.Projectile;
        ThrowerStats.DirectionMaxMin = Stats.DirectionMaxMin;
        ThrowerStats.Power = Stats.Power;
        ThrowerStats.ActAsSplash = Stats.ActAsSplash;
        ThrowerStats.Cadence = Stats.Cadence;
        ThrowerStats.DelayBeforeFirstShoot = Stats.DelayBeforeFirstShoot;
        ThrowerStats.InfiniteProj = Stats.InfiniteProj;
        ThrowerStats.LoopParent = Stats.LoopParent;
        ThrowerStats.Name = Stats.Name;
        ThrowerStats.*/
        
        ActualStartPosition = 0;
    }


    public GameObject GetStartPosition()
    {
        if (ThrowerStats.LoopParent)
        {
            GameObject Start = ThrowerStats.StartPositions[ActualStartPosition];
            ActualStartPosition += 1;
            if (ActualStartPosition >= ThrowerStats.StartPositions.Count)
            {
                ActualStartPosition = 0;
            }

            return Start;
        }
        else
        {
            return ThrowerStats.StartPositions[Random.Range(0, ThrowerStats.StartPositions.Count)];
        }
    }
    // Update is called once per frame
    void Update()
    {
        //BALLTHROWER NORMAL
        if (ThrowerStats.Repetition.z <= 0)
        {
            Destroy(gameObject);
            ThrowerStats.Manager.EndShooter();
        }
        timer += Time.deltaTime;
        if (timer >= Random.Range(ThrowerStats.Cadence.x, ThrowerStats.Cadence.y))
        {
            if(!ThrowerStats.ActAsSplash)
            {
                if (ThrowerStats.ShootAtPlayer)
                {
                    ShootClosest(GetStartPosition());
                    ReduceShoot();
                }
                else
                {
                    ShootDirection(Random.Range(transform.eulerAngles.y + ThrowerStats.DirectionMaxMin.x, transform.eulerAngles.y + ThrowerStats.DirectionMaxMin.y), GetStartPosition());
                    ReduceShoot();
                }
            }
            else
            {
                int ShootNbr = ThrowerStats.ProjPerBurst;
                float AnglePerIt = (ThrowerStats.MinMaxDirectionSplash.x - ThrowerStats.MinMaxDirectionSplash.y) / ThrowerStats.ProjPerBurst;
                float Angle = 0;
                for (int i = 0; i < ShootNbr; i++)
                {
                    ShootDirection(transform.eulerAngles.y + Angle, GetStartPosition());
                    Angle += AnglePerIt;
                }

                ReduceShoot();
            }
        }

        RotateThrower();
    }


    public void RotateThrower()
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + (ThrowerStats.RotateSpeed * Time.deltaTime), 0);
    }

    public void ReduceShoot()
    {
        if (ThrowerStats.InfiniteProj)
        {
            return;
        }
        else
        {
            ThrowerStats.Repetition.z--;
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
        GameObject Proj = Instantiate(ThrowerStats.Projectile, StartPosition.transform.position, transform.rotation);
        Ball ball = Proj.GetComponent<Ball>();
        ball.Speed = Random.Range(ThrowerStats.Power.x,ThrowerStats.Power.y);
        ball.SetDirection(new Vector3(0,Direction,0));
        ball.DelayDamageSelf = DelayDmgSelf;
        ball.DestroyOnHitWall = ThrowerStats.DestroyOnHit;
        ball.Returnable = ThrowerStats.Returnable;
    }

    //DEPEND ON A GAMEOBJECT//

    void InstantiateBall(GameObject target,GameObject StartPosition)
    {
        GameObject Proj = Instantiate(ThrowerStats.Projectile, StartPosition.transform.position, transform.rotation);
        Ball ball = Proj.GetComponent<Ball>();
        ball.Speed = Random.Range(ThrowerStats.Power.x, ThrowerStats.Power.y);
        ball.LookAtStart(target);
        ball.DelayDamageSelf = DelayDmgSelf;
        ball.DestroyOnHitWall = ThrowerStats.DestroyOnHit;
        ball.Returnable = ThrowerStats.Returnable;
    }
    

    //OLD VERSION
    void InstantiateBall(GameObject target)
    {
        GameObject Proj = Instantiate(ThrowerStats.Projectile, transform.position, transform.rotation);
        Proj.GetComponent<Ball>().Speed = Random.Range(ThrowerStats.Power.x, ThrowerStats.Power.y);
        Proj.GetComponent<Ball>().LookAtStart(target);
        Proj.GetComponent<Ball>().DelayDamageSelf = DelayDmgSelf;
    }


}
