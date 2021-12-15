using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealParticuleManager : MonoBehaviour
{
    public ParticleSystem aura;
    public ParticleSystem glow;
    public ParticleSystem light;
    public bool onPlay;

    Color color;
    public GameObject player;

    float intesiter;
    private void Awake()
    {

        switch(player.GetComponent<PlayerController>().GetPlayerColor())
        {
            case ColorEnum.BLEU:
                color = new Color(0.0627f, 0.1951f, 1);
                break;

            case ColorEnum.GREEN:
                color = new Color(0.0627f, 1, 0.0678f);
                break;

            case ColorEnum.RED:
                color = Color.red;
                break;

            case ColorEnum.ORANGE:
                color = new Color(1, 1, 0);
                break;
        }

        aura.startColor = color;
        glow.startColor = color;
        light.startColor = color;
        gameObject.GetComponent<ParticleSystem>().startColor = color ;

    }

    public void PlayerDead()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        if(!onPlay)
        {
            aura.Pause();
            glow.Pause();
            light.Pause();
        }

    }


    public void StartHeal()
    {
        Debug.Log("is okay");
        onPlay = true;
        aura.Play();
        glow.Play();
        light.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
