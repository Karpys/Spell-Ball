using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterValues Stats;
    public RollManager Roll_Manager;
    public CharacterController _controller;
    public GameObject CharacterVisual;
    private Vector2 MovementInput;
    [HideInInspector]public Vector2 MovementDir;
    public Animator Anim;
    

    public void OnMove(InputAction.CallbackContext ctx) => MovementInput = ctx.ReadValue<Vector2>();

    public void OnLookAround(InputAction.CallbackContext ctx) => RotatePlayer(ctx.ReadValue<Vector2>());

    public void OnRoll(InputAction.CallbackContext ctx) => TryRoll(ctx.ReadValueAsButton());

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        RollController();
        MovementDir = MovementInput;
    }

    public void Move()
    {
        float Axex = MovementInput.x;
        float Axey = MovementInput.y;

        Vector3 Axes = transform.right * Axex + transform.forward * Axey;
        Vector3 Move = Axes ;
        
        if(MovementDir == Vector2.zero)
        {
            Anim.SetBool("Running", false);
        }else
        {
            Anim.SetBool("Running", true);
        }

        if(Stats.CanMove)
        {
            if (Move != Vector3.zero)
            {
                Stats.LastMove = Move;
            }
            Move = Move * Stats.Speed * Time.deltaTime;
            _controller.Move(Move);
        }
        else if(Roll_Manager.IsRolling)
        {
            Move = Stats.LastMove * Roll_Manager.RollSpeed * Time.deltaTime;
            _controller.Move(Move);
        }

        
    }

    public void RollController()
    {
        if (Roll_Manager.RollCd >= 0 && !Roll_Manager.IsRolling)
        {
            Roll_Manager.RollCd -= Time.deltaTime;
        }

        if(Roll_Manager.RollDuration >= 0)
        {
            Roll_Manager.RollDuration -= Time.deltaTime;
        }else if (!Roll_Manager.HasReset)//FIN DE LA ROULADE
        {
            
            Roll_Manager.HasReset = true;
            Stats.CanMove = true;
        }
    }

    public void TryRoll(bool Roll)
    {
        if(Roll && Roll_Manager.CanRoll)
        {
            Anim.Play("Roll");
            RotatePlayerMovement(MovementDir);
            Roll_Manager.RollCd = Roll_Manager.RollCdSet;
            Roll_Manager.RollDuration = Roll_Manager.RollDurationSet;
            Stats.CanMove = false;
            Roll_Manager.HasReset = false;
        }
    }

    public void RotatePlayerMovement(Vector2 MoveDirection)
    {
        if (Stats.LastMove == Vector3.zero)
            return;

        CharacterVisual.transform.rotation = Quaternion.Slerp(transform.rotation,
            Quaternion.LookRotation(new Vector3(Stats.LastMove.x, 0, Stats.LastMove.z).normalized), Stats.rotationspeed);
    }

    void RotatePlayer(Vector2 lookDirection)
    {
        if(!Roll_Manager.IsRolling)
        {
            if (lookDirection == Vector2.zero)
                return;

            CharacterVisual.transform.rotation = Quaternion.Slerp(transform.rotation, 
                Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.y).normalized), Stats.rotationspeed);
        }
    }

    [System.Serializable]
    public struct CharacterValues
    {
        public float Speed;
        public float rotationspeed;
        public bool CanMove;
        [HideInInspector] public Vector3 LastMove;
    }
    [System.Serializable]
    public struct RollManager
    {
        public bool IsRolling => RollDuration > 0;
        [HideInInspector]
        public float RollCd;
        public float RollCdSet;
        [HideInInspector]
        public float RollDuration;
        public float RollDurationSet;
        public float RollSpeed;
        [HideInInspector]
        public bool HasReset;
        public bool CanRoll  => RollCd < 0;
    }
}
