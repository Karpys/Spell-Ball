using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour
{
    [Header("combo")]
    public int combo;
    public float comboSpeed;
    public TrailRenderer trail;

    private Rigidbody rb;
    private float timer = 0;

    [Header("infuse color")]
    public Material ballColor;


    public static Balle instance = null;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        ballColor.color = Color.white;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        combo = 0;
        // rb.AddForce(new Vector3(Random.Range(-50, 50), 0, Random.Range(-100, 100)), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //CheckColorInfuse();
        TrailCombo();
        //combo = comboSet;
    }

    public void TrailCombo()
    {
        switch(combo)
        {
            case 0:
            case 1:
                trail.startColor = Color.white;
                trail.endColor = Color.white;
                break;

            case 2:
                trail.startColor = Color.yellow;
                trail.endColor = Color.yellow;
                break;

            case 3:
                trail.startColor = Color.HSVToRGB(0.1f, 1f, 1f);
                trail.endColor = Color.HSVToRGB(0.1f, 1f, 1f);
                break;

            case 4:
                trail.startColor = Color.red;
                trail.endColor = Color.red;
                break;
        }
    }

    void CheckColorInfuse()
    {
        timer += Time.deltaTime;

        if (timer < 1) ballColor.color = ColorInfuse.instance.RB;
        else if (timer < 2) ballColor.color = ColorInfuse.instance.RV;
        else if (timer < 3) ballColor.color = ColorInfuse.instance.RO;
        else if (timer < 4) ballColor.color = ColorInfuse.instance.BV;
        else if (timer < 5) ballColor.color = ColorInfuse.instance.BO;
        else if (timer < 6) ballColor.color = ColorInfuse.instance.VO;
        else if (timer < 7) timer = 0;

    }

    public void InfuseColorRed()
    {
        if (ballColor.color == Color.blue)
            ballColor.color = ColorInfuse.instance.RB;
        else if (ballColor.color == Color.green)
            ballColor.color = ColorInfuse.instance.RV;
        else if (ballColor.color == Color.HSVToRGB(0.1f, 1f, 1f))
            ballColor.color = ColorInfuse.instance.RO;
        else
            ballColor.color = Color.red;

    }

    public void InfuseColorBleu()
    {
        if (ballColor.color == Color.red)
            ballColor.color = ColorInfuse.instance.RB;
        else if (ballColor.color == Color.green)
            ballColor.color = ColorInfuse.instance.BV;
        else if (ballColor.color == Color.HSVToRGB(0.1f, 1f, 1f))
            ballColor.color = ColorInfuse.instance.BO;

    }

    public void InfuseColorGreen()
    {
        if (ballColor.color == Color.blue)
            ballColor.color = ColorInfuse.instance.BV;
        else if (ballColor.color == Color.red)
            ballColor.color = ColorInfuse.instance.RV;
        else if (ballColor.color == Color.HSVToRGB(0.1f, 1f, 1f))
            ballColor.color = ColorInfuse.instance.VO;

    }

    public void InfuseColorOrange()
    {
        if (ballColor.color == Color.blue)
            ballColor.color = ColorInfuse.instance.BO;
        else if (ballColor.color == Color.green)
            ballColor.color = ColorInfuse.instance.VO;
        else if (ballColor.color == Color.red)
            ballColor.color = ColorInfuse.instance.RO;
    }

}
