using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class OnlineLobby : MonoBehaviourPunCallbacks
{
    public bool[] playersReady;

    public PhotonView view;

    public TMP_Text roomName;
    public TMP_Text messages;
    public TMP_Text numberOfPlayers;
    public TMP_InputField playerName;

    public string levelName;

    void Start()
    {
        playersReady = new bool[PhotonNetwork.CurrentRoom.MaxPlayers];
        PhotonNetwork.LocalPlayer.NickName = "Player " + PhotonNetwork.LocalPlayer.ActorNumber;
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;

        numberOfPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString() + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        numberOfPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString() + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
        Invoke("UpdateBoolsOnJoin", 1);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        numberOfPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString() + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
        view.RPC("ReadyPlayer", RpcTarget.All, otherPlayer.ActorNumber, false);
    }

    public void UpdateName()
    {
        PhotonNetwork.LocalPlayer.NickName = playerName.text;
        GameManager.instance.currentPlayers[PhotonNetwork.LocalPlayer.ActorNumber - 1].playerName = playerName.text;
    }

    public void LoadLevelWithDelay()
    {
        Invoke("LoadLevel", 0.3f);
    }

    void LoadLevel()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            messages.text = "Waiting for Host to start";
        }
        else if (PhotonNetwork.CurrentRoom.PlayerCount < PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            messages.text = "Not enough players to start";
        }
        else if (AllPlayersReady() == false)
        {
            messages.text = "All players must be ready to start";
        }
        else
        {
            SceneManager.LoadScene(levelName);
        }
    }

    bool AllPlayersReady()
    {
        foreach(bool item in playersReady)
        {
            if (item == false) return false;
        }
        return true;
    }

    [PunRPC]
    public void ReadyPlayer(int playerNumber, bool isReady)
    {
        playersReady[playerNumber -1] = isReady;
    }
    public void RunReadyPlayer(bool isReady)
    {
        view.RPC("ReadyPlayer", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber, isReady);
    }

    void UpdateBoolsOnJoin()
    {
        int playerNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        bool isReady = playersReady[playerNumber - 1];

        view.RPC("ReadyPlayer", RpcTarget.All, playerNumber, isReady);
    }
}
