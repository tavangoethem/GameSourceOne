using UnityEngine;
using UnityEngine.UI;

public class OptionPrefs : MonoBehaviour
{
    [SerializeField] private Slider _optionsSlider;

    [SerializeField] private TMPro.TMP_Text _optionsText;

    [SerializeField] private string _prefsName;

    public void SliderUpdatePrefs()
    {
        PlayerPrefs.SetFloat(_prefsName, _optionsSlider.value);
    }

    public void Start()
    {
        if(PlayerPrefs.HasKey(_prefsName) == false)
        {
            PlayerPrefs.SetFloat(_prefsName, _optionsSlider.maxValue / 2f);
        }

        _optionsSlider.value = PlayerPrefs.GetFloat(_prefsName);
    }

    public void Update()
    {
        _optionsText.text = $"{PlayerPrefs.GetFloat(_prefsName):N1}";
    }
}
