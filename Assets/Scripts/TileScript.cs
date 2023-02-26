using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    EntityMovement playerEMScript;
    GameObject playerObj;
    Grid grid;
    GameManager gameManager;

    [SerializeField]
    private bool walkable;

    public bool Walkable
    {
        get { return walkable; }
    }

    private bool isWithinWalkingDistance;

    private Vector3Int currentCellPositon;

    private void Start()
    {
        playerEMScript = FindObjectOfType<Player>().gameObject.GetComponent<EntityMovement>();
        playerObj = playerEMScript.gameObject;

        grid = GetComponentInParent<Grid>();
        gameManager = FindObjectOfType<GameManager>();

        isWithinWalkingDistance = false;

        currentCellPositon = grid.LocalToCell(transform.position);
    }

    //Checks for tiles within walking distance of an object based on their remaining AP
    public void IsWithinWalkingDistance(Vector3 objPosition, GameObject obj)
    {
        //Checks if within distance of the player and marks it accordingly
        if (this.transform.position != objPosition && ((int)Vector3.Distance(this.transform.position, objPosition)/2) <= obj.GetComponent<EntityMovement>().ActionPoints)
        {
            isWithinWalkingDistance = true;

            if (obj.tag == "Player")
            {
                ChangeBorderColor(Color.blue);
            } 
        }
    }

    //Defines a target position for the player
    private void OnMouseDown()
    {
        if (isWithinWalkingDistance && playerEMScript.DoneMoving && this.transform.position != new Vector3(playerObj.transform.position.x, 0, playerObj.transform.position.z))
        {
            Vector2Int cellDisplacement = new Vector2Int(currentCellPositon.x, currentCellPositon.z);

            gameManager.MovePlayer(cellDisplacement);
            ChangeBorderColor(Color.white);
        }
    }

    private void ChangeBorderColor(Color color)
    {
        foreach (MeshRenderer r in GetComponentsInChildren<MeshRenderer>())
        {
            if (r.tag == "Borders")
            {
                r.material.SetColor("_Color", color);
            }
        }
    }

    public void ResetColor()
    {
        //Makes all the tiles black except the one the player is trying to reach
        if (playerObj.GetComponent<EntityMovement>().TargetPos != new Vector3(this.transform.position.x, playerObj.transform.position.y, this.transform.position.z))
        {
            ChangeBorderColor(Color.black);
            isWithinWalkingDistance = false;
        }
        else
        {
            isWithinWalkingDistance = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //When the player stays on a tile, makes it green
        if (other.GetComponent<EntityMovement>().DoneMoving)
        {
            if (other.tag == "Player")
            {
                ChangeBorderColor(Color.green);
            }
            
            if (other.tag == "Enemy")
            {
                ChangeBorderColor(Color.red);
            }

            if (other.tag == "Obstacle")
            {
                ChangeBorderColor(Color.clear);
            }
        }
    }
}
