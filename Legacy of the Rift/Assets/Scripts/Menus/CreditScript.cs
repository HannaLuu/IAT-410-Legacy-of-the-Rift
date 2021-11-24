using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScript : MonoBehaviour
{
    public void LoadMenu()
    {
        SceneManager.LoadScene("Start");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Start");
        }
    }
}
