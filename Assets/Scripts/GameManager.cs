using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button actionButton;
    [SerializeField] private Dropdown actionDropdown;

    [SerializeField] private Player player;
    [SerializeField] private Unit target;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        actionButton.onClick.AddListener(ActionButtonHandler);
    }

    public void ActionButtonHandler()
    {
        typeof(Player).GetMethod(actionDropdown.captionText.text).Invoke(player, new object[] { target });
    }
}
