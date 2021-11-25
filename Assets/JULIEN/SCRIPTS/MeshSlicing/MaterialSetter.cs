using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MaterialSetter : MonoBehaviour
{
    [SerializeField] private Material[] _materials;

    private void Awake()
    {
        SetMaterialOfChildrens();
    }

    private void Start()
    {
        SetMaterialOfChildrens();
    }

    public void SetMaterialOfChildrens()
    {
        for (int i = 0 ; i < transform.childCount; i++)
        {
            #if UNITY_EDITOR
            transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().sharedMaterials = _materials;
            #else
            transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().materials = _materials;
            #endif
        }
    }

    public void SetMaterials(Material[] materials)
    {
        _materials = materials;
    }
}


#if UNITY_EDITOR
    [CustomEditor(typeof(MaterialSetter))]
    class MaterialSetterEditor : Editor
    {
        MaterialSetter obj;
 
        void OnSceneGUI()
        {
            obj = target as MaterialSetter;
        }
 
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
 
            if (GUILayout.Button("Set All Childrens Materials"))
            {
                if (obj)
                {
                    obj.SetMaterialOfChildrens();
                }
            }
        }
    }
#endif

