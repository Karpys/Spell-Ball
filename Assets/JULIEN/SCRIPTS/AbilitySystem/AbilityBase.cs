using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbilityBase : MonoBehaviour
{
    public class AbilityUseEvent : UnityEvent<float> { }
    public AbilityUseEvent OnAbilityUse = new AbilityUseEvent();

    [Header("Ability Info")]
    [SerializeField] private float cooldown = 1f;

    [SerializeField] protected float duration = 0f;

    private bool canUse = true;

    private float durationLeft;
    
    private void Update()
    {
        if (durationLeft > 0)
        {
            durationLeft -= Time.unscaledDeltaTime;
            if (durationLeft <= 0)
            {
                EndAbility();
                StartCooldown();
            }
        }
    }

    public void TriggerAbility()
    {
        if (canUse)
        {
            OnAbilityUse.Invoke(cooldown);
            Ability();
            canUse = false;
            durationLeft = duration;
        }
    }

    public abstract void Ability();

    void StartCooldown()
    {
        StartCoroutine(Cooldown());
        IEnumerator Cooldown()
        {
            yield return new WaitForSecondsRealtime(cooldown);
            canUse = true;
        }
    }

    public virtual void EndAbility()
    {
    }

}
