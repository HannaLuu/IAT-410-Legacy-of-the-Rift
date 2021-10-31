using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class UrsaUltTutorial : MonoBehaviour
{
    public Flowchart flowchart;

    public GameObject ursaBasicGlow;
    public GameObject ursaUltGlow;

    public GameObject dialogueShit;

    public PlayerZeal playerZeal;

    // Update is called once per frame
    void Update()
    {
        if (flowchart.GetBooleanVariable("isHalvarE") == true && flowchart.GetBooleanVariable("isUrsa") == false)
        {
            flowchart.SetBooleanVariable("isUrsa", true);
            playerZeal.overzealCheat = true;
            Fungus.Flowchart.BroadcastFungusMessage("UrsaTime");
        }
        if (flowchart.GetBooleanVariable("isUrsa") == true && Input.GetKeyDown(KeyCode.E) && flowchart.GetBooleanVariable("isUrsaE") == false)
        {
            playerZeal.overzealCheat = false;
            flowchart.SetBooleanVariable("isUrsaE", true);
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

    public void UrsaUltGlowOn()
    {
        ursaUltGlow.SetActive(true);
    }

    public void UrsaUltGlowOff()
    {
        ursaUltGlow.SetActive(false);
    }

    public void End()
    {
        dialogueShit.SetActive(false);
    }
}
