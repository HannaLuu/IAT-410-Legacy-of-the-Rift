using System.Collections;
using System.Collections.Generic;
using Fungus;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SuperTextSuperSeks : MonoBehaviour {
    public TextMeshProUGUI tmpText;
    public string defaultText;

    public void Start() {
        tmpText.text = defaultText;
    }
    
    public void SetText(string text) {
        tmpText.text = text;
    }

    public void ResetText() {
        tmpText.text = "";
    }
}
