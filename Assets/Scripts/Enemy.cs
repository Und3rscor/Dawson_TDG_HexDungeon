using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private bool randomMovement;
    public bool RandomMovement
    {
        get { return randomMovement; }
    }

    Entity entity;
    GameManager gameManager;

    private void Start()
    {
        entity = GetComponent<Entity>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (!gameManager.PlayerTurn)
        {
            entity.PlayOnYourTurn();
        }
    }
}
