using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager
{
    protected Unit unit { get; set;}

    private List<Node> heatMapNodes = new List<Node>();

    private PathFinding pathFinding = new PathFinding();

    public void AddUnit(Unit prefab, Node selectedNode)
    {
        unit = MonoBehaviour.Instantiate(prefab);
        unit.Spawn(prefab, selectedNode);
    }

    public void MakeNextAction()
    {
        
    }

    public Node GetCurrentNode()
    {
        return unit.Node;
    }
    
    public List<Node> GetHeatMapNodes()
    {
        // TODO: no se si deba estar esto aca verlo
        // ROWS = X 
        // COLUMN = Y
        
        /*
        int initialColumn = unit.Node.Index - unit.MovementSteps;
        int finalColumn = unit.Node.Index + unit.MovementSteps;

        for (int column = initialColumn; column <= finalColumn; column++)
        {
            heatMapNodes.Add(GridManager.Instance.GetNodes()[column]);
        }

        int initalrow = unit.Node.Index - unit.MovementSteps;
        int finalrow = unit.Node.Index + unit.MovementSteps;

        for (int row = initalrow; row <= finalrow; row++)
        {
            heatMapNodes.Add(GridManager.Instance.GetNodes()[row]);
        }*/

        // TODO: ver de hacer mas lindo esto
        // TODO: hacerlo en gridmanager, y ademas solo hace un cubo
        float initialColumn = unit.Node.Position.y - unit.MovementSteps;
        float finalColumn = unit.Node.Position.y + unit.MovementSteps;

        float initalrow = unit.Node.Position.x - unit.MovementSteps;
        float finalrow = unit.Node.Position.x + unit.MovementSteps;

        for (int column = (int)initialColumn; column <= finalColumn; column++)
        {
            for (int row = (int)initalrow; row <= finalrow; row++)
            {
                Node node = GridManager.Instance.GetNodeAtPosition(new Vector3(row, column), 8);
                List<Node> path = pathFinding.FindPath(unit.Node, node);
                if (!node.IsOccupied)
                {
                    if (path.Count <= GetMovementSteps() + 1) // para eliminar el uno podriamos hacer en pathfinder una funcion que devuelva una lista sin el startnode
                    {
                        Debug.Log("find path");
                        heatMapNodes.Add(node);
                    }
                }
            }
        }
        // heatMapNodes.Add(GridManager.Instance.GetNodeAtPosition(new Vector3(row, column), 8));
        return heatMapNodes;
    }

    public int GetMovementSteps()
    {
        return unit.MovementSteps;
    }

    public void SetMovementSteps()
    {
        unit.SetMovementSteps();
    }
}
