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
            scores[i].kills.text = TimeRemainingFormat(GameManager.instance.saveData.highScoreTimeRemainingOnCompletion[i]);
        }
    }

    string TimeRemainingFormat(float currentTime)
    {
        string minutes = Mathf.Floor(currentTime / 60).ToString("00");
        string seconds = (currentTime % 60).ToString("00");

        if (currentTime <= 0)
        {
            return "00:00";
        }
        else
        {
            return string.Format("{0}:{1}", minutes, seconds);
        }
    }
}
