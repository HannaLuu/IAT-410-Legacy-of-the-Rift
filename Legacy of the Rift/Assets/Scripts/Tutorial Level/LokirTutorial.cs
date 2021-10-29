using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class LokirTutorial : MonoBehaviour
{
    public Flowchart flowchart;

    public LokirAttacks lokirAttacksScript;

    // Start is called before the first frame update
    void Start()
    {
        lokirAttacksScript = gameObject.GetComponent<LokirAttacks>();
        LokirAttacks.OnDashCollide += BroadcastLokirDash;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(lokirAttacksScript.enemyCollided);
        if (flowchart.GetBooleanVariable("isLokirDash") == true && Input.GetKeyDown(KeyCode.Q))
        {
            flowchart.SetBooleanVariable("isLokirQ", true);
        }
        if (flowchart.GetBooleanVariable("isLokirQ") == true && Input.GetKeyDown(KeyCode.LeftShift))
        {
            flowchart.SetBooleanVariable("isLokirTP", true);
        }
    }

    public void RemoveLokirDashBroadcast()
    {
        LokirAttacks.OnDashCollide -= BroadcastLokirDash;
    }

    private void BroadcastLokirDash(object sender, EventArgs e)
    {
        Fungus.Flowchart.BroadcastFungusMessage("LokirBasicDone");
        flowchart.SetBooleanVariable("isLokirDash", true);
    }
}
