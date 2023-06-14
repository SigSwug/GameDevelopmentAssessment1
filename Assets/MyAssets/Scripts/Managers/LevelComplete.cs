using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class LevelComplete : MonoBehaviour
{
    public UnityEvent levelFinished;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelFinished.Invoke();
        }
    }
}
