using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public Tilemap grid;
    private int minimo;
    private int maximo;

    public void Start()
    {
        prueba();
    }

    private void prueba()
    {
        int count = 0;
        for(int x = grid.cellBounds.xMin; x < grid.cellBounds.xMax; x++)
        {
            for(int y = grid.cellBounds.yMin; y < grid.cellBounds.yMax; y++)
            {
                Vector3Int localPos = new Vector3Int(x, y , (int)grid.transform.position.y);

                if(grid.HasTile(localPos))
                {
                    count ++;
                }
            }
        }

        Debug.Log(count);
    }
}
