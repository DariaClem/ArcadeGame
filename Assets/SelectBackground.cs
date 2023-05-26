using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBackground : MonoBehaviour
{
    public void SelectBg(string backgroundName)
    {
        PlayerPrefs.SetString("currentBackground", backgroundName);
    }
}
