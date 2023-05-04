using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource buttonClick;

    public void PlayButtonAudio()
    {
        buttonClick.Play();
    }
}
