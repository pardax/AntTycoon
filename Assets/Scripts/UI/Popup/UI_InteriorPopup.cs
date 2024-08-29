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
                GetText((int)Texts.TitleText).text = "ȭ��";
                GetText((int)Texts.IntroductionText).text = "ȭ���̴�. �մԿ��� ���Կ� ���� ���� �λ��� �� �� �ִ�.";
                GetText((int)Texts.EffectText).text = "(���� +100)";
                GetText((int)Texts.PriceText).text = string.Format("{0}��", flowerCost); ;
                break;
            case 2:
                GetImage((int)Images.DefuserImage).gameObject.SetActive(true);
                GetText((int)Texts.TitleText).text = "��ǻ��";
                GetText((int)Texts.IntroductionText).text = "���� ���� ����. �մԵ��� �湮�ϴ� �󵵰� �����Ѵ�.";
                GetText((int)Texts.EffectText).text = "(���� +200)";
                GetText((int)Texts.PriceText).text = string.Format("{0}��", defuserCost); ;
                break;
            case 3:
                GetImage((int)Images.SignboardImage).gameObject.SetActive(true);
                GetText((int)Texts.TitleText).text = "����";
                GetText((int)Texts.IntroductionText).text = "���Ը� ���������� ȫ���Ѵ�. �մ��� ���� �󵵰� �����Ѵ�.";
                GetText((int)Texts.EffectText).text = "(���� +300)";
                GetText((int)Texts.PriceText).text = string.Format("{0}��", signboardCost); ;
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
