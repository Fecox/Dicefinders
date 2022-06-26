using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitManager : UnitManager
{
    public void MakeMove()
    {

    }

    // usar este tipo de logica de ahora en adelante
    public void CheckForNextAction()
    {
        Node selectNode = GridManager.Instance.GetNodeAtMousePos(false);
        List<Node> aroundNodes = new List<Node>(); // por ahora 
        
        for (int i = 0; i < aroundNodes.Count; i++)
        {
            if (selectNode.Index == aroundNodes[i].Index)
            {
                unit.MakeNextMove(selectNode);
            }
        }
    }
}
