using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GenatorMap : MonoBehaviour
{
    [SerializeField] int[] typeWay = new int[] { 0, 1, 2, 3, 4, 5 };
    [SerializeField] Dictionary<Vector2, Vector4> allPiece = new Dictionary<Vector2, Vector4>();
    public static int[,] realArray;

    private List<Vector2> unVisited = new List<Vector2>();
    private List<Vector2> stack = new List<Vector2>();
    private Vector2[] neightborPos = new Vector2[] { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
    private Vector2 currentPos;
    private Vector2 nextPos;
    private int row;
    private int col;

    void Start()
    {
        row = 5;
        col = 5;
        realArray = new int[row, col];
        currentPos = Vector2.zero;
        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < col; y++)
            {
                realArray[x, y] = -1;
                Vector2 pos = new Vector2(x, y);
                allPiece.Add(pos, Vector4.zero);
                unVisited.Add(pos);
            }
        }
        FindWay();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FindWay()
    {
        while (unVisited.Count > 0)
        {
            stack.Clear();
            List<Vector2> neighbor = GetNeighbor(currentPos);
            if (neighbor.Count != 0)
            {
                RandomWay(neighbor);
            }
            else if(unVisited.Count!=0)//make a new array or extent the current array
            {
                if (Random.Range(0, 1) == 1)//Extention 
                {
                    while (stack.Count > 0)
                    {
                        currentPos = stack[stack.Count - 1];
                        stack.Remove(currentPos);
                        neighbor = GetNeighbor(currentPos);
                        if (neighbor.Count != 0)
                        {
                            RandomWay(neighbor);
                        }
                    }
                }
                else// Make New
                {
                    unVisited.Remove(currentPos);
                    currentPos = unVisited[Random.Range(0, unVisited.Count - 1)];
                }
            }
        }

    }

    private void RandomWay(List<Vector2> neighbor)
    {
        Vector2 nPos = neighbor[Random.Range(0, neighbor.Count)];
        Vector2 dir = nPos - currentPos;
        allPiece[currentPos] += SetDirection(dir);
        allPiece[currentPos] += SetDirection(Vector2.zero-dir);
        stack.Add(currentPos);
        currentPos = nPos;
        unVisited.Remove(nPos);
    }
    Vector4 SetDirection(Vector2 dir)
    {
        Vector4 a = Vector4.zero;
        switch (dir)
        {
            case Vector2 v when v.Equals(Vector2.up):
                a = new Vector4(1, 0, 0, 0);
                break;
            case Vector2 v when v.Equals(Vector2.right):
                a = new Vector4(0, 1, 0, 0);
                break;
            case Vector2 v when v.Equals(Vector2.down):
                a = new Vector4(0, 0, 1, 0);
                break;
            case Vector2 v when v.Equals(Vector2.left):
                a = new Vector4(0, 0, 0, 1);
                break;
        }
        return a;
    }
    int RandomType(List<int> exception)
    {
        int random;
        do
        {
            random = Random.Range(0, 5);
        }
        while (exception.Contains(random));
        return random;
    }
    List<Vector2> GetNeighbor(Vector2 currenPos)
    {
        List<Vector2> neighborPiece = new List<Vector2>();
        foreach (Vector2 neighbor in neightborPos)
        {
            Vector2 n = currenPos + neighbor;
            if (unVisited.Contains(n))
                neighborPiece.Add(n);
        }
        return neighborPiece;
    }
}
