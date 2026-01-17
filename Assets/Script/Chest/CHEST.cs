using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class CHEST : MonoBehaviour
{
    public E_TypeBuy typeStatBuy;
    public E_QuantityBuy QuantityBuy;
    public TypeChest typeChest;

    public Text TimeCoolDownTxt;
    public Text Price_x1_txt;
    public Text Price_x10_txt;
    public Text NotiTxt;
    public GameObject NotiObj;

    public Button BUY_x1_btn;
    public Button BUY_x10_btn;

    public GameObject AdsObj;
    public GameObject CoinObj;

    public int Coin_x1 = 0;
    public int Coin_x10 = 0;
    public int Gem_x1 = 0;
    public int Gem_10 = 0;

    public int QuantityNormalChest;
    public int QuantityEpicChest;
    public int QuantityLegendChest;

    public DateTime nextTime;
    public TimeSpan timeCoolDown;
    public TimeSpan timeCoolDownNextDay;

    public ItemCritter itemCritter;
    public ItemCurrentcy itemCurrency;
  
    public virtual void BUY_CHEST_X1()
    {
        QuantityBuy = E_QuantityBuy.x1;
    }
    public virtual void BUY_CHEST_X10()
    {
        QuantityBuy = E_QuantityBuy.x10;
    }
    public virtual void SubCurrency(int value, E_TypeBuy _typeStatBuy)
    {
        switch (_typeStatBuy)
        {
            case E_TypeBuy.COIN:
                UI_Home.Instance.m_UICoinManager.Subcoin(value);
                break;
            case E_TypeBuy.GEM:
                UI_Home.Instance.m_UIGemManager.SubGem(value);
                break;
        }
    }
    public virtual void InitView()
    {

    }
}
public enum E_TypeChest
{
    NONE,
    NORMAL,
    EPIC,
    LEGEND
}
public enum E_TypeBuy
{
    NONE,
    ADS,
    COIN,
    GEM,
    PACK
}
public enum E_QuantityBuy
{
    NONE,
    x1,
    x10
}

