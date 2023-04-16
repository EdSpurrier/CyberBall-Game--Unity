using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPusher : MonoBehaviour
{
    [Title("Settings")]
    public Transform direction;
    public float velocity = 10f;



    [Button("Push")]
    public void TestPush()
    {
        if(!EditorInteractions.InPlayerButton())
        {
            return;
        };

        ActivatePush();
    }

    public void ActivatePush()
    {
        Frame.core.player.controller.rigidBody.rotation = direction.rotation;
        Frame.core.player.controller.ResetVelocity();
        Frame.core.player.controller.Push(velocity, direction.forward);
    }
}
