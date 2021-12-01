using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Fungus;

public class Door : MonoBehaviour
{
    public GameObject pickUpKey;

    public Flowchart flowchart;

    private bool nextToDoor = false;


    // Start is called before the first frame update
    void Start()
    {
        pickUpKey.SetActive(false);
        nextToDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && nextToDoor && flowchart.GetBooleanVariable("pickedUpKids") == true)
        {
            SceneManager.LoadScene("Dock_Level");
        }
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Lokir" || player.tag == "Halvar" || player.tag == "Ursa")
        {
            pickUpKey.SetActive(true);
            nextToDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.tag == "Lokir" || player.tag == "Halvar" || player.tag == "Ursa")
        {
            pickUpKey.SetActive(false);
            nextToDoor = false;
        }
    }
}
