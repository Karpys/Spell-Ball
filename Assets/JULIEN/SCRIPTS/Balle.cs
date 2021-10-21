using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour
{
    public TrailRenderer trail;
    public int combo;
    public int comboSpeed;

    private Rigidbody rb;
    private float timer = 0;

    [Header("infuse color")]
    public Material ballColor;
    private Material ballColorCopy;
    public Shader shader;



    private void Awake()
    {
        ballColorCopy = new Material(shader);
        ballColorCopy.CopyPropertiesFromMaterial(ballColor);
        gameObject.GetComponent<MeshRenderer>().material = ballColorCopy;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // rb.AddForce(new Vector3(Random.Range(-50, 50), 0, Random.Range(-100, 100)), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        TrailCombo();
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
        if (ballColorCopy.color == Color.blue)
            ballColorCopy.color = ColorInfuse.instance.RB;
        else if (ballColorCopy.color == Color.green)
            ballColorCopy.color = ColorInfuse.instance.RV;
        else if (ballColorCopy.color == Color.HSVToRGB(0.1f, 1f, 1f))
            ballColorCopy.color = ColorInfuse.instance.RO;
        else
            ballColorCopy.color = Color.red;
        
    }

    public void InfuseColorBleu()
    {
        if (ballColorCopy.color == Color.red)
            ballColorCopy.color = ColorInfuse.instance.RB;
        else if (ballColorCopy.color == Color.green)
            ballColorCopy.color = ColorInfuse.instance.BV;
        else if (ballColorCopy.color == Color.HSVToRGB(0.1f, 1f, 1f))
            ballColorCopy.color = ColorInfuse.instance.BO;
        else
            ballColorCopy.color = Color.blue;

    }

    public void InfuseColorGreen()
    {
        if (ballColorCopy.color == Color.blue)
            ballColorCopy.color = ColorInfuse.instance.BV;
        else if (ballColorCopy.color == Color.red)
            ballColorCopy.color = ColorInfuse.instance.RV;
        else if (ballColorCopy.color == Color.HSVToRGB(0.1f, 1f, 1f))
            ballColorCopy.color = ColorInfuse.instance.VO;
        else
            ballColorCopy.color = Color.green;

    }

    public void InfuseColorOrange()
    {
        if (ballColorCopy.color == Color.blue)
            ballColorCopy.color = ColorInfuse.instance.BO;
        else if (ballColorCopy.color == Color.green)
            ballColorCopy.color = ColorInfuse.instance.VO;
        else if (ballColorCopy.color == Color.red)
            ballColorCopy.color = ColorInfuse.instance.RO;
        else
            ballColorCopy.color = ColorInfuse.instance.orange;
    }

}
