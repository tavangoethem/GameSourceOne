using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Weaponry;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _ammoText;
    private PlayerInteractionAndWeaponPickup _pChar;

    // Start is called before the first frame update
    void Start()
    {
        _pChar = PlayerCharacter.Instance.GetComponent<PlayerInteractionAndWeaponPickup>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_pChar.CurWeapon != null && _pChar.CurWeapon.GetComponent<IReload>() != null)
            _ammoText.text = $"{_pChar.CurWeapon.GetComponent<IReload>().CurAmmo} / {_pChar.CurWeapon.GetComponent<IReload>().MaxAmmo}";
        else
            _ammoText.text = "";
    }
}
