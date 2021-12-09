using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Head_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<HeadMovement> Heads;

    public void LaunchHead(int id)
    {
        Heads[id].Timer = 0;
        Heads[id].State = HeadMovement.HEADSTATE.GOONBOARD;
    }

    public void RetractHead(int id)
    {
        Heads[id].Timer = 0;
        Heads[id].State = HeadMovement.HEADSTATE.RETURN;
    }
}
