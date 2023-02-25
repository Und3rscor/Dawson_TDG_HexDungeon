using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class SceneManagementScript : MonoBehaviour
{
    public int currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LoadNext()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(currentScene);
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadEndScenePrematurely()
    {
        SceneManager.LoadScene("End");
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("Start");
        Destroy(gameObject);
    }
}
