using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button actionButton;
    [SerializeField] private Dropdown actionDropdown;

    [SerializeField] private Player player;
    [SerializeField] public Unit target;

    private List<Unit> allUnits;
    [SerializeField] private List<Unit> allUnitsInTurnOrder;
    [SerializeField] public Unit unitOnAction;
    private int turnTracker;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        actionButton.onClick.AddListener(ActionButtonHandler);
        allUnits = new List<Unit>();
        allUnits.AddRange(GameObject.FindObjectsOfType<Unit>());

        // Randomize turn order
        allUnitsInTurnOrder = new List<Unit>(allUnits.OrderBy(unit => Random.Range(0f, 1f)));
        turnTracker = 0;
        unitOnAction = allUnitsInTurnOrder[turnTracker];
        unitOnAction.TurnStart();
    }

    public void TurnEndHandler(Unit unit)
    {
        if (!unit.Equals(unitOnAction))
        {
            return;
        }

        turnTracker++;
        if (turnTracker >= allUnitsInTurnOrder.Count)
        {
            turnTracker = 0;
        }
        unitOnAction = allUnitsInTurnOrder[turnTracker];
        unitOnAction.TurnStart();
    }

    public void UnitDies(Unit unit)
    {
        allUnitsInTurnOrder.Remove(unit);
    }

    public void ActionButtonHandler()
    {
        typeof(Player).GetMethod(actionDropdown.captionText.text).Invoke(player, new object[] { target });
    }
}
