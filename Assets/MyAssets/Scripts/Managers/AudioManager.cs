using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    public AudioSource buttonClick;

    public void PlayButtonAudio()
    {
        buttonClick.Play();
    }

    public void AdjustVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}
