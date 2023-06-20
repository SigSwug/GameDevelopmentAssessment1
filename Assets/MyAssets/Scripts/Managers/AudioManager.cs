using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    //Player prefs
    static readonly string firstPlay = "FirstPlay";
    static readonly string backgroundAudioPrefs = "backgroundAudioPrefs";
    static readonly string SFXAudioPrefs = "SFXAudioPrefs";

    int firstPlayInt;

    //Sliders
    public Slider backgroundAudioSlider, SFXSlider;
    //Floats
    float backgroundAudioFloat, SFXFloat;

    //Audio sources
    public AudioSource backgroundAudio;
    public AudioSource buttonClick;

    private void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(firstPlay);

        if(firstPlayInt == 0)
        {
            backgroundAudioFloat = 0.1f;
            SFXFloat = 0.2f;
            backgroundAudioSlider.value = backgroundAudioFloat;
            SFXSlider.value = SFXFloat;

            PlayerPrefs.SetFloat(backgroundAudioPrefs, backgroundAudioFloat);
            PlayerPrefs.SetFloat(SFXAudioPrefs, SFXFloat);
            PlayerPrefs.SetInt(firstPlay, -1);
        }
        else
        {
            backgroundAudioFloat = PlayerPrefs.GetFloat(backgroundAudioPrefs);
            backgroundAudioSlider.value = backgroundAudioFloat;

            SFXFloat = PlayerPrefs.GetFloat(SFXAudioPrefs);
            SFXSlider.value = SFXFloat;
        }
    }

    #region Save Settings
    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat(backgroundAudioPrefs, backgroundAudioSlider.value);
        PlayerPrefs.SetFloat(SFXAudioPrefs, SFXSlider.value);
    }
    void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveAudioSettings();
        }
    }
    #endregion

    public void UpdateSound()
    {
        backgroundAudio.volume = backgroundAudioSlider.value;
        buttonClick.volume = SFXSlider.value;
    }

    public void PlayButtonAudio()
    {
        buttonClick.Play();
    }
}
