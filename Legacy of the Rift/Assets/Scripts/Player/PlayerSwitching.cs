using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSwitching : MonoBehaviour
{
    public GameObject Lokir;
    public GameObject Halvar;
    public GameObject Ursa;
    public GameObject Player;

    public enum Hero { Lokir, Halvar, Ursa };
    public Hero pastHero;
    public Hero currHero;

    public bool isSwitch = false;

    private void Start()
    {
        if (Lokir.activeInHierarchy)
        {
            currHero = Hero.Lokir;
            Player.transform.position = Lokir.transform.position;
            Lokir.SetActive(true);
            Halvar.SetActive(false);
            Ursa.SetActive(false);
        }
        if (Halvar.activeInHierarchy)
        {
            currHero = Hero.Halvar;
            Player.transform.position = Halvar.transform.position;
            Halvar.SetActive(true);
            Lokir.SetActive(false);
            Ursa.SetActive(false);
        }
        if (Ursa.activeInHierarchy)
        {
            currHero = Hero.Ursa;
            Player.transform.position = Ursa.transform.position;
            Ursa.SetActive(true);
            Lokir.SetActive(false);
            Halvar.SetActive(false);
        }
        isSwitch = false;
    }

    // Update is called once per frame
    void Update()
    {
        isSwitch = false;
        pastHero = currHero;
        if (currHero != Hero.Lokir && Input.GetButtonDown("Lokir"))
        {
            isSwitch = true;
            SwitchCharacter(Hero.Lokir);
        }

        if (currHero != Hero.Halvar && Input.GetButtonDown("Halvar"))
        {
            isSwitch = true;
            SwitchCharacter(Hero.Halvar);
            Lokir.GetComponent<LokirAttacks>().ignoreEnemyCollision = false;
            Physics2D.IgnoreLayerCollision(7, 6, false);
            Physics2D.IgnoreLayerCollision(7, 12, false);
        }

        if (currHero != Hero.Ursa && Input.GetButtonDown("Ursa"))
        {
            isSwitch = true;
            SwitchCharacter(Hero.Ursa);
            Lokir.GetComponent<LokirAttacks>().ignoreEnemyCollision = false;
            Physics2D.IgnoreLayerCollision(7, 6, false);
            Physics2D.IgnoreLayerCollision(7, 12, false);
        }

        if (Input.GetKey(KeyCode.P))
        {
            SceneManager.LoadScene("Choice");
        }

        Player.transform.position = GetCurrentHeroObj().transform.position;
    }

    private void SwitchCharacter(Hero newHero)
    {
        Vector3 currHeroPosition = GetCurrentHeroObj().transform.position;
        Halvar.SetActive(false);
        Ursa.SetActive(false);
        Lokir.SetActive(false);

        switch (newHero)
        {
            case Hero.Halvar:
                Halvar.SetActive(true);
                Halvar.GetComponent<HalvarAttacks>().dontMove = false;
                Halvar.transform.position = currHeroPosition;
                break;

            case Hero.Ursa:
                Ursa.SetActive(true);
                Ursa.GetComponent<UrsaAttacks>().dontMove = false;
                Ursa.transform.position = currHeroPosition;
                break;

            case Hero.Lokir:
                Lokir.SetActive(true);
                Lokir.GetComponent<LokirAttacks>().dontMove = false;
                Lokir.transform.position = currHeroPosition;
                break;
        }
        currHero = newHero;
    }

    private GameObject GetCurrentHeroObj()
    {
        switch (currHero)
        {
            case Hero.Halvar:
                return Halvar;
            case Hero.Ursa:
                return Ursa;
            case Hero.Lokir:
                return Lokir;
        }

        Debug.LogError("currHero is invalid");
        return null;
    }
}
