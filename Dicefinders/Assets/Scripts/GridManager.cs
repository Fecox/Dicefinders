using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap Board;
    
    private List<Node> nodes = new List<Node>();

    public void Awake()
    {
        CreateNodes();
        // Prueba();
    }

    private void CreateNodes()
    {
        for(int x = Board.cellBounds.xMin; x < Board.cellBounds.xMax; x++)
        {
            for(int y = Board.cellBounds.yMin; y < Board.cellBounds.yMax; y++)
            {
                Vector3Int boardPos = new Vector3Int(x, y , (int)Board.transform.position.y);

                if(Board.HasTile(boardPos))
                {
                    Vector3 pos = Board.CellToWorld(boardPos);
                    Node node = new Node(nodes.Count, pos);
                    nodes.Add(node);
                }
            }
        }
    }

    private void Prueba()
    {
        for(int i = 0; i < nodes.Count; i++)
        {
            Debug.Log(nodes[i].Index);
        }
    }
}
