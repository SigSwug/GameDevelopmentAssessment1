using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameData saveData;

    //list of current player names
    public List<PlayerData> currentPlayers = new List<PlayerData>();

    public List<PlayerData> tempScoreBoard = new List<PlayerData>();

    void Start()
    {
        #region Singleton
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        //Attempt to load a save data.
        saveData = SaveSystem.instance.LoadGame();

        //Check if successful, if not, create a new save data.
        if (saveData == null) saveData = new GameData();

        //Create the temp list for sorting later.
        FillTempList();

        //Create a new current player list.
        currentPlayers = new List<PlayerData>(0);
        currentPlayers.Add(new PlayerData());
        currentPlayers.Add(new PlayerData());
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.J))
        {
            FillTempList();
            FillSaveData();
        }*/
    }

    public void FillTempList()
    {
        tempScoreBoard = new List<PlayerData>();
        for (int i = 0; i < 10; i++)
        {
            PlayerData data = new PlayerData();
            data.playerName = saveData.highScorePlayerNames[i];
            data.timeLeft = saveData.highScoreTimeRemainingOnCompletion[i];
            tempScoreBoard.Add(data);
        }
        foreach (PlayerData player in currentPlayers)
        {
            tempScoreBoard.Add(player);
        }
    }

    public void FillSaveData()
    {
        tempScoreBoard.Sort(PlayerSortFunction);

        for (int i = 0; i < 10; i++)
        {
            saveData.highScorePlayerNames[i] = tempScoreBoard[i].playerName;
            saveData.highScoreTimeRemainingOnCompletion[i] = tempScoreBoard[i].timeLeft;
        }
        SaveSystem.instance.SaveGame(saveData);
    }
    int PlayerSortFunction(PlayerData a, PlayerData b)
    {
        if (a.timeLeft < b.timeLeft)
        {
            return +1;
        }
        else if (a.timeLeft > b.timeLeft)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    //Example stuff that hasn't been removed
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
    /*void CheckForEmpties()
    {
        if (saveData.highScorePlayerNames.Length == 0)
        {
            saveData.highScorePlayerNames = new string[10];
        }
        if (saveData.highScoreTimeRemainingOnCompletion.Length == 0)
        {
            saveData.highScoreTimeRemainingOnCompletion = new float[10];
        }
    }*/
}
