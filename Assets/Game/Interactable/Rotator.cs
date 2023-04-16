using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Title("Settings")]
    public bool active = true;
    public Vector3 rotateDirection;
    public float smoothing = 1f;
    public float tolerance = 0.1f;

    [Title("Parts")]
    public Transform rotateObject;

    [Title("Speeds")]
    public float targetSpeed = 15f;
    public float normalSpeed = 4f;

    [Title("System")]
    public float currentSpeed = 1f;
    
    public void ResetRotateSpeed()
    {
        targetSpeed = normalSpeed;
    }

    public void UpdateRotateSpeed(float newSpeed)
    {
        targetSpeed = newSpeed;
    }

    


    private void FixedUpdate()
    {
        if(currentSpeed.Difference(targetSpeed) > tolerance)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, (smoothing * Time.deltaTime));
        };
    }








    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            return;
        };

        rotateObject.Rotate(rotateDirection * ( (currentSpeed * Time.deltaTime) * 10) );
    }




}
