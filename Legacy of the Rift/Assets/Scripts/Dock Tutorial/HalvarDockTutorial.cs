using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class HalvarDockTutorial : MonoBehaviour
{
    public Flowchart flowchart;

    public GameObject halvarBasicGlow;
    public GameObject halvarAbilityGlow;
    public GameObject ursaBasicGlow;

    // Update is called once per frame
    void Update()
    {
        if (flowchart.GetBooleanVariable("isLokirQ") == true && flowchart.GetBooleanVariable("isHalvar") == false)
        {
            flowchart.SetBooleanVariable("isHalvar", true);
            Fungus.Flowchart.BroadcastFungusMessage("HalvarTime");
        }
        if (flowchart.GetBooleanVariable("isHalvar") == true && Input.GetKeyDown(KeyCode.Q))
        {
            flowchart.SetBooleanVariable("isHalvarQ", true);
            Fungus.Flowchart.BroadcastFungusMessage("HalvarWallUp");
        }
    }

    public void HalvarBasicGlowOn()
    {
        halvarBasicGlow.SetActive(true);
    }

    public void HalvarBasicGlowOff()
    {
        halvarBasicGlow.SetActive(false);
    }

    public void HalvarAbilityGlowOn()
    {
        halvarAbilityGlow.SetActive(true);
    }

    public void HalvarAbilityGlowOff()
    {
        halvarAbilityGlow.SetActive(false);
    }

    public void UrsaBasicGlowOn()
    {
        ursaBasicGlow.SetActive(true);
    }

    public void UrsaBasicGlowOff()
    {
        ursaBasicGlow.SetActive(false);
    }
}
