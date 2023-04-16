using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_GameAction
{

    public enum GameActionType
    {
        SaveLevelTime,
        StopLevelTimer,
        PauseLevelTimer,
        UnpauseLevelTimer,
    }
    public GameActionType action;
    
    [Title("System")]
    public DeBugger debug;





    public void Activate()
    {
        if (action == GameActionType.SaveLevelTime)
        {
            Game.core.levelTimer.SaveLevelTime();
        }
        else if (action == GameActionType.StopLevelTimer)
        {
            Game.core.levelTimer.StopTimer();
        }
        else if (action == GameActionType.PauseLevelTimer)
        {
            Game.core.levelTimer.Pause();
        }
        else if (action == GameActionType.UnpauseLevelTimer)
        {
            Game.core.levelTimer.Unpause();
        };
    }


}
