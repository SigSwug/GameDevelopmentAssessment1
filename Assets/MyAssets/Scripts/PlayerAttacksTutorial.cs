using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacksTutorial : MonoBehaviour
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

    [Header("Controls")]
    public KeyCode ranged;
    public KeyCode melee;
    public KeyCode taunt;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(ranged))
        {
            anim.Play("Shoot");
            Shoot();
        }
        if (Input.GetKeyDown(melee))
        {
            anim.Play("MeleeAttack");
            Invoke("ActivateMeleeCollider", attackDelay);
        }
        if (Input.GetKey(taunt))
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
        GameObject currentProj = Instantiate(projectile, launchPoint.position, Quaternion.LookRotation(anim.transform.forward));
        currentProj.GetComponent<Rigidbody>().AddForce((anim.transform.forward * lobSpeed) + (Vector3.up * lobLift), ForceMode.Impulse);
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
