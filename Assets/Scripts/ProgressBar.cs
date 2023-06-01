using System;
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
    public ParticleSystem fireworks;
    
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

        // Afisam XP-ul jucatorului din XP-ul total necesar pentru a avansa la urmatorul nivel 
        range.text = currentScore + "/" + requiredXp;
        level.text = (currentLevel + 1).ToString();
        
        if (requiredXp <= currentScore + playerScore)
        {
            // Cat timp jucatorul a obtinut un scor mai mare decat necesarul pentru a avansa la urmatorul nivel, crestem nivelul acestuia
            while (requiredXp <= currentScore + playerScore)
            {
                currentLevel++;
                playerScore = playerScore - requiredXp + currentScore;
                
                requiredXp = CalculateRequiredXp(currentLevel);
                currentScore = 0;
            }

            // Setam nivelul si scorul curent al jucatorului
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            
            // Animatie
            fireworks.gameObject.SetActive(true);
            fireworks.Play();
            
            PlayerPrefs.SetInt("currentScore", playerScore);
        }
        else
        {
            // Setam scorul curent al jucatorului
            PlayerPrefs.SetInt("currentScore", currentScore + playerScore);
        }
        
        // Configuram sliderul ce va afisa scorul curent din punctajul total pentru a avansa
        _slider.value = (float) currentScore / requiredXp;
        float increment = (float) playerScore / requiredXp;
        
        IncrementProgress(increment);
        
        // Recalculam XP-ul jucatorului din XP-ul total necesar pentru a avansa la urmatorul nivel 
        level.text = (currentLevel + 1).ToString();
        range.text = Math.Min(requiredXp, currentScore + playerScore) + "/" + requiredXp;
    }

    void Update()
    {
        if (logic.playerScore != 0)
        {
            // Pornim animatia sliderului
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

    // Functie ce calculeaza XP-ul necesar pentru a avansa la urmatorul nivel dupa formula de calcul preluata de pe site-ul
    // https://oldschool.runescape.wiki/w/Experience si anume (1/4) * [Sum 1<=l<=L-1 (l + 300 * 2^(l/7)]
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
