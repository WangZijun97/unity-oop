using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Player : Unit
{
    [SerializeField] private GameObject playerTurnUI;
    [SerializeField] private GameObject notPlayerTurnUI;
    [SerializeField] private Dropdown actionField;
    [SerializeField] private Text playerHPText;
    [SerializeField] private Text playerAtkText;

    public override int MaxHealth
    {
        get { return base.MaxHealth; }
        set
        {
            base.MaxHealth = value;
            UpdateHealthText();
        }
    }

    public override int Health
    {
        get { return base.Health; }
        set
        {
            base.Health = value;
            UpdateHealthText();
        }
    }

    public override int AttackStrength
    {
        get => base.AttackStrength;
        set
        {
            base.AttackStrength = value;
            UpdateAttackStrengthText();
        }
    }

    protected override void Awake()
    {
        maxHealth = 30;
        initialAttackStrength = 5;
        actions.Add(Heal);
        base.Awake();
    }

    private void Start()
    {
        actionField.AddOptions(new List<string>(
            actions.Select<Action<Unit>, string>(action => action.Method.Name)));
    }

    public override void TurnStart()
    {
        Debug.Log($"[Player] {name} turn start");
        playerTurnUI.SetActive(true);
        notPlayerTurnUI.SetActive(false);
    }

    public override void TurnEnd()
    {
        base.TurnEnd();
        playerTurnUI.SetActive(false);
        notPlayerTurnUI.SetActive(true);
    }

    public void Heal(Unit target)
    {
        string infoString = $"{name} healed {target.name} for 5hp";
        Debug.Log(infoString);
        gameManager.DisplayActionText(infoString);

        target.Health += 5;
        TurnEnd();
    }

    public override bool Die()
    {
        gameManager.PlayerDies();
        return base.Die();
    }

    protected void UpdateHealthText()
    {
        playerHPText.text = $"Your HP: {health} / {maxHealth}";
    }

    protected void UpdateAttackStrengthText()
    { 
        playerAtkText.text = $"Attack: {attackStrength}"; 
    }
}
