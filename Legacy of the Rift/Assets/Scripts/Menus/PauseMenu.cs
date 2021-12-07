using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsUI;
    public GameObject instructionsUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void ShowSettings()
    {
        settingsUI.SetActive(true);
        instructionsUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }

    public void HideSettings()
    {
        settingsUI.SetActive(false);
        instructionsUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void ShowInstructions()
    {
        instructionsUI.SetActive(true);
        settingsUI.SetActive(false);
        pauseMenuUI.SetActive(false);
    }

    public void HideInstructions()
    {
        settingsUI.SetActive(false);
        instructionsUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game!");
        Application.Quit();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
