using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    [SerializeField]
    private TMP_Text scoreText;
    [SerializeField]
    private GameObject logicManager;
    [SerializeField] private IntSo ScoreSO;
    public CloudMiddleScript cloudMiddleScript;
    public int scoreForClouds;

    [ContextMenu("Increase Score")]
    public void Awake()
    {
        scoreForClouds = 0;
        playerScore = ScoreSO.Value;
        Debug.Log(ScoreSO.Value);
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        logicManager = GameObject.FindGameObjectWithTag("Logic");
        scoreText.text = ScoreSO.Value.ToString();
        //DontDestroyOnLoad(logicManager);
    }

    // functie pentru cresterea scorului
    public void addScore()
    {
        scoreForClouds++;
        playerScore++;
        scoreText.text = playerScore.ToString();
        ScoreSO.Value = playerScore;
    }
}
