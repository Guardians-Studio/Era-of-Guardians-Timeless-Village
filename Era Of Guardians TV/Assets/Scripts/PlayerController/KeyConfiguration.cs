using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyConfiguration : MonoBehaviour
{
    [Header("Key Assignement")]
    public KeyCode leftKey = KeyCode.Q;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode frontKey = KeyCode.Z;
    public KeyCode backKey = KeyCode.S;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;
    public int leftMouseKey = 0;
    public int rightMouseKey = 1;
    public KeyCode oneKey = KeyCode.Alpha1;
    public KeyCode twoKey = KeyCode.Alpha2;
    public KeyCode threeKey = KeyCode.Alpha3;
    public KeyCode fourKey = KeyCode.Alpha4;
}
