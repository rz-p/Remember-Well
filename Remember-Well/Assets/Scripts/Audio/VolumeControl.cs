using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour {
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider gameSlider;

    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume", 0.75f);
        gameSlider.value = PlayerPrefs.GetFloat("Game Volume", 0.75f);
    }
    public void SetMusicVolume (float sliderValue)
    {
	    mixer.SetFloat("BGMVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("Music Volume", sliderValue);
    }

    public void SetGameVolume (float sliderValue)
    {
	    mixer.SetFloat("Game Volume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("Game Volume", sliderValue);
    }

}

