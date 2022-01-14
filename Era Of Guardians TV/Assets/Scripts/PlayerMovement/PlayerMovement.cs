using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("KeyAssignement")]
    public KeyCode leftKey = KeyCode.Q;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode frontKey = KeyCode.Z;
    public KeyCode backKey = KeyCode.S;

    [Header("Variables")]
    public float movementSpeed = 15f;
    public float rbDrag = 2f;

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
        GetInput();
        ControlDrag();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
        //get horizontal movement
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


        //get vertical movement
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
        rb.drag = rbDrag;
    }

    private void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * movementSpeed, ForceMode.Acceleration);
    }
}
