using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class StartData
{
    [XmlAttribute]
    public string ShopName;
    [XmlAttribute]
    public int Date;
    [XmlAttribute]
    public float DayTime;
    [XmlAttribute]
    public int Money;
    [XmlAttribute]
    public int Reputation;
    [XmlAttribute]
    public int DailyBenefitMoney;
    [XmlAttribute]
    public int DailyBenefitReputation;
    [XmlAttribute]
    public int DailySpend;
    [XmlAttribute]
    public int DailyCustomer;
    [XmlAttribute]
    public int DailyOrdered;
    [XmlAttribute]
    public int Loan;
    [XmlAttribute]
    public int LoanChance;
    [XmlAttribute]
    public int CoffeeBean;
    [XmlAttribute]
    public int Cookie;
    [XmlAttribute]
    public int IceCream;
    [XmlAttribute]
    public int Sandwich;
    [XmlAttribute]
    public int Cake;
    [XmlAttribute]
    public bool BtnCoffeebean;
    [XmlAttribute]
    public bool BtnCookie;
    [XmlAttribute]
    public bool BtnIceCream;
    [XmlAttribute]
    public bool BtnSandwich;
    [XmlAttribute]
    public bool BtnCake;
    [XmlAttribute]
    public short CoffeeSelledCount;
    [XmlAttribute]
    public short CookieSelledCount;
    [XmlAttribute]
    public short IceCreamSelledCount;
    [XmlAttribute]
    public short SandwichSelledCount;
    [XmlAttribute]
    public short CakeSelledCount;
    [XmlAttribute]
    public bool HasFlower;
    [XmlAttribute]
    public bool HasDefuser;
    [XmlAttribute]
    public bool HasSignboard;
    [XmlAttribute]
    public bool AlreadyEnding;
}
