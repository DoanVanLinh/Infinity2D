using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(GenatorMap))]

public class GameManager : MonoBehaviour
{
    [SerializeField] int row;
    [SerializeField] int col;
    [SerializeField] GameObject[] piecesObj;
    [SerializeField] int connected;
    [SerializeField] int amountConnect;
    [SerializeField] float smooth;

    private int maxConnect;
    private int minConnect;
    private bool isEnd;
    private Piece[,] pieces;

    public static GameManager gameManager;

    public float Smooth { get => smooth; set => smooth = value; }
    public int Row { get => row; set => row = value; }
    public int Col { get => col; set => col = value; }

    void Start()
    {
        Singleton();
        this.pieces = new Piece[this.row, this.col];
        SpawnArray();
        this.amountConnect = CalculatorConect();
        this.connected = CalculatorConected();
        maxConnect = (row - 1) * col + row * (col - 1);
        minConnect = row * col;
        isEnd = false;
    }


    void Update()
    {
    }

    void SpawnArray()
    {
        for (int i = 0; i < this.row; i++)
        {
            for (int j = 0; j < this.col; j++)
            {
                SpawnPiece(i, j, GenatorMap.realArray[i, j]);
            }
        }
    }
    int CalculatorConect()
    {
        int amount = 0;
        foreach (var p in GameObject.FindGameObjectsWithTag("Piece"))
        {
            foreach (var p2 in p.GetComponent<Piece>().Values)
            {
                amount += p2;
            }
        }
        return amount / 2;
    }
    public int CalculatorConected()
    {
        int amount = 0;
        for (int i = 0; i < this.row; i++)
        {
            for (int j = 0; j < this.col; j++)
            {
                //left to right
                if (j != this.col - 1)
                    if (this.pieces[i, j].Values[1] == 1 && this.pieces[i, j + 1].Values[3] == 1)
                        amount++;
                //bottom to top
                if (i != this.row - 1)
                    if (this.pieces[i, j].Values[0] == 1 && this.pieces[i + 1, j].Values[2] == 1)
                        amount++;
            }
        }
        return connected = amount;
    }
    void Singleton()
    {
        if (gameManager == null)
            gameManager = this;
    }
    public bool EndGame()
    {
        if (connected == amountConnect)
            return isEnd = true;
        return isEnd = false;
    }

    void SpawnPiece(int i, int j, int type)
    {
        if (type>-1)
        {
            GameObject clone = Instantiate(this.piecesObj[type], new Vector2(j, i), Quaternion.identity);
            this.pieces[i, j] = clone.GetComponent<Piece>();
        }
    }

}
