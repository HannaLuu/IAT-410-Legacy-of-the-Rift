using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZeal : MonoBehaviour
{
    public int maxZeal = 150;
    public int overzealousAmount = 50;
    public float currentZeal;
    public float zealRegenAmount = 10f;
    public bool isOverzealous = false;
    public bool fullyZealous = false;
    public bool canSpendZeal = false;
    public bool overzealCheat;

    public ZealBar zealBar;

    public GameObject ultReadyIndicator;

    // Start is called before the first frame update
    void Start()
    {
        currentZeal = 0;
        isOverzealous = false;
        fullyZealous = false;
        canSpendZeal = false;
        zealBar.SetMaxZeal(maxZeal);
        zealBar.SetZeal(currentZeal);
        ultReadyIndicator.SetActive(false);
    }

    public void SpendZeal(int zealCost)
    {
        if(currentZeal >= zealCost)
        {
            canSpendZeal = true;
            currentZeal -= zealCost;
            zealBar.SetZeal(currentZeal);
        } else
        {
            canSpendZeal = false;
        }
    }

    public void AddOverzeal(int overzealAmount)
    {
        currentZeal += overzealAmount;
        zealBar.SetZeal(currentZeal);
    }

    // Update is called once per frame
    void Update()
    {
        if(overzealCheat == true)
        {
            currentZeal = maxZeal + overzealousAmount;
            isOverzealous = true;
            zealBar.SetZeal(currentZeal);
            fullyZealous = true;
        }
        if(currentZeal <= maxZeal - overzealousAmount)
        {
            isOverzealous = false;
            //fullyZealous = false;
        }
        if (isOverzealous == false)
        {
            currentZeal += zealRegenAmount * Time.deltaTime;
            zealBar.SetZeal(currentZeal);
            if (currentZeal >= maxZeal - overzealousAmount)
            {
                isOverzealous = true;
            }
        }
        if (currentZeal >= maxZeal)
        {
            ultReadyIndicator.SetActive(true);
            fullyZealous = true;
        } else
        {
            fullyZealous = false;
            ultReadyIndicator.SetActive(false);
        }
    }
}
