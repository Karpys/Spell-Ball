using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild : MonoBehaviour
{
    [Header("Sheild Option")]
    public ColorEnum color;
    public bool lastSield;

    [Header("Color")]
    public Material materialSheild;
    public Shader shaderSheild;
    private Material copieMaterialSheild;

    private void Awake()
    {
        copieMaterialSheild = new Material(shaderSheild);
        copieMaterialSheild.CopyPropertiesFromMaterial(materialSheild);
        gameObject.GetComponent<MeshRenderer>().material = copieMaterialSheild;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (lastSield)
        {
            copieMaterialSheild.color = new Color(copieMaterialSheild.color.r, copieMaterialSheild.color.g, copieMaterialSheild.color.b, 0.196f);
        }
        else
        {
            copieMaterialSheild.color = new Color(copieMaterialSheild.color.r, copieMaterialSheild.color.g, copieMaterialSheild.color.b, 0);
        }
    }

    public void SetColor(ColorEnum colorB)
    {
        color = colorB;

        switch (color)
        {
            case ColorEnum.BLEU:
                copieMaterialSheild.color = new Color(0, 0, 1, 0);
                break;

            case ColorEnum.RED:
                copieMaterialSheild.color = new Color(1, 0, 0, 0);
                break;

            case ColorEnum.GREEN:
                copieMaterialSheild.color = new Color(0, 1, 0, 0);
                break;

            case ColorEnum.ORANGE:
                copieMaterialSheild.color = new Color(1, 0.6f, 0, 0);
                break;
        }
    }
}
