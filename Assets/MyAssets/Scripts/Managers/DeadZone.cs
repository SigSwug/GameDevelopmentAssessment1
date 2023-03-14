using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int playerNumber = other.GetComponent<PlayerData>().playerNumber - 1;

            LevelManager.instance.players[playerNumber].transform.position = LevelManager.instance.playerRespawnPosition[playerNumber];
        }
    }
}
