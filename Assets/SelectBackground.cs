using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBackground : MonoBehaviour
{
    // Se memoreaza background-ul ales de jucator
    public void SelectBg(string backgroundName)
    {
        PlayerPrefs.SetString("currentBackground", backgroundName);
    }
}
