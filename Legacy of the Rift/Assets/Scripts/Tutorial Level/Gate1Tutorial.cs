using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate1Tutorial : MonoBehaviour
{
    public bool pastGate1 = false;

    // Start is called before the first frame update
    void Start()
    {
        pastGate1 = false;
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.tag == "Lokir" || player.tag == "Halvar" || player.tag == "Ursa")
        {
            pastGate1 = true;
        }
    }
}
