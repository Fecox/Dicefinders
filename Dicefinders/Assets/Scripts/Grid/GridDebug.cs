using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridDebug : MonoBehaviour
{
    [SerializeField] private float gizmoSize = 2f;

    private void OnDrawGizmos()
    {
        if(!Application.isPlaying) return;

        var nodes = GridManager.Instance.GetNodes();

        if (nodes == null)
        {
            return;
        }

        foreach (Node node in nodes)
        {
            Gizmos.color = node.IsOccupied ? Color.red : Color.green;

            Gizmos.DrawSphere(node.Position, gizmoSize);
            Handles.Label(node.Position, node.Index.ToString());
        }        
    }
}
