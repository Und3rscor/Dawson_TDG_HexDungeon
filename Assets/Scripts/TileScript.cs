using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnMouseDown()
    {
        gameManager.MovePlayer(new Vector3(this.transform.position.x, this.transform.position.y + .5f, this.transform.position.z));
        //Debug.Log(this.transform.position);
    }
}
