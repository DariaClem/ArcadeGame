using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogicScriptSecondGame : MonoBehaviour
{
    public int playerScore;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private GameObject logicManager;
    [SerializeField] private IntSo ScoreSO;

    [ContextMenu("Increase Score")]
    public void Awake()
    {
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        logicManager = GameObject.FindGameObjectWithTag("Logic");
        scoreText.text = logicManager.GetComponent<LogicScript>().playerScore.ToString();
        playerScore = ScoreSO.Value;
        //DontDestroyOnLoad(logicManager);
    }

    // functie pentru cresterea scorului
    public void addScore()
    {
        playerScore++;
        scoreText.text = playerScore.ToString();
        ScoreSO.Value = playerScore;
    }
}
