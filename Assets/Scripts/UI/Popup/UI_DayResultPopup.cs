using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Define;

public class UI_DayResultPopup : UI_Popup
{
    enum Texts
    {
        CloseButtonText,
        ShopNameText,
        BenefitMoneyText,
        BenefitReputationText,
        TaxText,
        VisitText,
        OrderedText,
        LoanText,
        SpendMoneyText
    }

    enum Buttons
    {
        CloseButton,
        ShangHwan
    }

    UI_PlayPopup _pop;

    public override bool Init()
    {
        if (!base.Init())
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));

        GetButton((int)Buttons.CloseButton).gameObject.BindEvent(OnClickCloseButton);
        GetButton((int)Buttons.ShangHwan).gameObject.BindEvent(OnClickPayed);


        GetShopName();
        GetDailyResult();

        return true;
    }

    void OnClickPayed()
    {
        if (Managers.Game.Money + Managers.Game.Loan < 0)
        {
            Debug.Log("��ȯ�ź�");
            return;
        }
        else
        {
            Managers.Game.Money += Managers.Game.Loan;
            Managers.Game.Loan = 0;
            Managers.Game.LoanChance = true;
            GetText((int)Texts.LoanText).text = string.Format("���� : {0}��", Managers.Game.Loan.ToString());
        }
    }

    void OnClickCloseButton()
    {
        NewDay();
        if (Managers.Game.LoanChance)
        {
            CheckLoan();
            Managers.UI.ClosePopupUI(this);
        }
        else
        {
            //����
            Debug.Log("�Ļ� ����");
            Managers.Sound.Stop(Sound.Bgm);
            _pop.SetBadEnd();
            Managers.UI.ShowPopupUI<UI_EndingPopup>().SetInfo(2);
            Managers.UI.ClosePopupUI(this);
        }
    }

    void CheckLoan()
    {
        if(Managers.Game.Loan < 0)
        {
            Debug.Log("�������� : false");
            Managers.Game.LoanChance = false;
        }
    }

    void GetShopName()
    {
        if (Managers.Game.ShopName != null)
        {
            GetText((int)Texts.ShopNameText).text = Managers.Game.ShopName;
        }
        else
        {
            Debug.Log("NoName");
        }
    }

    void GetDailyResult()
    {
        GetText((int)Texts.BenefitMoneyText).text = string.Format("�Ϸ� ���Աݾ� : {0}��", Managers.Game.DailyBenefitMoney.ToString());
        GetText((int)Texts.BenefitReputationText).text = string.Format("�Ϸ� ������� : {0}", Managers.Game.DailyBenefitReputation.ToString());
        GetText((int)Texts.SpendMoneyText).text = string.Format("���� : -{0}��", Managers.Game.DailySpend.ToString());
        GetText((int)Texts.TaxText).text = string.Format("���� : -{0}��", (Managers.Game.DailyBenefitMoney*0.1));
        GetText((int)Texts.VisitText).text = string.Format("�湮�� �մ� : {0}��", Managers.Game.DailyCustomer.ToString());
        GetText((int)Texts.OrderedText).text = string.Format("�ֹ����� �մ� : {0}��", Managers.Game.DailyOrdered.ToString());
        int tax = (int)(Managers.Game.DailyBenefitMoney * 0.1);
        int myMoney = Managers.Game.Money;
        if((myMoney - tax) < 0)
        {
            Managers.Game.Loan += (myMoney - tax);
            Managers.Game.Money = 0;
        }
        GetText((int)Texts.LoanText).text = string.Format("���� : {0}��", Managers.Game.Loan.ToString());
    }

    void NewDay()
    {

        if(Managers.Game.Loan !> 0)
        {
            Managers.Game.Money -= (int)(Managers.Game.DailyBenefitMoney * 0.1);
        }
        Managers.Game.DailyBenefitReputation = 0;
        Managers.Game.DailyBenefitMoney = 0;
        Managers.Game.DailySpend = 0;
        Managers.Game.DailyCustomer = 0;
        Managers.Game.DailyOrdered = 0;

        Managers.Game.SaveGame();
    }

    public void getPopup(UI_PlayPopup pop)
    {
        this._pop = pop;
    }
}
