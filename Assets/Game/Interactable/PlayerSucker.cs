using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSucker : MonoBehaviour
{
    [Title("Settings")] 
    public float suckPower = 1f;
    public Transform targetPosition;
    public bool deactivateOnPositionReached;
    public float tolerance = 0.05f;

    public bool active = false;

    [Button("Toggle Suck")]
    public void TestToggleSuck()
    {
        if (!EditorInteractions.InPlayerButton())
        {
            return;
        };

        Toggle();
    }



    public void Toggle()
    {
        active = !active;

        if (active)
        {
            Deactivate();
        } else
        {
            Activate();
        };

    }


    public void Activate()
    {
        active = true;
        Frame.core.player.controller.ResetVelocity();
        Frame.core.player.controller.ToggleGravity(false);
    }

    public void Deactivate()
    {
        active = false;
        Frame.core.player.controller.ToggleGravity(true);
    }


    private void FixedUpdate()
    {
        if (!active)
        {
            return;
        };


        
            if (Vector3.Distance(Frame.core.player.controller.rigidBody.position, targetPosition.position) < tolerance)
            {
                Frame.core.player.controller.rigidBody.MovePosition(targetPosition.position);
                if (deactivateOnPositionReached)
                {
                    active = false;
                };
                return;
            };



        Vector3 smoothedPosition = Vector3.Lerp(Frame.core.player.controller.rigidBody.position, targetPosition.position, ((suckPower*10f) * Time.deltaTime) );

        Frame.core.player.controller.rigidBody.MovePosition(smoothedPosition);


        
        

    }


}
