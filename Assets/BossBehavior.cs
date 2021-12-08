using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

public class BossBehavior : MonoBehaviour
{
    public List<Phase> Phases = new List<Phase>(4);
    [SerializeField] public int ActualPhase = 0;
    public bool BossStarted = false;

    public List<BossAction> ListActualAction;
    public int ActualAction;


    //EDITOR UTILS//
    public BossAction.BallThrowerInstantier BossBallThrower;
    public BossAction.LaserInstantier LaserInstantier;
    public BossAction.ShieldInstantier ShieldStats;
    public GameObject BaseLaser;
    public GameObject BaseShooter;
    public GameObject BaseShield;
    public GameObject BaseGameObject;
    public GameObject ActionHolder;
    public GameObject ShieldHolder;
    //
    public Manager_Life Life;
    //public BossHeadRotation HeadRotation;
    private static BossBehavior inst;
    public static BossBehavior Boss { get => inst; }
    void Awake()
    {
        if (Boss != null && Boss != this)
            Destroy(gameObject);

        inst = this;
        Life = GetComponent<Manager_Life>();
        //HeadRotation = GetComponent<BossHeadRotation>();
    }

    public void Start()
    {
        LaunchPhase(0);
    }


    public void LaunchPhase(int id)
    {
        BossStarted = true;
        ListActualAction.Clear();
        ListActualAction = Phases[id].ListAction;
        ActualPhase = id;
        Life.SetCurentLife(Phases[id].HpToSet);
        Life.maxHealth = Phases[id].HpToSet;
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

    public void Transition(int Phase)
    {
        //FONCTION DE TRANSITION A CALL ICI COROUTINE//
    }

    public void NextPhase()
    {
        if (BossStarted)
        {
            //PREMIERE Phase
            if (ActualPhase != -1)
            {
                ListActualAction[ActualAction].Deactivate();
            }

            //LAST PHASE

            if (ActualPhase >= Phases.Count - 1)
            {
                ListActualAction[ActualAction].Deactivate();
                //CLAIM END BOSS
                Debug.Log("Le boss est MOOORT");
                BossStarted = false;
                Life.SetCurentLife(15000);
                return;
            }

            ActualAction = 0;
            ActualPhase += 1;

            //TRANSITION VERS PROCHAINE PHASE PAS CALL LAUNCH PHASE TT DE SUITE//
            LaunchPhase(ActualPhase);
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
        IDLE,
        FOLLOWCLOSESTPLAYER,
    }
   
    [System.Serializable]
    public class Phase
    {
        public List<BossAction> ListAction;
        public BOSSMOVEMENT MovementBoss = BOSSMOVEMENT.UPDOWN;
        public int HpToSet;
        public bool RandomAction;
        public int HeadToActivate;
    }
}

