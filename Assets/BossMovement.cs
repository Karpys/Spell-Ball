using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameUtilities.GameUtilities;
public class BossMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public BossBehavior Movement;

    public LeftRightMovement LeftRight;

    public float FollowSpeed;

    public float RotationSpeed;
    // Update is called once per frame
    void Update()
    {
        if (Movement.ActualPhase >= 0)
        {
            switch (Movement.Phases[Movement.ActualPhase].MovementBoss)
            {
                case BossBehavior.BOSSMOVEMENT.IDLE:
                    break;
                case BossBehavior.BOSSMOVEMENT.LEFTRIGHT:
                    MovementLeftRight();
                    break;
                case BossBehavior.BOSSMOVEMENT.FOLLOWCLOSESTPLAYER:
                    FollowClosest();
                    break;

            }
        }
        /*if (Movement.Phases[Movement.ActualPhase].MovementBoss == BossBehavior.BOSSMOVEMENT.LEFTRIGHT)
        {
            Debug.Log("COUCOU JE BOUGE DE GAUCHE A DROITE");
        }*/
    }

    public void FollowClosest()
    {
        GameObject target = GetClosestGameObject(transform.position, ListToListGameObjects(FindObjectsOfType<PlayerController>().ToList()));
        if (target)
        {
            transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), FollowSpeed * Time.deltaTime);
            RotateTowardsClosestPlayer(target);
        }
    }

    public void RotateTowardsClosestPlayer(GameObject Target)
    {
        Vector3 _direction = (Target.transform.position - transform.position).normalized;

        Quaternion LookRotation = Quaternion.LookRotation(_direction);

        Quaternion newRot = Quaternion.Slerp(transform.rotation, LookRotation, RotationSpeed * Time.deltaTime);

        Vector3 eulerAng = newRot.eulerAngles;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, eulerAng.y, transform.eulerAngles.z);
    }

    public void MovementLeftRight()
    {
        if (LeftRight.Right)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(LeftRight.TransformRight.transform.position.x, transform.position.y, transform.position.z),LeftRight.Speed * Time.deltaTime);
            if (transform.position == new Vector3(LeftRight.TransformRight.transform.position.x, transform.position.y,
                transform.position.z))
            {
                LeftRight.Right = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(LeftRight.TransformLeft.transform.position.x, transform.position.y, transform.position.z), LeftRight.Speed * Time.deltaTime);
            if (transform.position == new Vector3(LeftRight.TransformLeft.transform.position.x, transform.position.y,
                transform.position.z))
            {
                LeftRight.Right = true;
            }
        }
    }

    [System.Serializable]
    public struct LeftRightMovement
    {
        public GameObject TransformRight;
        public GameObject TransformLeft;
        public float Speed;
        [HideInInspector]public bool Right;
    }
}
