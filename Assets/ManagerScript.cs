using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    public PenguinScript penguin;
    public ScriptCamera mainCamera;
    [SerializeField] GameObject featherPenguin;

    void Start()
    {
        // legatura cu camera
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScriptCamera>();
        // legatura cu pinguinul
        penguin = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinScript>();
    }

    private void Update()
    {
        // conditie de oprire daca pinguinul este sub pozitia camerei
        if (penguin.myRigidbody.position.y < mainCamera.transform.position.y - 6 || featherPenguin.transform.position.x > (float)8.49)
        {
            Restart();
        }
    }

    // resetez jocul
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
