using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnDamage : MonoBehaviour
{
    [SerializeField] private GameObject _damageParticle;

    private CharacterBase _character;

    private void Start()
    {
        _character = GetComponent<CharacterBase>();
        _character.Heath.OnDamage += OnDamage;
    }

    private void OnDamage(Vector3 pos)
    {
        Instantiate(_damageParticle, pos, Quaternion.identity);
    }
}
