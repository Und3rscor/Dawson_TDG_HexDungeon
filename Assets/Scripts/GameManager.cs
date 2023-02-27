using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    private int previousScore = 0;

    private float currentTimer = 0;
    public bool start;

    private int currentScene;

    private bool endTurnAvailable;
    public bool EndTurnAvailable
    {
        get { return endTurnAvailable; }
    }

    Entity playerEScript;
    GridManager gridManager;

    static GameManager instance;

    private void Start()
    {
        playerEScript = FindObjectOfType<Player>().gameObject.GetComponent<Entity>();
        gridManager = FindObjectOfType<GridManager>();

        endTurnAvailable = true;

        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (start)
            Timer();
    }

    void Timer()
    {
        currentTimer += Time.deltaTime;
    }

    //Tells the gridmanager to move the player
    public void MovePlayer(Vector2Int displacement)
    {
        gridManager.MoveObjectOnGrid(playerEScript.gameObject, displacement);
    }

    //Ends the player turn
    public void EndTurn()
    {
        endTurnAvailable = false;

        //Make enemies attack
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            enemy.AttackPlayer();
        }

        //Then reset the player's AP
        Invoke("ResetPlayerAP", 1f);
    }

    private void ResetPlayerAP()
    {
        playerEScript.ResetActionPoints();
        endTurnAvailable = true;
    }

    //Scene management stuff
    public void LoadNext()
    {
        previousScore = score;
        start = false;
        currentScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        score = previousScore;
        start = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("Start");
        Destroy(gameObject);
    }
}
