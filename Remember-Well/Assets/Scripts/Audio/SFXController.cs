using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    // Reference to the AudioSource component
    public AudioSource falseSfx;
    public AudioSource corrrectSfx;

    public void PlayFalse()
    {
        falseSfx.Play();
    }

    public void PlayCorrect()
    {
        corrrectSfx.Play();
    }
}
