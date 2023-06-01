using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlinePlayerAttacks : MonoBehaviour
{
    Animator anim;

    [Header("Ranged Attack Settings")]
    public GameObject projectile;
    public Transform launchPoint;
    public float lobSpeed, lobLift;

    [Header("Melee Attack Settings")]
    public GameObject meleeCollider;
    public float attackDelay;
    public float attackLifeTime;

    PhotonView view;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (!view.IsMine) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.Play("Shoot");
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            anim.Play("MeleeAttack");
            view.RPC("OnlineMeleeAttack", RpcTarget.All);
        }
        if (Input.GetKey(KeyCode.B))
        {
            anim.SetBool("Taunt", true);
        }
        else
        {
            anim.SetBool("Taunt", false);
        }
    }
    void Shoot()
    {
        GameObject currentProj = PhotonNetwork.Instantiate(projectile.name, launchPoint.position, Quaternion.LookRotation(anim.transform.forward));

        currentProj.GetComponent<Rigidbody>().AddForce((anim.transform.forward * lobSpeed) + (Vector3.up * lobLift), ForceMode.Impulse);
    }
    [PunRPC]
    void OnlineMeleeAttack()
    {
        Invoke("ActivateMeleeCollider", attackDelay);
    }
    void ActivateMeleeCollider()
    {
        meleeCollider.SetActive(true);
        Invoke("DeactivateMeleeCollider", attackLifeTime);
    }
    void DeactivateMeleeCollider()
    {
        meleeCollider.SetActive(false);
    }
}
