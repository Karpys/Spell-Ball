using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public int combo = 0;
    public int comboSpeed;
    public Text comboText;

    private TrailRenderer trail;

    public static ComboManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //combo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ComboTextUpdate();
    }

    void ComboTextUpdate()
    {
        if (combo == 0)
            comboText.text = "";
        else
            comboText.text = combo.ToString();
    
        if (combo <= 10)
            comboText.fontSize = 40 + combo * 5;

        comboText.color = GameObject.Find("Trail").GetComponent<TrailRenderer>().endColor;
    }
}
