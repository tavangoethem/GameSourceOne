using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLevelPref : MonoBehaviour
{
    [SerializeField] private int _intToSetTo;

    private const string PROGRESSPREFNAME = "PlayerProgres";

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerCharacter>() != null)
        {
            if(PlayerPrefs.GetFloat(PROGRESSPREFNAME, 0) <= _intToSetTo)
                PlayerPrefs.SetInt(PROGRESSPREFNAME, _intToSetTo);
        }
    }
}
