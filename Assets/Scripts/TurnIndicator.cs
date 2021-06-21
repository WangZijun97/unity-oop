using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnIndicator : MonoBehaviour
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.unitOnAction)
        {
            gameObject.SetActive(true);
            Unit unit = gameManager.unitOnAction;

            // Set indicator to be 1.5m up from top of center of unit
            transform.position = unit
                .GetComponent<BoxCollider>()
                .ClosestPointOnBounds(unit.transform.position + Vector3.up * 20) +
                Vector3.up * 1.5f;
        } 
        else
        {
            gameObject.SetActive(false);
        }
    }
}
