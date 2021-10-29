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

    public ZealBar zealBar;

    // Start is called before the first frame update
    void Start()
    {
        currentZeal = 0;
        isOverzealous = false;
        fullyZealous = false;
        zealBar.SetMaxZeal(maxZeal);
        zealBar.SetZeal(currentZeal);
    }

    public void SpendZeal(int zealCost)
    {
        if(currentZeal >= zealCost)
        {
            currentZeal -= zealCost;
            zealBar.SetZeal(currentZeal);
        } else
        {
            Debug.Log("You've Run Out of Zeal! Wait to Regen!");
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
            fullyZealous = true;
        }
    }
}
