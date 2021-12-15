using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Manager_Life))]
[RequireComponent(typeof(BossMovement))]
public class BossBehavior : MonoBehaviour
{
    public List<Phase> Phases = new List<Phase>(){new Phase(),new Phase(),new Phase(),new Phase()};
    [SerializeField] public int ActualPhase = 0;
    public bool BossStarted = false;

    public List<BossAction> ListActualAction;
    public int ActualAction;


    //EDITOR UTILS//
    /*public BossAction.BallThrowerInstantier BossBallThrower;
    public BossAction.LaserInstantier LaserInstantier;
    public BossAction.ShieldInstantier ShieldStats;*/
    /*public GameObject BaseLaser;
    public GameObject BaseShooter;
    public GameObject BaseShield;*/
    public GameObject BaseGameObject;
    public GameObject ActionHolder;
    public GameObject ShieldHolder;
    //
    public Manager_Life Life;
    public BossHeadRotation HeadRotation;

    private Boss_Music_Manager MusicManager;
    public Boss_Head_Manager Head;

    /*private static BossBehavior inst;*/
    /*public static BossBehavior Boss { get => inst; }*/
    void Awake()
    {
        /*if (Boss != null && Boss != this)
            Destroy(gameObject);

        inst = this;*/
        Life = GetComponent<Manager_Life>();
        MusicManager = GetComponent<Boss_Music_Manager>();
        Head = GetComponent<Boss_Head_Manager>();
        HeadRotation = GetComponent<BossHeadRotation>();
    }

    public void Start()
    {
        //LaunchPhase(0);
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
            ListActualAction[ActualAction].Activate(this);
        }
        else
        {
            ListActualAction[ActualAction].Deactivate();
            ActualAction += 1;
            if (ActualAction >= ListActualAction.Count)
            {
                ActualAction = 0;
            }
            ListActualAction[ActualAction].Activate(this);
        }
    }

    public void Transition(int Phase)
    {
        //FONCTION DE TRANSITION A CALL ICI COROUTINE//
    }

    public void NextPhase()
    {
        CameraShakeManager.CameraShake.Shake(1.5f, 1.5f, 15f);
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

            //
            /*if (BossVoiceManager.Voice)
            {
                BossVoiceManager.Voice.Play(BossVoiceManager.Voice.ChangePhase);
            }*/
            //TRANSITION VERS PROCHAINE PHASE PAS CALL LAUNCH PHASE TT DE SUITE//
            LaunchPhase(ActualPhase);
            MusicManager.LaunchLayer(ActualPhase+1);
        }
    }

    

    public void LaunchAction()
    {
        if (Phases[ActualPhase].RandomAction)
        {
            ActualAction = Random.Range(0, ListActualAction.Count);
            ListActualAction[ActualAction].Activate(this);
        }
        else
        {
            ActualAction = 0;
            ListActualAction[ActualAction].Activate(this);
        }
    }

    public void DeactivePhase(int id)
    {
        foreach (BossAction Action in Phases[id].ListAction)
        {
            Action.Deactivate();
        }
    }

    public void GetDamage()
    {
        if (BossVoiceManager.Voice)
        {
            BossVoiceManager.Voice.PlayHit();
        }

        GameObject_ScaleChange Scale = GetComponentInChildren<GameObject_ScaleChange>();
        Scale.GrowUp();
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

