using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(GenatorMap))]
[RequireComponent(typeof(CameraZoom))]


public class GameManager : MonoBehaviour
{
    [SerializeField] int row;
    [SerializeField] int col;
    [SerializeField] float smooth;
    [SerializeField] Animator finishAni;

    private Camera mainCamera;
    private bool isEnd;

    public static GameManager gameManager;

    public float Smooth { get => smooth; set => smooth = value; }
    public int Row { get => row; set => row = value; }
    public int Col { get => col; set => col = value; }
    public bool IsEnd { get => isEnd; set => isEnd = value; }

    void Start()
    {
        mainCamera = Camera.main;
        Singleton();
        isEnd = false;
    }


    void Update()
    {
    }
    public void End(bool In_Out)
    {
        finishAni.SetBool("In-Out", In_Out);
    }
    void Singleton()
    {
        if (gameManager == null)
            gameManager = this;
    }
}
