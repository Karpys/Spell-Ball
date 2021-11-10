using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDoor
{

    void OpenDoor();
    void CloseDoor();

    DoorState getDoorState();

}
