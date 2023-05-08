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
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScriptCamera>();
        penguin = GameObject.FindGameObjectWithTag("Player").GetComponent<PenguinScript>();
    }

    private void Update()
    {
        if (penguin.myRigidbody.position.y < mainCamera.transform.position.y - 6 || featherPenguin.transform.position.x > (float)8.49)
        {
            Restart();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
