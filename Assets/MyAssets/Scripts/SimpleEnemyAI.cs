using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyAI : MonoBehaviour
{
    public List<GameObject> players;

    public Transform target;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        foreach (PlayerData player in LevelManager.instance.players)
        {
            players.Add(player.gameObject);
        }
        CheckForClosestPlayer();

        InvokeRepeating("CheckForClosestPlayer", 3, 3);
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }

    void CheckForClosestPlayer()
    {
        Debug.Log("Checking for players");

        target = players[0].transform;

        for (int i = 1; i < players.Count; i++)
        {
            if (Vector3.Distance(transform.position, players[i].transform.position) < Vector3.Distance(transform.position, target.position))
            {
                Debug.Log("A new player is being targeted");
                target = players[i].transform;
            }
        }
    }
}
