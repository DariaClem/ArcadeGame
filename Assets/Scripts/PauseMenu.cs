using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{ 
    public GameObject logicManager;
    public LogicScript logicScript;
    [SerializeField] private IntSo ScoreSO;
    private void Start()
    {
        logicManager = GameObject.FindGameObjectWithTag("Logic");
        logicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    public void GoToMenu()
    {
        ScoreSO.Value = 0;
        SceneManager.LoadScene(1);
    }
}
