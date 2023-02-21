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

        Vector3 camh = cam.transform.right;
        Vector3 camv = Vector3.Cross(camh, Vector3.up);

        moveDirection = camh * h + camv * v;
        moveDirection.Normalize();

        cc.Move(moveDirection * movementSpeed * Time.deltaTime);
    }
}
