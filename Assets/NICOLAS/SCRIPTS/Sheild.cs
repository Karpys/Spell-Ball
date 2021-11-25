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

    [Header("shader")]
    public Shader disolve;

    private bool alphaSet = false;
    private Color tempShield;
    private float time;
    private bool startTime;
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
        if (lastSield && !alphaSet)
        {
            //Debug.Log("a marche");
            copieMaterialSheild.color = new Color(copieMaterialSheild.color.r, copieMaterialSheild.color.g, copieMaterialSheild.color.b, 0.196f);
            tempShield = copieMaterialSheild.color;
            //Debug.Log(tempShield);
            /*disolve.GetPropertyDefaultVectorValue(disolve.FindPropertyIndex("Color")).Set(copieMaterialSheild.color.r, copieMaterialSheild.color.g, copieMaterialSheild.color.b, 0.196f);
            Debug.Log(disolve.GetPropertyDefaultVectorValue(disolve.FindPropertyIndex("Color"))+ " a marche pas ");*/
            alphaSet = true;
        }
        else if (!alphaSet)
        {
            copieMaterialSheild.color = new Color(copieMaterialSheild.color.r, copieMaterialSheild.color.g, copieMaterialSheild.color.b, 0);
        }

        if(startTime && time > -1)
        {
            time -= Time.deltaTime;
            copieMaterialSheild.SetFloat("_time", time);
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

    public void ChangeShader()
    {
        //Debug.Log(disolve.FindPropertyIndex("Color"));
        //disolve.GetPropertyDefaultVectorValue((disolve.FindPropertyIndex("Color") * -1) - 1).Equals(copieMaterialSheild.color);// .Set(copieMaterialSheild.color.r, copieMaterialSheild.color.g, copieMaterialSheild.color.b, 0.196f);
        
        Color temp = copieMaterialSheild.color;
        copieMaterialSheild.shader = disolve;
        copieMaterialSheild.SetColor("_Color", tempShield);
        Debug.Log(copieMaterialSheild.GetColor("_Color"));
        startTime = true;
        time = 1;
        //Debug.Log(disolve.GetPropertyDefaultVectorValue((disolve.FindPropertyIndex("Color") * -1) - 1));

        //float time = 1;


    }
}
