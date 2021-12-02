using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject instructionsUI;
    public void PlayGame()
    {
        SceneManager.LoadScene("Intro_BeforeAttack");
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

    public void ShowInstructions()
    {
        instructionsUI.SetActive(true);
    }

    public void HideInstructions()
    {
        instructionsUI.SetActive(false);
    }
}
