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
    public Color VO = Color.green + Color.HSVToRGB(0.15f, 1f, 1f);


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
