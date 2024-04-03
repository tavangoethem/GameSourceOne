using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRifleEnemyCharacter : CharacterBase
{
    public override void Die()
    {
        Destroy(gameObject);
    }
}
