using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureLevels : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("currentLevel"))
        {
            PlayerPrefs.SetInt("currentLevel", 0);
            PlayerPrefs.SetInt("currentScore", 0);
            PlayerPrefs.SetString("currentBackground", "ShadowCanyon");
        }
    }
}
