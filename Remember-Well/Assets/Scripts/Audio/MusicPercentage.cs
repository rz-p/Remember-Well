using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioPercentage : MonoBehaviour {
    public Slider slider;
    public Text output;

    void Start()
    {
        /* slider.value = PlayerPrefs.GetFloat("Music Volume", 0.75f); */
    }

    void Update()
    {
        getMusicPercentage();
    }

    void getMusicPercentage()
    {
        output.text = (Mathf.Round(slider.value * 100)) + "%";
    }


}

