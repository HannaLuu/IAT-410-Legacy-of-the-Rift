using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class HalvarUltTutorial : MonoBehaviour
{
    public Flowchart flowchart;

    public GameObject halvarBasicGlow;
    public GameObject halvarUltGlow;
    public GameObject ursaBasicGlow;

    public GameObject phase2Enemy;

    public PlayerZeal playerZeal;

    // Update is called once per frame
    void Update()
    {
        if (flowchart.GetBooleanVariable("isLokirE") == true && flowchart.GetBooleanVariable("isHalvar") == false)
        {
            flowchart.SetBooleanVariable("isHalvar", true);
            playerZeal.overzealCheat = true;
            Fungus.Flowchart.BroadcastFungusMessage("HalvarTime");
        }
        if (flowchart.GetBooleanVariable("isHalvar") == true && Input.GetKeyDown(KeyCode.E) && flowchart.GetBooleanVariable("isHalvarE") == false)
        {
            flowchart.SetBooleanVariable("isHalvarE", true);
            Fungus.Flowchart.BroadcastFungusMessage("HalvarUltActivated");
            playerZeal.overzealCheat = false;
        }
    }

    public void SpawnTutorialEnemies()
    {
        phase2Enemy.SetActive(true);
    }

    public void HalvarBasicGlowOn()
    {
        halvarBasicGlow.SetActive(true);
    }

    public void HalvarBasicGlowOff()
    {
        halvarBasicGlow.SetActive(false);
    }

    public void HalvarUltGlowOn()
    {
        halvarUltGlow.SetActive(true);
    }

    public void HalvarUltGlowOff()
    {
        halvarUltGlow.SetActive(false);
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
