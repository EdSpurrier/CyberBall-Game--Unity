using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorSystem : MonoBehaviour
{

    [Title("System")]
    public bool active;

    [Title("Parts")]
    public Transform attractor;
    public Transform target;
    public Transform idleParent;



    [FoldoutGroup("Activate Event")]
    [HideLabel]
    public FrameCoreEvent activateEvent;


    [FoldoutGroup("Deactivate Event")]
    [HideLabel]
    public FrameCoreEvent deactivateEvent;

    private void Start()
    {
        if (!target)
        {
            target = Frame.core.player.controller.playerBase;
        };
    }

    public void Activate()
    {

        
        attractor.parent = target;

        attractor.localPosition = Vector3.zero;
        attractor.localRotation = Quaternion.identity;

        attractor.gameObject.SetActive(true);

    }

    public void Deactivate()
    {
        attractor.parent = idleParent;

        attractor.localPosition = Vector3.zero;
        attractor.localRotation = Quaternion.identity;

        attractor.gameObject.SetActive(false);
    }


}
