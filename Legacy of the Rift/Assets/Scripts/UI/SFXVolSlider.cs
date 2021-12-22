using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXVolSlider : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 0f);
        slider.value = sfxVol;
    }
}
