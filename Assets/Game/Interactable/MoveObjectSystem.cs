using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectSystem : MonoBehaviour
{


    [Title("Settings")]
    public float speed = 2f;
    public float tolerance = 0.01f;
    
    public float waitTime = 0.5f;

    public bool singleTrigger = false;

    [Title("System")]
    public bool active = false;
    public bool down = false;
    public float currentWaitTime = 0;

    [Title("Parts")]
    public Transform objectToMove;
    public Vector3 startPosition;
    public Vector3 endPosition;




    [FoldoutGroup("Activate Event")]
    [HideLabel]
    public FrameCoreEvent activateEvent;


    [FoldoutGroup("Deactivate Event")]
    [HideLabel]
    public FrameCoreEvent deactivateEvent;

    private void Update()
    {
        if (active)
        {
            if (down)
            {
                if (Vector3.Distance(objectToMove.localPosition, endPosition) <= tolerance)
                {
                    active = false;
                    objectToMove.localPosition = endPosition;
                    Activate();
                    return;
                }
                else {

                    objectToMove.localPosition = Vector3.Lerp(objectToMove.localPosition, endPosition, speed * Time.deltaTime);
                }
            }
            else {
                if (Vector3.Distance(objectToMove.localPosition, startPosition) <= tolerance)
                {
                    active = false;
                    objectToMove.localPosition = startPosition;
                    Deactivate();
                    return;
                }
                else {
                    
                    if (currentWaitTime > 0f)
                    {
                        currentWaitTime -= Time.deltaTime;
                    }
                    else {
                        objectToMove.localPosition = Vector3.Lerp(objectToMove.localPosition, startPosition, speed * Time.deltaTime);
                    };
                    
                };
            };
        };
    }


    public void Activate()
    {

        activateEvent.Activate();
        if(singleTrigger)
        {
            this.enabled = false;
        };
    }


    public void Deactivate()
    {
        deactivateEvent.Activate();
    }

    public void Trigger()
    {
        active = true;
        down = true;
    }

    public void UnTrigger()
    {
        active = true;
        down = false;
        currentWaitTime = waitTime;
    }


}
