using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScriptCamera : MonoBehaviour
{
    public float MovementSpeed;

    // pozitia de start a camerei
    void Start()
    {
        transform.position = transform.position + (Vector3.up * 0);
        MovementSpeed = (float)0;
    }

    public void startMoving()
    {
        MovementSpeed = (float)1.25;
        Update();
    }

    // modific pozitia camerei
    void Update()
    {
        transform.position = transform.position + (Vector3.up * MovementSpeed) * Time.deltaTime;
    }
}




