using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour
{
    [HideInInspector]
    public BossBehavior Boss;

    public void Start()
    {
    }
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
        public List<BallThrowerStats> ListBallThrower;
        public List<GameObject> ThrowerInst;
        public void InstAllBallThrower()
        {
            foreach (BallThrowerStats Thrower in ListBallThrower)
            {
                GameObject Parent = Thrower.Parents[Random.Range(0, Thrower.Parents.Count - 1)];
                GameObject Throw = Instantiate(new GameObject("Ball Thrower"), Parent.transform);
                Throw.AddComponent<BallThrower>().SetUpThrower(Thrower);
                ThrowerInst.Add(Throw);
            }
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
        public GameObject Projectile;
        public List<GameObject> Parents;
        public float Power;
        public float Cadence;
        public float DelayBeforeFirstShoot;
        public int NbrProj;
        public bool InfiniteProj;
        public Vector2 DirectionMaxMin;
        public bool ShootAtPlayer;
        public bool DestroyOnHit;
        public bool ActAsABurst;
        public int ProjPerBurst;
        public bool LoopParent;
    }
}
