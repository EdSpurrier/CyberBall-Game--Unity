﻿using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ActionManager
{
    [HideInInspector]
    public bool hideDetails = false;
    [HideIfGroup("hideDetails")]



    [HideLabel]
    [HorizontalGroup("hideDetails/Split", 0.05f)]
    public bool toggle = true;
  

    [HideLabel]
    [HorizontalGroup("hideDetails/Split", 0.35f)]
    [SuffixLabel("Action Name", Overlay = true)]
    public string actionName = "";

    
    [InlineButton("ActivateAction")]
    
    [EnumPaging]
    [EnumToggleButtons]
    [HorizontalGroup("hideDetails/Split", 0.6f)]
    [HideLabel]
    public ActionType actionType;
    


    private void ActivateAction()
    {
        if (EditorInteractions.InPlayerButton())
        {
            Activate();
        };
    }


    
    public enum ActionType
    {
        Activate,
        Deactivate,
        Destroy,
        Scale,
        RotateToFrom,
        Spawn,
        RandomizeRotation,
        Animation,
        EventManager,
        Unparent,
        Parent,
        RandomizePosition,
        Impact,
        Fade,
        SoundFX,
        SceneUpdate,
        ConsoleLog,
        PlayerAction,
        PopupAction,
        GameAction,
        MovePosition
    }

    


    [ShowIf("@this.actionType == ActionType.Activate || this.actionType == ActionType.Deactivate || this.actionType == ActionType.Destroy || this.actionType == ActionType.Unparent || this.actionType == ActionType.Parent")]
    public List<GameObject> objects;

    [ShowIf("actionType", ActionType.Parent)]
    [HideLabel]
    [OdinSerialize]
    [SuffixLabel("Parent", Overlay = true)]
    public Transform parent;



    [ShowIf("actionType", ActionType.RotateToFrom)]
    [HideLabel]
    [OdinSerialize]
    public Action_RotateToFrom rotateToFrom;

    
    [HideLabel]
    [ShowIf("actionType", ActionType.Spawn)]
    [OdinSerialize]
    public Action_Spawn spawn;

    [HideLabel]
    [ShowIf("actionType", ActionType.RandomizeRotation)]
    [OdinSerialize]
    public Action_RandomizeRotation randomizeRotation;


    [HideLabel]
    [ShowIf("actionType", ActionType.RandomizePosition)]
    [OdinSerialize]
    public Action_RandomizePosition randomizePosition;



    [HideLabel]
    [ShowIf("actionType", ActionType.Animation)]
    [OdinSerialize]
    public Action_Animation animation;


    [HideLabel]
    [ShowIf("actionType", ActionType.EventManager)]
    [OdinSerialize]
    public Action_ActivateEventManager eventManager;



    [ShowIf("actionType", ActionType.SoundFX)]
    [HideLabel]
    [OdinSerialize]
    public Action_SoundFX soundFX;


    [ShowIf("actionType", ActionType.SceneUpdate)]
    [HideLabel]
    [OdinSerialize]
    public Action_SceneUpdate sceneUpdate;

    [ShowIf("actionType", ActionType.ConsoleLog)]
    [HideLabel]
    [OdinSerialize]
    public Action_ConsoleLog consoleLog;

    [ShowIf("actionType", ActionType.PlayerAction)]
    [HideLabel]
    [OdinSerialize]
    public Action_PlayerAction playerAction;

    [ShowIf("actionType", ActionType.PopupAction)]
    [HideLabel]
    [OdinSerialize]
    public Action_PopupAction popupAction;

    [ShowIf("actionType", ActionType.GameAction)]
    [HideLabel]
    [OdinSerialize]
    public Action_GameAction gameAction;

    [ShowIf("actionType", ActionType.MovePosition)]
    [HideLabel]
    [OdinSerialize]
    public Action_MovePosition movePosition;

    

    public void Reset()
    {

    }



    public void Activate()
    {
        if (!toggle)
        {
            Debug.LogWarning("Action Disabled - " + actionName);
            return;
        };

        if (actionType == ActionType.SoundFX)
        {
            soundFX.Activate();
        }
        else if (actionType == ActionType.PlayerAction)
        {
            playerAction.Activate();
        }
        else if (actionType == ActionType.Unparent)
        {
            foreach (GameObject thisObject in objects)
            {
                thisObject.transform.parent = null;
            };
        }
        else if(actionType == ActionType.Parent)
        {
            foreach (GameObject thisObject in objects)
            {
                thisObject.transform.parent = parent;
            };
        }
        else if (actionType == ActionType.Activate)
        {
            objects.SetGameobjectsState(true);
        }

        else if(actionType == ActionType.Deactivate)
        {
            objects.SetGameobjectsState(false);
        }

        else if (actionType == ActionType.Destroy)
        {
            objects = Frame.core.actions.DestroyObjects(objects);
        }



        else if (actionType == ActionType.Spawn)
        {
            spawn.Activate();
        }

        else if (actionType == ActionType.RandomizeRotation)
        {
            randomizeRotation.Activate();
        }
        
        else if (actionType == ActionType.MovePosition)
        {
            movePosition.Activate();
        }

        else if (actionType == ActionType.RandomizePosition)
        {
            randomizePosition.Activate();
        }

        

        else if (actionType == ActionType.Animation)
        {
            animation.Activate();
        }
        
        else if (actionType == ActionType.EventManager)
        {
            eventManager.Activate();
        }
        else if (actionType == ActionType.PopupAction)
        {
            popupAction.Activate();
        }

        else if (actionType == ActionType.GameAction)
        {
            gameAction.Activate();
        }

        else if (actionType == ActionType.SceneUpdate)
        {
            sceneUpdate.Activate();
        }
        else if (actionType == ActionType.ConsoleLog)
        {
            consoleLog.Activate();
        };
        


    }

    
    
}
