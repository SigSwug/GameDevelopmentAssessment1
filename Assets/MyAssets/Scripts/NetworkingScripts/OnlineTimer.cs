using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class OnlineTimer : MonoBehaviour, IOnEventCallback
{
    PhotonView view;

    public float startTime;
    public float currentTime;

    public string displayTime;
    public bool isTiming = false;

    //define the photon event
    private const byte TIMER_TICK = 1;

    //public UnityEvent timesUp;

    #region Photon Raise Event Code
    //enable and disable the ability to listen to events
    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    //listen to the event and react to it
    public void OnEvent(EventData data)
    {
        if (data.Code == TIMER_TICK)
        {
            object[] localData = (object[])data.CustomData;
            displayTime = (string)localData[0];
            currentTime = (float)localData[1];
        }
    }
    #endregion

    void Start()
    {
        view = GetComponent<PhotonView>();
    }

    void FixedUpdate()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        if (isTiming)
        {
            currentTime -= Time.deltaTime;

            //format the time
            string minutes = Mathf.Floor(currentTime / 60).ToString("00");
            string seconds = (currentTime % 60).ToString("00");

            if (currentTime <= 0)
            {
                displayTime = "00:00";
                isTiming = false;
            }
            else
            {
                displayTime = string.Format("{0}:{1}", minutes, seconds);
                object[] data = new object[] { displayTime, currentTime };
                PhotonNetwork.RaiseEvent(TIMER_TICK, data, RaiseEventOptions.Default, SendOptions.SendUnreliable);
            }
        }
    }

    void Update()
    {
        if (OnlineLevelManager.instance.currentState == OnlineLevelManager.GameStates.Won)
        {
            isTiming = false;
        }
        else if (OnlineLevelManager.instance.currentState == OnlineLevelManager.GameStates.Lost)
        {
            isTiming = false;
        }
        else if (currentTime <= 0)
        {
            view.RPC("TimesUp", RpcTarget.All);
        }
    }
    [PunRPC]
    void TimesUp()
    {
        OnlineLevelManager.instance.currentState = OnlineLevelManager.GameStates.GameOver;
    }

    public void StartTimer(float length)
    {
        startTime = length;
        currentTime = startTime;
        isTiming = true;
    }
}