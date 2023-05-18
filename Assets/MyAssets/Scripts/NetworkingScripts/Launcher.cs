using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    #region Serializable Fields
    #endregion

    #region Private Fields

    //This is the clients version number.
    string gameVersion = "1";

    #endregion

    #region Monobehaviour Callbacks

    void Awake()
    {
        //CRITICAL
        //Make sure everyones scenes are synched when PhotonNetwork.LoadLevel is called;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    //On start, try to connect to the master server
    void Start()
    {
        Connect();
    }

    #endregion

    #region Public Methods

    public void Connect()
    {
        if (PhotonNetwork.IsConnected)
        {
            //Attempt to join lobby
            PhotonNetwork.JoinLobby();
        }
        else
        {
            //Attempt to connect using server settings, then set your game version
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    #endregion

    #region PUN Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Successfully connected to server. Attempting to join a lobby");
        PhotonNetwork.JoinLobby();
    }
    //Moniter for disconnecting
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("Failed to connect. OnDisconnected was called with the reason {0}", cause);
        SceneManager.LoadScene(0);
    }
    //Load the next scene if we successfully join a lobby.
    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("CreateOrJoinRoom");
    }

    #endregion
}
