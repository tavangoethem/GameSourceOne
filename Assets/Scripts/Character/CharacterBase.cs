using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IDamagable
{
    [SerializeField] protected Health health;
    public virtual void Awake()
    {
        health.Init();
        health.OnDeath += Die;
    }

    public virtual void TakeDamage(int damageToTake)
    {
        health.TakeDamage(damageToTake);
    }

    public virtual void Die()
    {

    }
}

[Serializable]
public class Health
{
    public int maxHealth = 10;
    public int curHealth;

    public event Action OnDeath;

    public void Init()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage(int damageToTake)
    {
        curHealth -= damageToTake;
        if(curHealth <= 0)
        {
            curHealth = 0;
            OnDeath?.Invoke();
        }
    }
}
