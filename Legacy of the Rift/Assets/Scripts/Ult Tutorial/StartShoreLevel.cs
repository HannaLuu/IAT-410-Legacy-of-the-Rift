using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class StartShoreLevel : MonoBehaviour
{
    public Flowchart flowchart;

    public GameObject waveManager, waveSpawnPoints, playerHandler, kids;

    private bool levelStarted = false;

    private void Start()
    {
        levelStarted = false;
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (levelStarted == false)
        {
            if (player.tag == "Lokir" || player.tag == "Halvar" || player.tag == "Ursa")
            {
                playerHandler.GetComponent<PlayerZeal>().overzealCheat = false;
                flowchart.SetBooleanVariable("canTalktoKids", false);
                waveSpawnPoints.SetActive(true);
                waveManager.SetActive(true);
                kids.GetComponent<BabyTutorial>().DestroyKids();
            }
        }
    }
}
