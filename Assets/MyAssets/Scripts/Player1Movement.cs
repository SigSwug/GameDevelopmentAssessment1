using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    [Tooltip("Movement Values")]
    [SerializeField] float movementSpeed, rotationSpeed;

    //Components
    CharacterController cc;
    Animator anim;

    Vector3 moveDirection;
    Camera cam;

    public Transform target;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        cam = Camera.main;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //determine camera direction on a flat plain
        Vector3 camh = cam.transform.right;
        Vector3 camv = Vector3.Cross(camh, Vector3.up);

        if (h != 0 || v != 0)
        {
            moveDirection = camh * h + camv * v;
            moveDirection.Normalize();

            cc.Move(moveDirection * movementSpeed * Time.deltaTime);

            anim.SetBool("HasInput", true);
        }
        else
        {
            anim.SetBool("HasInput", false);
        }

        Quaternion desiredDirection = Quaternion.LookRotation(moveDirection);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredDirection, rotationSpeed);

        Vector3 animationVector = anim.transform.InverseTransformDirection(cc.velocity);

        anim.SetFloat("HorizontalSpeed", animationVector.x);
        anim.SetFloat("VerticalSpeed", animationVector.z);
    }
}
