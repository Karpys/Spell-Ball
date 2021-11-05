using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MeshSaver
{

    public static void SaveMesh(Mesh mesh, string meshName, string parentObjectname, bool optimizeMesh)
    {
        string path = "Assets/JULIEN/MeshSlicer/Meshes/" + parentObjectname;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        path += "/" + meshName + ".asset";
        
        if (optimizeMesh)
            MeshUtility.Optimize(mesh);

        AssetDatabase.CreateAsset(mesh, path);
        AssetDatabase.SaveAssets();
    }
    
}
