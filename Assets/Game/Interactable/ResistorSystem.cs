using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistorSystem : MonoBehaviour
{
    public Transform startShockPoint;
    public Transform endShockPoint;

    public MaxMinVector3 shockPush;
    public float pushVelocity = 50f;

    public Vector3 pushDirection;
    public PlayerPusher playerPusher;

    public FrameCoreEvent shockEvent = new FrameCoreEvent { 
        eventName = "Shock Event"
    };

    public void ActivateShock()
    {
        endShockPoint.position = Frame.core.player.controller.playerBase.position;
        RandomizedPush();

        shockEvent.Activate();
    }

    public void RandomizedPush()
    {
        pushDirection = new Vector3(
               UnityEngine.Random.Range(shockPush.min.x, shockPush.max.x),
               UnityEngine.Random.Range(shockPush.min.y, shockPush.max.y),
               UnityEngine.Random.Range(shockPush.min.z, shockPush.max.z)
               );
        playerPusher.direction.eulerAngles = pushDirection;
        playerPusher.velocity = pushVelocity;
        playerPusher.ActivatePush();
    }
}
