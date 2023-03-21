using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public int score = 0;
    public float timeRemainingOnCompletion;
    public int numberOfLevelsUnlocked;

    //list of current player names
    public List<PlayerData> currentPlayers = new List<PlayerData>();

    //store the information of high scores into an array of 10
    public float[] highScoreTimeRemainingOnCompletion = new float[10];
    public string[] highScorePlayerNames = new string[10];

    public List<PlayerData> tempScoreBoard = new List<PlayerData>();

    public void FillTempList()
    {
        for (int i = 0; i < 10; i++)
        {
            PlayerData data = new PlayerData();
            data.playerName = highScorePlayerNames[i];
            tempScoreBoard.Add(data);
        }
    }
    public void FillSaveData()
    {
        tempScoreBoard.Sort(SortPlayerFunc);

        for (int i = 0; i < 10; i++)
        {
            highScorePlayerNames[i] = tempScoreBoard[i].playerName;
        }
    }
    int SortPlayerFunc(PlayerData a, PlayerData b)
    {
        if (a.playerNumber > b.playerNumber)
        {
            return +1;
        }
        else if (a.playerNumber < b.playerNumber)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
    public void AddPlayerScore(PlayerData data)
    {
        if (tempScoreBoard.Contains(data))
        {
            return;
        }
        else
        {
            tempScoreBoard.Add(data);
            FillSaveData();
        }
    }

    public void AddScore(int points)
    {
        score += points;
    }
    public void ResetData()
    {
        score = 0;
    }
}
