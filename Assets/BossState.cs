using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]
    public BossBehavior Boss;

    public void Start()
    {
        Boss = FindObjectOfType<BossBehavior>();
    }
    public virtual void Activate()
    {
        Debug.Log(this.name + " Activate");
    }

    public virtual void Deactivate()
    {
        Debug.Log(this.name + " Deactivate");
    }

}
