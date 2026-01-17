using Dragon.SDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopElement : MonoBehaviour
{
    public TypeShopGem typeGem;
    public TypeShopCoin typeCoin;
    public Text CostTxt;
    public Text Quantity_CurrencyTxt;
    public Button PurchaseButton;
    public MoveCoin m_MoveCoin;
    public GameObject CoinPrefabs;
    public Transform StartPos;
    public Transform PosEndCoin;
    public GameObject objAds;

   

    private void Awake()
    {
        PurchaseButton.onClick.AddListener(OnclickPurchaseButton);
    }
    private void Start()
    {
        LoadData();
        SetCost(typeGem);
    }
    public void LoadData()
    {
        Debug.Log("da loadddd");
        for (int i = 0; i < Controller.Instance.enapData.ProductGem.Count; i++)
        {
            if (typeGem == Controller.Instance.enapData.ProductGem[i].TypeShopGem)
            {
                if (typeGem != TypeShopGem.Gem_free)
                {
                    /* CostTxt.text = Controller.Instance.enapData.ProductGem[i].Cost.ToString() + "$";*/
                    Quantity_CurrencyTxt.text = Controller.Instance.enapData.ProductGem[i].Quantity_Gem.ToString();
                }
                else
                {
                    Quantity_CurrencyTxt.text = Controller.Instance.enapData.ProductGem[i].Quantity_Gem.ToString();
                    CostTxt.gameObject.SetActive(false);
                    objAds.SetActive(true);
                }

                break;
            }
        }
        for (int i = 0; i < Controller.Instance.enapData.ProductCoin.Count; i++)
        {
            if (typeCoin == Controller.Instance.enapData.ProductCoin[i].typeShopCoin)
            {
                CostTxt.text = Controller.Instance.enapData.ProductCoin[i].Cost.ToString();
                Quantity_CurrencyTxt.text = Controller.Instance.enapData.ProductCoin[i].Quantity_Coin.ToString();
                break;
            }
        }
    }
    public void SetCost(TypeShopGem type)
    {
        switch (type)
        {
            case TypeShopGem.Gem_1usd:
                CostTxt.text = SDKDGManager.Instance.IAPManager.GetPrice(ProduckID.Key_1_USD).ToString();
                break;
            case TypeShopGem.Gem_5usd:
                CostTxt.text = SDKDGManager.Instance.IAPManager.GetPrice(ProduckID.Key_5_USD).ToString();
                break;
            case TypeShopGem.Gem_10usd:
                CostTxt.text = SDKDGManager.Instance.IAPManager.GetPrice(ProduckID.Key_10_USD).ToString();
                break;
            case TypeShopGem.Gem_50usd:
                CostTxt.text = SDKDGManager.Instance.IAPManager.GetPrice(ProduckID.Key_50_USD).ToString();
                break;
        }
    }
    public void OnclickPurchaseButton()
    {
        /* EnapManager.Instance.A_PurchaseShopBtn(int.Parse(Quantity_CoinTxt.text), 0); // them gia vao day
                                                                                      // UI_Home.Instance.m_UIShop.MoveCoin();
        // m_MoveCoin.Action(CoinPrefabs, 50, StartPos.position, StartPos,EnapManager.Instance.AnimationCoin);
         m_NewCoinFLy.ACtionCoin(0);
         StartCoroutine(IE_delayCoin());*/

        if (typeCoin != TypeShopCoin.NONE)
        {
            BuyCoin();
        }
        if (typeGem != TypeShopGem.NONE && typeGem != TypeShopGem.Gem_free)
        {
            Debug.Log("khac");
            switch (typeGem)
            {
                case TypeShopGem.Gem_1usd:
                    Purchase(ProduckID.Key_1_USD);
                    break;
                case TypeShopGem.Gem_5usd:
                    Purchase(ProduckID.Key_5_USD);
                    break;
                case TypeShopGem.Gem_10usd:
                    Purchase(ProduckID.Key_10_USD);
                    break;
                case TypeShopGem.Gem_50usd:
                    Purchase(ProduckID.Key_50_USD);
                    break;
            }
        }
        else if (typeGem != TypeShopGem.NONE && typeGem == TypeShopGem.Gem_free)
        {
            ShowAdsReward();
        }
    }
    public void Purchase(string id)
    {
        SDKDGManager.Instance.IAPManager.Purchase(id, () =>
        {
           // BuyGem();
            Debug.Log(id + "$");
            Debug.Log("Pruchase Success Update UI");
            BuyGem();
        });
       
    }

    private void ShowAdsReward()
    {
        AdStatus adstatus = SDKDGManager.Instance.AdsManager.ShowRewardedAdsStatus(() =>
        {
            BuyGem();
            Debug.Log("Buy Gem In Shop");
            Controller.Instance.CountTime = 0;
        }, "Gem free in shop");
        switch (adstatus)
        {
            case AdStatus.NoInternet:
                popUpManager.Instance.m_PopUpNointernet.gameObject.SetActive(true);
                break;

            case AdStatus.NoVideo:
                popUpManager.Instance.m_PopUPNovideo.gameObject.SetActive(true);
                break;
        }
    }
    public void BuyGem()
    {
        UI_Home.Instance.m_UIShop.ActiionFlyGem();
        StartCoroutine(IE_DelaySetTextGem());
    }
    public void BuyCoin()
    {
        if (DataPlayer.GetGem() >= int.Parse(CostTxt.text))
        {
            UI_Home.Instance.m_UIShop.ActiionFlyCoin();
            UI_Home.Instance.m_UIGemManager.SubGem(int.Parse(CostTxt.text));
            StartCoroutine(IE_DelaySetTextCoin());
        }
        else
        {
            popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.GEM;
            popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
        }
    }
    IEnumerator IE_DelaySetTextCoin()
    {
        yield return new WaitForSeconds(UI_Home.Instance.m_UIShop.currentcy_Fly_Coin.MaxTimeMoveCoin);
        UI_Home.Instance.m_UICoinManager.SetTextCoin(int.Parse(Quantity_CurrencyTxt.text));
    }
    IEnumerator IE_DelaySetTextGem()
    {
        yield return new WaitForSeconds(UI_Home.Instance.m_UIShop.currentcy_Fly_Gem.MaxTimeMoveCoin);
        UI_Home.Instance.m_UIGemManager.SetTextGem(int.Parse(Quantity_CurrencyTxt.text));
    }
}
