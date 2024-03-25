using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PunchingBagCharacter : CharacterBase
{
    public OnDamageEvent OnDamageEvent;

    public override void TakeDamage(int damageToTake)
    {
        base.TakeDamage(damageToTake);
        OnDamageEvent?.Invoke();
    }

    public override void Die()
    {
        
    }
}

[Serializable]
public class OnDamageEvent : UnityEvent { }