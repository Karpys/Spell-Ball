using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] private GameObject brokenObject;

    public void DestroyObject()
    {
        Instantiate(brokenObject, transform.position, transform.rotation);
        Destroy(gameObject);

    }
}
