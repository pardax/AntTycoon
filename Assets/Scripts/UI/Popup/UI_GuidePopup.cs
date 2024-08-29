using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;


public class UI_GuidePopup : UI_Popup
{
    enum Buttons
    {
        RejectButton
    }


    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.RejectButton).gameObject.BindEvent(OnClickRejectButton);

        return true;
    }

    void OnClickRejectButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        Managers.UI.ClosePopupUI(this);
    }
}
