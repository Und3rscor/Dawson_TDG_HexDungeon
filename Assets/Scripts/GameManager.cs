using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        
    }

    public void MovePlayer(Vector3 targetPos)
    {
        player.TargetPos = targetPos;
    }

    public void EndTurn()
    {
        player.ResetActionPoints();
    }
}
