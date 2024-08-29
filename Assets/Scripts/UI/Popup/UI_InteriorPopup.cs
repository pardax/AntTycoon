using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class UI_InteriorPopup : UI_Popup
{
    enum Texts
    {
        TitleText,
        PriceText,
        IntroductionText,
        EffectText
    }

    enum Buttons
    {
        RejectButton,
        AcceptButton
    }

    enum Images
    {
        FlowerImage,
        DefuserImage,
        SignboardImage,
        ErrorBox
    }

    GameManagerEx _game;
    UI_PlayPopup _play;
    private int index;

    private const int flowerCost = 50000;
    private const int defuserCost = 75000;
    private const int signboardCost = 100000;


    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        GetButton((int)Buttons.RejectButton).gameObject.BindEvent(OnClickCloseButton);
        GetButton((int)Buttons.AcceptButton).gameObject.BindEvent(OnClickAcceptButton);

        GetImage((int)Images.FlowerImage).gameObject.SetActive(false);
        GetImage((int)Images.DefuserImage).gameObject.SetActive(false);
        GetImage((int)Images.SignboardImage).gameObject.SetActive(false);

        GetImage((int)Images.ErrorBox).gameObject.SetActive(false);


        setPopup(index);

        return true;
    }

    void setPopup(int index)
    {
        switch (this.index)
        {
            case 1:
                GetImage((int)Images.FlowerImage).gameObject.SetActive(true);
                GetText((int)Texts.TitleText).text = "화분";
                GetText((int)Texts.IntroductionText).text = "화분이다. 손님에게 가게에 대한 좋은 인상을 줄 수 있다.";
                GetText((int)Texts.EffectText).text = "(평판 +100)";
                GetText((int)Texts.PriceText).text = string.Format("{0}원", flowerCost); ;
                break;
            case 2:
                GetImage((int)Images.DefuserImage).gameObject.SetActive(true);
                GetText((int)Texts.TitleText).text = "디퓨저";
                GetText((int)Texts.IntroductionText).text = "좋은 향이 난다. 손님들이 방문하는 빈도가 증가한다.";
                GetText((int)Texts.EffectText).text = "(평판 +200)";
                GetText((int)Texts.PriceText).text = string.Format("{0}원", defuserCost); ;
                break;
            case 3:
                GetImage((int)Images.SignboardImage).gameObject.SetActive(true);
                GetText((int)Texts.TitleText).text = "간판";
                GetText((int)Texts.IntroductionText).text = "가게를 적극적으로 홍보한다. 손님이 오는 빈도가 증가한다.";
                GetText((int)Texts.EffectText).text = "(평판 +300)";
                GetText((int)Texts.PriceText).text = string.Format("{0}원", signboardCost); ;
                break;
        }
    }

    void OnClickCloseButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        Managers.UI.ClosePopupUI(this);
    }

    void OnClickAcceptButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        if (_game.Money < returnIndex(this.index))
        {
            GetImage((int)Images.ErrorBox).gameObject.SetActive(true);
        }
        else
        {
            switch (this.index)
            {
                case 1:
                    Managers.Game.Reputation += 100;
                    Managers.Game.HasFlower = true;
                    Managers.Game.Money -= flowerCost;
                    Managers.Game.DailySpend += flowerCost;
                    Managers.UI.ClosePopupUI(this);
                    break;
                case 2:
                    Managers.Game.Reputation += 200;
                    Managers.Game.HasDefuser = true;
                    Managers.Game.Money -= defuserCost;
                    Managers.Game.DailySpend += defuserCost;
                    Managers.UI.ClosePopupUI(this);
                    break;
                case 3:
                    Managers.Game.Reputation += 300;
                    Managers.Game.HasSignboard = true;
                    Managers.Game.Money -= signboardCost;
                    Managers.Game.DailySpend += signboardCost;
                    Managers.UI.ClosePopupUI(this);
                    break;
            }

        }
    }

    public void SetInfo(GameManagerEx ex, UI_PlayPopup play, int index)
    {
        this._game = ex;
        this._play = play;
        this.index = index;
    }

    private int returnIndex(int index)
    {
        switch (this.index)
        {
            case 1:
                return flowerCost;
            case 2:
                return defuserCost;
            case 3:
                return signboardCost;
        }
        return 0;
    }
}
