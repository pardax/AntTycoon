using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UI_EditPopup : UI_Popup
{
    enum Buttons
    {
        RejectButton,
        SaveButton,
        InfoButton,
        GuideButton,
        ExitButton
    }

    enum GameObjects
    {
        Save,
        Guide,
        Credit,
        Info,
        Exit
    }


    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));

        GetButton((int)Buttons.RejectButton).gameObject.BindEvent(OnClickRejectButton);
        GetButton((int)Buttons.SaveButton).gameObject.BindEvent(OnClickSaveButton);
        GetButton((int)Buttons.GuideButton).gameObject.BindEvent(OnClickGuideButton);
        GetButton((int)Buttons.InfoButton).gameObject.BindEvent(OnClickInfoButton);
        GetButton((int)Buttons.ExitButton).gameObject.BindEvent(OnClickExit);


        GetObject((int)GameObjects.Info).gameObject.SetActive(false);


        return true;
    }


    void OnClickSaveButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        Managers.Game.SaveGame();
        Managers.UI.ClosePopupUI(this);
    }

    void OnClickGuideButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_GuidePopup>();
    }
    void OnClickInfoButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        GetObject((int)GameObjects.Save).gameObject.SetActive(false);
        GetObject((int)GameObjects.Guide).gameObject.SetActive(false);
        GetObject((int)GameObjects.Credit).gameObject.SetActive(false);
        GetObject((int)GameObjects.Exit).gameObject.SetActive(false);
        GetObject((int)GameObjects.Info).gameObject.SetActive(true);

    }

    void OnClickRejectButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        Managers.UI.ClosePopupUI(this);
    }

    void OnClickExit()
    {
        Debug.Log("OnClickExit");
        Application.Quit();
    }
}
