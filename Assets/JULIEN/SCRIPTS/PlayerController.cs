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

    private bool canGrabBall = false;
    private bool couldGrabBall = true;

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
    private float _timer;
    

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
        }else
        {
        ManagePlayer.player2 = gameObject;
        }
    }

    /*public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();

    public void OnSprint(InputAction.CallbackContext ctx) => ChangeSprintValue(ctx.ReadValueAsButton());*/

    /*public void OnGrabBall(InputAction.CallbackContext ctx) => TryGrabBall(ctx.ReadValueAsButton());*/

    public void OnThrowBall(InputAction.CallbackContext ctx) => TryThrowBall(ctx.ReadValueAsButton(), ctx);

    /*public void OnLookAround(InputAction.CallbackContext ctx) => RotatePlayer(ctx.ReadValue<Vector2>());*/

    public void OnInfuse(InputAction.CallbackContext ctx) => TryInfuse(ctx.ReadValueAsButton(), ctx);


    void Update()
    {
        if (_timer > 0) 
            _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            couldGrabBall = true;
            _timer = 0;
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(250, 0, 250, 50), "Reset grab delay"))
        {
            _timer = 0;
        }

        GUI.Label(new Rect(250, 50, 250, 50), "Grab delay = " + _timer + " seconds ");
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
           _timer = grabDelay;
           StartCoroutine(ColorParticule()); 
           ThrowBall();
        }
    }


   public void TryThrowBall(bool buttonPressed, InputAction.CallbackContext ctx)
   { 
       SetBall();
       if (balle && _timer<=0)
       {
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
       balle.GetComponent<Ball>().SetDirection(new Vector3(0, CharacterVisual.transform.eulerAngles.y, 0));
       BallStats.AddEffect();
       BallsInRange.Remove(balle);
       EjectedBalls.Add(balle);

       if (FreezeFrame.Freezer)
       {
           FreezeFrame.Freezer.TryFreeze(0.2f);
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
            BallsInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (EjectedBalls.Contains(other.gameObject))
            {
                EjectedBalls.Remove(other.gameObject);
            }
            else
            {
                other.gameObject.GetComponent<Balle>().combo = 0;
                other.gameObject.GetComponent<Ball>().ResetSpeed();
                BallsInRange.Remove(other.gameObject);
            }
        }
    }

    public IEnumerator ColorParticule()
    {
        if (gameObject.name == "Character(Clone)")
        {
            particleSystem.startColor = balle.GetComponent<Balle>().InfuseColorRed();
        }
        else if (gameObject.name == "Character 1(Clone)")
        {
            particleSystem.startColor = balle.GetComponent<Balle>().InfuseColorOrange();
        }
        else if (gameObject.name == "Character 2(Clone)")
        {
            particleSystem.startColor = balle.GetComponent<Balle>().InfuseColorBleu();
        }            
        else if (gameObject.name == "Character 3(Clone)")
        {
            particleSystem.startColor = balle.GetComponent<Balle>().InfuseColorGreen();
        }

        particleSystem.Play();
        yield return new WaitForSeconds(0.5f);
        particleSystem.startColor = Color.white;
        //particleSystem.GetComponent<Renderer>().material.color = Color.white;

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
