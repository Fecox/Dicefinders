using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : Singleton<GridManager>
{
    public Tilemap Board;

    [SerializeField] private const float MAX_DISTANCE = 10.01f;

    private int totalRows = 8;
    private int totalColumns;
    private int playerSpawnZone = 2;
    private int IaSpawnZone = 6;

    public List<Node> nodes = new List<Node>(); // esto tiene que ser privado ahora lo hago publico para probar lo mas probable que tengamos que hacer un grid aparte para el path

    public new void Awake()
    {
        base.Awake();

        CreateNodes();
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
                    Node node = new Node(nodes.Count, pos, boardPos);
                    nodes.Add(node);
                }
            }
        }

        totalColumns = nodes.Count / totalRows;
    }

    private int GetColumnAtPosition(Vector3 position)
    {
        float lastClosestColumn = Mathf.Infinity;
        int column = -1;
        
        for (int i = 0; i < totalColumns; i++)
        {
            float distance = (nodes[i * totalRows].Position - position).magnitude;

            if (distance < lastClosestColumn)
            {
                column = i;
                lastClosestColumn = distance;                
            }
            else
            {
                break;
            }
        }
        return column;
    }

    private int GetRowAtPosition(Vector3 position, int rows)
    {
        float lastClosestRow = Mathf.Infinity;
        int row = -1;

        for (int i = 0; i < rows; i++)
        {
            float distance = (nodes[i].Position - position).magnitude;

            if (distance < lastClosestRow)
            {
                row = i;
                lastClosestRow = distance;
            }
            else
            {
                break;
            }
        }
        return row;
    }

    public int GetIndexAtCoordinates(int x, int y) // por ahora es publica capas paso lo otro para aca y la hago privada de nuevo
    {
        return y + totalRows * x;
    }

    private int GetColumnByIndex(int currentIndex)
    {
        return currentIndex / totalRows;
    }

    public Node GetNodeAtMousePos(bool IsSpawnTime = false)
    {
        int rows = IsSpawnTime ? playerSpawnZone : totalRows;

        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Node node = GetNodeAtPosition(position, rows);

        return node;
    }

    public Node GetNodeAtPosition(Vector3 position, int rows) // despues de pasar el codigo para aca hacerla privada
    {

        int column = GetColumnAtPosition(position);
        int row = GetRowAtPosition(position, rows);

        int index = GetIndexAtCoordinates(column, row);

        Vector3 distance = nodes[index].Position - position;

        if (distance.magnitude < MAX_DISTANCE)
        {
            return nodes[index];
        }
        return null;
    }

    public List<Node> GetNodes()
    {
        return nodes;
    }

    // si no va a ser necesario estos geters borrarlos los dejo por si se necesitan usar en un futuro
    public Node GetTopNode(Node currentNode)
    {
        int maxColumnValue = totalRows * (GetColumnByIndex(currentNode.Index) + 1);
        int index = currentNode.Index + 1;

        if (index >= nodes.Count || index >= maxColumnValue)
        {
            return null;
        }

        return nodes[index];
    }

    public Node GetBotNode(Node currentNode)
    {
        int minColumnValue = totalRows * GetColumnByIndex(currentNode.Index) - 1;
        int index = currentNode.Index - 1;

        if (index < 0 || index <= minColumnValue)
        {
            return null;
        }

        return nodes[index];
    }

    public Node GetRightNode(Node currentNode)
    {
        int index = currentNode.Index + totalRows;

        if (index >= nodes.Count)
        {
            return null;
        }

        return nodes[index];
    }

    public Node GetLeftNode(Node currentNode)
    {
        int index = currentNode.Index - totalRows;

        if (index < 0)
        {
            return null;
        }

        return nodes[index];
    }

    public Node GetRandomEnemySpawnNode()
    {
        List<Node> randomNodes = new List<Node>();

        for (int x = IaSpawnZone; x < totalRows; x++)
        {
            for (int y = 0; y < totalColumns; y++)
            {
                randomNodes.Add(nodes[GetIndexAtCoordinates(y, x)]);
            }
        }
        return randomNodes[Random.Range(0, randomNodes.Count)];
    }
}
