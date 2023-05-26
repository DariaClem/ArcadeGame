using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public LogicScript logic;
    private Slider _slider;
    public float fillSpeed = 0.5f;
    private ParticleSystem _particleSystem;
    private float _targetProgress = 0.1f;
    public TMP_Text range;
    public TMP_Text level;

    private String CurrentBackground(int currentLevel)
    {
        switch (currentLevel)
        {
            case 1:
                return "ShadowCanyon";
            case 2:
                return "WhisperSongMeados";
            case 3:
                return "FlameMountains";
            case 4:
                return "SunfireSands";
            case 5:
                return "GoldenPlans";
            case 6:
                return "EchoLake";
            default:
                return "CrystalWasteland";
        }
    }
    
    private void Awake()
    {
        _slider = gameObject.GetComponent<Slider>();
        _particleSystem = GameObject.Find("Particles").GetComponent<ParticleSystem>();
    }

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        int currentLevel = PlayerPrefs.GetInt("currentLevel");
        int requiredXp = CalculateRequiredXp(currentLevel);
        int currentScore = PlayerPrefs.GetInt("currentScore");
        int playerScore = logic.playerScore;

        range.text = currentScore + "/" + requiredXp;
        level.text = (currentLevel + 1).ToString();
        
        if (requiredXp <= currentScore + playerScore)
        {
            while (requiredXp <= currentScore + playerScore)
            {
                currentLevel++;
                playerScore = playerScore - requiredXp + currentScore;
                
                requiredXp = CalculateRequiredXp(currentLevel);
                currentScore = 0;
            }

            PlayerPrefs.SetInt("currentLevel", currentLevel);
            PlayerPrefs.SetInt("currentScore", playerScore);
        }
        else
        {
            PlayerPrefs.SetInt("currentScore", currentScore + playerScore);
        }
        
        PlayerPrefs.SetString("currentBackground", CurrentBackground(currentLevel+1));

        _slider.value = (float) currentScore / requiredXp;
        float increment = (float) playerScore / requiredXp;
        
        IncrementProgress(increment);
        
        level.text = (currentLevel + 1).ToString();
        range.text = Math.Min(requiredXp, currentScore + playerScore) + "/" + requiredXp;
    }

    void Update()
    {
        if (logic.playerScore != 0)
        {
            if (_slider.value < _targetProgress)
            {
                _slider.value += fillSpeed * Time.unscaledDeltaTime;
                if (!_particleSystem.isPlaying)
                    _particleSystem.Play();
            }
            else
            {
                _particleSystem.Stop();
            }   
        }
    }

    public void IncrementProgress(float newProgress)
    {
        _targetProgress = _slider.value + newProgress;
    }

    public static int CalculateRequiredXp(int currentLevel)
    {
        int requiredXp = 0;
        int nextLevel = currentLevel + 1;

        for (int levelCycle = 1; levelCycle <= nextLevel; levelCycle++)
        {
            requiredXp += (int)Math.Floor(levelCycle + 300 * MathF.Pow(2, levelCycle / 7f));
        }

        return requiredXp / 4;
    }
}
