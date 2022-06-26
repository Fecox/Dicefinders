using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [SerializeField] private List<Unit> unitPrefabs; // TODO: Hacer selector de clases

    public PlayerUnitManager playerUnit;
    public IAUnitManager iAUnit;
    private string currentAbility; // TODO: deberia de encargarse otra cosa de esto unit tal vez o dice manager nose, creo qu es mejor el unitmanager y definirse como caracteristica de unit
    private List<string> abilitys = new List<string>(); // TODO: esto es un place holder, las hablidadades tendrian que tener una clase especifica y ser conhtroladas desde ahi o desde un dicemanagers que deberia de ser un scriptable objects ver eso IMPORTANTE
    private GameState state;

    private List<Node> prueba;

    public void Start()
    {
        playerUnit = new PlayerUnitManager();
        iAUnit = new IAUnitManager();

        
        // TODO: es es debug borrar a la hora de crear las habilidades
        abilitys.Add("ability 1");
        abilitys.Add("ability 2");
        abilitys.Add("ability 3");
        // esto ya no es debug
        ChangeState(GameState.PLAYER_SETUP);
    }

    public void Update()
    {
        if(state == GameState.PLAYER_SETUP)
        {
            // TODO se puede programar drag an drop en un metodo
            if (Input.GetMouseButtonDown(0))
            {
                // me parece que se puede hacer mejor esto
                if (IsNodeAviableAtMousePos(false/* para hacer pruebas*/))
                {
                    ChangeState(GameState.IA_SETUP);
                }
            }
        }
        if(state == GameState.IA_SETUP)
        {
            iAUnit.AddUnit(unitPrefabs[Random.Range(0, unitPrefabs.Count)], GridManager.Instance.GetRandomEnemySpawnNode());
            ChangeState(GameState.PLAYER_TURN);
        }
        if (state == GameState.PLAYER_TURN)
        {
            // TODO: ver como hacer mejor esto, tambien ver si vamos a poner botones con respecto a atacar y mover
            if (Input.GetMouseButtonDown(0) && playerUnit.GetMovementSteps() > 0)
            {
                // playerUnit.CheckForNextAction();
                Debug.Log(" ahora te puedes mover " + playerUnit.GetMovementSteps() + " veces");
                if (playerUnit.GetMovementSteps() > 0)
                {
                   // FeedBackManager.Instance.ShowPossibleMoves(playerUnit.GetHeatMapNodes()); parfa mi habria que actualizar esto desde otra parte
                }
                /* prueba = pathFinding.FindPath(playerUnit.GetCurrentNode(), GridManager.Instance.GetNodeAtMousePos());
                if (prueba != null)
                {
                    Debug.Log("works");
                    foreach (Node node in prueba)
                    {
                        node.IsOccupied = true;
                    }
                }*/
            }
            if (Input.GetKeyDown(KeyCode.D)) // is for debug
            {   

            }
        }
    }



    // TODO: a la hora de hacer el drag an drop hay que cambiar esta logica
    private bool IsNodeAviableAtMousePos(bool spawnZone)
    {
        Node selectedNode = GridManager.Instance.GetNodeAtMousePos(spawnZone);
        if (selectedNode != null)
        {
            if (selectedNode.IsOccupied)
            {
                return false;
            }
            if (!spawnZone /* por ahora para hacer pruebas*/)
            {
                playerUnit.AddUnit(unitPrefabs[0] /* aca esto tiene que ser diferente*/, selectedNode);
            }
            return true;
        }
        return false;
    }

    private void ChangeState(GameState currentState)
    {
        state = currentState;
        
        if (state == GameState.PLAYER_SETUP)
        {
            // TODO: generar botones
            // TODO: generar feedback para darle a entender al jugador que tiene que poner a su personaje
            // HOTFIX: mas adelante esto deberia de estar en un menu drageable y traer de tus dados guardaods de un scriptable object
            ShowYoursDices();
        }
        if(state == GameState.IA_SETUP)
        {
            
        }
        if(state == GameState.PLAYER_TURN)
        {
            // TODO: esto es momentaneamente despues va a ver un input para esto
            playerUnit.SetMovementSteps();
            Debug.Log("te puedes mover " + playerUnit.GetMovementSteps() + " veces"); // TODO: mostrar cantidad de movijmientos totales y los que quedan en pantalla
            currentAbility = abilitys[Random.Range(0, abilitys.Count)];
            Debug.Log("Te toco la habilidad: " + currentAbility); // TODO: animacion del dado y tambien mostrar habilidad en pantalla
            // TODO: en realidad habria que poner un imput para cada cosa, uno para tirar dados de habilidad dos para tirar dado de movimiento, tres para accion de moverse o de tirar habiliad y cancelar las mismas acciones antes de realisarlas
            FeedBackManager.Instance.ShowPossibleMoves(playerUnit.GetHeatMapNodes());
        }
    }
    
    // esta funcion es probisioria por que despues hay que hacer que un scriptableobjects contenga tus dados y de ahi sacar esta info
    private void ShowYoursDices()
    {
        for (int i = 0; i < unitPrefabs.Count; i++)
        {
            Instantiate(unitPrefabs[i]);
        }
    }
}

public enum GameState
{
    PLAYER_SETUP = 0,
    IA_SETUP = 1,
    PLAYER_TURN = 2
}