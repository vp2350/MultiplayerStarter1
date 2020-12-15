using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalismanObjective : InteractableScript
{
    [SerializeField] PlayerStats playerStats;

    public override void Trigger()
    {
        playerStats.HasTransformAbility = true;
        base.Trigger();
    }
}
