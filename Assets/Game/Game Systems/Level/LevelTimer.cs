using Lean.Gui;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class LevelData
{
    public string levelName = "Level";
    public float bestLevelTime = 0f;
    public bool levelCompleted = false;
    public LeanButton leanButton;
    public GameObject levelLockedButton;
    public GameObject levelUnlockedButton;
    public Text levelTimeText;
    public bool reset = false;
    public bool forceUnlock = false;
    public bool forceLock = false;

    public void GetData()
    {
        if (!PlayerPrefs.HasKey(levelName + " Time"))
        {
            PlayerPrefs.SetFloat(levelName + " Time", 1000000);
        };

        if (!PlayerPrefs.HasKey(levelName + " Complete"))
        {
            PlayerPrefs.SetInt(levelName + " Complete", 0);
        };

        


        if (reset)
        {
            PlayerPrefs.SetFloat(levelName + " Time", 1000000);
            PlayerPrefs.SetInt(levelName + " Complete", 0);
        };


        bestLevelTime = PlayerPrefs.GetFloat(levelName + " Time");
        levelCompleted = (PlayerPrefs.GetInt(levelName + " Complete") == 1 ? true : false);
    }
}



public class LevelTimer : MonoBehaviour
{
    public LevelData levelData;
    public float levelTime = 0f;
    public bool timerActive = false;
    public Text uiCounter;

    public void SaveLevelTime()
    {

        StopTimer();

        if (levelTime < levelData.bestLevelTime)
        {
            levelData.bestLevelTime = levelTime;
        };
        
        PlayerPrefs.SetFloat(levelData.levelName + " Time", levelData.bestLevelTime);


        levelData.levelCompleted = true;
        
        PlayerPrefs.SetInt(levelData.levelName + " Complete", 1);

    }

    // Start is called before the first frame update
    void Awake()
    {
        levelData.GetData();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            levelTime += Time.deltaTime;

            UpdateTimerUI();
        };
    }

    public void UpdateTimerUI()
    {

        TimeSpan t = TimeSpan.FromSeconds(levelTime);

        uiCounter.text = t.Minutes.ToString() + ":" + t.Seconds.ToString("D2");

    }


    public void StartTimer()
    {
        if (Game.core.gameComplete)
        {
            return;
        };

        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }


    public void Pause()
    {
        timerActive = false;
    }

    public void Unpause()
    {
        if (Game.core.gameComplete)
        {
            return;
        };

        timerActive = true;
    }

}
