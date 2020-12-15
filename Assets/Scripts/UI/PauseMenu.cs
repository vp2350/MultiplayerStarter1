using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;
    public GameObject userInterface;
    void Update()
    {
        if (PlayerInfo.Instance.isDead) { return; }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        userInterface.SetActive(false);
        Time.timeScale = 0;
        pauseMenuUI.SetActive(true);
    }

    public void DeactivateMenu()
    {
        userInterface.SetActive(true);
        Time.timeScale = 1;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }
    //Load main game

    public void Pause()
    {

    }
    //Load Options
}
