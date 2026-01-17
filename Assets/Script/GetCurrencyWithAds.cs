using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Dragon.SDK;

public class GetCurrencyWithAds : Singleton<GetCurrencyWithAds>
{
    public int Quantity;

    public Image Icon;
    public Image FillAmount;
    public Text QuantityTxt;
    public Button PurchaseButton;

    public Sprite IconCoin;
    public Sprite IconGem;

    public int time;
    public int timeHidden;

    public System.DateTime TimCoolDownActiveObj;
    public System.TimeSpan TimeSpanActiveObj;

    public System.DateTime TimeCoolDownHiddenObj;
    public System.TimeSpan TimeSpanHiddenObj;

    public GameObject obj;

    int indexRan;
    public bool TypeCoin;

    bool isHidden;



    public bool isTimeCoolDownActive;
    public bool isTimeCoolDownHidden;

    public float TimeActive;
    public float TimeHidden;

    public float CurTimeActive;
    public float CurTimeHidden;

    public Slider slider;

    public Currentcy_Fly m_CurFly;

    public GameObject CoinPrefabs;
    public GameObject GemPrefabs;

    public Transform PosCoin;
    public Transform PosGem;

    private void Awake()
    {
        PurchaseButton.onClick.AddListener(ShowAdsReward);
    }
    private void Start()
    {
        TimCoolDownActiveObj = System.DateTime.Now.AddSeconds(10);
        isTimeCoolDownActive = true;
        isTimeCoolDownHidden = false;

        CurTimeHidden = TimeHidden;
        CurTimeActive = TimeActive;
        slider.maxValue = TimeHidden;
    }

    [System.Obsolete]
    private void Update()
    {
        if (CurTimeActive >= 0)
        {
            obj.SetActive(false);
        }
        /* if (UI_Home.Instance.uI_Battle.gameObject.activeInHierarchy)
         {
             if(CurTimeHidden > 0)
             {
                 gameObject.SetActive(true);
             }
         }*/
        if (isTimeCoolDownActive && !isTimeCoolDownHidden)
        {
            CurTimeActive -= Time.deltaTime;
        }
        if (isTimeCoolDownHidden && !isTimeCoolDownActive)
        {
            if (!UI_Home.Instance.uI_Battle.gameObject.activeInHierarchy)
            {
                CurTimeHidden -= Time.deltaTime;
                slider.value = CurTimeHidden;
            }
        }
        if (CurTimeActive <= 0 && !isTimeCoolDownHidden && isTimeCoolDownActive)
        {
            indexRan = Random.RandomRange(0, 2);
            if (indexRan == 0)
            {
                TypeCoin = true;
                Icon.sprite = IconCoin;
                m_CurFly.MinSizeCurrentcy = 0.7f;
                m_CurFly.MaxSizeCurrentcy = 0.9f;
                m_CurFly.CurrentcyPrefabs = CoinPrefabs;
                m_CurFly.EndPosMove = PosCoin;
                for (int i = 0; i < Controller.Instance.dataRewardsFree.L_rewards.Count; i++)
                {
                    if (BagManager.Instance.m_RuleController.CurLevel == Controller.Instance.dataRewardsFree.L_rewards[i].CurMap)
                    {
                        Quantity = Controller.Instance.dataRewardsFree.L_rewards[i].Coin;
                        QuantityTxt.text = Quantity.ToString();
                    }
                }
            }
            else
            {
                TypeCoin = false;
                Icon.sprite = IconGem;
                m_CurFly.CurrentcyPrefabs = GemPrefabs;
                m_CurFly.EndPosMove = PosGem;
                m_CurFly.MinSizeCurrentcy = 1.2f;
                m_CurFly.MaxSizeCurrentcy = 1.5f;
                for (int i = 0; i < Controller.Instance.dataRewardsFree.L_rewards.Count; i++)
                {
                    if (BagManager.Instance.m_RuleController.CurLevel == Controller.Instance.dataRewardsFree.L_rewards[i].CurMap)
                    {
                        Quantity = Controller.Instance.dataRewardsFree.L_rewards[i].Gem;
                        QuantityTxt.text = Quantity.ToString();
                    }
                }
            }
            if (!UI_Home.Instance.uI_Battle.gameObject.activeInHierarchy && CurTimeActive <= 0)
            {
                isTimeCoolDownActive = false;
                isTimeCoolDownHidden = true;
                obj.SetActive(true);
                CurTimeHidden = TimeHidden;
                slider.value = CurTimeHidden;
            }
        }
        if (CurTimeHidden <= 0 && !isTimeCoolDownActive && isTimeCoolDownHidden)
        {
            isTimeCoolDownActive = true;
            isTimeCoolDownHidden = false;
            obj.SetActive(false);
            CurTimeActive = TimeActive;
        }
    }
    public void ShowAdsReward()
    {
        AdStatus adstatus = SDKDGManager.Instance.AdsManager.ShowRewardedAdsStatus(() =>
        {
            RewardAds();
        }, "Rewards machine UIHome");
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

    public void RewardAds()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            obj.SetActive(false);
            UpdateView();
            CurTimeHidden = 0;
        }

    }
    public void UpdateView()
    {
        if (m_CurFly)
            m_CurFly.ActiveCurrency(0);
        if (TypeCoin)
        {
            StartCoroutine(IE_delaySetTExtCoin());
        }
        else
        {
            StartCoroutine(IE_delaySetTExtGemss());
        }
    }
    IEnumerator IE_delaySetTExtCoin()
    {
        yield return new WaitForSeconds(m_CurFly.MaxTimeMoveCoin);
        UI_Home.Instance.m_UICoinManager.SetTextCoin(Quantity);

    }
    IEnumerator IE_delaySetTExtGemss()
    {
        yield return new WaitForSeconds(m_CurFly.MaxTimeMoveCoin);
        UI_Home.Instance.m_UIGemManager.SetTextGem(Quantity);

    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
