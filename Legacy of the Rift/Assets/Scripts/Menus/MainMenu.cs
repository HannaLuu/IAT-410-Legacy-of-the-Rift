using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Temp_Instructions");
    }

    public void PlayShore()
    {
        SceneManager.LoadScene("Shore_Level");
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
}
