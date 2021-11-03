using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class LokirTutorial : MonoBehaviour
{
    public Flowchart flowchart;

    public GameObject lokirBasicGlow;
    public GameObject lokirAbilityGlow;

    public CMTutorialSwitcher cmSwitcher;

    // Update is called once per frame
    void Update()
    {
        if (flowchart.GetBooleanVariable("isUrsaQ") == true && flowchart.GetBooleanVariable("isLokir") == false)
        {
            flowchart.SetBooleanVariable("isLokir", true);
            Fungus.Flowchart.BroadcastFungusMessage("LokirTime");
            LokirBasicGlowOff();
        }
        if (flowchart.GetBooleanVariable("isLokir") == true && Input.GetKeyDown(KeyCode.Q) && flowchart.GetBooleanVariable("isLokirQ") == false)
        {
            flowchart.SetBooleanVariable("isLokirQ", true);
            LokirAbilityGlowOff();
            Fungus.Flowchart.BroadcastFungusMessage("LokirPlacedClone");
        }
    }

    public void SwitchCam()
    {
        cmSwitcher.phase5 = true;
        cmSwitcher.phase4 = false;
        cmSwitcher.SwitchCamera();
    }

    public void LokirBasicGlowOn()
    {
        lokirBasicGlow.SetActive(true);
    }

    public void LokirBasicGlowOff()
    {
        lokirBasicGlow.SetActive(false);
    }

    public void LokirAbilityGlowOn()
    {
        lokirAbilityGlow.SetActive(true);
    }

    public void LokirAbilityGlowOff()
    {
        lokirAbilityGlow.SetActive(false);
    }
}
