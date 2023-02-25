using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        player = FindObjectOfType<Player>();
        grid = GetComponentInParent<Grid>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void OnMouseDown()
    {
        if (!player.Moving && this.transform.position != player.transform.position && player.ActionPoints != 0)
        {
            Vector3Int currentCellPositon = grid.LocalToCell(transform.position);
            Vector2Int cellDisplacement = new Vector2Int(currentCellPositon.x, currentCellPositon.z);

            gameManager.MovePlayer(cellDisplacement);
            ChangeBorderColor(Color.white);
            //Debug.Log(cellDisplacement.ToString());
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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !player.Moving)
        {
            ChangeBorderColor(Color.green);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ChangeBorderColor(Color.black);
        }
    }
}
