using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject openingUi, gameUi;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("GameQuit");
        Application.Quit();
    }

    /*public void StartGame()
    {
        //Removes the Opening UI
        openingUi.SetActive(false);

        //Add the game Ui
        gameUi.SetActive(true);
    }*/

    public void MainMenu()
    {
        GameObject.Find("GameManager").GetComponent<SceneManagementScript>().GoBackToMainMenu();
    }
}
