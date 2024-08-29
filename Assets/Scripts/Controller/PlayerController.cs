using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class PlayerController : BaseController 
{
	public enum EmoticonType
	{
		None,
		Exclamation,
		Question
	}

	enum Buttons
    {
		OrderButton
	}

	enum Images
    {
		TimerGauge,
		CoffeeBean,
		Cookie,
		IceCream,
		Sandwich,
		Cake
	}

	public int locationID { get; set; }

	private int menuID = 0;

	List<PlayerController> entityList;

	GameManagerEx _game;

	GameObject _orderBallon;
	Image _timerGauge;

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		_orderBallon = Managers.Resource.Instantiate("Side/OrderButton", this.transform);
		_game.DailyCustomer++;

		BindButton(typeof(Buttons));
		BindImage(typeof(Images));

		GetButton((int)Buttons.OrderButton).gameObject.BindEvent(OnClickOrderButton);
		_timerGauge = GetImage((int)Images.TimerGauge);

		//메뉴선정
		getMenu();

		Managers.Sound.Play(Sound.Effect, "Sound_Exclamation", volume: 0.5f );

		return true;
	}

	private float entityTimer = 0.0f;	
	private int deadTime = 7;
	
    private void Update()
    {
		entityTimer += Time.deltaTime;
		_timerGauge.fillAmount = entityTimer / (float)deadTime;

		if ((int)entityTimer >= deadTime)
        {
			Managers.Sound.Play(Sound.Effect, "Sound_PlayerAttacked", volume: 0.5f );
			_game.Reputation--;
			entityList.RemoveAt(FindMyIndex());
			Destroy(this.gameObject);
		}
    }

    void OnClickOrderButton()
    {
		if (!checkIngredentIsEmpty(this.menuID))
        {
			Managers.Sound.Play(Sound.Effect, "Sound_Cancelbutton");
			return;
        }

		_game.DailyOrdered++;

		switch (this.menuID)
		{
			case 1: //커피 판매가
				Managers.Game.Money += 1500;
				_game.DailyBenefitMoney += 1500;
				_game.CoffeeSelledCount++;
				_game.checkSellCount();
				break;
			case 2: //쿠키 판매가
				Managers.Game.Money += 2000;
				_game.DailyBenefitMoney += 2000;
				_game.CookieSelledCount++;
				_game.checkSellCount();
				break;
			case 3: //아이스크림 판매가
				Managers.Game.Money += 3000;
				_game.DailyBenefitMoney += 3000;
				_game.IceCreamSelledCount++;
				_game.checkSellCount();
				break;
			case 4: //샌드위치 판매가
				Managers.Game.Money += 5000;
				_game.DailyBenefitMoney += 5000;
				_game.SandwichSelledCount++;
				_game.checkSellCount();
				break;
			case 5: //케이크 판매가
				Managers.Game.Money += 10000;
				_game.DailyBenefitMoney += 10000;
				_game.CakeSelledCount++;
				_game.checkSellCount();
				break;
		}

		Managers.Sound.Play(Sound.Effect, "Sound_Coin");

		//리스트 인덱스 삭제
		entityList.RemoveAt(FindMyIndex());
		minusMenuIngrediant();

		Destroy(this.gameObject);
	}

	public void getManager(GameManagerEx gameEx)
    {
		this._game = gameEx;
    }

	public void getList(List<PlayerController> list)
    {
		this.entityList = list;
    }

	private int FindMyIndex()
    {
		int value = 0;
		for (int i = 0; i < entityList.Count; i++)
		{
			if (entityList[i].locationID == entityList.Find(x => x.Equals(this)).locationID)
			{
				value = i;
				break;
			}
		}
		return value;
    }

	private void getMenu()
    {
		System.Random rand = new System.Random();
		this.menuID = rand.Next(1, getUnlockMenuCount());

		switch (this.menuID)
		{
			case 1:
				GetImage((int)Images.Cookie).gameObject.SetActive(false);
				GetImage((int)Images.IceCream).gameObject.SetActive(false);
				GetImage((int)Images.Sandwich).gameObject.SetActive(false);
				GetImage((int)Images.Cake).gameObject.SetActive(false);
				break;
			case 2:
				GetImage((int)Images.CoffeeBean).gameObject.SetActive(false);
				GetImage((int)Images.IceCream).gameObject.SetActive(false);
				GetImage((int)Images.Sandwich).gameObject.SetActive(false);
				GetImage((int)Images.Cake).gameObject.SetActive(false);
				break;
			case 3:
				GetImage((int)Images.CoffeeBean).gameObject.SetActive(false);
				GetImage((int)Images.Cookie).gameObject.SetActive(false);
				GetImage((int)Images.Sandwich).gameObject.SetActive(false);
				GetImage((int)Images.Cake).gameObject.SetActive(false);
				break;
			case 4:
				GetImage((int)Images.CoffeeBean).gameObject.SetActive(false);
				GetImage((int)Images.Cookie).gameObject.SetActive(false);
				GetImage((int)Images.IceCream).gameObject.SetActive(false);
				GetImage((int)Images.Cake).gameObject.SetActive(false);
				break;
			case 5:
				GetImage((int)Images.CoffeeBean).gameObject.SetActive(false);
				GetImage((int)Images.Cookie).gameObject.SetActive(false);
				GetImage((int)Images.IceCream).gameObject.SetActive(false);
				GetImage((int)Images.Sandwich).gameObject.SetActive(false);
				break;
		}
	}

	private void minusMenuIngrediant()
	{
		switch (this.menuID)
		{
			case 1:
				_game.CoffeeBean--;
				break;
			case 2:
				_game.Cookie--;
				break;
			case 3:
				_game.IceCream--;
				break;
			case 4:
				_game.Sandwich--;
				break;
			case 5:
				_game.Cake--;
				break;
		}
	}

	private int getUnlockMenuCount()
    {
		int result = 1;
		bool[] array = {_game.BtnCoffeebean ,_game.BtnCookie, _game.BtnIceCream, _game.BtnSandwich, _game.BtnCake };

		for(int i = 0; i < 5; i++)
        {
            if (array[i])
            {
				result++;
            }
        }
		return result;
    }

	bool checkIngredentIsEmpty(int num)
    {
        switch (num)
        {
			case 1:
				if(_game.CoffeeBean <= 0)
					return false;
				return true;
			case 2:
				if (_game.Cookie <= 0)
					return false;
				return true;
			case 3:
				if (_game.IceCream <= 0)
					return false;
				return true;
			case 4:
				if (_game.Sandwich <= 0)
					return false;
				return true;
			case 5:
				if (_game.Cake <= 0)
					return false;
				return true;
		}
		return true;
    }
	
	public void antSuicide()
    {
		Destroy(this.gameObject);
	}

	~PlayerController()
    {
		Debug.Log("Ant dead");
	}
	
}
