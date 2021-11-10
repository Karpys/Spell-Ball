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
        public BallThrowerStats BallThrower;
        [HideInInspector]
        public List<GameObject> ThrowerInst;
        public void InstAllBallThrower()
        {
            GameObject Parent = BallThrower.StartPositions[Random.Range(0, BallThrower.StartPositions.Count - 1)];
            GameObject Throw = Instantiate(BossBehavior.Boss.BaseGameObject, Parent.transform);
            Throw.AddComponent<BallThrower>().SetUpThrower(BallThrower);
            ThrowerInst.Add(Throw);
        }

        public void ClearAllThrower()
        {
            foreach (GameObject Obj in ThrowerInst)
            {
                Destroy(Obj);
            }
            ThrowerInst.Clear();
        }

    }

    [System.Serializable]
    public struct BallThrowerStats
    {
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
        [Header("Burst Options")]
        public bool ActAsABurst;
        public int ProjPerBurst;
    }
}
