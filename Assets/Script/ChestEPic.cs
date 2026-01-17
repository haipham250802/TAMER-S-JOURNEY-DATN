using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class ChestEPic : ChestParent
{
    public int QuantityAds;
    public int CountQuantityAds;
    public int Price;

    public Button PurchaseButtonAds;
    public Button PurchasebuttonCoin;

    public Text PriceTxt;
    public Text TimeCoolDownTxt;

    public GameObject Ads;
    public GameObject TimeCoolDownObj; // bat time khi coun < quantity &&

    public DateTime time;
    public DateTime nextTime;


    public DateTime StartTime;
    public DateTime EndTime;
    public DateTime timeCool;

    public ChestReward chestRw;
    private void Awake()
    {
        PurchaseButtonAds.onClick.AddListener(BuyChestWithAds);
        PurchasebuttonCoin.onClick.AddListener(this.BuyChestWithCoin);
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
        DateTime now = DataPlayer.GetTimeOutChestEpic();
        nextTime = now;
        Debug.Log("nexttime: " + now);
        PriceTxt.text = Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestEpic.ToString();
    }
    public TimeSpan cooldown;
    private void Update()
    {
        cooldown = nextTime - DateTime.Now;
        if (cooldown.Ticks > 0 && CountQuantityAds < QuantityAds)
        {
            if(m_PopUpChest.typeChest == TypeChest.ChestEpic && EtypePurchaseButton == typePurchaseButton.ADS)
            {
                PurchaseButtonAds.interactable = false;
                TimeCoolDownTxt.text = DateTimeHelper.TimeToString_HMS(cooldown);
                if (m_PopUpChest.gameObject.activeInHierarchy && EtypePurchaseButton == typePurchaseButton.ADS)
                {
                    m_PopUpChest.OpenMoreTxt.gameObject.SetActive(false);
                    m_PopUpChest.timeCooldown.gameObject.SetActive(true);
                    m_PopUpChest.timeCooldown.text = DateTimeHelper.TimeToString_HMS(cooldown);
                }
                TimeCoolDownObj.gameObject.SetActive(true);
                TimeCoolDownTxt.text = DateTimeHelper.TimeToString_HMS(cooldown);
            }
        }
        else
        {
            if (m_PopUpChest.gameObject.activeInHierarchy && EtypePurchaseButton == typePurchaseButton.ADS)
            {
                m_PopUpChest.OpenMore.interactable = true;
                m_PopUpChest.ActiveViewItemAds();
            }
            PurchaseButtonAds.interactable = true;
            Ads.SetActive(true);
            TimeCoolDownObj.gameObject.SetActive(false);
        }
    }
    public override void SubCoin()
    {
        base.SubCoin();
    }
    public void BuyChestWithAds()
    {
        m_PopUpChest.typeChest = TypeChest.ChestEpic;
        if (cooldown.Ticks <= 0 && DataPlayer.GetCountAdsEpic() < QuantityAds)
        {
            CountQuantityAds = DataPlayer.GetCountAdsEpic();
            BuyChestWithAds(CountQuantityAds, QuantityAds);
            CountQuantityAds++;
            DataPlayer.SetCountAdsEpic(CountQuantityAds);
            Ads.SetActive(false);
            if (DataPlayer.GetCountAdsEpic() < QuantityAds)
            {
                nextTime = DateTime.Now.AddSeconds(5);
            }
            else if (DataPlayer.GetCountAdsEpic() >= QuantityAds)
            {
                nextTime = DateTime.Now.AddSeconds(60);
                CountQuantityAds = 0;
                DataPlayer.SetCountAdsEpic(CountQuantityAds);
            }
            chestRw = Controller.Instance.dataChest.ChestRewardIndex(TypeChest.ChestEpic);
            DataPlayer.SetTimeOutChestEpic(nextTime);
        }

    }
    public override void BuyChestWithAds(int CountBuyAds, int MaxQuantityBuyAds)
    {
        base.BuyChestWithAds(CountBuyAds, MaxQuantityBuyAds);
    }
    private void InitView()
    {
        if (DataPlayer.GetCountAdsEpic() < QuantityAds && cooldown.Ticks <= 0)
        {
            Ads.SetActive(true);
            TimeCoolDownTxt.gameObject.SetActive(false);
        }
    }
    public override void BuyChestWithCoin()
    {
        m_PopUpChest.typeChest = TypeChest.ChestEpic;
        chestRw = Controller.Instance.dataChest.ChestRewardIndex(TypeChest.ChestEpic);
        base.BuyChestWithCoin();
    }

    public void updateCurrency()
    {
        if (chestRw.typeReward == TypeReward.Coin)
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
