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
    public PlayerData[] players;
    public Transform[] playerSpawns;

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
    }
    //run game over if timer runs out before level completion
    public void LevelEnded()
    {
        currentState = GameStates.GameOver;

        UIManager.UpdateUI();
    }
    //update saved highscore if time taken to complete the level is faster ther last
}
