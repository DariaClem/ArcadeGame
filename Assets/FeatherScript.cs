using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherScript : MonoBehaviour
{
    [SerializeField] GameObject penguin;
    [SerializeField] GameObject feather;
    [SerializeField] GameObject featherPenguin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        penguin.SetActive(false);
        feather.SetActive(false);
        featherPenguin.SetActive(true);
    }


}
