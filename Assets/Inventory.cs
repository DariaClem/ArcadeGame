using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public TMP_Text range;
    public TMP_Text level;

    // Start is called before the first frame update
    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel");
        int requiredXp = ProgressBar.CalculateRequiredXp(currentLevel);
        int currentScore = PlayerPrefs.GetInt("currentScore");

        range.text = currentScore + "/" + requiredXp;
        level.text = (currentLevel + 1).ToString();

        string lastAnimation = PlayerPrefs.GetString("lastAnimation");
        if (lastAnimation == "ShadowCanyon")
        {
            
        }
    }
}

