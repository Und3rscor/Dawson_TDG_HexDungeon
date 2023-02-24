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

    private bool moving;

    private Vector3 targetPos;
    public Vector3 TargetPos
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
        targetPos = transform.position;

        apCounter = GetComponentInChildren<TextMeshProUGUI>();
        apSlider = GetComponentInChildren<Slider>();
        apSlider.maxValue = startingActionPoints;
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerControler();
        APTracker();
    }

    private void PlayerControler()
    {
        if (targetPos != transform.position)
        {
            if (!moving)
            {
                actionPoints--;
            }

            MovePlayer();
        }

        if (moving && targetPos == transform.position && actionPoints != 0)
        {
            moving = false;
        }
    }

    private void MovePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        moving = true;
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
