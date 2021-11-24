using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateMultiple : MonoBehaviour
{
    [SerializeField] private DoorWithMultipleButton _door;

    public ColorEnum playerColorNeeded;
    
    private void OnTriggerEnter(Collider other)
    {
        _door.OnTriggerEnterEvent(other, this);
    }

    private void OnTriggerExit(Collider other)
    {
        _door.OnTriggerExitEvent(other, this);
    }

    private void OnTriggerStay(Collider other)
    {
        _door.OnTriggerStayEvent(other, this);
    }
}
