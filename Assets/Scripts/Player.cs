using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Player : MonoBehaviour
{
    private bool tilesReset;

    Slider apSlider;
    TextMeshProUGUI apCounter;
    EntityMovement emScript;

    // Start is called before the first frame update
    private void Start()
    {
        apCounter = GetComponentInChildren<TextMeshProUGUI>();
        apSlider = GetComponentInChildren<Slider>();
        emScript = GetComponent<EntityMovement>();
        apSlider.maxValue = emScript.StartingActionPoins;
    }

    // Update is called once per frame
    private void Update()
    {
        APTracker();
    }

    private void APTracker()
    {
        apSlider.value = emScript.ActionPoints;
        apCounter.text = emScript.ActionPoints.ToString();
    }
}
