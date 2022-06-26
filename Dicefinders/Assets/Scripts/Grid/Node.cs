using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int Index { get; }
    public Vector3 Position { get; }
    public bool IsOccupied { get ; set; }
    public Vector3Int Boardpos { get; }

    // START
    // se puede hacer qeu gridmanager pueda ser generic<T> y poder generar un grid de nodos especificos para el pathfinding abarcaria todo esto 
    public int gCost { get; set; }
    public int hCost { get; set; }
    public int fCost { get; set; }

    public Node previousNode { get; set; }
    // END

    public Node(int index, Vector3 position, Vector3Int boardpos)
    {
        Index = index;
        Position = position;
        IsOccupied = false;
        Boardpos = boardpos;

        // START
        
        gCost = int.MaxValue;
        fCost = gCost + hCost;
        previousNode = null;

        // END
    }
    
    // START 
    
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    //END
}
