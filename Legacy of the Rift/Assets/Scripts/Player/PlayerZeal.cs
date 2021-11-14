using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZeal : MonoBehaviour
{
    public int maxZeal = 100;
    public int maxOverzeal = 150;
    public float currentZeal;
    public float currentOverzeal;
    public float zealRegenAmount = 10f;
    public bool isOverzealous = false;
    public bool fullyZealous = false;
    public bool canSpendZeal = false;
    public bool canSpendOverzeal = false;
    public bool overzealCheat;

    public ZealBar2 zealBar;
    public OverzealBar overzealBar;
    public GameObject ultReadyIndicator;

    // Start is called before the first frame update
    void Start()
    {
        currentZeal = 0;
        currentOverzeal = 0;
        isOverzealous = false;
        fullyZealous = false;
        canSpendZeal = false;
        zealBar.SetMaxZeal(maxZeal);
        overzealBar.SetMaxOverzeal(maxOverzeal);
        zealBar.SetZeal(currentZeal);
        overzealBar.SetOverzeal(currentOverzeal);
        ultReadyIndicator.SetActive(false);
    }

    public void SpendZeal(int zealCost)
    {
        if (currentZeal >= zealCost)
        {
            canSpendZeal = true;
            currentZeal -= zealCost;
            zealBar.SetZeal(currentZeal);
        }
        else
        {
            canSpendZeal = false;
        }
    }
    public void SpendOverzeal(int zealCost)
    {
        if (currentOverzeal >= zealCost)
        {
            canSpendOverzeal = true;
            currentOverzeal -= zealCost;
            overzealBar.SetOverzeal(currentOverzeal);
        }
        else
        {
            canSpendOverzeal = false;
        }
    }

    public void AddOverzeal(int overzealAmount)
    {
        if (currentOverzeal < maxOverzeal)
        {
            currentOverzeal += overzealAmount;
            overzealBar.SetOverzeal(currentOverzeal);
        }
        if (currentOverzeal >= maxOverzeal)
        {
            Debug.Log("ULTIMATE IS READY");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (overzealCheat == true)
        {
            currentZeal = maxZeal;
            currentOverzeal = maxOverzeal;
            isOverzealous = true;
            zealBar.SetZeal(currentZeal);
            overzealBar.SetOverzeal(currentOverzeal);
            fullyZealous = true;
        }
        if (currentZeal < maxZeal)
        {
            isOverzealous = false;
        }
        if (isOverzealous == false)
        {
            currentZeal += zealRegenAmount * Time.deltaTime;
            zealBar.SetZeal(currentZeal);
            if (currentZeal >= maxZeal)
            {
                isOverzealous = true;
            }
        }
        if (currentOverzeal >= maxOverzeal)
        {
            fullyZealous = true;
            ultReadyIndicator.SetActive(true);
        }
        else
        {
            fullyZealous = false;
            ultReadyIndicator.SetActive(false);
        }
    }
}
