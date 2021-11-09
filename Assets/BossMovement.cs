using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public BossBehavior Movement;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Movement.Phases[Movement.ActualPhase].MovementBoss == BossBehavior.BOSSMOVEMENT.LEFTRIGHT)
        {
            Debug.Log("COUCOU JE BOUGE DE GAUCHE A DROITE");
        }
    }
}
