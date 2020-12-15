using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectiveNum : InteractableScript
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] int objectiveNum;

    public override void Trigger()
    {
        if(objectiveNum >= 0 && objectiveNum < playerStats.objectives.Length)
        {
            playerStats.objectives[objectiveNum] = true;
            base.Trigger();
        }
        else
        {
            Debug.LogError("Error: Objective index out of bounds");
        }
    }
}
