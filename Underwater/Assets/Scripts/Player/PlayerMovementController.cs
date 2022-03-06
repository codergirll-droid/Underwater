using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovementController : MonoBehaviour
{

    [Header("Move Variables")]
    public float moveSpeed = 6f;
    public float moveMultiplier = 10f;

    float horizontalMovement, verticalMovement;

    Vector3 moveDirection;
    Rigidbody rb;
    float rbDrag = 6f;

    [Header("Rotation Variables")]
    public float sensX;
    public float sensY;

    float mouseX, mouseY;
    float rotMultiplier = 0.1f;
    float xRot, yRot;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        controlDrag();
        rotatePlayer();

        if (Input.GetKeyDown(KeyCode.L))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    private void FixedUpdate()
    {
        movement();
    }


    void Inputs()
    {
        //movement
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

        //rotation
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRot += mouseX * sensX * rotMultiplier;
        xRot -= mouseY * sensY * rotMultiplier;

        xRot = Mathf.Clamp(xRot, -90f, 90f);

    }

    void movement()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);

    }

    void controlDrag()
    {
        rb.drag = rbDrag;
    }


    void rotatePlayer()
    {
        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
    }


}
