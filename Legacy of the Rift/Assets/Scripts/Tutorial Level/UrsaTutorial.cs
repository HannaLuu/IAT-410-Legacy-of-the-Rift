using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class UrsaTutorial : MonoBehaviour
{
    public Flowchart flowchart;

    //public HalvarAttacks halvarAttacksScript;

    public GameObject ursaBasicGlow;
    public GameObject ursaAbilityGlow;
    public GameObject lokirBasicGlow;

    public CMTutorialSwitcher cmSwitcher;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flowchart.GetBooleanVariable("isHalvarBasic") == true && flowchart.GetBooleanVariable("isUrsa") == false)
        {
            flowchart.SetBooleanVariable("isUrsa", true);
            Fungus.Flowchart.BroadcastFungusMessage("UrsaTime");
        }
        if (flowchart.GetBooleanVariable("isUrsa") == true && Input.GetMouseButtonDown(0) && flowchart.GetBooleanVariable("isUrsaBasic") == false)
        {
            flowchart.SetBooleanVariable("isUrsaBasic", true);
            UrsaBasicGlowOff();
            Fungus.Flowchart.BroadcastFungusMessage("UrsaBasicDone");
        }
        if (flowchart.GetBooleanVariable("isUrsaBasic") == true && Input.GetKeyDown(KeyCode.Q))
        {
            flowchart.SetBooleanVariable("isUrsaQ", true);
            cmSwitcher.phase4 = true;
            UrsaAbilityGlowOff();
            Fungus.Flowchart.BroadcastFungusMessage("UrsaAbilityDone");
            cmSwitcher.phase3 = false;
            cmSwitcher.SwitchCamera();
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

    public void UrsaAbilityGlowOn()
    {
        ursaAbilityGlow.SetActive(true);
    }

    public void UrsaAbilityGlowOff()
    {
        ursaAbilityGlow.SetActive(false);
    }

    public void LokirBasicGlowOn()
    {
        lokirBasicGlow.SetActive(true);
    }

    public void LokirBasicGlowOff()
    {
        lokirBasicGlow.SetActive(false);
    }
}
