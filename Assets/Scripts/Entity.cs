using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Entity : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private int startingHealth;
    public int StartingHealth
    {
        get { return startingHealth; }
    }

    private int health;
    public int Health
    {
        get { return health; }
    }

    [Header("Action Points")]
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
    }

    [Header("Movement")]
    public float speed;

    private bool doneMoving;
    public bool DoneMoving
    {
        get { return doneMoving; }
    }

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

    private bool walkablesTilesChecked;

    GridManager gridManager;

    Slider[] sliders;
    Slider hpSlider, apSlider;
    TextMeshProUGUI apCounter;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();

        apCounter = GetComponentInChildren<TextMeshProUGUI>();
        sliders = GetComponentsInChildren<Slider>();

        hpSlider = sliders[0];
        hpSlider.maxValue = startingHealth;
        health = startingHealth;

        apSlider = sliders[1];
        apSlider.maxValue = startingActionPoints;
        actionPoints = startingActionPoints;

        targetPos = this.transform.position;
        walkablesTilesChecked = false;        
    }

    private void Update()
    {        
        HPTracker();
    }

    public void PlayOnYourTurn()
    {
        Move();

        APTracker();
    }

    private void Move()
    {
        if (!doneMoving && this.transform.position != targetPos)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPos, speed * Time.deltaTime);
        }

        if (this.transform.position == targetPos)
        {
            doneMoving = true;

            if (!walkablesTilesChecked && tag == "Player")
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

                gridManager.ResetTiles();
            }
        }
    }

    public void ResetActionPoints()
    {
        actionPoints = startingActionPoints;
        walkablesTilesChecked = false;
    }

    private void HPTracker()
    {
        hpSlider.value = health;
    }

    private void APTracker()
    {
        apSlider.value = actionPoints;
        apCounter.text = actionPoints.ToString();
    }
}
