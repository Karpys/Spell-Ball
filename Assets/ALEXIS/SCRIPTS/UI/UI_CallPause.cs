using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class UI_CallPause : MonoBehaviour
{

    public UI_MenuPause pause;
    public float debugRemi = 0.5f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        pause = FindObjectOfType<UI_MenuPause>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
    }
    public void OnStart(InputAction.CallbackContext ctx) => OnPause(ctx.ReadValueAsButton());

    public void OnPause(bool isPressed)
    {

        if (isPressed && timer < 0 && pause.playerDoPause == false)
        {

            timer = debugRemi;
            pause = FindObjectOfType<UI_MenuPause>();
            pause.Pause();
        }
    }
}
