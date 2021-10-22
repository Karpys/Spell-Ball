using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerParticulShoot : MonoBehaviour
{
    [Header("particule Systeme")]
    public GameObject particule;
    public Material particuleColor;
    public Shader shaderParticule;
    private Material particuleColorCopy;

    private void Awake()
    {
        particuleColorCopy = new Material(shaderParticule);
        particuleColorCopy.CopyPropertiesFromMaterial(particuleColor);
        particule.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = particuleColorCopy;
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
