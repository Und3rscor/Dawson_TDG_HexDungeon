using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    Player player;
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
        player = FindObjectOfType<Player>();
        grid = GetComponentInParent<Grid>();
        gameManager = FindObjectOfType<GameManager>();

        isWithinWalkingDistance = false;

        currentCellPositon = grid.LocalToCell(transform.position);
    }

    public void IsWithinWalkingDistance(Vector3 objPosition)
    {
        if (this.transform.position != objPosition && ((int)Vector3.Distance(this.transform.position, objPosition)/2) <= player.ActionPoints)
        {
            Debug.Log("WithinDistance");
            ChangeBorderColor(Color.blue);
            isWithinWalkingDistance = true;
        }
    }

    private void OnMouseDown()
    {
        if (isWithinWalkingDistance && player.DoneMoving && this.transform.position != new Vector3(player.transform.position.x, 0, player.transform.position.z))
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
        if (player.TargetPos != new Vector3(this.transform.position.x, player.transform.position.y, this.transform.position.z))
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
        if (other.tag == "Player" && player.DoneMoving)
        {
            ChangeBorderColor(Color.green);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ResetColor();
        }
    }
}
