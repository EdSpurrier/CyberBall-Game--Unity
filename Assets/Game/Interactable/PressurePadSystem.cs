using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePadSystem : MonoBehaviour
{


    [Title("Settings")]
    public float speed = 2f;
    public float tolerance = 0.01f;
    public bool singleTrigger = false;
    public bool deactivateInstantOnUp = false;

    [Title("System")]
    public bool active = false;
    public bool down = false;

    [Title("Parts")]
    public Transform pad;
    public Vector3 upPosition;
    public Vector3 downPosition;




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
                if (Vector3.Distance(pad.localPosition, downPosition) <= tolerance)
                {
                    active = false;
                    pad.localPosition = downPosition;
                    Activate();
                    return;
                }
                else {
                    pad.localPosition = Vector3.Lerp(pad.localPosition, downPosition, speed * Time.deltaTime);
                }
            }
            else {
                if (Vector3.Distance(pad.localPosition, upPosition) <= tolerance)
                {
                    active = false;
                    pad.localPosition = upPosition;
                    if (!deactivateInstantOnUp)
                    {
                        Deactivate();
                    };
                    return;
                }
                else {
                    pad.localPosition = Vector3.Lerp(pad.localPosition, upPosition, speed * Time.deltaTime);
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

    public void PadDown()
    {
        active = true;
        down = true;
    }

    public void PadUp()
    {
        active = true;
        down = false;
        if (deactivateInstantOnUp)
        {
            Deactivate();
        };
    }


}
