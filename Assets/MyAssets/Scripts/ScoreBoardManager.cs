using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoardManager : MonoBehaviour
{
    [SerializeField] PlayerScoreCard[] scores = new PlayerScoreCard[10];

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            scores[i].playerName.text = GameManager.instance.saveData.highScorePlayerNames[i];
            scores[i].kills.text = "Time Left: " + GameManager.instance.saveData.highScoreTimeRemainingOnCompletion[i];
        }
    }
}
