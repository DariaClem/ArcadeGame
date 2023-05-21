using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider _slider;
    public float fillSpeed = 0.5f;
    private ParticleSystem _particleSystem;
    private float _targetProgress = 0.1f;

    private void Awake()
    {
        _slider = gameObject.GetComponent<Slider>();
        _particleSystem = GameObject.Find("Particles").GetComponent<ParticleSystem>();
    }

    void Start()
    {
        IncrementProgress(1f);
    }

    void Update()
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

    public void IncrementProgress(float newProgress)
    {
        _targetProgress = _slider.value + newProgress;
    } 
}
