using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScriptCamera : MonoBehaviour
{
    public float MovementSpeed;
    void Start()
    {
        transform.position = transform.position + (Vector3.up * 0);
        MovementSpeed = (float)1.25;
    }
    void Update()
    {
        transform.position = transform.position + (Vector3.up * MovementSpeed) * Time.deltaTime;
    }
}




