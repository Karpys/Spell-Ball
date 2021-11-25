using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSlicerUtilities
{

    public static Plane GetRandomPlaneOnGameObject(GameObject gameObject)
    {
        #if UNITY_EDITOR
        Bounds bounds = gameObject.GetComponent<MeshFilter>().sharedMesh.bounds;
        #else
        Bounds bounds = gameObject.GetComponent<MeshFilter>().mesh.bounds;
        #endif
        Vector3 point1 = RandomPointInBounds(bounds);
        Vector3 point2 = RandomPointInBounds(bounds);
        Vector3 point3 = RandomPointInBounds(bounds);

        Plane plane = new Plane();
        plane.Set3Points(point1, point2, point3);
        return plane;
    }
    
    public static Vector3 RandomPointInBounds(Bounds bounds) {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
    
}
