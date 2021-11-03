using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class BabyTutorial : MonoBehaviour
{
    public bool pickupAllowed = false;
    public bool fuck = false;

    public GameObject pickUpKey;

    public Flowchart flowchart;

    public CMTutorialSwitcher cmSwitcher;

    // Start is called before the first frame update
    void Start()
    {
        pickUpKey.SetActive(false);
        fuck = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(pickupAllowed && Input.GetKeyDown(KeyCode.F) && fuck)
        {
            Pickup();
        }
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        Debug.Log("BABY COLLISION DETECTED");
        if(player.tag == "Lokir" && fuck == true)
        {
            pickUpKey.SetActive(true);
            pickupAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.tag == "Lokir")
        {
            pickUpKey.SetActive(false);
            pickupAllowed = false;
        }
    }

    public void AllowPickup()
    {
        fuck = true;
    }

    public void Pickup()
    {
        flowchart.SetBooleanVariable("pickedUpBabies", true);
        Fungus.Flowchart.BroadcastFungusMessage("PickedUpBabies");
        this.gameObject.SetActive(false);
    }
}
