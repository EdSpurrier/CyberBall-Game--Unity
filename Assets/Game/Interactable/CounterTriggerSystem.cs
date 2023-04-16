using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterTriggerSystem : MonoBehaviour
{
    public int triggerCountNeeded = 2;
    public int triggerCount = 0;

    public FrameCoreEvent triggerCountReached;

    public void Trigger()
    {
        triggerCount++;

        if (triggerCount >= triggerCountNeeded)
        {
            triggerCountReached.Activate();
        };
    }



}
