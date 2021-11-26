using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour
{
    public TrailRenderer trail;
    public int combo;
    public int comboSpeed;

    /*private Rigidbody rb;*/
    private float timer = 0;

    [Header("infuse color")]
    public Material ballColor;
    private Material ballColorCopy;
    public List<GameObject> subParticles;
    public Shader shader;

    private Light ballLight;

    public ColorEnum color ;

    private ParticleSystem[] _particleSystem;
    private void Awake()
    {
        // ballColorCopy = new Material(shader);
        // ballColorCopy.CopyPropertiesFromMaterial(ballColor);

        ballLight = GetComponent<Light>();
        
        _particleSystem = new ParticleSystem[subParticles.Count];
        
        for (int i = 0; i < subParticles.Count; i++)
        {
            _particleSystem[i] = subParticles[i].GetComponent<ParticleSystem>();
        }
        
        ColorBallReset();

        // gameObject.GetComponent<MeshRenderer>().material = ballColorCopy;

        
    }

    // Start is called before the first frame update
    void Start()
    {
        /*rb = GetComponent<Rigidbody>();*/
        // rb.AddForce(new Vector3(Random.Range(-50, 50), 0, Random.Range(-100, 100)), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //particule.GetComponent<ParticleSystem>().Play();
        // TrailCombo();
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

    public Color InfuseColorRed()
    {

        foreach (ParticleSystem particle in _particleSystem)
        {
            particle.startColor = Color.red;
        }

        ballLight.color = Color.red;
        
        // ballColorCopy.color = Color.red;
        color = ColorEnum.RED;
        
        return Color.red;
    }

    public Color InfuseColorBleu()
    {
        foreach (ParticleSystem particle in _particleSystem)
        {
            particle.startColor = Color.blue;
        }
        
        ballLight.color = Color.blue;
        
        // ballColorCopy.color = Color.blue;
        color = ColorEnum.BLEU;
        return Color.blue;

    }

    public Color InfuseColorGreen()
    {
        foreach (ParticleSystem particle in _particleSystem)
        {
            particle.startColor = Color.green;
        }
        
        ballLight.color = Color.green;
        
        // ballColorCopy.color = Color.green;
        color = ColorEnum.GREEN;
        return Color.green;
    }

    public Color InfuseColorOrange()
    {
        foreach (ParticleSystem particle in _particleSystem)
        {
            particle.startColor = ColorInfuse.instance.orange;
        }
        
        ballLight.color = ColorInfuse.instance.orange;
        
        // ballColorCopy.color = ColorInfuse.instance.orange;
        color = ColorEnum.ORANGE;
        return ColorInfuse.instance.orange;
    }

    public void ColorBallReset()
    {
        foreach (ParticleSystem particle in _particleSystem)
        {
            particle.startColor = Color.white;
        }
        
        ballLight.color = Color.white;
        
        // ballColorCopy.color = Color.white;
        color = ColorEnum.WHITE;
    }

}
