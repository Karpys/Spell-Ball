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

    private bool canGrabBall = false;
    private bool couldGrabBall = true;
    private GameObject balle;

    private Vector2 movementInput;
    private Rigidbody rb;

    private bool isHoldingBall = false;
    private bool balleIsTake = false;

    public GameObject CharacterVisual;

    public Manager_NumbPlayers ManagePlayer;

    public ParticleSystem particleSystem;
    private ParticleManager particule;
    private float _timer;

    void Start()
    {
        CameraFocus.instance.AddTarget(transform);
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

    public void OnGrabBall(InputAction.CallbackContext ctx) => TryGrabBall(ctx.ReadValueAsButton());

    public void OnThrowBall(InputAction.CallbackContext ctx) => TryThrowBall(ctx.ReadValueAsButton());

    /*public void OnLookAround(InputAction.CallbackContext ctx) => RotatePlayer(ctx.ReadValue<Vector2>());*/

    public void OnInfuse(InputAction.CallbackContext ctx) => TryInfuse(ctx.ReadValueAsButton());


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

    public void TryThrowBall(bool buttonPressed)
    {
        if (!buttonPressed) return;
        if (!isHoldingBall) return;


        Transform balleT = grabPosition.Find("Balle");
        
        print(balleT);
        if (balleT == null)
        {
            balleT = grabPosition.Find("Balle(Clone)");
        }
        if(balleT == null)
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

    public void TryInfuse(bool buttonPressed)
    {
        if (!buttonPressed) return;
        if (!isHoldingBall) return;

        Transform balleT = grabPosition.Find("Balle");
        print(balleT);
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

        StartCoroutine("ColorParticule");

        isHoldingBall = false;
        couldGrabBall = false;
        _timer = grabDelay;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6 && !isHoldingBall)
        {
            canGrabBall = true;
            balle = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            canGrabBall = false;
            
            if (!balleIsTake)
            {
                balle.GetComponent<Balle>().combo = 0;
                Rigidbody balleRB = balle.GetComponent<Rigidbody>();
                Vector3 dir = balleRB.velocity.normalized;
                balleRB.velocity = new Vector3(0,0,0);
                balleRB.AddForce(dir * (power + (ComboManager.instance.combo * ComboManager.instance.comboSpeed)), ForceMode.Impulse);
            }


            balleIsTake = false;
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
}
