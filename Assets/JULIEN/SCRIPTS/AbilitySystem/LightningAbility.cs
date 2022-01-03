using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAbility : AbilityBase
{
    [SerializeField] private ParticleSystem _particleSystem;

    public override void Ability()
    {
        _particleSystem.Play();
    }

    public override void EndAbility()
    {
        _particleSystem.Stop();
    }
}
