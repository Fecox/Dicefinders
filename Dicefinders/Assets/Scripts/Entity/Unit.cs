using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int Health { get; protected set; }
    public int Defence { get; protected set; }
    public int MovementSteps { get; protected set; }
    public Node Node { get; protected set; }

    private Unit prefab; 

    private List<Node> aroundNodes = new List<Node>();

    private Directions[] directions = { Directions.RIGHT, Directions.LEFT, Directions.BOTTOM, Directions.TOP };

    public virtual void Spawn(Unit selectedPrefab, Node spawnNode)
    {
        prefab = selectedPrefab;

        ChangeCurrentNode(spawnNode);

        GetAroundNodes();

        Node.IsOccupied = true;
    }

    // a la hora de hacer el feedback, habria que ver esto
    public void GetPossibleMoves()
    {
        for (int i = 0; i < aroundNodes.Count; i++)
        {
            GameObject highLight = Instantiate(FeedBackManager.Instance.PrefabHighLight);
            highLight.transform.position = aroundNodes[i].Position;
        }
    }

    public List<Node> GetaroundNodes()
    {
        return aroundNodes;
    }

    public void SetMovementSteps()
    {
        MovementSteps = Random.Range(1, 4);
    }

    private void ChangeCurrentNode(Node node)
    {
        Node = node;
        transform.position = Node.Position;
    }

    private void GetAroundNodes()
    {
        aroundNodes.Clear();

    	foreach (Directions direction in directions)
        {
            Node node = GridManager.Instance.GetNodeAtDirection(direction, Node);

            if (node != null)
            {
                aroundNodes.Add(node);
            }  
        }
    }
}

public enum Directions
{
    RIGHT = 0,
    LEFT = 1,
    BOTTOM = 2,
    TOP = 3
}
