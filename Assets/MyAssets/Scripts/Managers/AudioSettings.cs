using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
    //Player prefs
    static readonly string backgroundAudioPrefs = "backgroundAudioPrefs";
    static readonly string SFXAudioPrefs = "SFXAudioPrefs";

    //Floats
    float backgroundAudioFloat, SFXFloat;

    //Audio sources
    public AudioSource backgroundAudio;
    public AudioSource buttonClick;

    #region Saved Settings From Manager
    void Awake()
    {
        ContinuedSettingsFromManager();
    }
    void ContinuedSettingsFromManager()
    {
        backgroundAudioFloat = PlayerPrefs.GetFloat(backgroundAudioPrefs);
        SFXFloat = PlayerPrefs.GetFloat(SFXAudioPrefs);

        backgroundAudio.volume = backgroundAudioFloat;
        buttonClick.volume = SFXFloat;
    }
    #endregion

    public void PlayButtonAudio()
    {
        buttonClick.Play();
    }
}
