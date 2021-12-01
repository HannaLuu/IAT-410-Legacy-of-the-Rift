using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKids : MonoBehaviour
{
    public GameObject pickUpKey;

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
        if (Input.GetKeyDown(KeyCode.F) && nextToKids)
        {
            DestroyKids();
        }
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Lokir" || player.tag == "Halvar" || player.tag == "Ursa")
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
        }
    }

    public void DestroyKids()
    {
        gameObject.SetActive(false);
    }
}
