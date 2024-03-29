using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    GameData saveData = new GameData();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            saveData.AddScore(-1);
            PrintScore();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            saveData.AddScore(1);
            PrintScore();
        }

        /*if (Input.GetKeyDown(KeyCode.S))
        {
            saveData.FillSaveData();
            SaveSystem.instance.SaveGame(saveData);
            Debug.Log("the game has been saved");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            saveData = SaveSystem.instance.LoadGame();
            saveData.FillTempList();
            Debug.Log("new data loaded");
            PrintScore();
        }*/
        if (Input.GetKeyDown(KeyCode.R))
        {
            saveData.ResetData();
            PrintScore();
        }
    }

    void PrintScore()
    {
        Debug.Log("The current score is " + saveData.score);
    }
}
