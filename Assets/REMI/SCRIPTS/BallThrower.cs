using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameUtilities.GameUtilities;

public class BallThrower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private BossAction.BallThrowerStats ThrowerStats;
    /*[SerializeField] private GameObject Projectile;
    [SerializeField] private List<GameObject> Parent;
    [SerializeField] private float Power;
    [SerializeField] private float TimeBetweenShot;
    [SerializeField] private int NbrShoot = 10;
    [SerializeField] private bool InfiniteProj;
    [SerializeField] private Vector2 DirectionMaxMin;
    [SerializeField] private bool ShootAtClosestPlayer;
    [SerializeField] private bool DestroyOnHitWall;
    [SerializeField] private bool ActAsABurst;
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
        timer = ThrowerStats.Cadence - ThrowerStats.DelayBeforeFirstShoot;
    }

    //SETUPTHOWERSTATS//
    public void SetUpThrower(BossAction.BallThrowerStats Stats)
    {
        ThrowerStats = Stats;
        /*ThrowerStats.Projectile = Stats.Projectile;
        ThrowerStats.DirectionMaxMin = Stats.DirectionMaxMin;
        ThrowerStats.Power = Stats.Power;
        ThrowerStats.ActAsABurst = Stats.ActAsABurst;
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
            GameObject Start = ThrowerStats.Parents[ActualStartPosition];
            ActualStartPosition += 1;
            if (ActualStartPosition >= ThrowerStats.Parents.Count)
            {
                ActualStartPosition = 0;
            }

            return Start;
        }
        else
        {
            return ThrowerStats.Parents[Random.Range(0, ThrowerStats.Parents.Count)];
        }
    }
    // Update is called once per frame
    void Update()
    {
        //BALLTHROWER NORMAL
        if (ThrowerStats.NbrProj <= 0)
        {
            if (BossBehavior.Boss!=null)
            {
                BossBehavior.Boss.NextAction();
            }
        }
        timer += Time.deltaTime;
        if (timer >= ThrowerStats.Cadence)
        {
            if(!ThrowerStats.ActAsABurst)
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
                if (ThrowerStats.ShootAtPlayer)
                {
                    int ShootNbr = ThrowerStats.ProjPerBurst;
                    for (int i = 0; i < ShootNbr; i++)
                    {
                        ShootClosest(GetStartPosition());
                    }
                    ReduceShoot();
                }
                else
                {
                    int ShootNbr = ThrowerStats.ProjPerBurst;
                    for (int i = 0; i < ShootNbr; i++)
                    {
                        ShootDirection(Random.Range(transform.eulerAngles.y + ThrowerStats.DirectionMaxMin.x, transform.eulerAngles.y + ThrowerStats.DirectionMaxMin.y), GetStartPosition());
                    }

                    ReduceShoot();
                }
            }
        }
    }


    public void ReduceShoot()
    {
        if (ThrowerStats.InfiniteProj)
        {
            return;
        }
        else
        {
            ThrowerStats.NbrProj--;
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
        Proj.GetComponent<Ball>().Speed = ThrowerStats.Power;
        Proj.GetComponent<Ball>().SetDirection(new Vector3(0,Direction,0));
        Proj.GetComponent<Ball>().DelayDamageSelf = DelayDmgSelf;
        Proj.GetComponent<Ball>().DestroyOnHitWall = ThrowerStats.DestroyOnHit;
    }

    //DEPEND ON A GAMEOBJECT//

    void InstantiateBall(GameObject target,GameObject StartPosition)
    {
        GameObject Proj = Instantiate(ThrowerStats.Projectile, StartPosition.transform.position, transform.rotation);
        Proj.GetComponent<Ball>().Speed = ThrowerStats.Power;
        Proj.GetComponent<Ball>().LookAtStart(target);
        Proj.GetComponent<Ball>().DelayDamageSelf = DelayDmgSelf;
        Proj.GetComponent<Ball>().DestroyOnHitWall = ThrowerStats.DestroyOnHit;
    }
    

    //OLD VERSION
    void InstantiateBall(GameObject target)
    {
        GameObject Proj = Instantiate(ThrowerStats.Projectile, transform.position, transform.rotation);
        Proj.GetComponent<Ball>().Speed = ThrowerStats.Power;
        Proj.GetComponent<Ball>().LookAtStart(target);
        Proj.GetComponent<Ball>().DelayDamageSelf = DelayDmgSelf;
    }


}
