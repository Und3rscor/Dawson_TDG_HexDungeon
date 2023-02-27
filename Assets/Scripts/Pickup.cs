using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (tag == "PickUp")
            {
                Entity playerEScript = other.GetComponent<Entity>();
                playerEScript.Health = playerEScript.StartingHealth;
                gameManager.score += 50;
                Destroy(gameObject);
            }

            if (tag == "EndZone")
            {
                Debug.Log("Reached EndZone");
                gameManager.LoadNext();
            }
        }
    }
}
