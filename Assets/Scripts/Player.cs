using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    private void Awake()
    {
        maxHealth = 20;
        initialAttackStrength = 5;
        actions.Add(Heal);
    }

    public void Heal(Unit target)
    {
        target.Health += 5;
    }
}
