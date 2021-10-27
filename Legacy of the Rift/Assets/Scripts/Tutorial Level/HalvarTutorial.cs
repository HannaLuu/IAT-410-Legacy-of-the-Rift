using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class HalvarTutorial : MonoBehaviour
{
    public Flowchart flowchart;

    public HalvarAttacks halvarAttacksScript;

    // Start is called before the first frame update
    void Start()
    {
        halvarAttacksScript = gameObject.GetComponent<HalvarAttacks>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flowchart.GetBooleanVariable("isLokirTP") == true)
        {
            flowchart.SetBooleanVariable("isHalvar", true);
        }
        if (halvarAttacksScript.enemyCollided == true)
        {
            flowchart.SetBooleanVariable("isHalvarBasic", true);
        }
        if (flowchart.GetBooleanVariable("isHalvarBasic") == true && Input.GetKeyDown(KeyCode.Q))
        {
            flowchart.SetBooleanVariable("isHalvarQ", true);
        }
    }
}
