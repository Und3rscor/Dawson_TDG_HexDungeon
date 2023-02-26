using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Entity playerEScript;
    GridManager gridManager;

    private bool playerTurn;
    public bool PlayerTurn
    {
        get { return playerTurn; }
    }

    static GameManager instance;

    private void Start()
    {
        playerEScript = FindObjectOfType<Player>().gameObject.GetComponent<Entity>();
        gridManager = FindObjectOfType<GridManager>();

        playerTurn = true;

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        
    }

    //Tells the gridmanager to move the player
    public void MovePlayer(Vector2Int displacement)
    {
        gridManager.MoveObjectOnGrid(playerEScript.gameObject, displacement);
    }

    //Ends the player turn
    public void EndTurn()
    {
        if (playerTurn)
        {
            //Resets enemy AP
            playerTurn = false;
        }
        else
        {
            //Resets player AP
            playerEScript.ResetActionPoints();
            playerTurn = true;
        }        
    }
}
