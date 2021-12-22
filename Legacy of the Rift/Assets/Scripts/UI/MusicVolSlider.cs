using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolSlider : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 0f);
        slider.value = musicVol;
    }
}
