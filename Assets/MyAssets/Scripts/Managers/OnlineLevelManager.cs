using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

/// <summary>
/// Manages Game State in a level, manages respawning, and timer
/// </summary>

public class OnlineLevelManager : MonoBehaviour
{
    #region Singleton
    public static OnlineLevelManager instance;

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
    public GameObject[] playerPrefabs;
    public GameObject[] inLevelPlayerNames;
    public Vector3[] playerRespawnPosition;
    public Transform[] playerSpawns;

    //timer
    [Header("LevelSettings")]
    public Timer timer;
    public PlayerData playerData;

    //game states
    public enum GameStates {Won, Lost, Paused, Start, GameOver}
    public GameStates currentState;

    [Header("Attached Components and Scripts")]
    public InLevelUIManager UIManager;
    PhotonView view;

    void Start()
    {
        view = GetComponent<PhotonView>();
        Time.timeScale = 1;

        //1800 for 30 minutes
        //1200 for 20 minutes
        //600 for 10 minutes
        timer.StartTimer(600f);

        /*for (int i = 0; i < inLevelPlayerNames.Length; i++)
        {
            inLevelPlayerNames[i].GetComponentInChildren<TMP_Text>().text = GameManager.instance.currentPlayers[i].playerName;
        }*/

        Invoke("SpawnPlayerAtStart", 1);
    }

    void SpawnPlayerAtStart()
    {
        //SpawnPlayer
        int a = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        GameObject player = PhotonNetwork.Instantiate(playerPrefabs[a].name, playerSpawns[a].position, Quaternion.identity).gameObject;
        if (player == null) Debug.Log("No player reference");
        view.RPC("AddPlayerToList", RpcTarget.All, a, player);

        //assign camera in scene
        player.GetComponentInChildren<Camera>().tag = "MainCamera";
    }

    [PunRPC]
    void AddPlayerToList(int num, GameObject player)
    {
        players[num] = player;
    }

    //Used in Unity Events on the finish line objects
    public void SetGameStateToWon()
    {
        currentState = GameStates.Won;
        GameManager.instance.currentPlayers[0].timeLeft = timer.currentTime;
        
        if (currentState == GameStates.Won)
        {
            UIManager.UpdateUI();
            UIManager.EndGameUI();
            Invoke("SaveResultsAndLoadScene", 1);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            timer.isTiming = false;
            Time.timeScale = 0.5f;
        }
    }
    public void SetGameStateToLost()
    {
        currentState = GameStates.Lost;
        GameManager.instance.currentPlayers[1].timeLeft = timer.currentTime;
        
        if (currentState == GameStates.Lost)
        {
            UIManager.UpdateUI();
            UIManager.EndGameUI();
            Invoke("SaveResultsAndLoadScene", 1);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            timer.isTiming = false;
            Time.timeScale = 0.5f;
        }
    }

    void Update()
    {
        //UI update
        if (currentState == GameStates.Start)
        {
            UIManager.UpdateUI();
        }
        
        //run game over if timer runs out before level completion
        else if (currentState == GameStates.GameOver)
        {
            UIManager.UpdateUI();
            UIManager.EndGameUI();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            timer.isTiming = false;
            Invoke("GameFailed", 0.6f);
            Time.timeScale = 0.2f;
        }
    }

    void SaveResultsAndLoadScene()
    {
        GameManager.instance.FillTempList();
        GameManager.instance.FillSaveData();
        SceneManager.LoadScene("Results");
    }
    void GameFailed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
