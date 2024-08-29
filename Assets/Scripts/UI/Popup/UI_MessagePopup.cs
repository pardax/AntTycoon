using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UI_MessagePopup : UI_Popup
{
    enum Buttons
    {
        RejectButton,
        AcceptButton
    }


    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.AcceptButton).gameObject.BindEvent(OnClickAceeptButton);
        GetButton((int)Buttons.RejectButton).gameObject.BindEvent(OnClickRejectButton);

        return true;
    }


    void OnClickAceeptButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_NamePopup>();
    }

    void OnClickRejectButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        Managers.UI.ClosePopupUI(this);
    }
}
