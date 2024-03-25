using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _pChar;
    [SerializeField] private TMPro.TMP_Text _healthText;

    // Update is called once per frame
    void Update()
    {
        _healthText.text = $"{_pChar.Heath.curHealth} / {_pChar.Heath.maxHealth}";
    }
}
