using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelComplete : MonoBehaviour
{
    public UnityEvent levelFinished;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            levelFinished.Invoke();
            //LevelManager.instance.currentState = LevelManager.GameStates.Won;
        }
    }
}
