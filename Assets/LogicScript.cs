using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public TMP_Text scoreText;

    [ContextMenu("Increase Score")]

    // functie pentru cresterea scorului
    public void addScore()
    {
        playerScore++;
        scoreText.text = playerScore.ToString();
    }

}
