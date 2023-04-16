using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollowCamera : MonoBehaviour
{
    public bool startAtOffset = false;

    // camera will follow this object
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public MaxMinFloat offsetY;
    public float speedOffset = 20f;

    public Transform cameraGimbal;

    public Vector2 distanceFromCameraCenter;
    public Vector2 rotationMultipler;
    public Vector2 maxRotation;

    public float smoothRotationSpeed = 0.125f;

    private void Start()
    {
        offset.y = offsetY.max;

        if(startAtOffset)
        {
            transform.position = target.position + offset;
        };
    }

    private void FixedUpdate()
    {
        offset.y = Frame.core.player.controller.currentVelocity / speedOffset;
        offset.y = Mathf.Clamp(offset.y, offsetY.min, offsetY.max);



        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, (smoothSpeed *10f) * Time.deltaTime);

        transform.position = smoothedPosition;

        distanceFromCameraCenter.x = transform.position.x - target.position.x;
        distanceFromCameraCenter.y = transform.position.z - target.position.z;

        Vector3 newRotation = new Vector3(  (distanceFromCameraCenter.y * rotationMultipler.y) , 0f, (distanceFromCameraCenter.x * rotationMultipler.x));

        newRotation.x = Mathf.Clamp(newRotation.x, -maxRotation.x, maxRotation.x);
        newRotation.z = Mathf.Clamp(newRotation.z, -maxRotation.y, maxRotation.y);



        cameraGimbal.eulerAngles = newRotation;



    }
}
