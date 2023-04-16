using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game
{
    public static GameCore core;
}



public class GameCore : MonoBehaviour
{
    [Title("Debug Mode")]
    public bool debugMode = false;

    [FoldoutGroup("Complete Level Event")]
    [HideLabel]
    public FrameCoreEvent completeLevelEvent;

    public bool gameComplete = false;

    [Title("Level Timer")]
    [HideLabel]
    public LevelTimer levelTimer;

    [Title("Input Preferences")]
    [HideLabel]
    public PlayerPreferences playerPreferences;

    public InGameMenuSystem inGameMenu;

    private void Awake()
    {
        Game.core = this;

        if (debugMode)
        {
            ActivateDebugMode();
        };

        inGameMenu = FindObjectOfType(typeof(InGameMenuSystem)) as InGameMenuSystem;
    }

    public void ActivateDebugMode()
    {
        Debug.Log("GameCore >> Debug Mode ACTIVE");
        Frame.core.sceneSettings.sceneAnimatorUI.SetBool("Debug Mode", true);
    }

    public void StartGame()
    {
        Debug.Log("GameCore >> Start Game");
        Frame.core.player.ActivatePlayer();
        gameComplete = false;
        levelTimer.StartTimer();
    }

    public void EndGame()
    {
        Debug.Log("GameCore >> End Game");
        Frame.core.player.DeactivatePlayer();
        gameComplete = true;
        levelTimer.StopTimer();
    }


    public void UnPauseGame()
    {
        Debug.Log("GameCore >> Unpause");
        Frame.core.player.ActivatePlayer();
        StartTime();
    }


    public void PauseGame()
    {
        Debug.Log("GameCore >> Pause");
        Frame.core.player.DeactivatePlayer();
        StopTime();
    }



    public void StopTime()
    {
        Time.timeScale = 0f;
        levelTimer.Unpause();
    }

    public void StartTime()
    {
        Time.timeScale = 1f;
        levelTimer.StartTimer();
    }

    public void CompleteLevel()
    {
        completeLevelEvent.Activate();
    }
}
