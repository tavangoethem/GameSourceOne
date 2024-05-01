using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPrefsLevel : MonoBehaviour
{
    [SerializeField] private Button _levelButton;

    [SerializeField] private GameObject _x;

    [SerializeField] private int _levelIndex;

    private const string PROGRESSPREFNAME = "PlayerProgres";

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt(PROGRESSPREFNAME, 0) < _levelIndex)
        {
            GetComponent<Button>().enabled = false;
            _x.SetActive(true);
        }
        else
        {
            GetComponent<Button>().enabled = true;
            _x.SetActive(false);
        }
    }
}
