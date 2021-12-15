using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LaserBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public Wwise_Script.WwiseMultiplePlay LaserSounds;
    public BossAction.LaserStats Stats;
    public LineRenderer Line;
    public LayerMask Layer;
    public LASERSTATE Laserstate= LASERSTATE.OPEN;

    private float _timer;
    private float _WaitTime;
    private float Lenght;

    void Awake()
    {
        Line = GetComponent<LineRenderer>();
    }
    void Start()
    {
        //SOUNDS:START TRIGGER LASER//
        LaserSounds.PlaySound(0);
        LaserSounds.PlaySound(1);
        transform.eulerAngles = Stats.StartPosition.transform.eulerAngles +  new Vector3(0, Stats.StartEndAngle.x, 0);
        if (Stats.Infinity)
        {
            Stats.Lenght = 100f;
        }
    }

    public void OnDestroy()
    {
        foreach (Wwise_Script.WwiseSounds sound in LaserSounds.Sounds)
        {
            sound.Sound.Stop(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Laserstate == LASERSTATE.UP || Laserstate == LASERSTATE.DOWN)
        {
            if (Laserstate == LASERSTATE.UP)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                _timer -= Time.deltaTime;
            }

            float ratio = _timer / Stats.Duration;
            transform.eulerAngles = Stats.StartPosition.transform.eulerAngles + new Vector3(0, Mathf.Lerp(Stats.StartEndAngle.x, Stats.StartEndAngle.y, ratio), 0);
            LaserSounds.PlaySound(2);
            LaserSounds.PlaySound(3);
            if (ratio > 1)
            {
                if (Stats.PingPong)
                {
                    Laserstate = LASERSTATE.DOWN;
                }
                else
                {
                    Laserstate = LASERSTATE.CLOSE;
                }
            }
            else if(ratio<0)
            {
                Laserstate = LASERSTATE.CLOSE;
            }

        }else
        {
            if (Laserstate == LASERSTATE.OPEN)
            {
                _WaitTime += Time.deltaTime;
            }
            else if (Laserstate == LASERSTATE.CLOSE)
            {
                LaserSounds.PlaySound(4);
                _WaitTime -= Time.deltaTime;
            }
            
            if (Stats.WaitTime == 0 && Laserstate == LASERSTATE.OPEN)
            {
                
                Line.endWidth = Stats.EndWidth;
                Line.startWidth = Stats.EndWidth;
                Laserstate = LASERSTATE.UP;
            }
            else
            {
                float waitRatio = _WaitTime / Stats.WaitTime;
                float Width = Mathf.Lerp(Stats.StartWidth, Stats.EndWidth, waitRatio);
                Lenght = Mathf.Lerp(0, Stats.Lenght, waitRatio);
                Line.endWidth = Width;
                Line.startWidth = Width;
                if (waitRatio > 1)
                {
                    Laserstate = LASERSTATE.UP;
                }else if (waitRatio <= 0 && Laserstate == LASERSTATE.CLOSE)
                {
                    //DESTRUCTION
                    
                    Stats.Manager.EndRay();
                    Destroy(gameObject);
                }
            }
        }

        Vector3 LaserCast = transform.forward * Lenght;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hit;
        Vector3 FirstHitPosition = Vector3.zero;
        if (Physics.RaycastAll(ray,Lenght,Layer).Length>0)
        {
            hit = Physics.RaycastAll(ray, Lenght, Layer);
            //DAMAGE FIRST Player  hit[0]//
            if (hit[0].collider.gameObject.TryGetComponent(out PlayerCollision player))
            {
                Manager_Life Life = hit[0].collider.gameObject.GetComponentInParent<Manager_Life>();
                if (Life.Timerinvis <= 0)
                {
                    Life.Timerinvis = 1;
                    Life.DamageHealth(1);
                }
            }
            FirstHitPosition = hit[0].point;
        }

        Line.SetPosition(0, transform.position);

        if (FirstHitPosition != Vector3.zero)
        {
            Line.SetPosition(1, FirstHitPosition);
        }
        else
        {
            Line.SetPosition(1, transform.position + LaserCast);
            
        }

    }

    public enum LASERSTATE
    {
        OPEN,
        UP,
        DOWN,
        CLOSE,
    }
}
