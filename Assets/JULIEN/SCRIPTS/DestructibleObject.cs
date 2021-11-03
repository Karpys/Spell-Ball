using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] private GameObject brokenObject;
    [SerializeField] private bool runtimeDestroyer;

    public void DestroyObject()
    {
        if (runtimeDestroyer)
        {
            MeshDestroyer meshDestroyer = new MeshDestroyer();
            List<GameObject> list = meshDestroyer.DestroyMesh(gameObject, 10);

            GameObject prefabParent = new GameObject( gameObject.name + "_destroyed");

            foreach (GameObject part in list)
            {
                part.transform.parent = prefabParent.transform;
            }

            brokenObject = prefabParent;
            Destroy(prefabParent);
        }
        
        Instantiate(brokenObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
