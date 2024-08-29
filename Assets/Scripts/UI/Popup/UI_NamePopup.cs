using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Define;


public class UI_NamePopup : UI_Popup
{
    enum GameObjects
    {
        InputField
    }

    enum Texts
    {
        ValueText,
        DateText,
        NameText
    }

    enum Buttons
    {
        ConfirmButton
    }

    TMP_InputField _inputField;

    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        getCurrentDate();

        GetButton((int)Buttons.ConfirmButton).gameObject.BindEvent(OnClickConfirmButton);
        _inputField = GetObject((int)GameObjects.InputField).gameObject.GetComponent<TMP_InputField>();

        return true;
    }

    void getCurrentDate()
    {
        string year = System.DateTime.Now.ToString("yyyy");
        string month = System.DateTime.Now.ToString("MM");
        string day = System.DateTime.Now.ToString("dd");

        GetText((int)Texts.DateText).text = year + " . " + month + " . " + day;
    }

    void OnClickConfirmButton()
    {
        Managers.Game.ResetGame();
        Managers.Game.ShopName = _inputField.text;
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_PlayPopup>();
        Debug.Log("Shop Name is : " + Managers.Game.ShopName);
    }
}
