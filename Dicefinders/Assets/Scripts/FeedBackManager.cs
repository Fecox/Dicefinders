using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedBackManager : Singleton<FeedBackManager>
{
    public GameObject PrefabHighLight;

    private GameObject parentHighLight;

    public new void Awake()
    {
        base.Awake();

        parentHighLight = new GameObject("Possible Movements"); // esto es hot fix
    }

    // todo esto tambien es hot fix, deberiamos usar un tilemap que este hoveriado y se muestre solo cuando se pueda hacer el movimiento, tambien ver si mostramos el moviiento completo o no, preguntar a cacho, movimiento en idagonal tambien
    public void ShowPossibleMoves(PlayerUnitManager playerUnit)
    {
        List<Node> aroundNodes = playerUnit.GetaroundNodes();

        for (int i = 0; i < aroundNodes.Count; i++)
        {
            GameObject highLight = Instantiate(PrefabHighLight, aroundNodes[i].Position, Quaternion.identity, parentHighLight.transform);
        }
    }
}
