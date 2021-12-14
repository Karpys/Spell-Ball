using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossAction : MonoBehaviour
{
    [HideInInspector]
    public BossBehavior Boss;
    public virtual void Activate()
    {
        Boss = FindObjectOfType<BossBehavior>();
        Debug.Log(this.name + " Activate");
    }

    public virtual void Deactivate()
    {
        Debug.Log(this.name + " Deactivate");
    }


    [System.Serializable]
    public class BallThrowerInstantier
    {
        public List<BallThrowerStats> BallThrower = new List<BallThrowerStats>{new BallThrowerStats()};
        [HideInInspector]
        public List<GameObject> ThrowerInst;
        public void InstAllBallThrower(BossShooter Manager)
        {
            foreach (BallThrowerStats Stats in BallThrower)
            {
                GameObject Parent = Stats.StartPositions[Random.Range(0, Stats.StartPositions.Count - 1)];
                GameObject Throw = Instantiate(BossBehavior.Boss.BaseShooter, Parent.transform);
                /*Throw.AddComponent<BallThrower>().SetUpThrower(Stats);*/
                Throw.GetComponent<BallThrower>().SetUpThrower(Stats);
                Throw.GetComponent<BallThrower>().ThrowerStats.Manager = Manager;
                ThrowerInst.Add(Throw);
            }
            
        }

        public void DeastroyShooter()
        {
            foreach (GameObject Thrower in ThrowerInst)
            {
                if (Thrower)
                {
                    Destroy(Thrower);
                }
            }
            ThrowerInst.Clear();
        }


    }

    [System.Serializable]
    public class LaserInstantier
    {
        public List<LaserStats> Stats = new List<LaserStats> {new LaserStats()};
        [HideInInspector]
        public List<GameObject> LaserInst;

        public void InstLaser(BossLaser Manager)
        {
            foreach (LaserStats stat in Stats)
            {
                GameObject Parent = stat.StartPosition;
                GameObject Laser = Instantiate(BossBehavior.Boss.BaseLaser, Parent.transform);
                Laser.GetComponent<LaserBehavior>().Stats = stat;
                Laser.GetComponent<LaserBehavior>().Stats.Manager = Manager;
                LaserInst.Add(Laser);
                
            }
        }
        public void DestroyLaser()
        {
            foreach (GameObject Laser in LaserInst)
            {
                if (Laser)
                {
                    Destroy(Laser);
                }
            }
            LaserInst.Clear();
        }
    }

    [System.Serializable]
    public class LaserStats
    {

        [HideInInspector]
        public BossLaser Manager;
        [Space(10)]
        [Header("Nom Du Groupe de Laser (Si multiple Premier Name pris en compte)")]
        [Space(10)]
        public string Name = "Laser";
        public bool ThrowHead = false;
        [Header("Durée du Laser et Temps de départ")]
        public float Duration = 2.0f;
        public float WaitTime = 1.0f;
        [Space(10)]
        [Header("Longueur du Laser / Infiny")]
        public float Lenght = 3f;
        public bool Infinity = false;

        [Space(10)] 
        [Header("Largueur du laser // Se Joue pendant le WaitTime")]
        public float StartWidth = 0f;
        public float EndWidth = 1f;
        [Space(10)]
        [Header("Position de départ et Angle du Début et de Fin")]
        public Vector2 StartEndAngle = new Vector2(0,360);
        public bool PingPong = false;
        public GameObject StartPosition;

    }

    [System.Serializable]
    public class BallThrowerStats
    {
        [HideInInspector]
        public BossShooter Manager;
        [Space(10)]
        [Header("Projectile GameObject Name and Prefab")]
        public string Name = "Throwr";
        public GameObject Projectile;
        public bool ThrowHead = false;
        [Space(10)]
        [Header("Projectile Thrower Stats")]
        public Vector2 Power = new Vector2(10,10);
        public Vector2 Cadence = new Vector2(0.5f,0.5f);
        public float DelayBeforeFirstShoot = 1f;
        public bool DestroyOnHit = false;
        public bool Returnable = false;
        [Space(10)]
        [Header("Number of repetitions |Min|Max|//|")]
        public Vector3Int Repetition = new Vector3Int(10,10,0);
        public bool InfiniteProj = false;
        [Space(10)]
        [Header("Start Positions")]
        public List<GameObject> StartPositions;
        public bool LoopParent = false;
        [Space(10)]
        [Header("Shoot Direction Options Transform/Angle")]
        public bool ShootAtPlayer = false;
        public Vector2 DirectionMaxMin;
        [Space(10)]
        [Header("Splash Options")]
        public bool ActAsSplash = false;
        public int ProjPerBurst = 15;
        public Vector2 MinMaxDirectionSplash;
        [Space(10)] [Header("Rotate Options / Angle par Secondes")] 
        public float RotateSpeed = 0f;
    }


    [System.Serializable]
    public class ShieldStats
    {
        [Space(10)]
        [Header("GameObject Name")]
        public string Name = "Shield";
        [Space(10)]
        [Header("Nombre Surface de Shield")]
        public int Number = 2;
        [Space(10)]
        [Header("Parent")]
        public GameObject Parent;
        [HideInInspector]
        public GameObject BaseShield;
    }

    [System.Serializable]
    public class ShieldInstantier
    {
        public ShieldStats Stats;
        public void CreateShield()
        {
            GameObject Shield = Instantiate(BossBehavior.Boss.BaseShield, Stats.Parent.transform);
            Shield.GetComponent<SheildManager>().Stats = Stats;
        }
    }
}
