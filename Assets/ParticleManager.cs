using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private ParticleSystemRenderer Particle;
    public Material Mat;
    private Material MatCopy;
    public Shader Shader;

    private Color ColorApply;

    void Awake()
    {
        MatCopy = new Material(Shader);
        MatCopy.CopyPropertiesFromMaterial(Mat);
        Particle = GetComponent<ParticleSystemRenderer>();
        Particle.material = MatCopy;
    }

    void Start()
    {
        Particle.material.color = ColorApply;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplyColor(Color ColorToApply)
    {
        ColorApply = ColorToApply;
    }

}
