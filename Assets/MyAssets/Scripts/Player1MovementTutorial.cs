using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1MovementTutorial : MonoBehaviour
{
    [Tooltip("Movement Values")]
    [SerializeField] float movementSpeed, rotationSpeed, gravityForce, jumpForce;

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

        cam = Camera.main;
    }

    void Update()
    {
        groundedPlayer = cc.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            if (anim.GetBool("Jump")) anim.SetBool("Jump", false);
            playerVelocity.y = 0;
        }

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
        anim.transform.rotation = Quaternion.Lerp(anim.transform.rotation, desiredDirection, rotationSpeed);

        Vector3 animationVector = anim.transform.InverseTransformDirection(cc.velocity);

        anim.SetFloat("HorizontalSpeed", animationVector.x);
        anim.SetFloat("VerticalSpeed", animationVector.z);

        ProcessGravity();
    }
    public void ProcessGravity()
    {
        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
        {
            anim.SetBool("Jump", true);
            playerVelocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravityForce);
        }
        playerVelocity.y += gravityForce * Time.deltaTime;
        cc.Move(playerVelocity * Time.deltaTime);
    }
}
