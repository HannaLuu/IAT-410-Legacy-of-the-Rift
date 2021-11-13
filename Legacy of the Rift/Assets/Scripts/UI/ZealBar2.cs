using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZealBar2 : MonoBehaviour
{
    public Slider slider;

    public void SetMaxZeal(float zeal)
    {
        slider.maxValue = zeal;
    }

    public void SetZeal(float zeal)
    {
        slider.value = zeal;
    }
}
