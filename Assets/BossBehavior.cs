using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class BossBehavior : MonoBehaviour
{
    // Start is called before the first frame update
   /* public List<BossAction> ListAction;*/
    public List<Phase> Phases;
    [SerializeField] public int ActualPhase = -1;
    public bool BossStarted = false;

    public List<BossAction> ListActualAction;
    public int ActualAction;


    //EDITOR UTILS//
    public BossAction.BallThrowerInstantier BossBallThrower;
    public GameObject BaseGameObject;
    public GameObject ActionHolder;


    private static BossBehavior inst;
    public static BossBehavior Boss { get => inst; }
    void Awake()
    {
        if (Boss != null && Boss != this)
            Destroy(gameObject);

        inst = this;
    }

    public void Start()
    {
        LaunchPhase(0);
    }


    public void LaunchPhase(int id)
    {
        ListActualAction.Clear();
        ListActualAction = Phases[id].ListAction;
        ActualPhase = id;
        LaunchAction();
    }

    public void NextAction()
    {
        if (Phases[ActualPhase].RandomAction)
        {
            ListActualAction[ActualAction].Deactivate();
            ActualAction = Random.Range(0, ListActualAction.Count);
            ListActualAction[ActualAction].Activate();
        }
        else
        {
            ListActualAction[ActualAction].Deactivate();
            ActualAction += 1;
            if (ActualAction >= ListActualAction.Count)
            {
                ActualAction = 0;
            }
            ListActualAction[ActualAction].Activate();
        }
    }

    public void LaunchAction()
    {
        if (Phases[ActualPhase].RandomAction)
        {
            ActualAction = Random.Range(0, ListActualAction.Count);
            ListActualAction[ActualAction].Activate();
        }
        else
        {
            ActualAction = 0;
            ListActualAction[ActualAction].Activate();
        }
    }

    public void DeactivePhase(int id)
    {
        foreach (BossAction Action in Phases[id].ListAction)
        {
            Action.Deactivate();
        }
    }


    public enum BOSSMOVEMENT
    {
        LEFTRIGHT,
        UPDOWN,
    }
   
    [System.Serializable]
    public class Phase
    {
        public List<BossAction> ListAction;
        public BOSSMOVEMENT MovementBoss = BOSSMOVEMENT.UPDOWN;
        public bool RandomAction;
    }
}

