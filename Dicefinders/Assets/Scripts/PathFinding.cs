using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinding
{
    // ver posible restructuracion de gridmanager y conttolar los nodos por un array en vez de list y que el array tome dos valores x e y

    private const int MOVE_COST = 10;

    private List<Node> toSearch;
    private List<Node> processed;

    public List<Node> FindPath(Node startNode, Node targetNode)
    {
        // para que quede un poco mas limpio el codigo podemos buscar el nodo inicial aqui
        if (startNode == null || targetNode == null)
        {
            Debug.Log("algun nodo es nulo"); // Borrar esto
            return null;
        }

        toSearch = new List<Node> { startNode };
        processed = new List<Node>();

        // ver esto, no me parece correcto hacer esto capas que es mejor hacer un grid aparte con las mismas coordenadas que el grid normal
        for (int i = 0; i < GridManager.Instance.nodes.Count; i++)
        {
            GridManager.Instance.nodes[i].gCost = int.MaxValue;
            GridManager.Instance.nodes[i].CalculateFCost();
            GridManager.Instance.nodes[i].previousNode = null;
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, targetNode);
        startNode.CalculateFCost();

        while (toSearch.Count() > 0)
        {
            Node currentNode = GetLowestFCostNode(toSearch);
            if (currentNode == targetNode)
            {
                Debug.Log("calculate path execute");// borrar
                return CalculatePath(targetNode); 
            }

            toSearch.Remove(currentNode);
            processed.Add(currentNode);

            foreach (Node neighbourNode in GetNeighbours(currentNode))
            {
                if (processed.Contains(neighbourNode)) continue;

                if (neighbourNode.IsOccupied)
                {
                    processed.Add(neighbourNode);
                    continue;
                }

                int possibleGCost = currentNode.gCost + CalculateDistance(currentNode, neighbourNode);
                if (possibleGCost < neighbourNode.gCost)
                {
                    neighbourNode.previousNode = currentNode;
                    neighbourNode.gCost = possibleGCost;
                    neighbourNode.hCost = CalculateDistance(neighbourNode, targetNode);
                    neighbourNode.CalculateFCost();
                    if (!toSearch.Contains(neighbourNode))
                    {
                        toSearch.Add(neighbourNode);
                    }
                }
            }
        }
        return null;
    }

    private int CalculateDistance(Node a, Node b)
    {
        // creo que deberiamos de crear una caracteristica de un nodo sea su X y su Y y sean numeros enteros del redondeo
        // ahora el redonde se esta haciendo aqui me parece que queda muy feo y enrevenzado, asi que ver la solucion si hacerlo con el cambio en el grid
        int xDistance = Mathf.Abs(a.Boardpos.x - b.Boardpos.x);
        int yDistance = Mathf.Abs(a.Boardpos.y - b.Boardpos.y);
        int totalDistance = xDistance + yDistance;
        return MOVE_COST * totalDistance;
    }

    private Node GetLowestFCostNode(List<Node> pathNodes)
    {
        Node lowestFCostNode = pathNodes[0];
        for (int i = 1; i < pathNodes.Count; i++)
        {
            if (pathNodes[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodes[i];
            }
        }
        return lowestFCostNode;
    }

    private List<Node> CalculatePath(Node targetNode)
    {
        List<Node> path = new List<Node>();
        path.Add(targetNode);

        Node currentNode = targetNode;

        while (currentNode.previousNode != null)
        {
            path.Add(currentNode.previousNode);
            currentNode = currentNode.previousNode;
        }
        path.Reverse();

        return path;
    }

    private List<Node> GetNeighbours(Node currentNode) // TODO:  esto podemos cambiarlo con el tema de la funcion GetCardinals o algo parecido, ahora lo hago asi para hacerlo rapido
    {
        List<Node> neighboursNodes = new List<Node>();

        if (GridManager.Instance.GetBotNode(currentNode) != null)
        {
            neighboursNodes.Add(GridManager.Instance.GetBotNode(currentNode));
        }
        if (GridManager.Instance.GetTopNode(currentNode) != null)
        {
            neighboursNodes.Add(GridManager.Instance.GetTopNode(currentNode));
        }
        if (GridManager.Instance.GetLeftNode(currentNode) != null)
        {
            neighboursNodes.Add(GridManager.Instance.GetLeftNode(currentNode));
        }
        if (GridManager.Instance.GetRightNode(currentNode) != null)
        {
            neighboursNodes.Add(GridManager.Instance.GetRightNode(currentNode));
        }

        return neighboursNodes;
    }
}
