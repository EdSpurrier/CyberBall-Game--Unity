using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuSystem : MonoBehaviour
{
    public GameObject joystickControls;



    public void DeactivateJoystick ()
    {
        joystickControls.SetActive(false);
    }

    public void ActivateJoystick()
    {
        joystickControls.SetActive(true);
    }

}
