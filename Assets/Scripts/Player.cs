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

    private bool moving;
    public bool Moving
    {
        get { return moving; }
    }

    private Vector3Int targetPos;
    public Vector3Int TargetPos
    {
        set
        {
            if (!moving && targetPos != value)
            {
                targetPos = value;
            }
        }
    }

    Slider apSlider;
    TextMeshProUGUI apCounter;

    // Start is called before the first frame update
    private void Start()
    {
        actionPoints = startingActionPoints;
        targetPos = Vector3Int.FloorToInt(transform.position);

        apCounter = GetComponentInChildren<TextMeshProUGUI>();
        apSlider = GetComponentInChildren<Slider>();
        apSlider.maxValue = startingActionPoints;
    }

    // Update is called once per frame
    private void Update()
    {
        //PlayerControler();
        APTracker();
    }

    private void PlayerControler()
    {
        if (targetPos != Vector3Int.FloorToInt(transform.position))
        {
            if (!moving)
            {
                actionPoints--;
            }

            //Move
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            moving = true;
        }

        if (moving && targetPos == Vector3Int.FloorToInt(transform.position) && actionPoints != 0)
        {
            moving = false;
        }
    }

    public void ResetActionPoints()
    {
        actionPoints = startingActionPoints;
    }

    private void APTracker()
    {
        apSlider.value = actionPoints;
        apCounter.text = actionPoints.ToString();
    }
}
