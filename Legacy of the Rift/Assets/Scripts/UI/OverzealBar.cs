using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverzealBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxOverzeal(float zeal)
    {
        slider.maxValue = zeal;
    }

    public void SetOverzeal(float zeal)
    {
        slider.value = zeal;
    }
}
