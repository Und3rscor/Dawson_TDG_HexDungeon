using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    EntityMovement playerEMScript;
    GridManager gridManager;

    public bool start;

    static GameManager instance;

    private void Start()
    {
        playerEMScript = FindObjectOfType<Player>().gameObject.GetComponent<EntityMovement>();
        gridManager = FindObjectOfType<GridManager>();
    }

    private void Update()
    {
        
    }

    //Tells the gridmanager to move the player
    public void MovePlayer(Vector2Int displacement)
    {
        gridManager.MoveObjectOnGrid(playerEMScript.gameObject, displacement);
    }

    //Ends the player turn
    public void EndTurn()
    {
        //Resets player AP
        playerEMScript.ResetActionPoints();
    }
}
