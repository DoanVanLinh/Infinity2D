using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] int[] values;
    [SerializeField] float smooth;

    private int angle;
    public int[] Values { get => values; set => values = value; }

    private void Start()
    {
        smooth = GameManager.gameManager.Smooth;
    }
    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), smooth);
    }
    private void OnMouseDown()
    {
        Rotate();
        RotateValue();
        //GameManager.gameManager.CalculatorConected();
        GameManager.gameManager.EndGame();
    }
    private void Rotate()
    {
        angle += 90;
    }
    private void RotateValue()
    {
        int firstValue = values[0];
        for (int i = 0; i < 3; i++)
        {
            values[i] = values[i + 1];
        }
        values[3] = firstValue;
    }
}
