using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name_PNJ : MonoBehaviour
{
    public Transform Looktarget;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(Looktarget.position.x,transform.position.y,Looktarget.position.z);
        transform.LookAt(targetPosition);
    }
}
