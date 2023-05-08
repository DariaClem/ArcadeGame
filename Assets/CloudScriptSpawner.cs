using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScriptSpawner : MonoBehaviour
{
    public GameObject[] cloud;
    public double spawnRate = 0.5;
    public float spawnNumber = 0;
    private float timer = 0;
    public float coordPeY = -4;

    void Start()
    {
        coordPeY += 3;
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (spawnNumber < 25)
            {
                SpawnCloud();
                timer = 0;
            }
        }
    }

    void SpawnCloud()
    {
        if (spawnNumber % 2 == 0)
        {
            GameObject gameObject = Instantiate(cloud[Random.Range(0, cloud.Length)], new Vector3(Random.Range(-6, 3), (coordPeY + 1)), transform.rotation);
        }
        else
        {
            GameObject gameObject = Instantiate(cloud[Random.Range(0, cloud.Length)], new Vector3(Random.Range(3, 5), (coordPeY + 1)), transform.rotation);
        }
        coordPeY += 3;
        spawnNumber++;
    }
}
