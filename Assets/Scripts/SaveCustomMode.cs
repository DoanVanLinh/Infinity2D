using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveCustomMode : MonoBehaviour
{
    private int number;
    void Start()
    {
        PlayerPrefs.SetInt("Col", 3);
        PlayerPrefs.SetInt("Row", 3);
    }

    public void SetRowCol(string colOrRow)
    {
        number = int.Parse(GetComponent<TMP_InputField>().text);
        PlayerPrefs.SetInt(colOrRow, number);
    }
}
