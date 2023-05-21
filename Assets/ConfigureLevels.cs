using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureLevels : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Level1", 100);
        PlayerPrefs.SetInt("Level2", 250);
        PlayerPrefs.SetInt("Level3", 450);
        PlayerPrefs.SetInt("Level4", 700);
        PlayerPrefs.SetInt("Level5", 1000);
        PlayerPrefs.SetInt("Level6", 1350);
        PlayerPrefs.SetInt("Level7", 1750);
    }
}
