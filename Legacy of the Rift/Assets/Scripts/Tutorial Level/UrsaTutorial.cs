using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class UrsaTutorial : MonoBehaviour
{
    public Flowchart flowchart;

    //public HalvarAttacks halvarAttacksScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (flowchart.GetBooleanVariable("isHalvarQ") == true)
        {
            flowchart.SetBooleanVariable("isUrsa", true);
        }
        if (flowchart.GetBooleanVariable("isUrsa") == true && Input.GetMouseButtonDown(0))
        {
            flowchart.SetBooleanVariable("isUrsaBasic", true);
        }
        if (flowchart.GetBooleanVariable("isUrsaBasic") == true && Input.GetKeyDown(KeyCode.Q))
        {
            flowchart.SetBooleanVariable("isUrsaQ", true);
        }
    }
}
