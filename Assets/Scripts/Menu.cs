using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject openingUi, gameUi;

    private void Start()
    {
        openingUi.SetActive(true);
        gameUi.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("GameQuit");
        Application.Quit();
    }

    public void StartGame()
    {
        //Tells the game manager to start the game
        GameObject.Find("GameManager").GetComponent<GameManager>().start = true;

        //Removes the Opening UI
        openingUi.SetActive(false);

        //Add the game Ui
        gameUi.SetActive(true);
    }

    public void MainMenu()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().GoBackToMainMenu();
    }
}
