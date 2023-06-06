using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    #region Fields

    public TMP_InputField joinRoomName;
    public TMP_InputField createRoomName;
    public TMP_Text errorLog;

    public byte maxPlayersPerRoom = 2;

    #endregion

    #region Public Functions

    public void JoinRoomWithDelay()
    {
        Invoke("JoinRoom", 0.3f);
    }
    public void CreateRoomWithDelay()
    {
        Invoke("CreateRoom", 0.3f);
    }

    void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomName.text);
    }
    void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomName.text, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    #endregion

    #region PUN Callbacks

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        string errorMessage = "Failed to join room. Error: " + message;
        Debug.Log(errorMessage);
        errorLog.text = errorMessage;
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("WaitingForPlayers");
    }

    #endregion
}
