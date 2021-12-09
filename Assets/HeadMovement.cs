using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameUtilities.GameUtilities;

public class HeadMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject BoardTransform;
    public GameObject BaseTransform;
    public GameObject Visual;
    public HEADSTATE State;

    public float FollowSpeed;
    public float Timer;
    public float OnBoardDuration;
    public AnimationCurve Curve;
    private Vector3 LastPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case HEADSTATE.GOONBOARD:

                Timer += Time.deltaTime;
                transform.position = Vector3.Slerp(BaseTransform.transform.position, BoardTransform.transform.position, Timer / OnBoardDuration);
                if (Timer / OnBoardDuration >= 1)
                {
                    State = HEADSTATE.CHASEPLAYER;
                    Timer = 0;
                }
                break;

            case HEADSTATE.CHASEPLAYER:

                GameObject target = GetClosestGameObject(transform.position, ListToListGameObjects(FindObjectsOfType<PlayerController>().ToList()));
                if (target)
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                        new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z), FollowSpeed * Time.deltaTime);
                }
                Timer += Time.deltaTime;
                Visual.transform.position = new Vector3(transform.position.x, transform.position.y + Curve.Evaluate(Timer), transform.position.z);
                LastPosition = transform.position;
                break;

            case HEADSTATE.RETURN:
                Timer += Time.deltaTime;
                transform.position = Vector3.Slerp(LastPosition, BaseTransform.transform.position, Timer / OnBoardDuration);
                if (Timer / OnBoardDuration >= 1)
                {
                    State = HEADSTATE.ENDMOVEMENT;
                }
                break;

        }
    }


    public enum HEADSTATE
    {
        ENDMOVEMENT,
        GOONBOARD,
        CHASEPLAYER,
        RETURN,
    }
}
