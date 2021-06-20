using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 10;
    [SerializeField] protected int health;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    [SerializeField] protected int initialAttackStrength = 2;
    [SerializeField] protected int attackStrength;
    [SerializeField]
    public int AttackStrength
    {
        get { return attackStrength; }
        set { attackStrength = value; }
    }

    protected List<Action<Unit>> actions = new List<Action<Unit>>();

    private void Awake()
    {
        Health = maxHealth;
        AttackStrength = initialAttackStrength;
        actions.Add(Attack);
    }

    // Represents a unit's basic attack
    // Modify to change basic attack
    public virtual void Attack(Unit target)
    {
        target.GetHit(AttackStrength);
    }

    // Represents a unit getting hit
    // Modify for armor/resistance like effects
    public virtual int GetHit(int damage)
    {
        Health -= damage;
        return damage;
    }
}
