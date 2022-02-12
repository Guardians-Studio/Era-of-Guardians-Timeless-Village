using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerLook : MonoBehaviour
{
    [Header("GameObjects affectation")]
    [SerializeField] Transform cam;
    [SerializeField] Transform capsule;
    [SerializeField] WallRun wallRun;
    [SerializeField] GameObject weaponHolder;

    private float sensitivityX = 100f;
    private float sensitivityY = 100f;
    private float sensitivityMultiplier = 0.01f;

    private float mouseX;
    private float mouseY;

    private float rotationX;
    private float rotationY;

    PhotonView view;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
        {
            GetInput();

            cam.transform.localRotation = Quaternion.Euler(rotationX, 0, wallRun.tilt);
            capsule.transform.localRotation = Quaternion.Euler(0, rotationY, 0);
            weaponHolder.transform.localRotation = Quaternion.Euler(rotationX, 0, 0); // rotate position of weapons with capsule
        }
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
