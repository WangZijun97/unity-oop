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
    public int AttackStrength
    {
        get { return attackStrength; }
        set { attackStrength = value; }
    }

    protected List<Action<Unit>> actions = new List<Action<Unit>>();

    private GameManager gameManager;

    protected virtual void Awake()
    {
        Health = maxHealth;
        AttackStrength = initialAttackStrength;
        actions.Add(Attack);
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public virtual void TurnStart()
    {
        Debug.Log($"{name} turn start");
        TurnEnd();
    }

    public virtual void TurnEnd()
    {
        Debug.Log($"{name} turn end");
        gameManager.TurnEndHandler(this);
    }

    // Represents a unit's basic attack
    // Modify to change basic attack
    public virtual void Attack(Unit target)
    {
        Debug.Log($"{name} attacked {target.name}");
        target.GetHit(AttackStrength);
        TurnEnd();
    }

    // Represents a unit getting hit
    // Modify for armor/resistance like effects
    public virtual int GetHit(int damage)
    {
        Health -= damage;
        if (Health < 1)
        {
            Die();
        }
        return damage;
    }

    // Represents death
    // Modify for guardian angel effects
    public virtual bool Die()
    {
        Destroy(gameObject);
        TurnEnd();
        gameManager.UnitDies(this);
        return true;
    }
}
