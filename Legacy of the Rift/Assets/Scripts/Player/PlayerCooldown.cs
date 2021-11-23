using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCooldown : MonoBehaviour
{
    public CooldownBar lokirAbilityCooldownBar;
    public CooldownBar lokirUltCooldownBar;

    public CooldownBar halvarAbilityCooldownBar;
    public CooldownBar halvarUltCooldownBar;

    public CooldownBar ursaAbilityCooldownBar;
    public CooldownBar ursaUltCooldownBar;

    public GameObject Lokir;
    public GameObject Halvar;
    public GameObject Ursa;

    // Start is called before the first frame update
    void Start()
    {
        lokirAbilityCooldownBar.SetMaxCooldown(Lokir.GetComponent<LokirAttacks>().maxAbilityCooldown);
        lokirUltCooldownBar.SetMaxCooldown(Lokir.GetComponent<LokirAttacks>().maxUltCooldown);
        Lokir.GetComponent<LokirAttacks>().currAbilityCooldown = Lokir.GetComponent<LokirAttacks>().maxAbilityCooldown;
        Lokir.GetComponent<LokirAttacks>().currUltCooldown = Lokir.GetComponent<LokirAttacks>().maxUltCooldown;

        halvarAbilityCooldownBar.SetMaxCooldown(Halvar.GetComponent<HalvarAttacks>().maxAbilityCooldown);
        halvarUltCooldownBar.SetMaxCooldown(Halvar.GetComponent<HalvarAttacks>().maxUltCooldown);
        Halvar.GetComponent<HalvarAttacks>().currAbilityCooldown = Halvar.GetComponent<HalvarAttacks>().maxAbilityCooldown;
        Halvar.GetComponent<HalvarAttacks>().currUltCooldown = Halvar.GetComponent<HalvarAttacks>().maxUltCooldown;

        ursaAbilityCooldownBar.SetMaxCooldown(Ursa.GetComponent<UrsaAttacks>().maxAbilityCooldown);
        ursaUltCooldownBar.SetMaxCooldown(Ursa.GetComponent<UrsaAttacks>().maxUltCooldown);
        Ursa.GetComponent<UrsaAttacks>().currAbilityCooldown = Ursa.GetComponent<UrsaAttacks>().maxAbilityCooldown;
        Ursa.GetComponent<UrsaAttacks>().currUltCooldown = Ursa.GetComponent<UrsaAttacks>().maxUltCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        lokirAbilityCooldownBar.SetCooldown(Lokir.GetComponent<LokirAttacks>().currAbilityCooldown);
        lokirUltCooldownBar.SetCooldown(Lokir.GetComponent<LokirAttacks>().currUltCooldown);

        halvarAbilityCooldownBar.SetCooldown(Halvar.GetComponent<HalvarAttacks>().currAbilityCooldown);
        halvarUltCooldownBar.SetCooldown(Halvar.GetComponent<HalvarAttacks>().currUltCooldown);

        ursaAbilityCooldownBar.SetCooldown(Ursa.GetComponent<UrsaAttacks>().currAbilityCooldown);
        ursaUltCooldownBar.SetCooldown(Ursa.GetComponent<UrsaAttacks>().currUltCooldown);

        if (Lokir.GetComponent<LokirAttacks>().attackActivated == true)
        {
            StartCoroutine(Lokir.GetComponent<LokirAttacks>().BasicCooldown());
        }
        if (Lokir.GetComponent<LokirAttacks>().abilityActivated == true)
        {
            StartCoroutine(Lokir.GetComponent<LokirAttacks>().AbilityCooldown());
        }
        //if (Lokir.GetComponent<LokirAttacks>().ultActivated == true)
        //{
        //    StartCoroutine(Lokir.GetComponent<LokirAttacks>().UltCooldown());
        //}

        if (Halvar.GetComponent<HalvarAttacks>().attackActivated == true)
        {
            StartCoroutine(Halvar.GetComponent<HalvarAttacks>().BasicCooldown());
        }
        if (Halvar.GetComponent<HalvarAttacks>().abilityActivated == true)
        {
            StartCoroutine(Halvar.GetComponent<HalvarAttacks>().AbilityCooldown());
        }
        //if (Halvar.GetComponent<HalvarAttacks>().ultActivated == true)
        //{
        //    StartCoroutine(Halvar.GetComponent<HalvarAttacks>().UltCooldown());
        //}

        if (Ursa.GetComponent<UrsaAttacks>().attackActivated == true)
        {
            StartCoroutine(Ursa.GetComponent<UrsaAttacks>().BasicCooldown());
        }
        if (Ursa.GetComponent<UrsaAttacks>().abilityActivated == true)
        {
            StartCoroutine(Ursa.GetComponent<UrsaAttacks>().AbilityCooldown());
        }
        //if (Ursa.GetComponent<UrsaAttacks>().ultActivated == true)
        //{
        //    StartCoroutine(Ursa.GetComponent<UrsaAttacks>().UltCooldown());
        //}
    }
}
