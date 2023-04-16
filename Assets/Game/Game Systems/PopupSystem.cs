using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PopupCore
{
    public static PopupSystem system;
}


[System.Serializable]
public class Popup
{
    public string popupName = "";

    public string header = "Header";
    public string subHeader = "Sun Header";
    [TextArea]
    public string content = "Text Content";

    public RawImage textPointImage; 


    public FrameCoreEvent popupOpenEvent = new FrameCoreEvent
    {
        eventName = "Popup Open"
    };

    public FrameCoreEvent popupCloseEvent = new FrameCoreEvent
    {
        eventName = "Popup Close"
    };

}


public class PopupSystem : MonoBehaviour
{
    public bool popupIsActive = false;
    public List<Popup> popups;
    public Popup activePopup = null;


    public GameObject popupCanvas;
    public Text headerTextUI;
    public Text subHeaderTextUI;
    public Text contentTextUI;
    public RawImage textPointImageUI;

    [Title("Global Popup Events")]
    public FrameCoreEvent popupOpenEvent = new FrameCoreEvent
    {
        eventName = "Popup Open"
    };

    public FrameCoreEvent popupCloseEvent = new FrameCoreEvent
    {
        eventName = "Popup Close"
    };

    public FrameCoreEvent completeLevelEvent = new FrameCoreEvent
    {
        eventName = "Complete Level"
    };

    public FrameCoreEvent loadNextSceneEvent = new FrameCoreEvent
    {
        eventName = "Load Next Scene"
    };

    public void UpdatePopupContent()
    {
        headerTextUI.text = activePopup.header;
        subHeaderTextUI.text = activePopup.subHeader;
        contentTextUI.text = activePopup.content;

        textPointImageUI.gameObject.SetActive(activePopup.textPointImage);

        if (activePopup.textPointImage)
        {
            textPointImageUI.texture = activePopup.textPointImage.texture;
            textPointImageUI.SetNativeSize();
        };
    }

    public void OpenPopup(string popupName)
    {
        popups.ForEach(popup => {
            if (popup.popupName == popupName)
            {
                popupIsActive = true;
                activePopup = popup;

                popupCanvas.SetActive(true);

                UpdatePopupContent();

                popup.popupOpenEvent.Activate();

                popupOpenEvent.Activate();
            };
        });
    }
    public void ClosePopup(string popupName)
    {
        if (!popupIsActive)
        {
            return;
        };

        if (activePopup.popupName == popupName)
        {
            CloseActivePopup();
        };
    }


    public void CloseActivePopup()
    {
        if (!popupIsActive)
        {
            return;
        };
        popupCanvas.SetActive(false);
        activePopup.popupCloseEvent.Activate();
        popupCloseEvent.Activate();
        popupIsActive = false;
    }

    public void CompleteLevelPopup()
    {
        popupCanvas.SetActive(true);
        Game.core.levelTimer.SaveLevelTime();
        completeLevelEvent.Activate();
    }

    public void LoadNextScene()
    {
        loadNextSceneEvent.Activate();
    }

    // Start is called before the first frame update
    void Start()
    {
        PopupCore.system = this;
        popupIsActive = false;
    }


}
