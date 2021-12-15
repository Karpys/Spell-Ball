using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balle : MonoBehaviour
{
    //public TrailRenderer trail;
    public int combo;
    public int comboSpeed;

    /*private Rigidbody rb;*/
    private float timer = 0;

    [Header("infuse color")]
    public Material ballColor;
    private Material ballColorCopy;
    public List<GameObject> subParticles;
    public Shader shader;

    public float timeInfuse;


    private Light ballLight;

    public ColorEnum color ;

    private ParticleSystem[] _particleSystem;

    public Color BadColor;
    public bool EnnemyBalle;

    private PlayerController lastPlayerLaunchingIt;

   
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

        if (EnnemyBalle)
        {
            /*ColorBallResetEnnemy();*/
        }
        else
        {
            
            ColorBallReset();
        }

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
        if (combo > 10)
            combo = 10;
        if (combo < 0)
            combo = 0;

        //particule.GetComponent<ParticleSystem>().Play();
        // TrailCombo();
    }

    /*public void TrailCombo()
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

    }*/

    /*void CheckColorInfuse()
    {
        timer += Time.deltaTime;

        if (timer < 1) ballColor.color = ColorInfuse.instance.RB;
        else if (timer < 2) ballColor.color = ColorInfuse.instance.RV;
        else if (timer < 3) ballColor.color = ColorInfuse.instance.RO;
        else if (timer < 4) ballColor.color = ColorInfuse.instance.BV;
        else if (timer < 5) ballColor.color = ColorInfuse.instance.BO;
        else if (timer < 6) ballColor.color = ColorInfuse.instance.VO;
        else if (timer < 7) timer = 0;

    }*/

    public Color InfuseColorRed(Color Infuse)
    {

        /*foreach (ParticleSystem particle in _particleSystem)
        {
            particle.startColor = Infuse;
        }*/

        for (int i = 0; i < _particleSystem.Length; i++)
        {
            _particleSystem[i].startColor = Infuse;
        }

        _particleSystem[0].startColor = Color.white;
        _particleSystem[1].startColor = Color.white;


        ballLight.color = Infuse;
        
        // ballColorCopy.color = Color.red;
        color = ColorEnum.RED;
        
        return Infuse;
    }

    public Color InfuseColorBleu(Color Infuse)
    {
        foreach (ParticleSystem particle in _particleSystem)
        {
            particle.startColor = Infuse;
        }
        
        ballLight.color = Infuse;
        
        // ballColorCopy.color = Color.blue;
        color = ColorEnum.BLEU;
        return Infuse;

    }

    public Color InfuseColorGreen(Color Infuse)
    {
        foreach (ParticleSystem particle in _particleSystem)
        {
            particle.startColor = Infuse;
        }
        
        ballLight.color = Infuse;
        
        // ballColorCopy.color = Color.green;
        color = ColorEnum.GREEN;
        return Infuse;
    }

    public Color InfuseColorOrange(Color Infuse)
    {
        foreach (ParticleSystem particle in _particleSystem)
        {
            particle.startColor = Infuse;
        }
        
        ballLight.color = Infuse;
        // ballColorCopy.color = ColorInfuse.instance.orange;
        color = ColorEnum.ORANGE;
        return Infuse;
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

    public void ColorBallResetEnnemy()
    {
        /*foreach (ParticleSystem particle in _particleSystem)
        {
            particle.main.startColor = BadColor;
        }*/

        for (int i = 0; i < _particleSystem.Length; i++)
        {
            ParticleSystem.MainModule main = _particleSystem[i].main;
            main.startColor = BadColor;
        }

        ballLight.color = Color.white;

        // ballColorCopy.color = Color.white;
        color = ColorEnum.WHITE;
    }

    public void InfuseSysteme()
    {
        StartCoroutine(TimeInfuse());
    }
     public IEnumerator TimeInfuse()
    {
        
        combo = combo + 2;
        yield return new WaitForSeconds(timeInfuse);
        combo -= 2;
        ColorBallReset();
        
    }

     public void SetLastLauncher(PlayerController playerController)
     {
         lastPlayerLaunchingIt = playerController;
     }
}
