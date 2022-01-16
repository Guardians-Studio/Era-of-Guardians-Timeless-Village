using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [Header("GameObject Assignement")]
    [SerializeField] Transform orientation;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] Camera cam;
    [SerializeField] GameObject playerCapsule;
    public KeyConfiguration keyConfiguration;

    CapsuleCollider playerCollider;
    private float playerHeight;

    private float movementSpeed = 6f;
    private float walkSpeed = 4f;
    private float sprintSpeed = 6f;
    private float acceleration = 10f;
    private float movementMultiplier = 10f;
    private float groundDrag = 6f;
    
    public bool isGrounded { get; private set; }
    private bool isCrouching = false;
    private float groundDistance = 0.2f;

    private float jumpForce = 7f;
    private float airMultiplier = 0.4f;
    private float airDrag = 2f;


    private float fov = 60f;
    private float sprintFov = 80f;
    private float SprintFovTime = 10f;

    private float horizontalMovement;
    private float verticalMovement;

    private Vector3 moveDirection;
    private Vector3 slopeDirection;

    Rigidbody rb;

    RaycastHit slopeHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        playerCollider = GetComponentInChildren<CapsuleCollider>();
        playerHeight = playerCollider.height;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // detect if player is grounded or not 

        GetInput();
        ControlDrag();
        ControlSpeed();

        if (Input.GetKeyDown(keyConfiguration.jumpKey) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(keyConfiguration.crouchKey) && isGrounded && !isCrouching)
        {
            isCrouching = true;
            HandleCrouch();
        }
        else if(Input.GetKeyDown(keyConfiguration.crouchKey) && isGrounded && !Physics.Raycast(transform.position, Vector3.up, 0.5f) && isCrouching)
        {
            isCrouching = false;
            CrouchStand();
        }

        slopeDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Jump()
    {
        if(isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void HandleCrouch()
    {
        playerCapsule.transform.localScale -= new Vector3(0, 0.5f, 0);
        playerCapsule.transform.position -= new Vector3(0, 0.5f, 0);
    }
    private void CrouchStand()
    {
        playerCapsule.transform.position += new Vector3(0, 0.5f, 0);
        playerCapsule.transform.localScale += new Vector3(0, 0.5f, 0);
    }

    private void GetInput()
    {
        // get horizontal movement
        if(Input.GetKey(keyConfiguration.leftKey))
        {
            horizontalMovement = -1f;
        }
        else if(Input.GetKey(keyConfiguration.rightKey))
        {
            horizontalMovement = 1f;
        }
        else
        {
            horizontalMovement = 0f;
        }


        // get vertical movement
        if (Input.GetKey(keyConfiguration.backKey))
        {
            verticalMovement = -1f;
        }
        else if (Input.GetKey(keyConfiguration.frontKey))
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

    private void ControlSpeed() 
    {
        if(Input.GetKey(keyConfiguration.sprintKey) && isGrounded)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, sprintFov, SprintFovTime * Time.deltaTime);
            movementSpeed = Mathf.Lerp(movementSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, SprintFovTime * Time.deltaTime);
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    private void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeDirection.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    private bool OnSlope() // check if player is on slope
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
