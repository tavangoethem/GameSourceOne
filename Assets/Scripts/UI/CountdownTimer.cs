using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    //Ignore the name of this script, this is for handling the player's healthbar on the HUD
    [SerializeField] GameObject countdownBar;
    [SerializeField] GameObject player;
    private float barLength;
    [SerializeField] private PlayerCharacter _pChar;
    private void Start()
    {
        //Debug.Log(_pChar.Heath.curHealth);
    }
    private void Update()
    {
        barLength = (float)_pChar.Heath.curHealth / _pChar.Heath.maxHealth;
        countdownBar.transform.localScale = new Vector3(barLength, 1, 1);

        //Debug.Log(barLength);
        if (barLength > 0)
        {

            if (barLength > 0.5)
            {
                countdownBar.GetComponent<Image>().color = Color.green;
            }
            else 
            
            if (barLength > 0.25)
            {
                countdownBar.GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                countdownBar.GetComponent<Image>().color = Color.red;
            }

        }
        else
        {
            barLength = 0;
            countdownBar.transform.localScale = Vector3.zero;
        }
    }
}
