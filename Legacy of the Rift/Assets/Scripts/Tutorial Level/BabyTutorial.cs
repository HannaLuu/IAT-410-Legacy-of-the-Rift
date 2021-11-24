using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class BabyTutorial : MonoBehaviour
{
    public GameObject pickUpKey;

    public Flowchart flowchart;

    private bool nextToKids = false;


    // Start is called before the first frame update
    void Start()
    {
        pickUpKey.SetActive(false);
        nextToKids = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && nextToKids)
        {
            flowchart.SetBooleanVariable("canTalktoKids", true);
            flowchart.ExecuteBlock("Kids Give Hint");
        }
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if(player.tag == "Lokir" || player.tag == "Halvar" || player.tag == "Ursa")
        {
            pickUpKey.SetActive(true);
            nextToKids = true;
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.tag == "Lokir" || player.tag == "Halvar" || player.tag == "Ursa")
        {
            pickUpKey.SetActive(false);
            nextToKids = false;
            flowchart.SetBooleanVariable("canTalktoKids", false);
        }
    }

    public void DestroyKids()
    {
        Destroy(gameObject);
    }
}
