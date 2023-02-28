using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    Entity playerEScript;
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
    public bool IsWithinWalkingDistance
    {
        get { return isWithinWalkingDistance;}
    }

    private Vector3Int currentCellPositon;
    public Vector3Int CurrentCellPositon
    {
        get { return currentCellPositon;}
    }

    private bool occupied;
    private GameObject occupiedBy;

    private void Start()
    {
        playerEScript = FindObjectOfType<Player>().gameObject.GetComponent<Entity>();
        playerObj = playerEScript.gameObject;

        grid = GetComponentInParent<Grid>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        isWithinWalkingDistance = false;

        currentCellPositon = grid.LocalToCell(transform.position);
    }

    //Checks for tiles within walking distance of an object based on their remaining AP
    public void CheckIfTileIsWithinWalkingDistance(Vector3 objPosition, GameObject obj)
    {
        //Checks if within distance of the obj and marks it accordingly
        if (!isWithinWalkingDistance && ((int)Vector3.Distance(this.transform.position, objPosition)/2) <= obj.GetComponent<Entity>().ActionPoints)
        {
            isWithinWalkingDistance = true;

            //if it's the player that's asking, mark the tile blue
            if (obj.tag == "Player")
            {
                ChangeBorderColor(Color.blue);
            }
        }
    }

    //Defines a target position for the player
    private void OnMouseDown()
    {
        if (playerEScript.DoneMoving && this.transform.position != new Vector3(playerObj.transform.position.x, 0, playerObj.transform.position.z))
        {
            if (isWithinWalkingDistance)
            {
                Vector2Int cellDisplacement = new Vector2Int(currentCellPositon.x, currentCellPositon.z);

                gameManager.MovePlayer(cellDisplacement);
                ChangeBorderColor(Color.white);
            }
            else if (occupied && playerEScript.ActionPoints >= 2 && Vector3.Distance(occupiedBy.transform.position, playerObj.transform.position) <= 3)
            {
                playerEScript.Attack(occupiedBy.transform);
                occupiedBy.GetComponent<Enemy>().Health -= playerEScript.AttackDamage;
            }
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

    public void ResetTile()
    {
        //Makes all the tiles black except the one the player is trying to reach
        if (!occupied && playerObj.GetComponent<Entity>().TargetPos != new Vector3(this.transform.position.x, playerObj.transform.position.y, this.transform.position.z))
        {
            ChangeBorderColor(Color.black);
            isWithinWalkingDistance = false;
        }
        else
        {
            isWithinWalkingDistance = false;
        }
    }

    private void SetColorToObjectColor(Collider other)
    {
        Color entityColor = other.GetComponent<MeshRenderer>().material.color;
        ChangeBorderColor(entityColor);
    }

    private void OnTriggerStay(Collider other)
    {
        //When the player stays on a tile, makes it green
        if (other.tag == "Player" && other.GetComponent<Entity>().DoneMoving)
        {
            ChangeBorderColor(Color.green);
            occupied = false;
        }
        
        if (other.tag == "Enemy" || other.tag == "Barrel")
        {
            SetColorToObjectColor(other);
            isWithinWalkingDistance = false;
            occupied = true;
            occupiedBy = other.gameObject;
        }

        if (other.tag == "PickUp" || other.tag == "EndZone")
        {
            SetColorToObjectColor(other);
            occupied = true;
        }
    }
}
