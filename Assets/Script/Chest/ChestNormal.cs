using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using System;
public class ChestNormal : ChestParent
{
    public int QuantityAds;
    public int CountQuantityAds;
    public int Price;

    public Button PurchaseButtonAds;
    public Button PurchasebuttonCoin;

    public Text PriceTxt;
    public Text TimeCoolDownTxt;

    public GameObject TimeCoolDownObj; // bat time khi coun < quantity &&

    public DateTime time;
    public DateTime nextTime;

    public DateTime StartTime;
    public DateTime EndTime;
    public DateTime timeCool;

    public string fomat = "mm:ss";
    public ChestReward chestRw = new ChestReward();

    private void Awake()
    {
        PurchaseButtonAds.onClick.AddListener(BuyChestWithAds);
        PurchasebuttonCoin.onClick.AddListener(BuyChestWithCoin);
    }
    private void OnEnable()
    {
        StartCoroutine(IEdelay());
    }
    IEnumerator IEdelay()
    {
        yield return null;
        InitView();
    }
    private void Start()
    {
        DateTime now = DataPlayer.GetTimeOutChestNormal();
        nextTime = now;
        Debug.Log("nexttime: " + now);
        PriceTxt.text = Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestNormal.ToString();
    }
    public TimeSpan cooldown;
    private void Update()
    {
        cooldown = nextTime - DateTime.Now;
        InitView();

        if (cooldown.Ticks > 0)
        {
            Debug.Log("cooldown: " + cooldown.Ticks);
            if(m_PopUpChest.typeChest == TypeChest.ChestNormal && EtypePurchaseButton == typePurchaseButton.ADS)
            {
                // PurchaseButtonAds.interactable = false;
                PurchaseButtonAds.gameObject.SetActive(false);
                PurchasebuttonCoin.gameObject.SetActive(true);
                TimeCoolDownTxt.text = DateTimeHelper.TimeToString_HMS(cooldown);
                if (m_PopUpChest.gameObject.activeInHierarchy && EtypePurchaseButton == typePurchaseButton.ADS)
                {
                    m_PopUpChest.OpenMoreTxt.gameObject.SetActive(false);
                    m_PopUpChest.timeCooldown.gameObject.SetActive(true);
                }
                TimeCoolDownObj.gameObject.SetActive(true);
                TimeCoolDownTxt.text = DateTimeHelper.TimeToString_HMS(cooldown);
            }
        }
        else if(cooldown.Ticks < 0 && CountQuantityAds < QuantityAds)
        {
            if (m_PopUpChest.gameObject.activeInHierarchy && EtypePurchaseButton == typePurchaseButton.ADS)
            {
                m_PopUpChest.OpenMore.interactable = true;
                m_PopUpChest.ActiveViewItemAds();
            }
            PurchaseButtonAds.gameObject.SetActive(true);
            PurchasebuttonCoin.gameObject.SetActive(false);
            TimeCoolDownTxt.text = "0m00s";

        }
    }
    public override void SubCoin()
    {
        base.SubCoin();
    }
    public void BuyChestWithAds()
    {
        m_PopUpChest.typeChest = TypeChest.ChestNormal;
        if (cooldown.Ticks <= 0 && DataPlayer.GetCountAdsNormal() < QuantityAds)
        {
            CountQuantityAds = DataPlayer.GetCountAdsNormal();
            BuyChestWithAds(CountQuantityAds, QuantityAds);
            CountQuantityAds++;
            DataPlayer.SetCountAdsNormal(CountQuantityAds);
            if (DataPlayer.GetCountAdsNormal() < QuantityAds)
            {
                nextTime = DateTime.Now.AddSeconds(60);
            }
            else if (DataPlayer.GetCountAdsNormal() >= 3)
            {
                nextTime = DateTime.Now.AddSeconds(30);
                CountQuantityAds = 0;
                DataPlayer.SetCountAdsNormal(CountQuantityAds);
            }
            chestRw = Controller.Instance.dataChest.ChestRewardIndex(EtypeChest);
            DataPlayer.SetTimeOutChestNormal(nextTime);
        }
    }
    public override void BuyChestWithAds(int CountBuyAds, int MaxQuantityBuyAds)
    {
        m_PopUpChest.typeChest = TypeChest.ChestNormal;
        base.BuyChestWithAds(CountBuyAds, MaxQuantityBuyAds);
    }
    private void InitView()
    {
        if(cooldown.Ticks <= 0)
        {
            TimeCoolDownTxt.text = "0m:00s";
            PurchaseButtonAds.gameObject.SetActive(true);
            PurchasebuttonCoin.gameObject.SetActive(false);
        }
        else
        {
            TimeCoolDownTxt.text = DateTimeHelper.TimeToString_HMS(cooldown);
            PurchaseButtonAds.gameObject.SetActive(false);
            PurchasebuttonCoin.gameObject.SetActive(true);
        }
    }
    public override void BuyChestWithCoin()
    {
        m_PopUpChest.typeChest = TypeChest.ChestNormal;
        chestRw = Controller.Instance.dataChest.ChestRewardIndex(EtypeChest);
        base.BuyChestWithCoin();
    }

    public override ChestReward GetItemWhenRandomChestNormal()
    {
        return base.GetItemWhenRandomChestNormal();
    }
    public void updateCurrency()
    {
        if(chestRw.typeReward == TypeReward.Coin)
        {
            int coin = chestRw.QuantityOrStar;
            int coinPlus = DataPlayer.GetCoin() + coin;
            TextCoinAnimation.Instance.ActionAnimationText(UI_Home.Instance.m_UICoinManager.CoinTxt,
                  DataPlayer.GetCoin(), coinPlus, 0.3f);
            DataPlayer.SetCoin(coinPlus);
        }
    }
    private void OnApplicationQuit()
    {
        EndTime = DateTime.Now;
    }
}
