using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("GameObjects affectation")]
    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;

    [Header("Variables affectation")]
    public float sensitivityX = 100f;
    public float sensitivityY = 100f;
    public float sensitivityMultiplier = 0.01f;

    private float mouseX;
    private float mouseY;

    private float rotationX;
    private float rotationY;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        GetInput();

        cam.transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.transform.rotation = Quaternion.Euler(0, rotationY, 0);
    }

    private void GetInput()
    {
        mouseY = Input.GetAxisRaw("Mouse X");
        mouseX = Input.GetAxisRaw("Mouse Y") * (-1);

        rotationX += mouseX * sensitivityX * sensitivityMultiplier;
        rotationY += mouseY * sensitivityY * sensitivityMultiplier;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
    }
}
