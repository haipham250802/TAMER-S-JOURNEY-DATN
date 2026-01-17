using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChestParent : MonoBehaviour
{
    public int CoinBuyChest;
    public PopUpChestt m_PopUpChest;
    public typePurchaseButton EtypePurchaseButton;
    public TypeChest EtypeChest;

    public virtual void BuyChestWithCoin()
    {
        if (DataPlayer.GetCoin() > CoinBuyChest)
        {
            SetCurrencyBuyChest();
            EtypePurchaseButton = typePurchaseButton.COIN;
            SubCoin();
            m_PopUpChest.gameObject.SetActive(true);
            m_PopUpChest.ActionSpine();
        }
        else
        {
            Debug.Log("Khong du tien");
        }
    }
    public virtual void BuyChestWithAds(int CountBuyAds, int MaxQuantityBuyAds)
    {
        if (CountBuyAds < MaxQuantityBuyAds)
        {
            EtypePurchaseButton = typePurchaseButton.ADS;
            m_PopUpChest.gameObject.SetActive(true);
            m_PopUpChest.ActionSpine();
        }
        else
        {
            Debug.Log("Da het so luong ads");
        }
    }
    public virtual void BuyChestWithGem()
    {
        EtypePurchaseButton = typePurchaseButton.GEM;
        m_PopUpChest.gameObject.SetActive(true);
        m_PopUpChest.ActionSpine();
    }
    public virtual void BuyChestX10WithCoin()
    {
        EtypePurchaseButton = typePurchaseButton.COIN;
        SubCoin();
        m_PopUpChest.gameObject.SetActive(true);
        m_PopUpChest.ActionSpine();
    }
    public virtual void SubCoin()
    {
        SetCurrencyBuyChest();
        TextCoinAnimation.Instance.ActionAnimationText(UI_Home.Instance.m_UICoinManager.CoinTxt,
               DataPlayer.GetCoin(), (DataPlayer.GetCoin() - CoinBuyChest), 0.3f);
        DataPlayer.SetCoin(DataPlayer.GetCoin() - CoinBuyChest);
    }

    public virtual void SetCurrencyBuyChest()
    {
        switch (EtypeChest)
        {
            case TypeChest.ChestNormal:
                CoinBuyChest = Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestNormal;
                break;
            case TypeChest.ChestEpic:
                CoinBuyChest = Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestEpic;
                break;
            case TypeChest.ChestLegend:

                break;
            case TypeChest.ChestNormalx10:
                CoinBuyChest = (int)((Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestNormal * 10) * 0.8f);
                break;

        }
    }
    public virtual void BuyX10Chest()
    {
        m_PopUpChest.gameObject.SetActive(true);
        m_PopUpChest.ActionSpine();
    }
    public virtual ChestReward GetItemWhenRandomChestNormal()
    {
        ChestReward chestRwd = Controller.Instance.dataChest.ChestRewardIndex(TypeChest.ChestNormal);
        return chestRwd;
    }
    public virtual ChestReward GetItemWhenRandomChestEpic()
    {
        ChestReward chestRwd = Controller.Instance.dataChest.ChestRewardIndex(TypeChest.ChestNormal);
        return chestRwd;
    }

}
public enum typePurchaseButton
{
    NONE,
    ADS,
    COIN,
    GEM
}
