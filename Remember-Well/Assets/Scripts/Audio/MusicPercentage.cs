using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioPercentage : MonoBehaviour {
    public Slider musicSlider;
    public Slider gameSlider;
    public Text output;

    void getMusicPercentage()
    {
        output.text = PlayerPrefs.GetFloat("Music Volume") + "%";
    }

    void getGamePercentage()
    {
        output.text = PlayerPrefs.GetFloat("Game Volume") + "%";
    }


}

