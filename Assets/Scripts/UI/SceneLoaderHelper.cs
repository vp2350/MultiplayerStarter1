using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderHelper : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }

    public void LoadSceneAdditive(string sceneName)
    {
        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }

    public void UnloadScene(string sceneName)
    {
        if (sceneName != null)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }

}
