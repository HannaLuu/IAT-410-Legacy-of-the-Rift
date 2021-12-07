using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionsUI;
    public GameObject settingsUI;

    public void PlayGame()
    {
        SceneManager.LoadScene("Intro");
    }

    public void QuitGame()
    {
        Debug.Log("Application Quit Running!");
        Application.Quit();
    }

    public void PlayCredits()
    {
        //SceneManager.LoadScene("MenuCredits");
    }

    public void ShowSettings()
    {
        settingsUI.SetActive(true);
        instructionsUI.SetActive(false);
    }

    public void HideSettings()
    {
        settingsUI.SetActive(false);
        instructionsUI.SetActive(false);
    }

    public void ShowInstructions()
    {
        instructionsUI.SetActive(true);
    }

    public void HideInstructions()
    {
        instructionsUI.SetActive(false);
    }
}
