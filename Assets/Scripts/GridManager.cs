using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private Vector2Int gridSize;

    [SerializeField]
    private Vector2Int startPoint;

    [SerializeField]
    // private List<GridCell> _fullGrid = new List<GridCell>();
    private Dictionary<Vector2Int, TileScript> fullGrid = new Dictionary<Vector2Int, TileScript>();

    private Vector3 objTargetGridPos, objTargetPos;

    private int tileID = -1;
    TileScript[] availableTilesToEnemy;

    private void Awake()
    {
        foreach (Transform tf in transform)
        {
            fullGrid.Add(ToGridCoord(grid.LocalToCell(tf.position)), tf.GetComponent<TileScript>());
        }
    }

    //Converts a position into a grid position
    private Vector2Int ToGridCoord(Vector3Int vect)
    {
        return new Vector2Int(vect.x, vect.z);
    }

    public Vector2Int MoveObjectOnGrid(GameObject obj, Vector2Int displacement)
    {
        Vector3Int currentPos = grid.WorldToCell(obj.transform.position);
        Vector3Int targetPos = new Vector3Int(displacement.x, 0, displacement.y);

        if (MoveObjectToGridPosition(obj, targetPos))
        {
            return ToGridCoord(targetPos);
        }
        return ToGridCoord(currentPos);
    }

    public bool MoveObjectToGridPosition(GameObject obj, Vector3Int targetPos)
    {
        if (Walkable(ToGridCoord(targetPos)))
        {
            objTargetGridPos = grid.GetCellCenterLocal(targetPos);
            objTargetPos = new Vector3(objTargetGridPos.x, obj.transform.position.y, objTargetGridPos.z);

            obj.GetComponent<Entity>().TargetPos = objTargetPos;
            return true;
        }

        return false;
    }

    private bool Walkable(Vector2Int newPosition)
    {
        if (fullGrid.ContainsKey(newPosition))
        {
            TileScript tile = fullGrid[newPosition];
            return tile.Walkable;
        }
        else
        {
            return false;
        }
    }

    public void CheckIfTileIsWithinWalkingDistance(Vector3 objPosition, GameObject obj)
    {
        foreach (TileScript tile in GetComponentsInChildren<TileScript>())
        {
            tile.CheckIfTileIsWithinWalkingDistance(objPosition, obj);
            Debug.Log("Tile checked");

            if (obj.tag == "Enemy" && obj.GetComponent<Enemy>().RandomMovement && tile.IsWithinWalkingDistance)
            {
                tileID += 1;
                Debug.Log("Adding tile " + tile.CurrentCellPositon + " to array");
                availableTilesToEnemy[tileID] = tile;
            }
        }

        /*if (obj.tag == "Enemy" && obj.GetComponent<Enemy>().RandomMovement)
        {
            MoveEnemyToRandomGridPosition(obj);
        }*/
    }

    public void MoveEnemyToRandomGridPosition(GameObject enemy)
    {
        int randomlyChosenTile = UnityEngine.Random.Range(0, availableTilesToEnemy.Length);

        Vector3Int chosenTileCellPosition = availableTilesToEnemy[randomlyChosenTile].CurrentCellPositon;

        Vector2Int chosenTargetPos = new Vector2Int(chosenTileCellPosition.x, chosenTileCellPosition.z);

        MoveObjectOnGrid(enemy, chosenTargetPos);
        tileID = -1;
        Array.Clear(availableTilesToEnemy, 0, availableTilesToEnemy.Length);
    }

    public void ResetTiles()
    {
        foreach (TileScript tiles in GetComponentsInChildren<TileScript>())
        {
            tiles.Reset();
        }
    }
}
