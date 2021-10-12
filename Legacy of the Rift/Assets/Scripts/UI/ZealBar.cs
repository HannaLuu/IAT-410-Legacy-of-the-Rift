using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZealBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxZeal(float zeal)
    {
        slider.maxValue = zeal;
    }

    public void SetZeal(float zeal)
    {
        slider.value = zeal;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
