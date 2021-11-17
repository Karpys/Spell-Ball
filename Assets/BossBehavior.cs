using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public List<BossState> ListState;
    [SerializeField] private int ActualState = -1;
    public BossHpManager HpManager;
    public bool BossStarted = false;
    void Start()
    {
        StartState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartState()
    {
        if (ActualState != 0)
        {

            ActualState = 0;
            ActivateState(ActualState);
        }
        else
        {
            //State 0 Deja Actif//
            return;
        }
    }
    public void NextState()
    {
        if (ActualState == ListState.Count - 1)
        {
            DeactivateLastState();
        }
        else if(ActualState < ListState.Count - 1)
        {
            ActivateNextState();
        }
        else
        {
            Debug.Log("No more State");
        }
    }

    public void ActivateNextState()
    {
        //Deactivate Actual State , Start Next State//
        DeactivateState(ActualState);
        ActualState += 1;
        ActivateState(ActualState);
    }

    public void DeactivateLastState()
    {
        //Deactivate Last State//
        DeactivateState(ActualState);
        ActualState += 1;
    }

    public void DeactivateState(int id)
    {
        ListState[id].Deactivate();
    }

    public void ActivateState(int id)
    {
        ListState[id].Activate();
    }
}

