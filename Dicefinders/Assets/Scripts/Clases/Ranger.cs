using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranger : Unit
{
    [Header("Stats")]
    [SerializeField] private int health = 14;
    [SerializeField] private int defence = 0;


    public override void Spawn(Unit selectedPrefab, Node spawnNode)
    {
        base.Spawn(selectedPrefab, spawnNode);

        Health = health;
        Defence = defence;
        MovementSteps = 0;
    }
}
