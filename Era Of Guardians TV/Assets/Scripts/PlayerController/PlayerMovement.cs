using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("KeyAssignement")]
    [SerializeField] KeyCode leftKey = KeyCode.Q;
    [SerializeField] KeyCode rightKey = KeyCode.D;
    [SerializeField] KeyCode frontKey = KeyCode.Z;
    [SerializeField] KeyCode backKey = KeyCode.S;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;

    [Header("Variables")]

    [Header("ground")]
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float movementMultiplier = 10f;
    [SerializeField] float groundDrag = 6f;
    [SerializeField] bool isGrounded;
    
    [Header("jump/air")]
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float airMultiplier = 0.4f;
    [SerializeField] float airDrag = 2f;
    
    [Header("Others")]
    [SerializeField] float playerHeight = 2f; // modify regarding playerHeight bcs of isGrounded verification
    

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
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f); // detect if player is grounded or not 

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
        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;

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
