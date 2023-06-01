using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    public GameObject logicManager;
    public LogicScript logicScript;
    private void Start()
    {
        logicManager = GameObject.FindGameObjectWithTag("Logic");
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    public void GoToMenu()
    {
        // Incarcam scena cu meniul static
        Destroy(logicManager);
        SceneManager.LoadScene(1);
    }
}
