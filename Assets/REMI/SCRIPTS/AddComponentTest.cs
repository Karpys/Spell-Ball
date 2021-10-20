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
            entity.SetShakeParameters(new Vector3(1, 1, 1));
        }
    }
}
