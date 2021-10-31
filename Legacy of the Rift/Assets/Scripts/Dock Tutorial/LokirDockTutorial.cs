using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class LokirDockTutorial : MonoBehaviour
{
    public Flowchart flowchart;

    public GameObject lokirAbilityGlow;
    public GameObject halvarBasicGlow;
    public GameObject tutorialEnemies;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && flowchart.GetBooleanVariable("isLokirQ") == false)
        {
            flowchart.SetBooleanVariable("isLokirQ", true);
            Fungus.Flowchart.BroadcastFungusMessage("LokirPlacedClone");
        }
    }

    public void SpawnTutorialEnemies()
    {
        tutorialEnemies.SetActive(true);
    }

    public void LokirAbilityGlowOn()
    {
        lokirAbilityGlow.SetActive(true);
    }

    public void LokirAbilityGlowOff()
    {
        lokirAbilityGlow.SetActive(false);
    }

    public void HalvarBasicGlowOn()
    {
        halvarBasicGlow.SetActive(true);
    }

    public void HalvarBasicGlowOff()
    {
        halvarBasicGlow.SetActive(false);
    }
}
