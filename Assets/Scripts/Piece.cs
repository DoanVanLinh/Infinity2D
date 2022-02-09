using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] int[] values;
    [SerializeField] float smooth;
    public bool isNew;

    public int angle;

    public void SetValues(Piece exitsPiece)
    {
        this.values = exitsPiece.values;
        this.isNew = exitsPiece.isNew;
    }

    public int[] Values { get => values; set => values = value; }
    
    private void Start()
    {
        
        smooth = GameManager.Instance.Smooth;
        if (isNew)
        {
            int randomRotate = Random.Range(1, 4);
            for (int i = 0; i < randomRotate; i++)
            {
                Rotate();
                RotateValue();
            }
            isNew = false;
        }
    }
    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), smooth);
    }
    private void OnMouseDown()
    {
        if (GameManager.Instance.IsEnd) return;
        Rotate();
        RotateValue();
        GenatorMap.Instance.CalculatorConected();
        if (GenatorMap.Instance.Complete())
            GameManager.Instance.End(true);
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
