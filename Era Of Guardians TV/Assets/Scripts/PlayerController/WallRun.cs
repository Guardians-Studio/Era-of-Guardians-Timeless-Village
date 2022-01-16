using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [Header("GameObject Affectation")]
    [SerializeField] Transform orientation;
    [SerializeField] Camera cam;
    [SerializeField] PlayerMovement playerMovement;
    public KeyConfiguration keyConfiguration;

    private float wallDistance = 1f;
    private float minimumJumpHeight = 0.5f;
    private float wallRunGravity = 0.2f;
    private float wallRunJumpForce = 2f;

    [Header("Camera effects")]
    private float fov = 60f;
    private float wallRunFov = 80f;
    private float wallRunFovTime = 10f;
    private float camTilt = 10f;
    private float camTiltTime = 10f;

    public float tilt { get; private set; }

    private bool wallLeft = false;
    private bool wallRight = false;

    RaycastHit leftWallHit;
    RaycastHit rightWallHit;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        CheckWall();
        print("héa");
        if (CanWallRun())
        {
            print("héu");
            if (wallLeft || wallRight)
            {
                StartWallRun();
            }
            else
            {
                StopWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }

    private bool CanWallRun()
    {
        return (!Physics.Raycast(transform.position, Vector3.down, minimumJumpHeight) && !playerMovement.isGrounded);
    }


    private void CheckWall()
    {
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallDistance);
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallDistance);
    }
    private void StartWallRun()
    {
        print("héo");
        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, wallRunFov, wallRunFovTime * Time.deltaTime);

        if (wallLeft)
        {
            tilt = Mathf.Lerp(tilt, -camTilt, camTiltTime * Time.deltaTime);
        }
        else if (wallRight)
        {
            tilt = Mathf.Lerp(tilt, camTilt, camTiltTime * Time.deltaTime);
        }
           
        if (Input.GetKeyDown(keyConfiguration.jumpKey))
        {
            if (wallLeft)
            {
                Vector3 wallRunJumpDirection = transform.up + leftWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 150, ForceMode.Force);
            }
            else if (wallRight)
            {
                Vector3 wallRunJumpDirection = transform.up + rightWallHit.normal;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); 
                rb.AddForce(wallRunJumpDirection * wallRunJumpForce * 150, ForceMode.Force);
            }
        }
    }

    private void StopWallRun()
    {
        rb.useGravity = true;
        if(playerMovement.isGrounded)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fov, wallRunFovTime * Time.deltaTime);
        }
        tilt = Mathf.Lerp(tilt, 0, camTiltTime * Time.deltaTime);
    }
}