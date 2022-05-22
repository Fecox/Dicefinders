using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int Index { get; }
    public Vector3 Pos { get; }

    public bool IsOccupied { get ; set; }

    public Node(int index, Vector3 pos)
    {
        Index = index;
        Pos = pos;
        IsOccupied = false;
    }
}
