using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TheTide.utils;
using UnityEngine;
using Plane = UnityEngine.Plane;

public class MeshSlicer : MonoBehaviour
{
    public static GameObject[] Slice(GameObject objectToCut, int numberOfCuts = 5, bool isSolid = true,
        bool reverseWindTriangles = false, bool useGravity = false, bool shareVertices = false,
        bool smoothVertices = false)
    {
        List<GameObject> slicedGameObjects = new List<GameObject>();

        for (int i = 0; i < numberOfCuts; i++)
        {
            GameObject obj;
            if (slicedGameObjects.Count < 1)
            {
                obj = objectToCut;
            }
            else
            {
                obj = slicedGameObjects[i - 1];
            }

            GameObject[] gameObjects = Slice(MeshSlicerUtilities.GetRandomPlaneOnGameObject(obj), obj, isSolid, reverseWindTriangles, useGravity, shareVertices, smoothVertices);

            slicedGameObjects.Add(gameObjects[0]);
            slicedGameObjects.Add(gameObjects[1]);
        }

        for (int i = 0; i < numberOfCuts - 1; i++)
        {
            DestroyImmediate(slicedGameObjects[i]);
        }
        slicedGameObjects.RemoveRange(0, numberOfCuts - 1);

        for (int i = slicedGameObjects.Count-1; i >= 0; i--)
        {
            GameObject slicedPart = slicedGameObjects[i];
            SetupCollidersAndRigidBodys(ref slicedPart, slicedPart.GetComponent<MeshFilter>().mesh, useGravity);
        }
        
        return slicedGameObjects.ToArray();
    }

    public static GameObject[] Slice(Plane plane, GameObject objectToCut, bool isSolid = true,
        bool reverseWindTriangles = false, bool useGravity = false, bool shareVertices = false,
        bool smoothVertices = false)
    {
        #if  UNITY_EDITOR
        Mesh mesh = objectToCut.GetComponent<MeshFilter>().sharedMesh;
        #else
        Mesh mesh = objectToCut.GetComponent<MeshFilter>().mesh;
        #endif
        var a = mesh.GetSubMesh(0);

        //Create left and right slice of hollow object
        MeshSlicerMetadata slicesMeta =
            new MeshSlicerMetadata(plane, mesh, isSolid, reverseWindTriangles, useGravity, shareVertices);

        GameObject positiveObject = CreateMeshGameObject(objectToCut);
        positiveObject.name = string.Format("{0}_positive", objectToCut.name);

        GameObject negativeObject = CreateMeshGameObject(objectToCut);
        negativeObject.name = string.Format("{0}_negative", objectToCut.name);

        var positiveSideMeshData = slicesMeta.PositiveSideMesh;
        var negativeSideMeshData = slicesMeta.NegativeSideMesh;
        
        positiveObject.GetComponent<MeshFilter>().mesh = positiveSideMeshData;
        negativeObject.GetComponent<MeshFilter>().mesh = negativeSideMeshData;

        return new [] { positiveObject, negativeObject };
    }

    private static GameObject CreateMeshGameObject(GameObject originalObject)
    {
        #if UNITY_EDITOR
        var originalMaterial = originalObject.GetComponent<MeshRenderer>().sharedMaterials;
        #else
        var originalMaterial = originalObject.GetComponent<MeshRenderer>().materials;
        #endif
        
        GameObject meshGameObject = new GameObject();

        meshGameObject.AddComponent<MeshFilter>();
        meshGameObject.AddComponent<MeshRenderer>();

#if UNITY_EDITOR
        meshGameObject.GetComponent<MeshRenderer>().sharedMaterials = originalMaterial;
#else
        meshGameObject.GetComponent<MeshRenderer>().materials = originalMaterial;
#endif

        meshGameObject.tag = originalObject.tag;

        meshGameObject.transform.localScale = originalObject.transform.localScale;
        
        return meshGameObject;
    }

    private static void SetupCollidersAndRigidBodys(ref GameObject gameObject, Mesh mesh, bool useGravity)
    {
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        meshCollider.convex = true;

        var rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = useGravity;
    }
}