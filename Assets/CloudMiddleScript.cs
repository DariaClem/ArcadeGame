using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMiddleScript : MonoBehaviour
{
    public LogicScript logic;
    public PenguinScript penguin;
    public BoxCollider2D boxCollider2D;
    public ScriptCamera mainCamera;

    void Start()
    {
        // Legatura cu logicScript
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        // Legatura cu script-ul pentru camera
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScriptCamera>();
    }

    void Update()
    {
        // La final camera se va opri astfel incat pana sa fie in centrul ecranului
        if (mainCamera.transform.position.y > 75)
        {
            mainCamera.MovementSpeed = 0;
        }
        // daca scorul jucatorului se produc modificari asupra vitezei de miscare a camerei
        if (logic.scoreForClouds == 5)
        {
            mainCamera.MovementSpeed = (float)1.5;
        }
        else if (logic.scoreForClouds == 10){
            mainCamera.MovementSpeed = (float)1.75;
        }
        else if (logic.scoreForClouds == 15) {
            mainCamera.MovementSpeed = 2; 
        }
        else if (logic.scoreForClouds == 20)
        {
            mainCamera.MovementSpeed = (float)2.5;
        }
        else if (logic.scoreForClouds == 25)
        {
            mainCamera.MovementSpeed = (float)3.5;
            if (mainCamera.transform.position.y > 75)
            {
                mainCamera.MovementSpeed = 0;
            }
        }
    }

    // functie pentru cresterea scorului odata ce pinguinul a ajuns pe un nor urmator
    private void OnTriggerEnter2D(Collider2D collision)
    {
        logic.addScore();
        boxCollider2D.enabled = false;
    }
}
