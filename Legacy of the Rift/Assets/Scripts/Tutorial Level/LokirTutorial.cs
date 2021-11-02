using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class LokirTutorial : MonoBehaviour
{
    public Flowchart flowchart;

    public LokirAttacks lokirAttacksScript;

    public GameObject lokirBasicGlow;
    public GameObject lokirAbilityGlow;

    public CMTutorialSwitcher cmSwitcher;

    // Start is called before the first frame update
    void Start()
    {
        lokirAttacksScript = gameObject.GetComponent<LokirAttacks>();
        LokirAttacks.OnDashCollide += BroadcastLokirDash;
    }

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
        if (flowchart.GetBooleanVariable("isLokirDash") == true && lokirAttacksScript.teleported == true && flowchart.GetBooleanVariable("isLokirTP") == false)
        {
            flowchart.SetBooleanVariable("isLokirTP", true);
            cmSwitcher.phase5 = true;
            LokirAbilityGlowOff();
            Fungus.Flowchart.BroadcastFungusMessage("LokirTeleported");
            cmSwitcher.phase4 = false;
            cmSwitcher.SwitchCamera();
        }
    }

    public void RemoveLokirDashBroadcast()
    {
        LokirAttacks.OnDashCollide -= BroadcastLokirDash;
    }

    private void BroadcastLokirDash(object sender, EventArgs e)
    {
        if (flowchart.GetBooleanVariable("isLokirQ") == true && flowchart.GetBooleanVariable("isLokirDash") == false)
        {
            lokirBasicGlow.SetActive(false);
            Fungus.Flowchart.BroadcastFungusMessage("LokirBasicDone");
            flowchart.SetBooleanVariable("isLokirDash", true);
        }
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
