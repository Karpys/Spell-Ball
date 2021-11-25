#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;
 
namespace TheTide.utils
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(MeshFilter))]
    public class SerializeMesh : MonoBehaviour
    {
        [HideInInspector] [SerializeField] Vector2[] uv;
        [HideInInspector] [SerializeField] Vector3[] verticies;
        [HideInInspector] [SerializeField] int[] triangles;
        [HideInInspector] [SerializeField]bool serialized = false;
        [HideInInspector] [SerializeField] private Material[] materials;
        // Use this for initialization
 
        void Awake()
        {
            if (serialized)
            {
                GetComponent<MeshFilter>().mesh = Rebuild();
                GetComponent<MeshRenderer>().materials = materials;
            }
        }
 
        void Start()
        {
            if (serialized) return;
 
            Serialize();
        }
 
        public void Serialize()
        {
            var mesh = GetComponent<MeshFilter>().mesh;
 
            uv = mesh.uv;
            verticies = mesh.vertices;
            triangles = mesh.triangles;
            materials = GetComponent<MeshRenderer>().materials;
 
            serialized = true;
        }
 
        public Mesh Rebuild()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = verticies;
            mesh.triangles = triangles;
            mesh.uv = uv;
           
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
 
            return mesh;
        }
    }
 
#if UNITY_EDITOR
    [CustomEditor(typeof(SerializeMesh))]
    class SerializeMeshEditor : Editor
    {
        SerializeMesh obj;
 
        void OnSceneGUI()
        {
            obj = (SerializeMesh)target;
        }
 
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
 
            if (GUILayout.Button("Rebuild"))
            {
                if (obj)
                {
                    obj.gameObject.GetComponent<MeshFilter>().mesh = obj.Rebuild();
                }
            }
 
            if (GUILayout.Button("Serialize"))
            {
                if (obj)
                {
                   obj.Serialize();
                }
            }
        }
    }
#endif
}
