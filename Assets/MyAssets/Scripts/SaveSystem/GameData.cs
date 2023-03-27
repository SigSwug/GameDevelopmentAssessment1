using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int score = 0;

    //store the information of high scores into an array of 10
    public float[] highScoreTimeRemainingOnCompletion = new float[10];
    public string[] highScorePlayerNames = new string[10];

    public void AddScore(int points)
    {
        score += points;
    }
    public void ResetData()
    {
        score = 0;
    }
}
