using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScriptCamera : MonoBehaviour
{
    public float MovementSpeed;
    
    void Start()
    {
        // Pozitia de start a camerei
        transform.position = transform.position + (Vector3.up * 0);
        MovementSpeed = (float)0;
    }

    public void startMoving()
    {
        MovementSpeed = (float)1.25;
        Update();
    }
    
    void Update()
    {
        // modific pozitia camerei
        transform.position = transform.position + (Vector3.up * MovementSpeed) * Time.deltaTime;
    }
}




