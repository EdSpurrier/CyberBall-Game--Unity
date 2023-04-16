using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Title("Controller System")]
    public bool grounded = false;
    public bool rolling = false;

    [Title("Stats")]
    public float gyroInputMultiplier = 55f;
    public float movementSpeed = 25f;
    public float jumpVelocity = 25f;
    public float rollTollerance = 0.05f;

    [BoxGroup("Player FX")]
    [FoldoutGroup("Player FX/Disappear")]
    [HideLabel]
    public FrameCoreEvent disappearEvent = new FrameCoreEvent
    {
        eventName = "Disappear"
    };
    [FoldoutGroup("Player FX/Reappear")]
    [HideLabel]
    public FrameCoreEvent reappearEvent = new FrameCoreEvent
    {
        eventName = "Reappear"
    };
    [FoldoutGroup("Player FX/Bounce")]
    [HideLabel]
    public FrameCoreEvent bounceEvent = new FrameCoreEvent
    {
        eventName = "Bounce"
    };

    [BoxGroup("Player FX")]
    [FoldoutGroup("Player FX/Roll Start")]
    [HideLabel]
    public FrameCoreEvent rollStart = new FrameCoreEvent
    {
        eventName = "Roll Start"
    };
    [FoldoutGroup("Player FX/Roll Stop")]
    [HideLabel]
    public FrameCoreEvent rollStop = new FrameCoreEvent
    {
        eventName = "Roll Stop"
    };

    [Title("Parts")]
    public Transform playerBase;
    public Rigidbody rigidBody;
    public Collider bodyCollider;

    [Title("System")]
    public float playerHeight = 0f;
    public Vector3 movementForce = Vector3.zero;
    public float currentVelocity = 0f;

    public Vector3 lastPosition = Vector3.zero;

    public void Awake()
    {
        playerHeight = bodyCollider.bounds.size.y;
    }

    void FixedUpdate()
    {
        currentVelocity = Vector3.Distance(transform.position, lastPosition) / Time.deltaTime;
        lastPosition = transform.position;

        if (currentVelocity > rollTollerance)
        {
            if (rolling && !grounded)
            {
                rollStop.Activate();
                rolling = false;
            }
            else if (!rolling && grounded)
            {
                rollStart.Activate();
                rolling = true;
            };
        }
        else {

            if (rolling)
            {
                rollStop.Activate();
                rolling = false;
            };
        };
        
    }

    public void ResetVelocity()
    {
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    public void MovePlayerToPoint(Vector3 point)
    {
        rigidBody.transform.position = point;
        rigidBody.transform.rotation = Quaternion.identity;
    }

    public void ToggleGravity(bool state)
    {
        rigidBody.useGravity = state;
    }

    public void Push(float velocity, Vector3 direction)
    {
        Vector3 pushForce = direction * velocity;

        //  ADD FORCE TO BALL
        rigidBody.AddForce(pushForce);
    }


    public void ForceJump()
    {
        //  CHECK IF PLAYER IS GROUNDED
        CheckGround();


        //  IF GROUNDED THEN GET JUMP INPUT
        float jumpInput = (grounded ? 1f : 0f);

        //  ADD JUMP VELOCITY
        jumpInput *= (jumpVelocity * 10f);


        //  SET FORCE TO XZ FROM XY INPUT
        Vector3 movementForce = new Vector3(0f, jumpInput, 0f);


        //  ADD FORCE TO BALL
        rigidBody.AddForce(movementForce);
    }



    public void UpdateController(float deltaTime)
    {

        //  CHECK IF PLAYER IS GROUNDED
        CheckGround();


        if (Frame.core.input.inputType == InputType.Gyro)
        {
            Vector3 input = Frame.core.input.GetGyroInput();




            //  ADD MOVEMENT SPEED
            input.x *= ((movementSpeed * deltaTime) * 10f);
            input.z *= ((movementSpeed * deltaTime) * 10f);

            input.x *= (gyroInputMultiplier * deltaTime);
            input.z *= (gyroInputMultiplier * deltaTime);

            //  ADD FORCE TO BALL
            rigidBody.AddForce(input);

        }
        else
        {
            //  GET INPUT
            Vector2 input = Frame.core.input.GetInputAxis();

            //  IF GROUNDED THEN GET JUMP INPUT
            float jumpInput = (grounded && Frame.core.input.GetInputJump() ? 1f : 0f);

            //  ADD JUMP VELOCITY
            jumpInput *= (jumpVelocity * 10f);


            //  ADD MOVEMENT SPEED
            input *= ((movementSpeed * deltaTime) * 10f);




            //  SET FORCE TO XZ FROM XY INPUT
            Vector3 movementForce = new Vector3(input.x, jumpInput, input.y);


            //  ADD FORCE TO BALL
            rigidBody.AddForce(movementForce);
        };

    }

    void CheckGround() {
        if (Physics.Raycast(rigidBody.position, Vector3.down, playerHeight))
        {
            if (!grounded)
            {
                bounceEvent.Activate();
            };
            grounded = true;

        }
        else
        {
            grounded = false;
        }
    }



}
