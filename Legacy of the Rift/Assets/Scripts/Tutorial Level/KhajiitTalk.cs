using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class KhajiitTalk : MonoBehaviour
{
    public bool pickupAllowed = false;

    public GameObject pickUpKey, continueButton;

    public Flowchart flowchart;

    // Start is called before the first frame update
    void Start()
    {
        pickUpKey.SetActive(false);
        continueButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(flowchart.GetBooleanVariable("firstTalkDone") == true)
        {
            continueButton.SetActive(true);
        }

        //this doesn't work yet and idk why/how LOL
        //if (pickupAllowed && Input.GetKeyDown(KeyCode.F) && flowchart.GetBooleanVariable("isTalk") == false)
        //{
        //    Talk();
        //}
    }

    //this doesn't work yet and idk why/how LOL
    //private void OnTriggerEnter2D(Collider2D player)
    //{
    //    if (flowchart.GetBooleanVariable("firstTalkDone") == true)
    //    {
    //        pickUpKey.SetActive(true);
    //        pickupAllowed = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D player)
    //{
    //    if (flowchart.GetBooleanVariable("firstTalkDone") == true)
    //    {
    //        pickUpKey.SetActive(false);
    //        pickupAllowed = false;
    //    }
    //}

    public void Talk()
    {
        flowchart.SetBooleanVariable("isTalk", true);
    }
}
