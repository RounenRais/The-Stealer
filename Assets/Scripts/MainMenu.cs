using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenu:MonoBehaviour
{
    public string startSceneName;
    public void StartGame()
    {
    SceneManager.LoadScene(startSceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
