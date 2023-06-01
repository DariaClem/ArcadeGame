using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureLevels : MonoBehaviour
{
    void Start()
    {
        // Se configureaza nivelul, scorul si punctajul default pentru un utilizator care se joaca pentru prima data
        if (!PlayerPrefs.HasKey("currentLevel"))
        {
            PlayerPrefs.SetInt("currentLevel", 0);
            PlayerPrefs.SetInt("currentScore", 0);
            PlayerPrefs.SetString("currentBackground", "ShadowCanyon");
        }
    }
}
