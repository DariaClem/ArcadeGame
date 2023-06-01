using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButtons : MonoBehaviour
{
    public Button button;

    public Animator animZone;
    public Animator animLock;

    public GameObject zone;
    public GameObject lockObj;

    void SetActiveSelectButton()
    {
        button.interactable = true;
        button.Select();
        button.gameObject.SetActive(true);

        PlayerPrefs.SetString("lastAnimation", animZone.gameObject.name);
    }
}
