using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ManagerScript : MonoBehaviour
{
    private LogicScript logic;
    [SerializeField]
    private GameObject logicManager;
    public PenguinScript penguin;
    public ScriptCamera mainCamera;
    public MenuManager menuManager;
    public TMP_Text pointsText;
    public TMP_Text highScore;
    public PenguinScript penguinScript;
    public GameObject backgrounds;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject featherPenguin;
    [SerializeField] private IntSo ScoreSO;

    void HighScore()
    {
        highScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore", 0).ToString();  
    }

    void GameOver()
    {
        ScoreSO.Value = 0;
        // Apare meniul de game over
        gameOverMenu.SetActive(true);
        
        // Setam ca pinguinul sa nu se mai poate misca 
        penguinScript.enabled = false;
        mainCamera.MovementSpeed = 0;
        
        // Calculam scorul obtinut de jucator
        menuManager.Score(logic.playerScore);

        // Verificam daca scorul este highscore si actualizam daca este cazul
        if (logic.playerScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", logic.playerScore);
            HighScore();
        }
    }

    void Start()
    {
        // legatura cu camera
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScriptCamera>();
        // legatura cu pinguinul
        penguin = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinScript>();
        penguinScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinScript>();
        //logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        HighScore();

        // Configuram backgroundul curent in functie de cel selectat de jucator in inventar
        foreach (Transform t in backgrounds.transform) {
            if (t.parent == backgrounds.transform) {
                if (t.gameObject.name == PlayerPrefs.GetString("currentBackground"))
                    t.gameObject.SetActive(true);
                else
                {
                    t.gameObject.SetActive(false);
                }
            }
        }
    }

    private void Update()
    {
        // conditie de oprire daca pinguinul este sub pozitia camerei
        if (penguin.myRigidbody.position.y < mainCamera.transform.position.y - 6)
        {
            GameOver();
        }
        else if (featherPenguin.transform.position.x > (float)8.35)
        {
            SceneManager.LoadScene(2);
        }
    }

    // Repornim jocul
    public void Restart()
    {
        ScoreSO.Value = 0;
        // Dezactivam meniul game over 
        gameOverMenu.SetActive(false);
        penguinScript.enabled = true;
        logic.playerScore = 0;
        //Destroy(logicManager);
        // Repornim jocul
        SceneManager.LoadScene(2);
    }
}
