using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class EntityMovement : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private int startingActionPoints;
    public int StartingActionPoins
    {
        get { return startingActionPoints; }
    }

    private int actionPoints;
    public int ActionPoints
    {
        get { return actionPoints; }

        set
        {
            actionPoints = value;
        }
    }

    private bool doneMoving;
    public bool DoneMoving
    {
        get { return doneMoving; }
    }

    private bool walkablesTilesChecked;

    private Vector3 targetPos;
    public Vector3 TargetPos
    {
        get { return targetPos; }

        set
        {
            targetPos = value;
            distanceOfTargetPos = ((int)Vector3.Distance(this.transform.position, value)) / 2;
            actionPoints -= distanceOfTargetPos;
        }
    }

    private int distanceOfTargetPos;
    public int DistanceOfTargetPos
    {
        get { return distanceOfTargetPos; }
    }

    GridManager gridManager;

    private void Start()
    {
        actionPoints = startingActionPoints;
        doneMoving = true;
        targetPos = this.transform.position;
        walkablesTilesChecked = false;

        gridManager = FindObjectOfType<GridManager>();
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if (!doneMoving && this.transform.position != targetPos)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);
        }

        if (this.transform.position == targetPos)
        {
            doneMoving = true;

            if (!walkablesTilesChecked)
            {
                gridManager.CheckIfTileIsWithinWalkingDistance(new Vector3(this.transform.position.x, 0, this.transform.position.z), this.gameObject);
                walkablesTilesChecked = true;
            }
        }
        else
        {
            doneMoving = false;
            if (walkablesTilesChecked)
            {
                walkablesTilesChecked = false;

                if (this.gameObject.tag == "Player")
                {
                    gridManager.ResetTileColors();
                }
            }
        }
    }

    public void ResetActionPoints()
    {
        actionPoints = startingActionPoints;
        walkablesTilesChecked = false;
    }
}
