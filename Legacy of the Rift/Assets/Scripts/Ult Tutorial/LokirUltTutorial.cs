using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class LokirUltTutorial : MonoBehaviour
{
    public Flowchart flowchart;

    public GameObject lokirUltGlow;
    public GameObject halvarBasicGlow;
    public GameObject phase1Enemy;
    public PlayerZeal playerZeal;

    private void Start()
    {
        playerZeal.overzealCheat = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && flowchart.GetBooleanVariable("isLokirE") == false)
        {
            flowchart.SetBooleanVariable("isLokirE", true);
            playerZeal.overzealCheat = false;
            Fungus.Flowchart.BroadcastFungusMessage("LokirUltActivated");
        }
    }

    //public void SpawnTutorialEnemies()
    //{
    //    phase1Enemy.SetActive(true);
    //}

    public void LokirUltGlowOn()
    {
        lokirUltGlow.SetActive(true);
    }

    public void LokirUltGlowOff()
    {
        lokirUltGlow.SetActive(false);
    }

    public void HalvarBasicGlowOn()
    {
        halvarBasicGlow.SetActive(true);
    }

    public void HalvarBasicGlowOff()
    {
        halvarBasicGlow.SetActive(false);
    }
}
