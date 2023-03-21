using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Manages Game State in a level, manages respawning, and timer
/// </summary>

public class LevelManager : MonoBehaviour
{
    #region Singleton
    public static LevelManager instance;

    private void Awake()
    {
        //If there's already a script of this in the scene, destroy this script
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    //list of player prefabs
    [Header("Players")]
    public GameObject[] players;
    //public Transform[] playerSpawns;

    public Vector3[] playerRespawnPosition;

    //timer
    [Header("LevelSettings")]
    public Timer timer;

    //game states
    public enum GameStates {Won, Lost, Paused, Start, GameOver}
    public GameStates currentState;

    [Header("Attached Components and Scripts")]
    public InLevelUIManager UIManager;

    void Start()
    {
        //1800 for 30 minutes
        //1200 for 20 minutes
        //600 for 10 minutes
        timer.StartTimer(300f);
    }

    void Update()
    {
        //UI update
        if (currentState == GameStates.Start)
        {
            UIManager.UpdateUI();
        }
        else if (currentState == GameStates.Won)
        {
            UIManager.UpdateUI();
            UIManager.EndGameUI();
            
            Time.timeScale = 0;
        }
        else if (currentState == GameStates.Lost)
        {
            UIManager.UpdateUI();
            UIManager.EndGameUI();

            Time.timeScale = 0;
        }
        //run game over if timer runs out before level completion
        else if (currentState == GameStates.GameOver)
        {
            UIManager.UpdateUI();
            UIManager.EndGameUI();

            Time.timeScale = 0;
        }
    }
    public void SetGameStateToWon()
    {
        currentState = GameStates.Won;
    }
    public void SetGameStateToLost()
    {
        currentState = GameStates.Lost;
    }
}
