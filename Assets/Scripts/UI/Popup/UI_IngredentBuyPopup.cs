using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UI_IngredentBuyPopup : UI_Popup
{
    enum Texts
    {
        TitleText,
        PriceText,
        EA_Text
    }

    enum Buttons
    {
        RejectButton,
        AcceptButton,
        UpButton,
        DownButton
    }

    enum Images
    {
        Coffee,
        Cookie,
        IceCream,
        Sandwich,
        Cake,
        ErrorBox
    }

    GameManagerEx _game;
    private int index, totalCost = 0, ea = 10;

    private const int coffeeCost = 100;
    private const int cookieCost = 300;
    private const int iceCost = 500;
    private const int sandwichCost = 1000;
    private const int cakeCost = 3000;

    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        GetButton((int)Buttons.RejectButton).gameObject.BindEvent(OnClickCloseButton);
        GetButton((int)Buttons.AcceptButton).gameObject.BindEvent(OnClickAcceptButton);
        GetButton((int)Buttons.UpButton).gameObject.BindEvent(OnClickUp);
        GetButton((int)Buttons.DownButton).gameObject.BindEvent(OnClickDown);


        GetImage((int)Images.Coffee).gameObject.SetActive(false);
        GetImage((int)Images.Cookie).gameObject.SetActive(false);
        GetImage((int)Images.IceCream).gameObject.SetActive(false);
        GetImage((int)Images.Sandwich).gameObject.SetActive(false);
        GetImage((int)Images.Cake).gameObject.SetActive(false);

        GetImage((int)Images.ErrorBox).gameObject.SetActive(false);

        setPopup(index);

        totalCost = ea * returnIndex(this.index);

        GetText((int)Texts.EA_Text).text = ea.ToString();
        GetText((int)Texts.PriceText).text = string.Format("{0}원", totalCost);

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
        if (_game.Money < totalCost)
        {
            Managers.Sound.Play(Sound.Effect, "Sound_Cancelbutton");
            GetImage((int)Images.ErrorBox).gameObject.SetActive(true);
        }
        else
        {
            switch (this.index)
            {
                case 1:
                    _game.CoffeeBean += ea;
                    _game.Money -= totalCost;
                    break;
                case 2:
                    _game.Cookie += ea;
                    _game.Money -= totalCost;
                    break;
                case 3:
                    _game.IceCream += ea;
                    _game.Money -= totalCost;
                    break;
                case 4:
                    _game.Sandwich += ea;
                    _game.Money -= totalCost;
                    break;
                case 5:
                    _game.Cake += ea;
                    _game.Money -= totalCost;
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
                GetImage((int)Images.Coffee).gameObject.SetActive(true);
                GetText((int)Texts.TitleText).text = "커피";
                break;
            case 2:
                GetImage((int)Images.Cookie).gameObject.SetActive(true);
                GetText((int)Texts.TitleText).text = "쿠키";
                break;
            case 3:
                GetImage((int)Images.IceCream).gameObject.SetActive(true);
                GetText((int)Texts.TitleText).text = "아이스크림";
                break;
            case 4:
                GetImage((int)Images.Sandwich).gameObject.SetActive(true);
                GetText((int)Texts.TitleText).text = "샌드위치";
                break;
            case 5:
                GetImage((int)Images.Cake).gameObject.SetActive(true);
                GetText((int)Texts.TitleText).text = "케이크";
                break;
        }
    }

    public void SetInfo(GameManagerEx ex, int index)
    {
        this._game = ex;
        this.index = index;
    }
    
    void OnClickUp()
    {
        ea += 10;
        totalCost = ea * returnIndex(this.index);

        GetText((int)Texts.EA_Text).text = ea.ToString();
        GetText((int)Texts.PriceText).text = string.Format("{0}원", totalCost);
    }

    void OnClickDown()
    {
        if(ea > 0)
        {
            ea -= 10;
            totalCost = ea * returnIndex(this.index);

            GetText((int)Texts.EA_Text).text = ea.ToString();
            GetText((int)Texts.PriceText).text = string.Format("{0}원", totalCost);
        }
    }

    private int returnIndex(int index)
    {
        switch (this.index)
        {
            case 1:
                return coffeeCost;
            case 2:
                return cookieCost;
            case 3:
                return iceCost;
            case 4:
                return sandwichCost;
            case 5:
                return cakeCost;
        }
        return 0;
    }




}
