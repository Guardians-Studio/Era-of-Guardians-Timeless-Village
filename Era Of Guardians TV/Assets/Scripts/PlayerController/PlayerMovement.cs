using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("GameObject Assignement")]
    [SerializeField] Transform orientation;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;

    [Header("Key Assignement")]
    [SerializeField] KeyCode leftKey = KeyCode.Q;
    [SerializeField] KeyCode rightKey = KeyCode.D;
    [SerializeField] KeyCode frontKey = KeyCode.Z;
    [SerializeField] KeyCode backKey = KeyCode.S;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Variables")]
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float movementMultiplier = 10f;
    [SerializeField] float groundDrag = 6f;
    [SerializeField] bool isGrounded;
    [SerializeField] float groundDistance = 0.4f;

    [Header("jump/air")]
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float airMultiplier = 0.4f;
    [SerializeField] float airDrag = 2f;


    private float horizontalMovement;
    private float verticalMovement;

    private Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // detect if player is grounded or not 

        GetInput();
        ControlDrag();

        if(Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
            
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void GetInput()
    {
        // get horizontal movement
        if(Input.GetKey(leftKey))
        {
            horizontalMovement = -1f;
        }
        else if(Input.GetKey(rightKey))
        {
            horizontalMovement = 1f;
        }
        else
        {
            horizontalMovement = 0f;
        }


        // get vertical movement
        if (Input.GetKey(backKey))
        {
            verticalMovement = -1f;
        }
        else if (Input.GetKey(frontKey))
        {
            verticalMovement = 1f;
        }
        else
        {
            verticalMovement = 0f;
        }

        // affect the direction
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

    }

    private void ControlDrag()
    {
        if(isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void MovePlayer()
    {
        if(isGrounded)
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * airMultiplier, ForceMode.Acceleration);
        }
    }
}
