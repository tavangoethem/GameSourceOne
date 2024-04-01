using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PunchingBagCharacter : CharacterBase
{
    public override void TakeDamage(int damageToTake, Vector3 damagePosition)
    {
        base.TakeDamage(damageToTake, damagePosition);
    }

    public override void Die()
    {
        
    }
}
