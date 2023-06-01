using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirstGameManagerMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public TMP_Text pointsText;
    
    public GameObject pauseMenuUI;
    public GameManager gameManagerScript;

    private void Start()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("ManagerGame").GetComponent<GameManager>();
    }
    
    // Functie de repornire a jocului
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        gameManagerScript.enabled = true;
        Time.timeScale = 1f;
        Debug.Log(Time.timeScale);
        GameIsPaused = false;
    }

    // Functie de oprire a jocului
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        gameManagerScript.enabled = false;
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    // Functia afiseaza scorul obtinut intr-o sesiune de joc
    public void Score(int score)
    {
        pointsText.text = score.ToString() + " POINTS";
    }
}