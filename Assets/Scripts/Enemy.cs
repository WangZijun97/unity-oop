using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Enemy : Unit
{
    private Player target;

    protected override void Awake()
    {
        base.Awake();
        target = GameObject.FindObjectOfType<Player>();
    }
    public override void TurnStart()
    {
        Debug.Log($"[Enemy] {name} turn start");
        actions[Random.Range(0, actions.Count)].Invoke(target);
    }

}
