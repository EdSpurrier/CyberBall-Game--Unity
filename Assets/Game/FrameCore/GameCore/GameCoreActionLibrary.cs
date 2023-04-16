using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCoreActionLibrary : MonoBehaviour
{
    public void ResetGyroButtonDown()
    {
       // Frame.core.input.gyroInput.ResetGyro();
    }

    public void JumpButtonDown()
    {
        Frame.core.player.controller.ForceJump();
    }


    public void StartGame()
    {
        Game.core.StartGame();
    }

    public void EndGame()
    {
        Game.core.EndGame();
    }


    public void UnPauseGame()
    {
        Game.core.UnPauseGame();
    }


    public void PauseGame()
    {
        Game.core.PauseGame();
    }



    public void StopTime()
    {
        Game.core.StopTime();
    }

    public void StartTime()
    {
        Game.core.StartTime();
    }

}
