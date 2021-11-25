using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public enum DoorSlideDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    FRONT,
    BACK
}

public enum DoorState
{
    OPEN,
    OPENING,
    CLOSE,
    CLOSING
}
public class DoorSliding : MonoBehaviour, IDoor
{

    [SerializeField] private DoorSlideDirection openningDirection;
    [SerializeField] private float movingDistance;

    [SerializeField] private DoorState doorState = DoorState.CLOSE;
    private float _currentDistance;
    
    public void OpenDoor()
    {
        doorState = DoorState.OPENING;
    }

    private void Update()
    {
        if (doorState == DoorState.OPENING)
        {
            transform.Translate(getDirection());
            _currentDistance++;
            if (_currentDistance >= movingDistance)
            {
                doorState = DoorState.OPEN;
            }
        } 
        else if (doorState == DoorState.CLOSING)
        {
            Vector3 dir = getDirection();
            dir.Scale(new Vector3(-1, -1, -1));
            transform.Translate(dir);
            _currentDistance--;
            if (_currentDistance <= 0)
            {
                doorState = DoorState.CLOSE;
            }
        }
    }

    Vector3 getDirection()
    {
        switch (openningDirection)
        {
            case DoorSlideDirection.UP:
                return new Vector3(0, 1, 0);
            case DoorSlideDirection.DOWN:
                return new Vector3(0, -1, 0);
            case DoorSlideDirection.FRONT:
                return new Vector3(0, 0, 1);
            case DoorSlideDirection.BACK:
                return new Vector3(0, 0, -1);
            case DoorSlideDirection.LEFT:
                return new Vector3(-1, 0, 0);
            case DoorSlideDirection.RIGHT:
                return new Vector3(1, 0, 0);
            default:
                return new Vector3(0, 1, 0);
        }
    }

    public void CloseDoor()
    {
        doorState = DoorState.CLOSING;
    }

    public DoorState getDoorState()
    {
        return doorState;
    }

    private void OnDrawGizmosSelected()
    {
        if (doorState == DoorState.OPEN || doorState == DoorState.CLOSE)
        {
            Vector3 dir = getDirection();
            if (doorState == DoorState.CLOSE)
            {
                dir.Scale(new Vector3(movingDistance, movingDistance, movingDistance));
            }
            else if (doorState == DoorState.OPEN)
            {
                dir.Scale(new Vector3(movingDistance * (-1), movingDistance * (-1), movingDistance * (-1)));
            }
            
            BoxCollider boxCollider = GetComponent<BoxCollider>();

            Gizmos.matrix = transform.localToWorldMatrix;

            Vector3 opennedPosition = Vector3.zero + dir + boxCollider.center;

            Gizmos.DrawWireCube(opennedPosition, boxCollider.size);
        }
    }
}
