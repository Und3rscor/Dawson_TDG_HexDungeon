using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player player;
    GridManager gridManager;

    public bool start;

    static GameManager instance;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        gridManager = FindObjectOfType<GridManager>();
    }

    private void Update()
    {
        
    }

    public void MovePlayer(Vector2Int displacement)
    {
        gridManager.MoveObjectOnGrid(player.gameObject, displacement);
    }

    public void EndTurn()
    {
        player.ResetActionPoints();
    }
}
