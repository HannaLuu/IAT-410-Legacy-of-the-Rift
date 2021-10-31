using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class UrsaDockTutorial : MonoBehaviour
{
    public Flowchart flowchart;

    public GameObject ursaBasicGlow;

    public CMDockSwitcher cmSwitcher;

    public GameObject tutorialEnemies;

    // Update is called once per frame
    void Update()
    {
        if (flowchart.GetBooleanVariable("isHalvarQ") == true && flowchart.GetBooleanVariable("isUrsa") == false)
        {
            flowchart.SetBooleanVariable("isUrsa", true);
            Fungus.Flowchart.BroadcastFungusMessage("UrsaTime");
        }
        if (flowchart.GetBooleanVariable("isUrsa") == true && Input.GetMouseButtonDown(0) && flowchart.GetBooleanVariable("isUrsaBasic") == false)
        {
            flowchart.SetBooleanVariable("isUrsaBasic", true);
            Fungus.Flowchart.BroadcastFungusMessage("End");
        }
    }

    public void UrsaBasicGlowOn()
    {
        ursaBasicGlow.SetActive(true);
    }

    public void UrsaBasicGlowOff()
    {
        ursaBasicGlow.SetActive(false);
    }
    public void DeleteTutorialEnemies()
    {
        tutorialEnemies.SetActive(false);
    }

    public void End()
    {
        cmSwitcher.phase2 = true;
        cmSwitcher.phase1 = false;
        cmSwitcher.SwitchCamera();
    }
}
