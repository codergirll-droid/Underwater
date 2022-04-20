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

    Vector3 playerInstantPosition;

    public float floatSpeed;
    public float floatDistance;
    bool canFloat = false;
    Vector3 point1, point2;
    Vector3 target;

    private void Start()
    {
        playerInstantPosition = this.transform.position;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        //Cursor.lockState = CursorLockMode.Locked;

        //Cursor.visible = false;

        moveMultiplierOriginal = moveMultiplier;


        point1 = playerInstantPosition + (Vector3.up * floatDistance);
        point2 = playerInstantPosition - (Vector3.up * floatDistance);

        target = point1;
    }

    // Update is called once per frame
    void Update()
    {

        Inputs();
 



        if (Input.GetKeyDown(KeyCode.L))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


    }

    private void FixedUpdate()
    {
        controlDrag();

        floating();
        rotatePlayer();
        boostMovement();

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

        if(Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow) 
            || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            canFloat = true;
            Debug.Log("playerInstantPosition " + playerInstantPosition);

        }

        if (horizontalMovement != 0 || verticalMovement != 0)
        {
            playerInstantPosition = this.transform.position;
            point1 = playerInstantPosition + (Vector3.up * floatDistance);
            point2 = playerInstantPosition - (Vector3.up * floatDistance);
            target = point1;

            rb.AddForce(moveDirection.normalized * moveSpeed * moveMultiplier, ForceMode.Acceleration);
            canFloat = false;

        }
        else if(horizontalMovement == 0 && verticalMovement == 0)
        {
            canFloat = true;
            

        }


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





    public void floating()
    {

        point1 = playerInstantPosition + (Vector3.up * floatDistance);
        point2 = playerInstantPosition - (Vector3.up * floatDistance);


        if (canFloat)
        {

            if (Vector3.Distance(transform.position, target) <= 0.2f)
            {
                if(target == point1)
                {
                    target = point2;
                }
                else if(target == point2)
                {
                    target = point1;
                }


            }

            Debug.Log("Target is " + target);
            transform.position = Vector3.MoveTowards(transform.position, target, floatSpeed * Time.deltaTime);

        }

    }

}
