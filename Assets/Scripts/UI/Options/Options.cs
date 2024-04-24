using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class Option
{ 
    public static void SetPrefsFloat(string prefsName, float prefsValue)
    {
        PlayerPrefs.SetFloat(prefsName, prefsValue);
    }
    public static void SetPrefsInt(string prefsName, int prefsValue)
    {
        PlayerPrefs.SetInt(prefsName, prefsValue);
    }
    public static void SetPrefsString(string prefsName, string prefsValue)
    {
        PlayerPrefs.SetString(prefsName, prefsValue);
    }



    public static float GetPrefsFloat(string prefsName)
    {
        return PlayerPrefs.GetFloat(prefsName);
    }

    public static int GetPrefsInt(string prefsName)
    {
        return PlayerPrefs.GetInt(prefsName);
    }

    public static string GetPrefsString(string prefsName)
    {
        return PlayerPrefs.GetString(prefsName);
    }
}
