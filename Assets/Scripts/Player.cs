using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    Entity entity;

    private void Start()
    {
        entity = GetComponent<Entity>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        entity.PlayOnYourTurn();
    }
}
