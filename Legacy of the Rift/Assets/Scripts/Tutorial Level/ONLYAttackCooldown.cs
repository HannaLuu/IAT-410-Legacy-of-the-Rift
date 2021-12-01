using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ONLYAttackCooldown : MonoBehaviour
{
    public GameObject Lokir;
    public GameObject Halvar;
    public GameObject Ursa;

    // Update is called once per frame
    void Update()
    {
        if (Lokir.GetComponent<LokirAttacks>().attackActivated == true)
        {
            StartCoroutine(Lokir.GetComponent<LokirAttacks>().BasicCooldown());
        }
        if (Halvar.GetComponent<HalvarAttacks>().attackActivated == true)
        {
            StartCoroutine(Halvar.GetComponent<HalvarAttacks>().BasicCooldown());
        }
        if (Ursa.GetComponent<UrsaAttacks>().attackActivated == true)
        {
            StartCoroutine(Ursa.GetComponent<UrsaAttacks>().BasicCooldown());
        }
    }
}
