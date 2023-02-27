using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Entity playerEScript = other.GetComponent<Entity>();
            playerEScript.Health = playerEScript.StartingHealth;
            Destroy(gameObject);
        }
    }
}
