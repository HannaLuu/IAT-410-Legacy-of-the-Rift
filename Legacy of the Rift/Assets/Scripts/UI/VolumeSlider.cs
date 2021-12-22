using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        float masterVol = PlayerPrefs.GetFloat("MasterVolume", 0f);
        slider.value = masterVol;
    }
}
