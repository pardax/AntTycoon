using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using static Define;

[Serializable]
public class GameData
{
    public string ShopName;

    public int Date; //날짜
    public float DayTime; //시간

    public int Money = 0;
    public int Reputation = 0;

    public int DailyBenefitMoney = 0;
    public int DailyBenefitReputation = 0;
    public int DailySpend = 0;
    public int DailyCustomer = 0;
    public int DailyOrdered = 0;
    public int Loan = 0;
    public bool LoanChance = true;

    public int CoffeeBean = 10;
    public int Cookie = 10;
    public int IceCream = 10;
    public int Sandwich = 10;
    public int Cake = 10;

    public bool BtnCoffeebean = true;
    public bool BtnCookie = false;
    public bool BtnIceCream = false;
    public bool BtnSandwich = false;
    public bool BtnCake = false;

    public short CoffeeSelledCount = 0;
    public short CookieSelledCount = 0;
    public short IceCreamSelledCount = 0;
    public short SandwichSelledCount = 0;
    public short CakeSelledCount = 0;

    public bool HasFlower = false;
    public bool HasDefuser = false;
    public bool HasSignboard = false;

    public bool AlreadyEnding = false;
}

public class GameManagerEx
{
    GameData _gameData = new GameData();
    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }

    #region 스탯
    public string ShopName
    {
        get { return _gameData.ShopName; }
        set { _gameData.ShopName = value; }
    }

    public int Money
    {
        get { return _gameData.Money; }
        set { _gameData.Money = value; }
    }

    public int Reputation
    {
        get { return _gameData.Reputation; }
        set { _gameData.Reputation = value; }
    }

    public int DailyBenefitMoney
    {
        get { return _gameData.DailyBenefitMoney; }
        set { _gameData.DailyBenefitMoney = value; }
    }

    public int DailyBenefitReputation
    {
        get { return _gameData.DailyBenefitReputation; }
        set { _gameData.DailyBenefitReputation = value; }
    }

    public int DailySpend
    {
        get { return _gameData.DailySpend; }
        set { _gameData.DailySpend = value; }
    }

    public int DailyCustomer
    {
        get { return _gameData.DailyCustomer; }
        set { _gameData.DailyCustomer = value; }
    }

    public int DailyOrdered
    {
        get { return _gameData.DailyOrdered; }
        set { _gameData.DailyOrdered = value; }
    }

    public int Loan
    {
        get { return _gameData.Loan; }
        set { _gameData.Loan = value; }
    }

    public bool LoanChance
    {
        get { return _gameData.LoanChance; }
        set { _gameData.LoanChance = value; }
    }

    public int CoffeeBean
    {
        get { return _gameData.CoffeeBean; }
        set { _gameData.CoffeeBean = value; }
    }

    public int Cookie
    {
        get { return _gameData.Cookie; }
        set { _gameData.Cookie = value; }
    }

    public int IceCream
    {
        get { return _gameData.IceCream; }
        set { _gameData.IceCream = value; }
    }

    public int Sandwich
    {
        get { return _gameData.Sandwich; }
        set { _gameData.Sandwich = value; }
    }

    public int Cake
    {
        get { return _gameData.Cake; }
        set { _gameData.Cake = value; }
    }

    public short CoffeeSelledCount
    {
        get { return _gameData.CoffeeSelledCount; }
        set { _gameData.CoffeeSelledCount = value; }
    }

    public short CookieSelledCount
    {
        get { return _gameData.CookieSelledCount; }
        set { _gameData.CookieSelledCount = value; }
    }

    public short IceCreamSelledCount
    {
        get { return _gameData.IceCreamSelledCount; }
        set { _gameData.IceCreamSelledCount = value; }
    }

    public short SandwichSelledCount
    {
        get { return _gameData.SandwichSelledCount; }
        set { _gameData.SandwichSelledCount = value; }

    }

    public short CakeSelledCount
    {
        get { return _gameData.CakeSelledCount; }
        set { _gameData.CakeSelledCount = value; }
    }

    public bool AlreadyEnding
    {
        get { return _gameData.AlreadyEnding; }
        set { _gameData.AlreadyEnding = value; }
    }
    #endregion

    #region 시간
    public int Date
    {
        get { return _gameData.Date; }
        set { _gameData.Date = value; }
    }
    public float DayTime
    {
        get { return _gameData.DayTime; }
        set { _gameData.DayTime = value; }
    }
    #endregion

    #region 잠금
    public bool BtnCoffeebean
    {
        get { return _gameData.BtnCoffeebean; }
        set { _gameData.BtnCoffeebean = value; }
    }

    public bool BtnCookie
    {
        get { return _gameData.BtnCookie; }
        set { _gameData.BtnCookie = value; }
    }

    public bool BtnIceCream
    {
        get { return _gameData.BtnIceCream; }
        set { _gameData.BtnIceCream = value; }
    }

    public bool BtnSandwich
    {
        get { return _gameData.BtnSandwich; }
        set { _gameData.BtnSandwich = value; }
    }

    public bool BtnCake
    {
        get { return _gameData.BtnCake; }
        set { _gameData.BtnCake = value; }
    }

    public bool HasFlower
    {
        get { return _gameData.HasFlower; }
        set { _gameData.HasFlower = value; }
    }

    public bool HasDefuser
    {
        get { return _gameData.HasDefuser; }
        set { _gameData.HasDefuser = value; }
    }

    public bool HasSignboard
    {
        get { return _gameData.HasSignboard; }
        set { _gameData.HasSignboard = value; }
    }
    #endregion

    public void Init()
    {
        //데이터 매니저가서 스타트 변수 따옴, 스타트 변수는 xml를 딴 데이터
        StartData data = Managers.Data.Start;

        Debug.Log("ExManager Init");

        Debug.Log(data.Date);
        Debug.Log(data.Money);
        Debug.Log(data.Reputation);


        //GameData var = LoadSingleXML<StartData>
        Date = data.Date;
        Money = data.Money;
        Reputation = data.Reputation;
    }

    //판매당 평판 상승치
    public void checkSellCount()
    {
        if(CoffeeSelledCount >= 1)
        {
            Reputation += 1;
            DailyBenefitReputation += 1;
            CoffeeSelledCount = 0;
        }

        if (CookieSelledCount >= 1)
        {
            Reputation += 2;
            DailyBenefitReputation += 2;
            CookieSelledCount = 0;
        }

        if (IceCreamSelledCount >= 1)
        {
            Reputation += 3;
            DailyBenefitReputation += 3;
            IceCreamSelledCount = 0;
        }

        if (SandwichSelledCount >= 1)
        {
            Reputation += 4;
            DailyBenefitReputation += 4;
            SandwichSelledCount = 0;
        }

        if (CakeSelledCount >= 1)
        {
            Reputation += 5;
            DailyBenefitReputation += 5;
            CakeSelledCount = 0;
        }
    }

    #region save & load
    public string _path = Application.persistentDataPath + "/SaveData.json";
    public string _resetPath = Application.persistentDataPath + "/StartData.json";

    public void SaveGame()
    {
        string jsonStr = JsonUtility.ToJson(Managers.Game.SaveData);
        File.WriteAllText(_path, jsonStr);
        Debug.Log($"Save Game Completed : {_path}");
    }

    public bool LoadGame()
    {
        if (File.Exists(_path) == false)
            return false;

        string fileStr = File.ReadAllText(_path);
        GameData data = JsonUtility.FromJson<GameData>(fileStr);
        if (data != null)
        {
            Managers.Game.SaveData = data;
        }

        Debug.Log($"Save Game Loaded : {_path}");
        return true;
    }

    public void ResetGame()
    {
        Debug.Log("reset stat");

        this.ShopName = "";

        this.Date = 0; //날짜
        this.DayTime = 0; //시간

        Money = 0;
        Reputation = 0;

        DailyBenefitMoney = 0;
        DailyBenefitReputation = 0;
        DailySpend = 0;
        DailyCustomer = 0;
        DailyOrdered = 0;
        Loan = 0;
        LoanChance = true;

        CoffeeBean = 10;
        Cookie = 10;
        IceCream = 10;
        Sandwich = 10;
        Cake = 10;

        BtnCoffeebean = true;
        BtnCookie = false;
        BtnIceCream = false;
        BtnSandwich = false;
        BtnCake = false;

        CoffeeSelledCount = 0;
        CookieSelledCount = 0;
        IceCreamSelledCount = 0;
        SandwichSelledCount = 0;
        CakeSelledCount = 0;

        HasFlower = false;
        HasDefuser = false;
        HasSignboard = false;
        AlreadyEnding = false;
}
    #endregion
}