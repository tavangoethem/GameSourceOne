using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IDamagable, IHealable
{
    [SerializeField] protected Health health;
    public Health Heath { get { return health; } }
    public virtual void Awake()
    {
        health.Init();
        health.OnDeath += Die;
    }

    public virtual void TakeDamage(int damageToTake, Vector3 damagePosition)
    {
        health.TakeDamage(damageToTake, damagePosition);
    }

    public virtual void Die()
    {

    }

    public void HealDamage(int valueToHeal)
    {
        health.HealDamage(valueToHeal);
    }
}

[Serializable]
public class Health
{
    public int maxHealth = 10;
    public int curHealth;

    public event Action<Vector3> OnDamage;
    public event Action OnDeath;

    public void Init()
    {
        curHealth = maxHealth;
    }

    public void HealDamage(int valueToHeal)
    {
        curHealth += valueToHeal;
        if(curHealth > maxHealth)
            curHealth = maxHealth;
    }

    public void TakeDamage(int damageToTake, Vector3 position)
    {
        curHealth -= damageToTake;
        OnDamage?.Invoke(position);
        if(curHealth <= 0)
        {
            curHealth = 0;
            OnDeath?.Invoke();
        }
    }
}
