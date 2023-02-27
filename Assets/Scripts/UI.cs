using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    GameManager gameManager;
    Button button;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        button = GetComponentInChildren<Button>();
    }

    private void Update()
    {
        if (gameManager.EndTurnAvailable)
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }
    }

    public void EndTurn()
    {
        gameManager.EndTurn();
    }
}
