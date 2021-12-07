using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform grabPosition;
    [SerializeField] private float power = 20f;
    [SerializeField] private LayerMask wallMask;
    [SerializeField] private float grabDelay;
    [SerializeField] private int balleLayer = 6;
    [SerializeField] private ColorEnum playerColor;
    [SerializeField] private Animator _animator;
    public Color ColorInfuse;

    private bool canGrabBall = false;
    private bool couldGrabBall = true;

    [SerializeField] private bool SetDirectionAtEndFreezeFrame = true;

    public GameObject balle;

    private Vector2 movementInput;
    private Rigidbody rb;

    private bool isHoldingBall = false;
    private bool balleIsTake = false;

    public GameObject CharacterVisual;

    public List<GameObject> BallsInRange = new List<GameObject>();
    public List<GameObject> EjectedBalls = new List<GameObject>();

    public Manager_NumbPlayers ManagePlayer;

    public ParticleSystem particleSystem;

    private ParticleManager particule;

    public float TimeForRevive = 5;
    private float TimeRevive;

    [SerializeField] GameObject SlowDownEffect;
    private float _timer;

    private bool revive;
    private bool tryRevive;
    private GameObject playerNeedHelp = null;
    private ParticuleHeal healParticule;


    void Start()
    {
        particule = particleSystem.GetComponent<ParticleManager>();
        particule.destoy = false;
        particleSystem.GetComponent<Renderer>().material.color = Color.white;
        rb = GetComponent<Rigidbody>();
        ManagePlayer = FindObjectOfType<Manager_NumbPlayers>();

        if(!ManagePlayer.player1)
        {
            ManagePlayer.player1 = gameObject;
        }
        else if (!ManagePlayer.player2)
        {
            ManagePlayer.player2 = gameObject;
        }
        else if (!ManagePlayer.player3)
        {
            ManagePlayer.player3 = gameObject;
        }
        else if (!ManagePlayer.player4)
        {
            ManagePlayer.player4 = gameObject;
        }
    }

    /*public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnSprint(InputAction.CallbackContext ctx) => ChangeSprintValue(ctx.ReadValueAsButton());*/

    /*public void OnGrabBall(InputAction.CallbackContext ctx) => TryGrabBall(ctx.ReadValueAsButton());*/

    public void OnThrowBall(InputAction.CallbackContext ctx) => TryThrowBall(ctx.ReadValueAsButton(), ctx);

    /*public void OnLookAround(InputAction.CallbackContext ctx) => RotatePlayer(ctx.ReadValue<Vector2>());*/

    public void OnInfuse(InputAction.CallbackContext ctx) => TryInfuse(ctx.ReadValueAsButton(), ctx);

    public void OnRevive(InputAction.CallbackContext ctx) => TryRevive(ctx.ReadValueAsButton(), ctx);


    void Update()
    {
        if (_timer > 0) 
            _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            couldGrabBall = true;
            _timer = 0;
        }

        if(tryRevive)
        {
            
            if(TimeRevive<TimeForRevive)
            {
                Debug.Log(playerNeedHelp.transform.Find("ParticleHeal").GetComponent<ParticleSystem>().isPlaying);
                if(!playerNeedHelp.transform.Find("ParticleHeal").GetComponent<ParticleSystem>().isPlaying)
                    playerNeedHelp.transform.Find("ParticleHeal").GetComponent<ParticleSystem>().Play();
                TimeRevive += Time.deltaTime;
            }
            else
            {
                playerNeedHelp.GetComponent<Manager_Life>().SetCurentLife(playerNeedHelp.GetComponent<Manager_Life>().maxHealth);
                tryRevive = false;
                Debug.Log("it is alive");
            }
        }

    }

    void OnGUI()
    {
        /*
        if (GUI.Button(new Rect(250, 0, 250, 50), "Reset grab delay"))
        {
            _timer = 0;
        }

        GUI.Label(new Rect(250, 50, 250, 50), "Grab delay = " + _timer + " seconds ");
        */
    }

    void SetCanGrab(bool canGrab)
    {
        canGrabBall = canGrab;
    }

    public bool CanGrab()
    {
        return canGrabBall;
    }

   public void TryInfuse(bool buttonPressed, InputAction.CallbackContext ctx)
   {
       SetBall();
       if (balle && _timer <= 0)
       {
           print("INFUSE");
           Balle balleData = balle.GetComponent<Balle>();
           balleData.color = playerColor;
           if(Infuse_Sound_Manager.Infuse)
            Infuse_Sound_Manager.Infuse.PlayInfuseSound(balleData.color,balleData.combo);
           _timer = grabDelay;
           StartCoroutine(ColorParticule()); 
           ThrowBall();
        }
    }

    public void TryRevive(bool buttonPressed, InputAction.CallbackContext ctx)
    {
        if (playerNeedHelp == null) return;
        //Debug.Log("je suis en cours ");
        tryRevive = buttonPressed;
        //Debug.Log(tryRevive);

        if(!tryRevive)
        {
            playerNeedHelp.transform.Find("ParticleHeal").GetComponent<ParticleSystem>().Stop();
            TimeRevive = 0;
        }
    }

   public void TryThrowBall(bool buttonPressed, InputAction.CallbackContext ctx)
   { 
       SetBall();
       if (balle && _timer<=0)
       {
           if (Infuse_Sound_Manager.Infuse && Infuse_Sound_Manager.Infuse.Hit_Sound != null)
           {
               Infuse_Sound_Manager.Infuse.Hit_Sound.Post(gameObject);
           }
           _timer = grabDelay;
           ControllerHaptics.instance.ShakeController(ctx.control.device.deviceId, .6f, .8f, 2);
           ThrowBall();
       }
   }

   public void ThrowBall()
   {
       Ball BallStats = balle.GetComponent<Ball>();
       balle.GetComponent<Balle>().combo++;
       BallStats.SetSpeed(BallStats.Speed + (balle.GetComponent<Balle>().combo * balle.GetComponent<Balle>().comboSpeed));
       BallStats.AddEffect();
       BallsInRange.Remove(balle);
       EjectedBalls.Add(balle);
       
       _animator.Play("Hit");

       if (FreezeFrame.Freezer)
       {
           if (SetDirectionAtEndFreezeFrame)
           {
               FreezeFrame.Freezer.TryFreeze(Mathf.Clamp(balle.GetComponent<Balle>().combo * 0.05f,0,0.2f),
               balle.GetComponent<Ball>(),CharacterVisual);
           }
           else
           {
               balle.GetComponent<Ball>().SetDirection(new Vector3(0, CharacterVisual.transform.eulerAngles.y, 0));
               FreezeFrame.Freezer.TryFreeze(Mathf.Clamp(balle.GetComponent<Balle>().combo * 0.05f, 0, 0.2f));
           }
       }
       else
       {
           balle.GetComponent<Ball>().SetDirection(new Vector3(0, CharacterVisual.transform.eulerAngles.y, 0));
        }


       if (CameraShakeManager.CameraShake)
       {
           StartCoroutine(CameraShakeManager.CameraShake.Shake(0.25f, 1, 15, 0.1f));
       }
       
        balle = null;
        
   }

   public void SetBall()
   {
       if (BallsInRange.Count > 0)
       {
           balle = BallsInRange[0];
           BallsInRange.Remove(BallsInRange[0]);
       }
   }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (other.gameObject.GetComponent<Ball>().Returnable)
            {
                BallsInRange.Add(other.gameObject);
            }
        }

        if(other.gameObject.layer == 7)
        {
            if (other.gameObject.GetComponent<Manager_Life>().GetCurentLife() == 0)
            {
                playerNeedHelp = other.gameObject;
                healParticule = playerNeedHelp.GetComponentInChildren<ParticuleHeal>();
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (other.gameObject.GetComponent<Ball>().Returnable)
            {
                if (EjectedBalls.Contains(other.gameObject))
                {
                    EjectedBalls.Remove(other.gameObject);
                }
                else
                {
                    other.gameObject.GetComponent<Balle>().combo = 0;
                    other.gameObject.GetComponent<Ball>().ResetSpeed();
                    Instantiate(SlowDownEffect, other.gameObject.transform.position,SlowDownEffect.transform.rotation,other.transform);
                    BallsInRange.Remove(other.gameObject);
                }
            }
        }

        if (other.gameObject.layer == 7)
        {
            if(other.gameObject.GetComponent<Manager_Life>().GetCurentLife() == 0)
            {
                playerNeedHelp.transform.Find("ParticleHeal").GetComponent<ParticleSystem>().Stop();
                playerNeedHelp = null;
            }
        }
    }

    public IEnumerator ColorParticule()
    {
        if (playerColor == ColorEnum.RED)
        {
            particleSystem.startColor = balle.GetComponent<Balle>().InfuseColorRed(ColorInfuse);
        }
        else if (playerColor == ColorEnum.ORANGE)
        {
            particleSystem.startColor = balle.GetComponent<Balle>().InfuseColorOrange(ColorInfuse);
        }
        else if (playerColor == ColorEnum.BLEU)
        {
            particleSystem.startColor = balle.GetComponent<Balle>().InfuseColorBleu(ColorInfuse);
        }            
        else if (playerColor == ColorEnum.GREEN)
        {
            particleSystem.startColor = balle.GetComponent<Balle>().InfuseColorGreen(ColorInfuse);
        }

        particleSystem.Play();
        yield return new WaitForSeconds(0.5f);
        particleSystem.startColor = Color.white;
        //particleSystem.GetComponent<Renderer>().material.color = Color.white;

    }

    public ColorEnum GetPlayerColor()
    {
        return playerColor;
    }

    /* public void TryThrowBall(bool buttonPressed, InputAction.CallbackContext ctx)
    {
        if (!buttonPressed) return;
        if (!isHoldingBall) return;


        Transform balleT = null;
        for (int i = 0; i < grabPosition.childCount; i++)
        {
            if (grabPosition.GetChild(i).gameObject.layer == balleLayer)
            {
                balleT = grabPosition.GetChild(i);
                break;
            }
        }
        if (balleT == null)
            return;
        balle = balleT.gameObject;

        balle.transform.SetParent(null);

        SphereCollider ballCollider = balle.GetComponent<SphereCollider>();
        ballCollider.enabled = true;

        Rigidbody balleRB = balle.GetComponent<Rigidbody>();
        balleRB.isKinematic = false;
        balleRB.useGravity = true;
        balleRB.freezeRotation = false;
        
        balleRB.AddForce(CharacterVisual.transform.forward * (power + (balle.GetComponent<Balle>().combo * balle.GetComponent<Balle>().comboSpeed)), ForceMode.Impulse);

        particleSystem.Play();
        balle.GetComponent<Balle>().combo++;
        balleIsTake = true;

        isHoldingBall = false;
        couldGrabBall = false;
        _timer = grabDelay;

        ControllerHaptics.instance.ShakeController(ctx.control.device.deviceId, .6f, .8f, 2);
    }

    public void TryGrabBall(bool buttonPressed)
    {
        if (!buttonPressed) return;
        if (!couldGrabBall) return;
        _timer += grabDelay;
        couldGrabBall = false;
        if (!canGrabBall) return;
        if (isHoldingBall) return;

        SphereCollider ballCollider = balle.GetComponent<SphereCollider>();
        ballCollider.enabled = false;

        balle.transform.SetParent(grabPosition);
        balle.transform.position = grabPosition.position;
        balle.transform.rotation = grabPosition.rotation;

        Rigidbody ballRB = balle.GetComponent<Rigidbody>();
        ballRB.velocity = new Vector3(0, 0, 0);
        ballRB.useGravity = false;
        ballRB.freezeRotation = false;
        ballRB.isKinematic = true;

        isHoldingBall = true;
    }

    public void TryInfuse(bool buttonPressed, InputAction.CallbackContext ctx)
    {
        if (!buttonPressed) return;
        if (!isHoldingBall) return;

        Transform balleT = null;
        for (int i = 0; i < grabPosition.childCount; i++)
        {
            if (grabPosition.GetChild(i).gameObject.layer == balleLayer)
            {
                balleT = grabPosition.GetChild(i);
                break;
            }
        }
        if (balleT == null) return;
        balle = balleT.gameObject;

        balle.transform.SetParent(null);

        SphereCollider ballCollider = balle.GetComponent<SphereCollider>();
        ballCollider.enabled = true;

        Rigidbody balleRB = balle.GetComponent<Rigidbody>();
        balleRB.isKinematic = false;
        balleRB.useGravity = true;
        balleRB.freezeRotation = true;

        balleRB.AddForce(CharacterVisual.transform.forward * (power + (balle.GetComponent<Balle>().combo * balle.GetComponent<Balle>().comboSpeed)), ForceMode.Impulse);
        balle.GetComponent<Balle>().combo++;
        balleIsTake = true;

        StartCoroutine(ColorParticule());

        isHoldingBall = false;
        couldGrabBall = false;
        _timer = grabDelay;

        ControllerHaptics.instance.ShakeController(ctx.control.device.deviceId, .6f, .8f, 2);
    }*/
}
