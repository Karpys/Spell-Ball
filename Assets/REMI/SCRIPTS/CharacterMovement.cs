using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float _speed;
    public CharacterController _controller;
    public GameObject CharacterVisual;
    private Vector2 MovementInput;
    [SerializeField]
    private float rotationspeed;

    public void OnMove(InputAction.CallbackContext ctx) => MovementInput = ctx.ReadValue<Vector2>();

    public void OnLookAround(InputAction.CallbackContext ctx) => RotatePlayer(ctx.ReadValue<Vector2>());

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        float Axex = MovementInput.x;
        float Axey = MovementInput.y;

        Vector3 Axes = transform.right * Axex + transform.forward * Axey;
        Vector3 Move = Axes * _speed * Time.deltaTime;
        _controller.Move(Move);
    }

    void RotatePlayer(Vector2 lookDirection)
    {
        if (lookDirection == Vector2.zero)
            return;

        CharacterVisual.transform.rotation = Quaternion.Slerp(transform.rotation, 
            Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.y).normalized), rotationspeed);
    }
}
