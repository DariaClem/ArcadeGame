using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameButtons : MonoBehaviour
{
    public AudioSource music;
    public AudioSource penguinJumpSound;
    public PenguinScript penguinScript;
    

    public void Start()
    {
        music = GameObject.FindGameObjectWithTag("SoundMusic").GetComponent<AudioSource>();
    }

    public void Volume()
    {
        music.volume = 0.2f;
        penguinJumpSound.volume = 1f;
    }

    public void Mute()
    {
        music.volume = 0;
        penguinJumpSound.volume = 0;
    }
    
}
