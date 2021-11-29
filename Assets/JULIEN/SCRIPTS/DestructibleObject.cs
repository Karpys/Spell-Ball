using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] private GameObject brokenObject;
    [SerializeField] private bool runtimeDestroyer;
    [SerializeField] private int _numberOfCuts = 5;
    [SerializeField] private bool _useGravity = true;
    [SerializeField] private bool _isSolid = true;
    [SerializeField] private bool _shareVertices = false;
    [SerializeField] private bool _smoothVertices = false;
    [SerializeField] private bool _reverseWindTriangles = false;
    [SerializeField] private float _destroyAfterXSeconds;

    public void DestroyObject()
    {
        if (runtimeDestroyer)
        {
            GameObject[] parts = MeshSlicer.Slice(gameObject, _numberOfCuts, useGravity: _useGravity, isSolid: _isSolid, shareVertices: _shareVertices, smoothVertices: _smoothVertices, reverseWindTriangles: _reverseWindTriangles);

            GameObject prefabParent = new GameObject( gameObject.name + "_destroyed");

            foreach (GameObject part in parts)
            {
                part.transform.parent = prefabParent.transform;
            }

            brokenObject = prefabParent;
            Destroy(prefabParent);
        }
        
        GameObject broken = Instantiate(brokenObject, transform.position, transform.rotation);
        Destroy(gameObject);
        Destroy(broken, _destroyAfterXSeconds);
    }
}
