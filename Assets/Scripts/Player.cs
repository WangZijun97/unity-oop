using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    [SerializeField] private Dropdown actionField;

    protected override void Awake()
    {
        maxHealth = 20;
        initialAttackStrength = 5;
        actions.Add(Heal);
        base.Awake();
    }

    private void Start()
    {
        actionField.AddOptions(new List<string>(actions.Select<Action<Unit>, string>(action => action.Method.Name)));
    }

    public void Heal(Unit target)
    {
        target.Health += 5;
    }
}
