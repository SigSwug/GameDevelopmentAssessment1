using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacksTutorial : MonoBehaviour
{
    Animator anim;

    public GameObject projectile;
    public Transform launchPoint;

    public float lobSpeed, lobLift;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            anim.Play("Shoot");
            Shoot();
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
        GameObject currentProj = Instantiate(projectile, launchPoint.position, Quaternion.LookRotation(anim.transform.forward));
        currentProj.GetComponent<Rigidbody>().AddForce((anim.transform.forward * lobSpeed) + (Vector3.up * lobLift), ForceMode.Impulse);
    }
}
