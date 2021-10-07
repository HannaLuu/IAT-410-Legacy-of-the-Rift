using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitching : MonoBehaviour
{
    public GameObject Lokir;
    public GameObject Halvar;
    public GameObject Ursa;
    public GameObject Player;

    public enum Hero {Lokir, Halvar, Ursa};
    public Hero currHero;

    private void Start()
    {
        currHero = Hero.Lokir;
        Player.transform.position = Lokir.transform.position;
        Lokir.SetActive(true);
        Halvar.SetActive(false);
        Ursa.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Lokir"))
        {
            if(currHero == Hero.Halvar)
            {
                Lokir.transform.position = Halvar.transform.position;
            }

            if (currHero == Hero.Ursa)
            {
                Lokir.transform.position = Ursa.transform.position;
            }

            currHero = Hero.Lokir;
        }

        if (Input.GetButtonDown("Halvar"))
        {
            if (currHero == Hero.Lokir)
            {
                Halvar.transform.position = Lokir.transform.position;
            }

            if (currHero == Hero.Ursa)
            {
                Halvar.transform.position = Ursa.transform.position;
            }

            currHero = Hero.Halvar;
        }

        if (Input.GetButtonDown("Ursa"))
        {
            if (currHero == Hero.Lokir)
            {
                Ursa.transform.position = Lokir.transform.position;
            }

            if (currHero == Hero.Halvar)
            {
                Ursa.transform.position = Halvar.transform.position;
            }

            currHero = Hero.Ursa;
        }

        switch (currHero)
        {
            case Hero.Lokir:
                Lokir.SetActive(true);
                Halvar.SetActive(false);
                Ursa.SetActive(false);
                Player.transform.position = Lokir.transform.position;
                break;
            case Hero.Halvar:
                Lokir.SetActive(false);
                Halvar.SetActive(true);
                Ursa.SetActive(false);
                Player.transform.position = Halvar.transform.position;
                break;
            case Hero.Ursa:
                Lokir.SetActive(false);
                Halvar.SetActive(false);
                Ursa.SetActive(true);
                Player.transform.position = Ursa.transform.position;
                break;
        }
    }
}
