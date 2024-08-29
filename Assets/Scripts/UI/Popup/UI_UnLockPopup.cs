using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UI_UnLockPopup : UI_Popup
{
    enum Texts
    {
        TitleText,
        PriceText,
    }

    enum Buttons
    {
        RejectButton,
        AcceptButton,
    }

    enum Images
    {
        Cookie,
        IceCream,
        Sandwich,
        Cake,
        ErrorBox
    }

    private int index, totalCost = 0;

    private const int cookieCost = 30000;
    private const int iceCost = 50000;
    private const int sandwichCost = 70000;
    private const int cakeCost = 100000;

    UI_PlayPopup play_popup;

    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        GetButton((int)Buttons.RejectButton).gameObject.BindEvent(OnClickCloseButton);
        GetButton((int)Buttons.AcceptButton).gameObject.BindEvent(OnClickAcceptButton);

        GetImage((int)Images.Cookie).gameObject.SetActive(false);
        GetImage((int)Images.IceCream).gameObject.SetActive(false);
        GetImage((int)Images.Sandwich).gameObject.SetActive(false);
        GetImage((int)Images.Cake).gameObject.SetActive(false);

        GetImage((int)Images.ErrorBox).gameObject.SetActive(false);

        setPopup(index);

        return true;
    }

    void OnClickCloseButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        Managers.UI.ClosePopupUI(this);
    }

    void OnClickAcceptButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        if (Managers.Game.Money < returnIndex(this.index))
        {
            Managers.Sound.Play(Sound.Effect, "Sound_Cancelbutton");
            GetImage((int)Images.ErrorBox).gameObject.SetActive(true);
        }
        else
        {
            switch (this.index)
            {
                case 1:
                    Managers.Game.BtnCookie = true;
                    Managers.Game.Money -= cookieCost;
                    Managers.Game.DailySpend += cookieCost;
                    play_popup.RefreshLock();
                    break;
                case 2:
                    Managers.Game.BtnIceCream = true;
                    Managers.Game.Money -= iceCost;
                    Managers.Game.DailySpend += iceCost;

                    play_popup.RefreshLock();
                    break;
                case 3:
                    Managers.Game.BtnSandwich = true;
                    Managers.Game.Money -= sandwichCost;
                    Managers.Game.DailySpend += sandwichCost;
                    play_popup.RefreshLock();
                    break;
                case 4:
                    Managers.Game.BtnCake = true;
                    Managers.Game.Money -= cakeCost;
                    Managers.Game.DailySpend += cakeCost;
                    break;
            }
            Managers.Game.DailySpend += totalCost;
            Managers.UI.ClosePopupUI(this);
        }
    }

    private void setPopup(int index)
    {
        switch (this.index)
        {
            case 1:
                GetImage((int)Images.Cookie).gameObject.SetActive(true);
                GetText((int)Texts.TitleText).text = "쿠키 해금";
                GetText((int)Texts.PriceText).text = string.Format("{0}원", cookieCost);
                break;
            case 2:
                GetImage((int)Images.IceCream).gameObject.SetActive(true);
                GetText((int)Texts.TitleText).text = "아이스크림 해금";
                GetText((int)Texts.PriceText).text = string.Format("{0}원", iceCost);
                break;
            case 3:
                GetImage((int)Images.Sandwich).gameObject.SetActive(true);
                GetText((int)Texts.TitleText).text = "샌드위치 해금";
                GetText((int)Texts.PriceText).text = string.Format("{0}원", sandwichCost);
                break;
            case 4:
                GetImage((int)Images.Cake).gameObject.SetActive(true);
                GetText((int)Texts.TitleText).text = "케이크 해금";
                GetText((int)Texts.PriceText).text = string.Format("{0}원", cakeCost);
                break;
        }
    }

    public void SetInfo(UI_PlayPopup play_popup, int index)
    {
        this.play_popup = play_popup;
        this.index = index;
    }

    private int returnIndex(int index)
    {
        switch (this.index)
        {
            case 1:
                return cookieCost;
            case 2:
                return iceCost;
            case 3:
                return sandwichCost;
            case 4:
                return cakeCost;
        }
        return 0;
    }

}
