using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioScript : MonoBehaviour
{
    public static MenuAudioScript audio;
    private void Awake()
    {
        if (audio == null)
        {
            audio = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            destroyAudio();
        }
    }
    
    public void destroyAudio()
    {
        Destroy(gameObject);
    }
}
