using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        penguinScript.enabled = true;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        penguinScript.enabled = false;
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void Score(int score)
    {
        pointsText.text = score.ToString() + " POINTS";
    }
}
