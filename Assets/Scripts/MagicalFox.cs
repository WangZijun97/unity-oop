using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalFox : Enemy
{
    protected override void Awake()
    {
        maxHealth = 8;
        initialAttackStrength = 2;
        actions.Add(Chill);
        base.Awake();
    }

    public void Chill(Unit target)
    {
        Debug.Log($"{name} chilled {target.name}, {target.name} loses 1 attack strength");
        target.AttackStrength -= 1;
        target.GetHit(Mathf.RoundToInt(AttackStrength / 2.0f));
        TurnEnd();
    }
}
