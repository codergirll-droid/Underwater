using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovementController : MonoBehaviour
{

    [Header("Move Variables")]
    public float moveSpeed = 6f;
    public static float moveMultiplier = 10f;
    public float boostMultiplier = 1.5f;
    float moveMultiplierOriginal;

    

    float horizontalMovement, verticalMovement;

    Vector3 moveDirection;
    Rigidbody rb;
    float rbDrag = 6f;

    [Header("Rotation Variables")]
    public float sensX;
    public float sensY;
    public float rotMultiplier = 0.5f;


    float mouseX, mouseY;
    float xRot, yRot;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        //Cursor.lockState = CursorLockMode.Locked;

        //Cursor.visible = false;

        moveMultiplierOriginal = moveMultiplier;
    }

    // Update is called once per frame
    void Update()
    {

        Inputs();
        controlDrag();
        rotatePlayer();
        boostMovement();


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
        //Vector3.Lerp(rb.rotation.eulerAngles, new Vector3(xRot, yRot, 0), 0.5f);
        
        rb.rotation = Quaternion.Euler(xRot, yRot, 0);
        
        //transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        //transform.rotation = Quaternion.Euler(xRot, yRot, 0);

    }

    void boostMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveMultiplier = moveMultiplierOriginal * boostMultiplier;
        }
        else
        {
            moveMultiplier = moveMultiplierOriginal;
        }
    }

}
