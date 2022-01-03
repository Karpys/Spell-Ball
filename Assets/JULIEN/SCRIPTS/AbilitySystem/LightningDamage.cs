using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDamage : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.TryGetComponent(out Manager_Life otherManagerLife))
        {
            otherManagerLife.DamageHealth(1);
        }
    }
}
