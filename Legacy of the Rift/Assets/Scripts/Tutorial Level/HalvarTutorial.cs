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
        //if (flowchart.GetBooleanVariable("isLokirTP") == true)
        //{
        //    flowchart.SetBooleanVariable("isHalvar", true);
        //}
        //if (halvarAttacksScript.enemyCollided == true)
        //{
        //    flowchart.SetBooleanVariable("isHalvarBasic", true);
        //}
        if (Input.GetKeyDown(KeyCode.Q))
        {
            flowchart.SetBooleanVariable("isHalvarQ", true);
            cmSwitcher.phase2 = true;
            Fungus.Flowchart.BroadcastFungusMessage("HalvarWallUp");
            halvarAbilityGlow.SetActive(false);
            cmSwitcher.SwitchCamera();
        }
    }
}
