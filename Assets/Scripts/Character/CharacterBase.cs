using System;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour, IDamagable, IHealable
{
    [SerializeField] protected Health health;
    public Health Heath { get { return health; } }

    public ArmorType ArmorType { get { return _armorType; } }
    [SerializeField] private ArmorType _armorType;

    public AudioClip TakeDamageSound;
    public AudioClip DieSound;

    public virtual void Awake()
    {
        health.Init();
        health.OnDeath += Die;
    }

    public virtual void TakeDamage(float damageToTake, Vector3 damagePosition, ArmorType levelOfPierce)
    {
        AudioManager.instance.PlaySFX(TakeDamageSound, this.transform, 1);
        if ((int)levelOfPierce > (int)_armorType)
            health.TakeDamage((damageToTake), damagePosition);
        else
            health.TakeDamage((damageToTake) / ((int)levelOfPierce / (int)_armorType), damagePosition);

    }

    public virtual void Die()
    {
        AudioManager.instance.PlaySFX(TakeDamageSound, this.transform.position, 1);
    }

    public void HealDamage(float valueToHeal)
    {
        health.HealDamage(valueToHeal);
    }
}

[Serializable]
public class Health
{
    public float maxHealth = 10;
    public float curHealth;

    public event Action<Vector3> OnDamage;
    public event Action OnDeath;

    public void Init()
    {
        curHealth = maxHealth;
    }

    public void HealDamage(float valueToHeal)
    {
        curHealth += valueToHeal;
        if(curHealth > maxHealth)
            curHealth = maxHealth;
    }

    public void TakeDamage(float damageToTake, Vector3 position)
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
