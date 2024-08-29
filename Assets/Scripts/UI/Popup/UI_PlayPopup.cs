using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_PlayPopup : UI_Popup
{
    enum GameObjects
    {
        AbilityBar,
        Reveal,
        Ingredients,
        Interiors,
        BooUp,
        Location1, Location2, Location3, Location4,
        Location5, Location6, Location7, Location8
    }
    enum Texts
    {
        MoneyText,
        ReputationText,
        RequireReputationText,
        DateText,
        CoffeeEA,
        CookieEA,
        IceCreamEA,
        SandwichEA,
        CakeEA,
        FlowerText,
        DefuserText,
        SignboardText
    }
    enum Buttons
    {
        HideButton,
        RevealButton,
        CoffeeButton,
        CookieButton,
        IceButton,
        SandButton,
        CakeButton,
        IngredientsButton,
        InteriorButton,
        FlowerButton,
        DefuserButton,
        SignboardButton,
        BooUpButton,
        BearButton,
        EditButton
    }
    enum Images
    {
        LockIce,
        LockSand,
        LockCake,
        Flower,
        Defuser,
        Signboard,
        TimerGauge
    }
    public enum Playtab
    {
        UI_DayEndPopup
    }

    List<PlayerController> _EntityList = new List<PlayerController>();

    GameManagerEx _game;
    Image _timerGauge;

    private bool BadEnd = false;

	private bool FolderSwitch = true;

    private const int RequireReputation = 1000;
    
    private string[] SpawnCharacters = { "Sinib", "Intern", "Daeri", "Gwajang", "Bujang", "Esa", "Sajang" };

    private System.Random sysRandom = new System.Random();

    public override bool Init()
	{
		if (base.Init() == false)
			return false;
        
        Managers.Game.SaveGame();

        _game = Managers.Game;

        BindObject(typeof(GameObjects));
        BindImage(typeof(Images));
        BindText(typeof(Texts));
		BindButton(typeof(Buttons));

        //요구 평판치
        GetText((int)Texts.RequireReputationText).text = RequireReputation.ToString();

        GetButton((int)Buttons.HideButton).gameObject.BindEvent(OnClickFolderSwitch);
        GetButton((int)Buttons.RevealButton).gameObject.BindEvent(OnClickFolderSwitch);
        GetButton((int)Buttons.IngredientsButton).gameObject.BindEvent(OnClickIngredients);
        GetButton((int)Buttons.InteriorButton).gameObject.BindEvent(OnClickInteriors);
        GetButton((int)Buttons.FlowerButton).gameObject.BindEvent(OnClickFlowerButton);
        GetButton((int)Buttons.DefuserButton).gameObject.BindEvent(OnClickDefuserButton);
        GetButton((int)Buttons.SignboardButton).gameObject.BindEvent(OnClickSignboardButton);
        GetButton((int)Buttons.CoffeeButton).gameObject.BindEvent(OnClickCoffeeButton);
        GetButton((int)Buttons.CookieButton).gameObject.BindEvent(OnClickCookieButton);
        GetButton((int)Buttons.IceButton).gameObject.BindEvent(OnClickIceCreamButton);
        GetButton((int)Buttons.SandButton).gameObject.BindEvent(OnClickSandwichButton);
        GetButton((int)Buttons.CakeButton).gameObject.BindEvent(OnClickCakeButton);


        GetButton((int)Buttons.EditButton).gameObject.BindEvent(OnClickEditButton);
        GetButton((int)Buttons.BearButton).gameObject.BindEvent(OnClickBearButton);
        GetButton((int)Buttons.BooUpButton).gameObject.BindEvent(OnClickBooUpButton);

        _timerGauge = GetImage((int)Images.TimerGauge);


        GetImage((int)Images.Flower).gameObject.SetActive(false);
        GetImage((int)Images.Defuser).gameObject.SetActive(false);
        GetImage((int)Images.Signboard).gameObject.SetActive(false);

        RefreshLock();

        //처음에 찾고 꺼놔야함, 아니면 널 뜸
        GetObject((int)GameObjects.Reveal).SetActive(false);
        GetObject((int)GameObjects.Interiors).gameObject.SetActive(false);
        GetObject((int)GameObjects.BooUp).gameObject.SetActive(false);

        GetText((int)Texts.CookieEA).text = "잠김";
        GetText((int)Texts.IceCreamEA).text = "잠김";
        GetText((int)Texts.SandwichEA).text = "잠김";
        GetText((int)Texts.CakeEA).text = "잠김";

        RefreshMoney();
        RefreshReputation();
        SyncronizeDate();

        Managers.Sound.Clear();
        Managers.Sound.Play(Sound.Bgm, "Sound_MainPlayBGM", volume: 0.2f);

        return true;
	}

    //하루 n분
    //private const float EndDayTime = 10.0f;
    private const float EndDayTime = 60.0f;

    private const int devideTime = 1;
    private int cycleTemp = 0;

    private void Update()
    {
        //Peek 메서드를 통해 스택의 최상위가 this가 아닌경우, 해당 메서드에서 업데이트를 반복
        if (Managers.UI.PeekPopupUI<UI_PlayPopup>() != this)
            return;

        //엔딩 이미 봤는지 검사
        if (!Managers.Game.AlreadyEnding)
        {
            if(Managers.Game.Reputation >= 1000)
            {
                Managers.Game.AlreadyEnding = true;
                Managers.UI.ShowPopupUI<UI_EndingPopup>().SetInfo(1);
                Managers.Sound.Stop(Sound.Bgm);
            }
        }

        //배드엔딩
        if (BadEnd)
        {
            Managers.UI.ShowPopupUI<UI_TitlePopup>();
            Managers.UI.ClosePopupUI(this);
        }

        DayCycle();

        RefreshMoney();
        RefreshReputation();
        RefreshButtons();
        RefreshCoffeeBean();
        RefreshCookie();
        RefreshIceCream();
        RefreshSandwich();
        RefreshCake();
        RefreshInterior();

    }

    private void DayCycle()
    {
        //시간 사이클
        if (_game.DayTime >= EndDayTime)
        {
            //시간 초기화
            _game.DayTime = 0.0f;
            _game.Date++;
            cycleTemp = 0;

            _timerGauge.fillAmount = 0 / (float)EndDayTime;

            Debug.Log(_EntityList.Count);

            SyncronizeDate();
            ShowDayEndPopup();
            Debug.Log("Day End");
        }
        else
        {
            _game.DayTime += Time.deltaTime;
            _timerGauge.fillAmount = _game.DayTime / (float)EndDayTime;
            if ((int)_game.DayTime > cycleTemp)
            {
                cycleTemp = (int)_game.DayTime;
                //2초가 될 때 마다 주사위 굴림
                if (cycleTemp % devideTime == 0)
                {
                    if((int)_game.DayTime >= 52)
                    {
                        return;
                    }
                    TrySpawnCustomer();
                }
            }
        }
    }

    //테스트 값 (초기 등장확률 10%)
    private const int absPercentage = 2500;

    #region 개미 스폰 알고리즘
    private void TrySpawnCustomer()
    {
        //당첨번호
        int getDice = sysRandom.Next(0, 10000);
        Debug.Log("Run Dice");
        if (absPercentage + (9 * _game.Reputation) >= getDice)
        {
            if(_EntityList.Count >= 8)
            {
                //자리초과
                Debug.Log("Ant List Full");
                return;
            }
            else
            {
                //당첨 시 객체 생성
                GameObject antEntity = Managers.Resource.Instantiate($"Characters/{SpawnCharacters[sysRandom.Next(0, 6)]}", this.transform);

                if (_EntityList.Count == 0)
                {
                    //빈 리스트 일 경우
                    int tempNum = sysRandom.Next(1, 8);
                    antEntity.transform.position = FilterLocation(tempNum);

                    PlayerController entity = antEntity.AddComponent<PlayerController>();
                    entity.locationID = tempNum;

                    _EntityList.Add(entity);
                    entity.getList(_EntityList);
                    entity.getManager(_game);

                }
                else
                {
                    //리스트 원소 아이디 추출 및 정렬
                    List<int> tempList = new List<int>();
                    int NewID = 1;
                    int tempNum = sysRandom.Next(1, 8);
                    bool checkCanRandom = true;

                    for (int i = 0; i < _EntityList.Count; i++)
                    {
                        tempList.Add(_EntityList[i].locationID);
                    }

                    tempList.Sort();
                    
                    for(int i = 0; i < _EntityList.Count; i++)
                    {
                        if (tempNum == tempList[i])
                        {
                            checkCanRandom = false;
                            break;
                        }
                    }

                    if (checkCanRandom)
                    {
                        NewID = tempNum;
                    }
                    else
                    {
                        for (int i = 0; i < _EntityList.Count; i++)
                        {
                            if (NewID == tempList[i])
                            {
                                NewID++;
                            }
                        }
                    }
                    antEntity.transform.position = FilterLocation(NewID);
                    PlayerController entity = antEntity.AddComponent<PlayerController>();
                    entity.locationID = NewID;
                    _EntityList.Add(entity);                    
                    entity.getList(_EntityList);
                    entity.getManager(_game);
                }
            }
        }
    }
    #endregion

    #region 새로고침 메소드
    void SyncronizeDate()
    {
        GetText((int)Texts.DateText).text = _game.Date.ToString();
    }
    void RefreshMoney()
    {
        GetText((int)Texts.MoneyText).text = _game.Money.ToString();
    }

    void RefreshReputation()
    {
        GetText((int)Texts.ReputationText).text = _game.Reputation.ToString();
    }
    //재료
    void RefreshCoffeeBean()
    {
        GetText((int)Texts.CoffeeEA).text = String.Format("{0}개", _game.CoffeeBean.ToString());
    }

    void RefreshCookie()
    {
        if (_game.BtnCookie)
        {
            GetText((int)Texts.CookieEA).text = String.Format("{0}개", _game.Cookie.ToString());
        }
    }

    void RefreshIceCream()
    {
        if (_game.BtnIceCream)
        {
            GetText((int)Texts.IceCreamEA).text = String.Format("{0}개", _game.IceCream.ToString());
        }
    }

    void RefreshSandwich()
    {
        if (_game.BtnSandwich)
        {
            GetText((int)Texts.SandwichEA).text = String.Format("{0}개", _game.Sandwich.ToString());
        }
    }

    void RefreshCake()
    {
        if (_game.BtnCake)
        {
            GetText((int)Texts.CakeEA).text = String.Format("{0}개", _game.Cake.ToString());
        }
    }

    public void RefreshButtons()
    {
        if (_game.BtnCookie)
        {
            GetButton((int)Buttons.CookieButton).enabled = true;
        }
        if (_game.BtnIceCream)
        {
            GetButton((int)Buttons.IceButton).enabled = true;
        }
        if (_game.BtnSandwich)
        {
            GetButton((int)Buttons.SandButton).enabled = true;
        }
        if (_game.BtnCake)
        {
            GetButton((int)Buttons.CakeButton).enabled = true;
        }
        if (_game.HasFlower)
        {
            GetButton((int)Buttons.FlowerButton).gameObject.SetActive(false);
            GetText((int)Texts.FlowerText).text = "구매완료";
        }
        if (_game.HasDefuser)
        {
            GetButton((int)Buttons.DefuserButton).gameObject.SetActive(false);
            GetText((int)Texts.DefuserText).text = "구매완료";
        }
        if (_game.HasSignboard)
        {
            GetButton((int)Buttons.SignboardButton).gameObject.SetActive(false);
            GetText((int)Texts.SignboardText).text = "구매완료";
        }
    }


    #endregion

    #region 이벤트
	void OnClickFolderSwitch()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        if (FolderSwitch)
        {
            GetObject((int)GameObjects.AbilityBar).SetActive(false);
            GetObject((int)GameObjects.Reveal).SetActive(true);
            FolderSwitch = false;
        }
        else
        {
            GetObject((int)GameObjects.AbilityBar).SetActive(true);
            GetObject((int)GameObjects.Reveal).SetActive(false);
            FolderSwitch = true;
        }
    }

    void OnClickIngredients()
    {
        GetObject((int)GameObjects.Ingredients).gameObject.SetActive(true);
        GetObject((int)GameObjects.Interiors).gameObject.SetActive(false);
        GetObject((int)GameObjects.BooUp).gameObject.SetActive(false);
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
    }

    void OnClickInteriors()
    {
        GetObject((int)GameObjects.Ingredients).gameObject.SetActive(false);
        GetObject((int)GameObjects.Interiors).gameObject.SetActive(true);
        GetObject((int)GameObjects.BooUp).gameObject.SetActive(false);
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
    }

    //재료 구입
    void OnClickCoffeeButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");


        Managers.UI.ShowPopupUI<UI_IngredentBuyPopup>().SetInfo(_game, 1);

    }

    void OnClickCookieButton()
    {

        if (!Managers.Game.BtnCookie)
        {
            Managers.UI.ShowPopupUI<UI_UnLockPopup>().SetInfo(this, 1);
        }
        else
        {
            Managers.UI.ShowPopupUI<UI_IngredentBuyPopup>().SetInfo(_game, 2);
        }

    }

    void OnClickIceCreamButton()
    {
        if (!Managers.Game.BtnIceCream)
        {
            Managers.UI.ShowPopupUI<UI_UnLockPopup>().SetInfo(this, 2);
        }
        else
        {
            Managers.UI.ShowPopupUI<UI_IngredentBuyPopup>().SetInfo(_game, 3);
        }

    }

    void OnClickSandwichButton()
    {
        if (!Managers.Game.BtnSandwich)
        {
            Managers.UI.ShowPopupUI<UI_UnLockPopup>().SetInfo(this, 3);
        }
        else
        {
            Managers.UI.ShowPopupUI<UI_IngredentBuyPopup>().SetInfo(_game, 4);
        }

    }

    void OnClickCakeButton()
    {
        if (!Managers.Game.BtnCake)
        {
            Managers.UI.ShowPopupUI<UI_UnLockPopup>().SetInfo(this, 4);
        }
        else
        {
            Managers.UI.ShowPopupUI<UI_IngredentBuyPopup>().SetInfo(_game, 5);
        }
    }

    //장식품 구입
    void OnClickFlowerButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");

        Managers.UI.ShowPopupUI<UI_InteriorPopup>().SetInfo(_game, this, 1);

    }

    void OnClickDefuserButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");

        Managers.UI.ShowPopupUI<UI_InteriorPopup>().SetInfo(_game, this, 2);

    }

    void OnClickSignboardButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");

        Managers.UI.ShowPopupUI<UI_InteriorPopup>().SetInfo(_game, this, 3);

    }

    public void RefreshLock()
    {
        if (Managers.Game.BtnCookie)
        {
            GetImage((int)Images.LockIce).gameObject.SetActive(false);
        }
        if (Managers.Game.BtnIceCream)
        {
            GetImage((int)Images.LockSand).gameObject.SetActive(false);
        }
        if (Managers.Game.BtnSandwich)
        {
            GetImage((int)Images.LockCake).gameObject.SetActive(false);
        }
    }

    public void RefreshInterior()
    {
        if (Managers.Game.HasFlower)
        {
            GetImage((int)Images.Flower).gameObject.SetActive(true);
        }
        if (Managers.Game.HasDefuser)
        {
            GetImage((int)Images.Defuser).gameObject.SetActive(true);
        }
        if (Managers.Game.HasSignboard)
        {
            GetImage((int)Images.Signboard).gameObject.SetActive(true);
        }
    }

    private const int bearCost = 20;

    void OnClickBearButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_UpgradeDone");
        Managers.Game.Money += bearCost;
        Managers.Game.DailyBenefitMoney += bearCost;
    }

    void OnClickBooUpButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");
        GetObject((int)GameObjects.Ingredients).gameObject.SetActive(false);
        GetObject((int)GameObjects.Interiors).gameObject.SetActive(false);
        GetObject((int)GameObjects.BooUp).gameObject.SetActive(true);
    }

    void OnClickEditButton()
    {
        Managers.Sound.Play(Sound.Effect, "Sound_MainButton");

        Managers.UI.ShowPopupUI<UI_EditPopup>();
    }

    #endregion

    void ShowDayEndPopup()
    {
        Managers.UI.ShowPopupUI<UI_DayResultPopup>().getPopup(this);
    }

    public void SetBadEnd()
    {
        this.BadEnd = true;
    }

    public Vector3 FilterLocation(int num)
    {
        switch (num)
        {
            case 1:
                return GetObject((int)GameObjects.Location1).transform.position;
            case 2:
                return GetObject((int)GameObjects.Location2).transform.position;
            case 3:
                return GetObject((int)GameObjects.Location3).transform.position;
            case 4:
                return GetObject((int)GameObjects.Location4).transform.position;
            case 5:
                return GetObject((int)GameObjects.Location5).transform.position;
            case 6:
                return GetObject((int)GameObjects.Location6).transform.position;
            case 7:
                return GetObject((int)GameObjects.Location7).transform.position;
            case 8:
                return GetObject((int)GameObjects.Location8).transform.position;
        }
        //더미
        Debug.Log("Error in FilterLocation");
        return GetObject((int)GameObjects.Location1).transform.position;
    }
}
