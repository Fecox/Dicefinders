using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int Index { get; }
    public Vector3 Position { get; }

    public bool IsOccupied { get ; set; }

    public Node(int index, Vector3 position)
    {
        Index = index;
        Position = position;
        IsOccupied = false;
    }
}
