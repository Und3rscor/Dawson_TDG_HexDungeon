using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    public float speed;

    public int startingActionPoints;
    private int actionPoints;
    public int ActionPoints
    {
        get { return actionPoints;}

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

    private bool walkablesTilesChecked, tilesReset;

    private Vector3 targetPos;
    public Vector3 TargetPos
    {
        get { return targetPos; }

        set
        {
            targetPos = value;
            distanceOfTargetPos = ((int)Vector3.Distance(this.transform.position, value))/2;
            actionPoints -= distanceOfTargetPos;
        }
    }

    private int distanceOfTargetPos;
    public int DistanceOfTargetPos
    {
        get { return distanceOfTargetPos; }
    }

    Slider apSlider;
    TextMeshProUGUI apCounter;
    GridManager gridManager;

    // Start is called before the first frame update
    private void Start()
    {
        actionPoints = startingActionPoints;
        doneMoving = true;
        targetPos = this.transform.position;
        walkablesTilesChecked = false;

        gridManager = FindObjectOfType<GridManager>();
        apCounter = GetComponentInChildren<TextMeshProUGUI>();
        apSlider = GetComponentInChildren<Slider>();
        apSlider.maxValue = startingActionPoints;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();

        APTracker();
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
                gridManager.CheckIfTileIsWithinWalkingDistance(new Vector3(this.transform.position.x, 0, this.transform.position.z));
                walkablesTilesChecked = true;
                tilesReset = false;
            }
        }
        else
        {
            doneMoving = false;
            if (!tilesReset)
            {
                gridManager.ResetTileColors();
                Debug.Log("Tile colors reset");
                tilesReset = true;
                walkablesTilesChecked = false;
            }
        }
    }

    public void ResetActionPoints()
    {
        actionPoints = startingActionPoints;
        walkablesTilesChecked = false;
    }

    private void APTracker()
    {
        apSlider.value = actionPoints;
        apCounter.text = actionPoints.ToString();
    }
}
