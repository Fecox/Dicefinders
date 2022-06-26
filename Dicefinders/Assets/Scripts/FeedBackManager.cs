using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FeedBackManager : Singleton<FeedBackManager>
{
    [SerializeField] private Tilemap feedBackTileMap;
    [SerializeField] private Tile feedBackTile;

    public void ShowPossibleMoves(List<Node> heatMapNodes)
    {
        // despues de pasar heatmap a gridmanager no pedir parametro solo llamar desde aca el gridmanager.get
        for (int i = 0; i < heatMapNodes.Count; i++)
        {
            feedBackTileMap.SetTile(heatMapNodes[i].Boardpos, feedBackTile);
        }
    }

    public void Reset()
    {
        feedBackTileMap.ClearAllTiles();
    }
}
