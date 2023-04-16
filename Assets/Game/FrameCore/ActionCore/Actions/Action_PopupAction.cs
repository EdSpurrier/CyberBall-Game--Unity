using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Action_PopupAction
{

    public enum PopupActionType
    {
        OpenPopup,
        ClosePopup,
        CompleteLevelPopup
    }
    public PopupActionType action;

    [HideIf("@this.action == PopupActionType.CompleteLevelPopup")]
    public string popupName = "";
    
    [Title("System")]
    public DeBugger debug;





    public void Activate()
    {
        if (action == PopupActionType.OpenPopup)
        {
            PopupCore.system.OpenPopup(popupName);
        } else if (action == PopupActionType.ClosePopup)
        {
            PopupCore.system.ClosePopup(popupName);
        }
        else if (action == PopupActionType.CompleteLevelPopup)
        {
            PopupCore.system.CompleteLevelPopup();
        };
    }


}
