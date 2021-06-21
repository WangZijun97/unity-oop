using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button actionButton;
    [SerializeField] private Dropdown actionDropdown;
    [SerializeField] private GameObject playerDeathScreen;

    [SerializeField] private Player player;
    [SerializeField] public Unit target;

    private List<Unit> allUnits;
    [SerializeField] private List<Unit> allUnitsInTurnOrder;
    [SerializeField] public Unit unitOnAction;

    private bool isGameRunning;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        actionButton.onClick.AddListener(ActionButtonHandler);
        allUnits = new List<Unit>();
        allUnits.AddRange(GameObject.FindObjectsOfType<Unit>());

        isGameRunning = true;

        // Randomize turn order
        allUnitsInTurnOrder = new List<Unit>(allUnits.OrderBy(unit => Random.Range(0f, 1f)));
        unitOnAction = allUnitsInTurnOrder[0];
        unitOnAction.TurnStart();
    }

    // starts the next turn after a turn ends
    public void TurnEndHandler(Unit unit)
    {
        if (!unit.Equals(unitOnAction) || !isGameRunning)
        {
            return;
        }

        int index = allUnitsInTurnOrder.IndexOf(unitOnAction);
        unitOnAction = allUnitsInTurnOrder[(index + 1) % allUnitsInTurnOrder.Count];
        unitOnAction.TurnStart();
    }

    public void UnitDies(Unit unit)
    {
        // temp set unit on action to previous unit if dying unit was on turn
        if (unit.Equals(unitOnAction))
        {
            int index = allUnitsInTurnOrder.IndexOf(unitOnAction);
            unitOnAction = allUnitsInTurnOrder[(index - 1) % allUnitsInTurnOrder.Count];
        }
        
        allUnitsInTurnOrder.Remove(unit);
    }

    public void ActionButtonHandler()
    {
        typeof(Player).GetMethod(actionDropdown.captionText.text).Invoke(player, new object[] { target });
    }

    public void PlayerDies()
    {
        playerDeathScreen.SetActive(true);
        isGameRunning = false;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
    }
}
