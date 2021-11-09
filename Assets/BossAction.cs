using System.Collections.Generic;
using UnityEngine;

public class BossAction : MonoBehaviour
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
        public BallThrowerStats BallThrower;
        [HideInInspector]
        public List<GameObject> ThrowerInst;
        public void InstAllBallThrower()
        {
            GameObject Parent = BallThrower.Parents[Random.Range(0, BallThrower.Parents.Count - 1)];
            GameObject Throw = Instantiate(new GameObject("Ball Thrower"), Parent.transform);
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
    public class BallThrowerStats
    {
        public string Name;
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
