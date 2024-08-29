using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_TitlePopup : UI_Popup
{
    enum Buttons
    {
        StartNewButton,
        ContinueButton,
        ExitButton
    }

    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.StartNewButton).gameObject.BindEvent(OnClickStartNew);
        GetButton((int)Buttons.ContinueButton).gameObject.BindEvent(OnClickContinue);
        GetButton((int)Buttons.ExitButton).gameObject.BindEvent(OnClickExit);

        //GetText((int)Texts.TestButtonText).text = Managers

        Managers.Sound.Clear();
        Managers.Sound.Play(Sound.Effect, "Sound_MainTitle", volume: 0.5f);

        return true;
    }

    void OnClickStartNew()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        if (!(Managers.UI._popupStack.Count >= 2))
        {
            if (Managers.Game.LoadGame())
            {
                Debug.Log("Data Exist");
                Managers.UI.ShowPopupUI<UI_MessagePopup>();
                return;
            }
            Debug.Log("OnClickStartNew");
            Managers.UI.ShowPopupUI<UI_NamePopup>();
            Managers.UI.ClosePopupUI(this);
        }

    }
    void OnClickContinue()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        Debug.Log("OnClickContinue");

        Managers.Game.LoadGame();

        Managers.UI.ShowPopupUI<UI_PlayPopup>();
        Managers.UI.ClosePopupUI(this);
    }
    void OnClickExit()
    {
        Debug.Log("OnClickExit");
        Application.Quit();
    }
}
