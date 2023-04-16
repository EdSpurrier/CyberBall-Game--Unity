using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroInput : MonoBehaviour
{
    public bool isFlat = true;
    public Vector3 tilt;

    void Start()
    {


    }



    protected void Update()
    {
        tilt = Input.acceleration;

        if (isFlat)
        {
            tilt = Quaternion.Euler(90, 0, 0) * tilt;
        };

        Debug.DrawRay(transform.position + Vector3.up, tilt, Color.cyan);
    }

    public Vector3 GetTilt()
    {
        return tilt;
    }


}