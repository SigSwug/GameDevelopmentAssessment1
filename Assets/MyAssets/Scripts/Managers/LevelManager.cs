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
        timer.StartTimer(3f);
    }

    void Update()
    {
        //UI update
    }
    //run game over if timer runs out before level completion
    public void LevelEnded()
    {
        //UI update
    }
    //update saved highscore if time taken to complete the level is faster ther last
}
