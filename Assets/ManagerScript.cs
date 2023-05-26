using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    public GameObject backgrounds;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject featherPenguin;

    void Start()
    {
        // legatura cu camera
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScriptCamera>();
        // legatura cu pinguinul
        penguin = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinScript>();
        penguinScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinScript>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        highScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore", 0).ToString();

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
        if (penguin.myRigidbody.position.y < mainCamera.transform.position.y - 6 || featherPenguin.transform.position.x > (float)8.49)
        {
            gameOverMenu.SetActive(true);
            penguinScript.enabled = false;
            mainCamera.MovementSpeed = 0;
            menuManager.Score(logic.playerScore);

            if (logic.playerScore > PlayerPrefs.GetInt("HighScore", 0))
            {
                PlayerPrefs.SetInt("HighScore", logic.playerScore);
                highScore.text = "HIGH SCORE: " + logic.playerScore.ToString();
            }
        }
    }

    // resetez jocul
    public void Restart()
    {
        gameOverMenu.SetActive(false);
        penguinScript.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
