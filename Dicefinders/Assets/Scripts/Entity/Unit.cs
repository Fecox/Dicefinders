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

    public virtual void Spawn(Unit selectedPrefab, Node spawnNode)
    {
        prefab = selectedPrefab;

        ChangeCurrentNode(spawnNode);

        Node.IsOccupied = true;
    }

    public void SetMovementSteps()
    {
        MovementSteps = Random.Range(1, 4);
    }

    public void MakeNextMove(Node selectedNode)
    {
        FeedBackManager.Instance.Reset();
        ChangeCurrentNode(selectedNode);
        TakeMovementSteps();
        transform.position = selectedNode.Position;
    }

    private void ChangeCurrentNode(Node node)
    {
        Node = node;
        transform.position = Node.Position;
    }

    private void TakeMovementSteps()
    {
        MovementSteps--;
    }
}
