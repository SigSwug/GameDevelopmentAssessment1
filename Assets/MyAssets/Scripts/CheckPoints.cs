using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    private Renderer ren;

    public Color inactiveBase;
    public Color inactiveEmission;
    public Color activeBase;
    public Color activeEmission;

    void Start()
    {
        ren = GetComponent<Renderer>();
        ren.material.SetColor("_Color", inactiveBase);
        ren.material.SetColor("_EmissionColor", inactiveEmission);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            int playerNumber = other.GetComponent<PlayerData>().playerNumber - 1;
            LevelManager.instance.playerRespawnPosition[playerNumber] = transform.position;

            ren.material.SetColor("_Color", activeBase);
            ren.material.SetColor("_EmissionColor", activeEmission);
        }
    }
}
