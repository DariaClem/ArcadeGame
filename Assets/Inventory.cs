using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public TMP_Text range;
    public TMP_Text level;
    public GameObject selectButtons;
    public GameObject whispersongMeadowsAvailable;
    public GameObject flameMountainsAvailable;
    public GameObject sunfireSandsAvailable;
    public GameObject crystalWastelandAvailable;
    public GameObject goldenPlainsAvailable;
    public GameObject echoLakeAvailable;
    public GameObject whispersongMeadows;
    public GameObject flameMountains;
    public GameObject sunfireSands;
    public GameObject crystalWasteland;
    public GameObject goldenPlains;
    public GameObject echoLake;

    // Start is called before the first frame update
    void Start()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel")+1;
        int requiredXp = ProgressBar.CalculateRequiredXp(currentLevel-1);
        int currentScore = PlayerPrefs.GetInt("currentScore");

        range.text = currentScore + "/" + requiredXp;
        level.text = (currentLevel).ToString();

        int count = 0;
        foreach (Transform t in selectButtons.transform) {
            if (t.parent == selectButtons.transform)
            {
                count++;
                if (count == 2 && currentLevel >= 5)
                {
                    t.gameObject.SetActive(true);
                    whispersongMeadowsAvailable.gameObject.SetActive(true);
                    whispersongMeadows.gameObject.SetActive(false);
                }
                if (count == 3 && currentLevel >= 13)
                {
                    t.gameObject.SetActive(true);
                    flameMountainsAvailable.gameObject.SetActive(true);
                    flameMountains.gameObject.SetActive(false);
                }
                if (count == 4 && currentLevel >= 24)
                {
                    t.gameObject.SetActive(true);
                    crystalWastelandAvailable.gameObject.SetActive(true);
                    crystalWasteland.gameObject.SetActive(false);
                }
                if (count == 5 && currentLevel >= 38)
                {
                    t.gameObject.SetActive(true);
                    echoLakeAvailable.gameObject.SetActive(true);
                    echoLake.gameObject.SetActive(false);
                }
                if (count == 6 && currentLevel >= 55)
                {
                    t.gameObject.SetActive(true);
                    goldenPlainsAvailable.gameObject.SetActive(true);
                    goldenPlains.gameObject.SetActive(false);
                }
                if (count == 7 && currentLevel >= 75)
                {
                    t.gameObject.SetActive(true);
                    sunfireSandsAvailable.gameObject.SetActive(true);
                    sunfireSands.gameObject.SetActive(false);
                }
            }
        }
    }

}

