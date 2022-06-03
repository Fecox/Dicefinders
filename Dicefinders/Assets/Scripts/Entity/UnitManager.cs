using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager
{
    public Unit unit { get; protected set; }

    public void AddUnit(Unit prefab, Node selectedNode)
    {
        unit = MonoBehaviour.Instantiate(prefab);
        unit.Spawn(prefab, selectedNode);
    }

    public void MakeNextAction()
    {
        
    }




    public int GetMovementSteps()
    {
        return unit.MovementSteps;
    }

    public void GetPossibleMoves()
    {
        unit.GetPossibleMoves();
    }

    public List<Node> GetaroundNodes()
    {
        return unit.GetaroundNodes();
    }

    public void SetMovementSteps()
    {
        unit.SetMovementSteps();
    }
}
