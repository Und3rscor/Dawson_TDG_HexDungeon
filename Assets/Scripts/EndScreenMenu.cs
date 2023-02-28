using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndScreenMenu : MonoBehaviour
{
    GameManager gameManager;

    public GameObject scoreboard;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //Scoreboard changes
        scoreboard.GetComponent<TextMeshProUGUI>().text = "Time: " + gameManager.CurrentTimer.ToString("f2") + "     Score: " + gameManager.score + "     Deaths: " + (gameManager.Deaths);
    }
}
