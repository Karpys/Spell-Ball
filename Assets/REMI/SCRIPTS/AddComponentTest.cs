using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AddComponentTest : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Entity;
    public InputAction Input;
    void Start()
    {
        Input.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.triggered)
        {
            ShakerEntity entity = Entity.AddComponent<ShakerEntity>();
            entity.SetShakeParameters(0.25f, 10f, 15f, new Vector3(0, 1, 0));
        }
    }
}
