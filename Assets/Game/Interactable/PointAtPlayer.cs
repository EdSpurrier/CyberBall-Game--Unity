using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtPlayer : MonoBehaviour
{
    public bool active = false;
    public bool matchY = true;
    public Transform pointer;

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            return;
        };

        RotateToPlayer();
    }

    public void RotateToPlayer()
    {
        if (matchY)
        {
            pointer.position = new Vector3(pointer.position.x, Frame.core.player.controller.rigidBody.position.y, pointer.position.z);
        };

        pointer.LookAt(Frame.core.player.controller.rigidBody.position);
    }
}
