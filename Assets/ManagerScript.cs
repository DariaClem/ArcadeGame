using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    public LogicScript logic;
    public PenguinScript penguin;
    public ScriptCamera mainCamera;
    public MenuManager menuManager;
    public TMP_Text pointsText;
    public TMP_Text highScore;
    public PenguinScript penguinScript;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject featherPenguin;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScriptCamera>();
        penguin = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinScript>();
        penguinScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinScript>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    private void Update()
    {
        if (penguin.myRigidbody.position.y < mainCamera.transform.position.y - 6 || featherPenguin.transform.position.x > (float)8.49)
        {
            gameOverMenu.SetActive(true);
            penguinScript.enabled = false;
            mainCamera.MovementSpeed = 0;
            menuManager.Score(logic.playerScore);

            if (logic.playerScore > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", logic.playerScore);
                highScore.text = logic.playerScore.ToString();
            }
        }
    }

    public void Restart()
    {
        gameOverMenu.SetActive(false);
        penguinScript.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
