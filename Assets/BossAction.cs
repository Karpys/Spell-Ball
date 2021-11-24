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
    public struct BallThrowerInstantier
    {
        public List<BallThrowerStats> BallThrower;
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
            ThrowerInst.Clear();
        }


    }

    [System.Serializable]
    public struct LaserInstantier
    {
        public List<LaserStats> Stats;
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
            LaserInst.Clear();
        }
    }

    [System.Serializable]
    public struct LaserStats
    {

        [HideInInspector]
        public BossLaser Manager;
        [Space(10)]
        [Header("Nom Du Groupe de Laser (Si multiple Premier Name pris en compte)")]
        [Space(10)]
        public string Name;
        [Header("Durée du Laser et Temps de départ")]
        public float Duration;
        public float WaitTime;
        [Space(10)]
        [Header("Longueur du Laser / Infiny")]
        public float Lenght;
        public bool Infinity;

        [Space(10)] 
        [Header("Largueur du laser // Se Joue pendant le WaitTime")]
        public float StartWidth;
        public float EndWidth;
        [Space(10)]
        [Header("Position de départ et Angle du Début et de Fin")]
        public Vector2 StartEndAngle;
        public bool PingPong;
        public GameObject StartPosition;

    }

    [System.Serializable]
    public struct BallThrowerStats
    {
        [HideInInspector]
        public BossShooter Manager;
        [Space(10)]
        [Header("Projectile GameObject Name and Prefab")]
        public string Name;
        public GameObject Projectile;
        [Space(10)]
        [Header("Projectile Thrower Stats")]
        public Vector2 Power;
        public Vector2 Cadence;
        public float DelayBeforeFirstShoot;
        public bool DestroyOnHit;
        [Space(10)]
        [Header("Number of repetitions")]
        public Vector3Int Repetition;
        public bool InfiniteProj;
        [Space(10)]
        [Header("Start Positions")]
        public List<GameObject> StartPositions;
        public bool LoopParent;
        [Space(10)]
        [Header("Shoot Direction Options")]
        public bool ShootAtPlayer;
        public Vector2 DirectionMaxMin;
        [Space(10)]
        [Header("Splash Options")]
        public bool ActAsSplash;
        public int ProjPerBurst;
        public Vector2 MinMaxDirectionSplash;
        [Space(10)] [Header("Rotate Options / Angle par Secondes")] 
        public float RotateSpeed;
    }


    [System.Serializable]
    public struct ShieldStats
    {
        [Space(10)]
        [Header("GameObject Name")]
        public string Name;
        [Space(10)]
        [Header("Nombre Surface de Shield")]
        public int Number;
        [Space(10)]
        [Header("Parent")]
        public GameObject Parent;
        [HideInInspector]
        public GameObject BaseShield;
    }

    [System.Serializable]
    public struct ShieldInstantier
    {
        public ShieldStats Stats;
        public void CreateShield()
        {
            GameObject Shield = Instantiate(BossBehavior.Boss.BaseShield, Stats.Parent.transform);
            Shield.GetComponent<SheildManager>().Stats = Stats;
        }
    }
}
