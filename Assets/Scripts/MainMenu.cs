using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject audioManagerScript;
    public AudioSource audioMenu;
    public void Start()
    {
        audioManagerScript = GameObject.FindGameObjectWithTag("SoundMusic");
        Destroy(audioManagerScript);
        audioMenu = GameObject.FindGameObjectWithTag("AudioMenu").GetComponent<AudioSource>();
        audioMenu.mute = false;
        DontDestroyOnLoad(audioMenu);
    }

    public void PlayGame()
    {
        // Incarcam scena cu jocul
        SceneManager.LoadScene(2);
    }

    public void GoToStaticMainMenu()
    {
        audioMenu.mute = true;
        // Incarcam scena cu meniul static
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        // Iesim din joc
        Application.Quit();
    }
}
