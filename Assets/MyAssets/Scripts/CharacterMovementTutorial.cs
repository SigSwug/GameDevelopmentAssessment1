using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterMovementTutorial : MonoBehaviour
{
    [Header("Movement Values")]
    [SerializeField] float movementSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float gravityForce;
    [SerializeField] float jumpForce;

    [Header("Controls")]
    public string horizontalMovement, verticalMovement;
    public KeyCode jump;
    public KeyCode sprint;

    //Components
    CharacterController cc;
    Animator anim;

    Vector3 moveDirection;
    Camera cam;

    public Transform target;

    //Gravity and jump
    Vector3 playerVelocity;
    public bool groundedPlayer;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        cam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        groundedPlayer = cc.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            if (anim.GetBool("Jump")) anim.SetBool("Jump", false);
            playerVelocity.y = 0;
        }

        float h = Input.GetAxis(horizontalMovement);
        float v = Input.GetAxis(verticalMovement);

        if (Input.GetKeyDown(sprint))
        {
            movementSpeed = 5;
        }
        else if (Input.GetKeyUp(sprint))
        {
            movementSpeed = 3;
        }

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
        anim.transform.rotation = Quaternion.Lerp(anim.transform.rotation, desiredDirection, rotationSpeed);

        Vector3 animationVector = anim.transform.InverseTransformDirection(cc.velocity);

        anim.SetFloat("HorizontalSpeed", animationVector.x);
        anim.SetFloat("VerticalSpeed", animationVector.z);

        ProcessGravity();
    }
    public void ProcessGravity()
    {
        if (Input.GetKeyDown(jump) && groundedPlayer)
        {
            anim.SetBool("Jump", true);
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravityForce);
        }
        playerVelocity.y += gravityForce * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);
    }
}
