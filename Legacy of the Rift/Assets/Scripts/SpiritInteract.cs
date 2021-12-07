using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class SpiritInteract : MonoBehaviour
{
    public GameObject interactKey;

    public Flowchart flowchart;

    public WaveSpawner waveManager;

    public GameObject levelObstacle;

    private bool canInteract = false;

    public GameObject enemiesLeftText, wavesCompleteText;

    // Start is called before the first frame update
    void Start()
    {
        interactKey.SetActive(false);
        canInteract = false;
        levelObstacle.SetActive(true);

        enemiesLeftText.SetActive(true);
        wavesCompleteText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(waveManager.allWavesCompleted == true)
        {
            levelObstacle.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.F) && canInteract && waveManager.allWavesCompleted == true)
        {
            flowchart.ExecuteBlock("Waves Complete");
        }
        if (Input.GetKeyDown(KeyCode.F) && canInteract && waveManager.allWavesCompleted == false)
        {
            flowchart.ExecuteBlock("Waves Not Complete");
        }

        if (waveManager.allWavesCompleted == true)
        {
            enemiesLeftText.SetActive(false);
            wavesCompleteText.SetActive(true);
        }

        if (waveManager.allWavesCompleted == false)
        {
            enemiesLeftText.SetActive(true);
            wavesCompleteText.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Lokir" || player.tag == "Halvar" || player.tag == "Ursa")
        {
            interactKey.SetActive(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.tag == "Lokir" || player.tag == "Halvar" || player.tag == "Ursa")
        {
            interactKey.SetActive(false);
            canInteract = false;
        }
    }
}
