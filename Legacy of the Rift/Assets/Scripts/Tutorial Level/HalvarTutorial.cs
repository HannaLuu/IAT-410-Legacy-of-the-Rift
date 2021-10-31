using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class HalvarTutorial : MonoBehaviour
{
    public Flowchart flowchart;

    public HalvarAttacks halvarAttacksScript;

    public GameObject halvarBasicGlow;
    public GameObject halvarAbilityGlow;
    public GameObject ursaBasicGlow;

    public CMTutorialSwitcher cmSwitcher;

    // Start is called before the first frame update
    void Start()
    {
        cmSwitcher = GameObject.FindObjectOfType<CMTutorialSwitcher>();
        halvarAttacksScript = gameObject.GetComponent<HalvarAttacks>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            flowchart.SetBooleanVariable("isHalvarQ", true);
            cmSwitcher.phase2 = true;
            Fungus.Flowchart.BroadcastFungusMessage("HalvarWallUp");
            HalvarAbilityGlowOff();
            cmSwitcher.phase1 = false;
            cmSwitcher.SwitchCamera();
        }
        if(flowchart.GetBooleanVariable("isHalvarQ") == true && flowchart.GetBooleanVariable("isHalvarBasic") == false && halvarAttacksScript.enemyCollided == true)
        {
            flowchart.SetBooleanVariable("isHalvarBasic", true);
            cmSwitcher.phase3 = true;
            Fungus.Flowchart.BroadcastFungusMessage("HalvarBasicDone");
            HalvarBasicGlowOff();
            cmSwitcher.phase2 = false;
            cmSwitcher.SwitchCamera();
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
