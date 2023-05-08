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
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScriptCamera>();
    }

    void Update()
    {
        if (logic.playerScore == 5)
        {
            mainCamera.MovementSpeed = (float)1.5;
        }
        else if (logic.playerScore == 10){
            mainCamera.MovementSpeed = (float)1.75;
        }
        else if (logic.playerScore == 15) {
            mainCamera.MovementSpeed = 2; 
        }
        else if (logic.playerScore == 20)
        {
            mainCamera.MovementSpeed = (float)2.5;
        }
        else if (logic.playerScore == 25)
        {
            mainCamera.MovementSpeed = (float)3.5;
            if (mainCamera.transform.position.y > 75)
            {
                mainCamera.MovementSpeed = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        logic.addScore();
        boxCollider2D.enabled = false;
    }
}
