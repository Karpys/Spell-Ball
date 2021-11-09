using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BossBehavior : MonoBehaviour
{
    // Start is called before the first frame update
   /* public List<BossAction> ListAction;*/
    public List<Phase> Phases;
    [SerializeField] public int ActualState = -1;
    public bool BossStarted = false;

    public BossAction.BallThrowerInstantier BossBallThrower;
    public GameObject BaseGameObject;


    public void Update()
    {

    }

    public void Start()
    {
        PlayPhase(0);
    }

    public void PlayPhase(int id)
    {
        foreach (BossAction Action in Phases[id].ListAction)
        {
            Action.Activate();
        }
    }

    public void DeactivePhase(int id)
    {
        foreach (BossAction Action in Phases[id].ListAction)
        {
            Action.Deactivate();
        }
    }
    

    [System.Serializable]
    public class Phase
    {
        public List<BossAction> ListAction;
    }

    
}

