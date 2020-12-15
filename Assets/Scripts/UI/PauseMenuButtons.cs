using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    public void LoadGameScene(string SceneName)
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
