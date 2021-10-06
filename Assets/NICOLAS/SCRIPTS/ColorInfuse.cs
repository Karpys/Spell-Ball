using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorInfuse : MonoBehaviour
{
    public Color RB = Color.red + Color.blue;
    public Color RV = Color.red + Color.green;
    public Color RO = Color.red + Color.HSVToRGB(0.1f, 1f, 1f);
    public Color BV = Color.blue + Color.green;
    public Color BO = Color.blue + Color.HSVToRGB(0.1f, 1f, 1f);
    public Color VO = Color.green + Color.HSVToRGB(0.1f, 1f, 1f);
    public Color RBV = Color.HSVToRGB(0, 0, 0.45f);
    public Color RBO = Color.red + Color.blue + Color.HSVToRGB(0.1f, 1f, 1f);
    public Color OBV = Color.blue + Color.green + Color.HSVToRGB(0.1f, 1f, 1f);
    public Color RVO = Color.red + Color.green + Color.HSVToRGB(0.1f, 1f, 1f);
    public Color ALL = Color.white;

    public static ColorInfuse instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
