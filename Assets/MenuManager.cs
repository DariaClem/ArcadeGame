using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public TMP_Text pointsText;
    
    public GameObject pauseMenuUI;
    public PenguinScript penguinScript;

    private void Start()
    {
        penguinScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinScript>();
    }
    
    // Functie de repornire a jocului
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        penguinScript.enabled = true;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // Functie de oprire a jocului
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        penguinScript.enabled = false;
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    // Functia afiseaza scorul obtinut intr-o sesiune de joc
    public void Score(int score)
    {
        pointsText.text = score.ToString() + " POINTS";
    }
}
