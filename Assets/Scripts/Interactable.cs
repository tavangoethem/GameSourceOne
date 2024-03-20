using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool _isHighlighted = false;

    public TMP_Text displayText;
    public string displayName;

    private void Awake()
    {
        displayText.text = "";
    }

    public void DisplayInteractable()
    {
        if (!_isHighlighted)
        {
            _isHighlighted = true;
            this.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            displayText.text = displayName;
        }
    }

    public void HideInteractable()
    {
        _isHighlighted = false;
        this.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
        displayText.text = "";
    }
}
