using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitching : MonoBehaviour
{
    public GameObject Lokir;
    public GameObject Halvar;
    public GameObject Ursa;
    public GameObject Player;

    public bool isLokir;
    public bool isHalvar;
    public bool isUrsa;

    private void Start()
    {
        Player.transform.position = Lokir.transform.position;
        Lokir.SetActive(true);
        Halvar.SetActive(false);
        Ursa.SetActive(false);
        isLokir = true;
        isHalvar = false;
        isUrsa = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Lokir"))
        {
            if (isHalvar)
            {
                Lokir.transform.position = Halvar.transform.position;
            }
            if (isUrsa)
            {
                Lokir.transform.position = Ursa.transform.position;
            }
            Lokir.SetActive(true);
            Halvar.SetActive(false);
            Ursa.SetActive(false);
            isLokir = true;
            isHalvar = false;
            isUrsa = false;
        }

        if (Input.GetButtonDown("Halvar"))
        {
            if (isLokir)
            {
                Halvar.transform.position = Lokir.transform.position;
            }
            if (isUrsa)
            {
                Halvar.transform.position = Ursa.transform.position;
            }
            Lokir.SetActive(false);
            Halvar.SetActive(true);
            Ursa.SetActive(false);
            isLokir = false;
            isHalvar = true;
            isUrsa = false;
        }

        if (Input.GetButtonDown("Ursa"))
        {
            if (isLokir)
            {
                Ursa.transform.position = Lokir.transform.position;
            }
            if (isHalvar)
            {
                Ursa.transform.position = Halvar.transform.position;
            }
            Lokir.SetActive(false);
            Halvar.SetActive(false);
            Ursa.SetActive(true);
            isLokir = false;
            isHalvar = false;
            isUrsa = true;
        }

        if (isLokir)
        {
            Player.transform.position = Lokir.transform.position;
        }
        if (isHalvar)
        {
            Player.transform.position = Halvar.transform.position;
        }
        if (isUrsa)
        {
            Player.transform.position = Ursa.transform.position;
        }
    }
}
