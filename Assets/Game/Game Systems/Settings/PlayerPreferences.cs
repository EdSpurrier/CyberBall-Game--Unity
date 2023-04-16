using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;






[System.Serializable]
public class ControlPrefData
{
    public InputType inputType;

    string inputPrefName = "Input Type";
    
    

    public void SavePlayerPref(string inputType)
    {
        this.inputType = (InputType)Enum.Parse(typeof(InputType), inputType);
        PlayerPrefs.SetString(inputPrefName, inputType.ToString());
    }


    public void SavePlayerPref(InputType inputType)
    {
        this.inputType = inputType;
        PlayerPrefs.SetString(inputPrefName, inputType.ToString());
    }

    public void GetData()
    {
        if (!PlayerPrefs.HasKey(inputPrefName))
        {
            PlayerPrefs.SetString(inputPrefName, InputType.Touch.ToString());
        };


        inputType = (InputType)Enum.Parse(typeof(InputType), PlayerPrefs.GetString(inputPrefName));
    }
}










public class PlayerPreferences : MonoBehaviour
{

    public ControlPrefData controlPrefData;
    [FoldoutGroup("Input Preference Events")]
    
    [BoxGroup("Input Preference Events/Touch Setup")]
    [HideLabel]
    public FrameCoreEvent touchSetupEvent = new FrameCoreEvent
    {
        eventName = "Touch Setup"
    };

    [BoxGroup("Input Preference Events/Gyro Setup")]
    [HideLabel]
    public FrameCoreEvent gyroSetupEvent = new FrameCoreEvent
    {
        eventName = "Gyro Setup"
    };

    // Start is called before the first frame update
    void Awake()
    {
        GetSettingsAndSetup();
    }


    public void GetSettingsAndSetup()
    {
        controlPrefData.GetData();

        Frame.core.input.inputType = controlPrefData.inputType;

        if (controlPrefData.inputType == InputType.Gyro)
        {
            gyroSetupEvent.Activate();

            if (Game.core)
            {
                Game.core.inGameMenu.DeactivateJoystick();
            };
        }
        else if (controlPrefData.inputType == InputType.Touch)
        {
            touchSetupEvent.Activate();

            if (Game.core)
            {
                Game.core.inGameMenu.ActivateJoystick();
            };
        };

        
    }

}
