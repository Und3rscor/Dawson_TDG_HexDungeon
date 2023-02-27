using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    Entity entity;

    private void Start()
    {
        entity = GetComponent<Entity>();
    }

    private void Update()
    {
        entity.PlayOnYourTurn();
    }
}
