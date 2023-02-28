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
    public float CurrentTimer
    {
        get { return currentTimer; }
    }

    private int deaths;
    public int Deaths { get { return deaths; } }

    public bool start;

    private int currentScene;
    public int CurrentScene
    {
        get { return currentScene; }
    }

    private bool endTurnAvailable;
    public bool EndTurnAvailable
    {
        get { return endTurnAvailable; }
    }

    public GameObject playerObj;
    Entity playerEScript;

    static GameManager instance;

    private void Start()
    {
        playerEScript = GameObject.Find("Player").GetComponent<Entity>();
        playerObj = playerEScript.gameObject;

        endTurnAvailable = true;
        deaths = 0;

        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (playerObj == null)
        {
            playerEScript = GameObject.Find("Player").GetComponent<Entity>();
            playerObj = playerEScript.gameObject;
        }

        if (start)
            Timer();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LoadNext();
        }
    }

    void Timer()
    {
        currentTimer += Time.deltaTime;
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
        deaths++;
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Destroy(gameObject);
    }
}
