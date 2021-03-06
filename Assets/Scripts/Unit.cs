using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // ENCAPSULATION
    [SerializeField] protected int maxHealth = 10;
    public virtual int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value < 0 ? 0 : value;
        }
    }

    // ENCAPSULATION
    [SerializeField] protected int health;
    public virtual int Health
    {
        get { return health; }
        set
        {
            health = value < 0 ? 0 : 
                value > maxHealth ? maxHealth : value;
        }
    }

    [SerializeField] protected int initialAttackStrength = 2;

    // ENCAPSULATION
    [SerializeField] protected int attackStrength;
    public virtual int AttackStrength
    {
        get { return attackStrength; }
        set { attackStrength = value < 1 ? 1 : value; }
    }

    protected List<Action<Unit>> actions = new List<Action<Unit>>();

    protected GameManager gameManager;

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
        IEnumerator TurnEndCoroutine()
        {
            Debug.Log($"{name} turn end");
            yield return new WaitForSeconds(gameManager.TurnTimer);
            gameManager.TurnEndHandler(this);
        }

        StartCoroutine(TurnEndCoroutine());
        
    }

    // Represents a unit's basic attack
    // Modify to change basic attack
    public virtual void Attack(Unit target)
    {
        string infoString = $"{name} attacked {target.name}";
        Debug.Log(infoString);
        gameManager.DisplayActionText(infoString);

        target.GetHit(AttackStrength);
        TurnEnd();
    }

    // Represents a unit getting hit
    // Modify for armor/resistance like effects
    public virtual int GetHit(int damage)
    {
        string infoString = $"{name} got hit for {damage} dmg";
        Debug.Log(infoString);
        gameManager.DisplayAdditionalActionText(infoString);

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
        gameManager.UnitDies(this);
        TurnEnd();
        return true;
    }
}
