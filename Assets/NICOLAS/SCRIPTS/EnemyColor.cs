using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColor : MonoBehaviour
{
    public Material material;
    private Material copyMaterial;
    public Shader shader;

    public bool random = false;
    private bool switchColor = true;

    private void Awake()
    {
        copyMaterial = new Material(shader);
        copyMaterial.CopyPropertiesFromMaterial(material);
        gameObject.GetComponent<MeshRenderer>().material = copyMaterial;
    }

    // Start is called before the first frame update
    void Start()
    {

        
    }

    public Material GetColorCopy()
    {
        return copyMaterial;
    }
    // Update is called once per frame
    void Update()
    {
        if (random && switchColor)
        {
            int i = Random.Range(0, 5);
            switch (i)
            {
                case 0:
                    copyMaterial.color = Color.white;
                    break;

                case 1:
                    copyMaterial.color = Color.red;
                    break;

                case 2:
                    copyMaterial.color = Color.blue;
                    break;

                case 3:
                    copyMaterial.color = Color.green;
                    break;

                case 4:
                    copyMaterial.color = ColorInfuse.instance.orange;
                    break;

            }
            switchColor = false;
        }
    }
}
