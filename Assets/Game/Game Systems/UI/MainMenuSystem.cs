using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class MainMenuSystem : MonoBehaviour
{
    public bool resetAllLevels = false;
    public List<LevelData> levelData;
    public ControlPrefData controlPrefData;

    // Start is called before the first frame update
    void Start()
    {
        bool unlockNextLevel = true;

        controlPrefData.GetData();

        levelData.ForEach(level => {

            if (resetAllLevels)
            {
                level.reset = resetAllLevels;
            };

            level.GetData();

            level.reset = false;

            if (level.levelCompleted)
            {
                unlockNextLevel = true;
            } else if (level.forceUnlock) {
                unlockNextLevel = true;
            };

            if (level.forceLock)
            {
                unlockNextLevel = false;
            };


            level.levelLockedButton.SetActive(!unlockNextLevel);
            level.levelUnlockedButton.SetActive(unlockNextLevel);
            level.leanButton.interactable = unlockNextLevel;



            if (level.bestLevelTime == 1000000)
            {
                level.levelTimeText.text = "00:00";
            }
            else
            {
                TimeSpan t = TimeSpan.FromSeconds(level.bestLevelTime);
                level.levelTimeText.text = t.Minutes.ToString("D2") + ":" + t.Seconds.ToString("D2");
            };


            unlockNextLevel = level.levelCompleted;

        });
    }





    public void SetControlType (string inputType)
    {
        controlPrefData.SavePlayerPref(inputType);
    }
}


