using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{

    private GameState state;

    public void Start()
    {
        ChangeState(GameState.STARTING_TILE);
    }

    public void Update()
    {
        if(state == GameState.STARTING_TILE)
        {
            // se puede programar drag an drop en un metodo
            if (Input.GetMouseButtonDown(0))
            {
                if ()
                {
                    
                }
            }
        }
    }


    private void ChangeState(GameState currentState)
    {
        state = currentState;
        
        if (state == GameState.STARTING_TILE)
        {
            // generar botones
            // generar feedback para darle a entender al jugador que tiene que poner a su personaje
        }
    }
}

public enum GameState
{
    STARTING_TILE = 0
}