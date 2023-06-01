using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManagerScript : MonoBehaviour
{
    public static AudioManagerScript audio;
    private void Awake()
    {
        if (audio == null)
        {
            audio = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
