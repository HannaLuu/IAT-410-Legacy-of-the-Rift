using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffsBaseClass : MonoBehaviour
{
    private float dmgMultiplier;

    public DebuffsBaseClass()
    {
        dmgMultiplier = 0f;
    }

    public void DamageTakenDebuff(float multiplier)
    {
        dmgMultiplier = multiplier;
    }
}

