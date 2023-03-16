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

            //Get a reference to the movement script and deactivate it.
            CharacterMovementTutorial moveScript = other.GetComponent<CharacterMovementTutorial>();
            moveScript.enabled = false;

            //Move the player to the spawn point.
            other.gameObject.transform.position = LevelManager.instance.playerRespawnPosition[playerNumber];

            //Starts a corutine to restart the movesciprt after a brief pause.
            StartCoroutine(ReactivateMovement(moveScript));
        }
    }

    //Restarts the move script
    IEnumerator ReactivateMovement(CharacterMovementTutorial player)
    {
        yield return new WaitForSeconds(1);
        player.enabled = true;
        yield return null;
    }
}
