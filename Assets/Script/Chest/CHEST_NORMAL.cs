using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dragon.SDK;

public class CHEST_NORMAL : CHEST
{
    public List<EnemyStat> L_enemy = new List<EnemyStat>();

    public POP_UP_CHEST popUpChest;
    public ChestReward chestRw;

    public int countClickAdsChestNormal;
    public int maxCountClickAdsChestNormal;
    public bool isCanBuyAds;

    public int CountQuantity;
    public int Quantity;

    public AnimationCurve curveAnimMove;

    private void Awake()
    {
        BUY_x1_btn.onClick.AddListener(BUY_CHEST_X1);
        BUY_x10_btn.onClick.AddListener(BUY_CHEST_X10);
        QuantityNormalChest = DataPlayer.GetQuantityChestNormalPack();
        
    }
    private void OnEnable()
    {
        QuantityNormalChest = DataPlayer.GetQuantityChestNormalPack();
    }
    private void Start()
    {
        System.DateTime now = DataPlayer.GetTimeOutChestNormal();
        countClickAdsChestNormal = DataPlayer.GetCountAdsNormal();
        nextTime = now;
        InitView();
    }
    private void Update()
    {
        timeCoolDown = nextTime - System.DateTime.Now;
        countClickAdsChestNormal = DataPlayer.GetCountAdsNormal();

        if (timeCoolDown.Seconds > 0)
        {
            TimeCoolDownTxt.text = DateTimeHelper.TimeToString_HM(timeCoolDown);
            isCanBuyAds = false;
            InitView();
        }
        if (timeCoolDown.Seconds <= 0 && countClickAdsChestNormal < maxCountClickAdsChestNormal)
        {
            isCanBuyAds = true;
            TimeCoolDownTxt.text = "0m00s";
            InitView();
        }

        if (timeCoolDown.Seconds <= 0 && countClickAdsChestNormal >= maxCountClickAdsChestNormal)
        {
            isCanBuyAds = true;
            countClickAdsChestNormal = 0;
            DataPlayer.SetCountAdsNormal(countClickAdsChestNormal);
            InitView();
        }
        /*else
        {
            TimeCoolDownTxt.text = "0m00s";
        }*/
    }
    public void RewardChestSpinWheel()
    {
        popUpChest.gameObject.SetActive(true);
        popUpChest.EtypeChest = TypeChest.ChestNormal;
        typeChest = TypeChest.ChestNormal;
        popUpChest.ChestSpine.Skeleton.SetSkin("Skin_blue");
        popUpChest.ChestSpine.LateUpdate();
    }
    public override void BUY_CHEST_X1()
    {
        if (QuantityNormalChest <= 0)
        {
            if (!isCanBuyAds)
            {
                typeStatBuy = E_TypeBuy.COIN;
                popUpChest.EstatBuy = E_TypeBuy.COIN;
            }
            else
            {
                typeStatBuy = E_TypeBuy.ADS;
                popUpChest.EstatBuy = E_TypeBuy.ADS;
            }
            if (typeStatBuy == E_TypeBuy.COIN && !isCanBuyAds)
            {
                if (DataPlayer.GetCoin() >= Coin_x1)
                {
                    SubCurrency(Coin_x1, typeStatBuy);
                    base.BUY_CHEST_X1();
                    popUpChest.gameObject.SetActive(true);
                    popUpChest.EtypeChest = TypeChest.ChestNormal;
                    typeChest = TypeChest.ChestNormal;
                    popUpChest.ChestSpine.Skeleton.SetSkin("Skin_blue");
                    popUpChest.ChestSpine.LateUpdate();
                    InitView();
                }
                else
                {
                    popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.COIN;
                    popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                }
            }
            else if (isCanBuyAds && typeStatBuy == E_TypeBuy.ADS)
            {
                ShowAdsRewardsChetsNormal();
            }

           
        }
        else
        {
            base.BUY_CHEST_X1();
            typeStatBuy = E_TypeBuy.PACK;
            popUpChest.EstatBuy = E_TypeBuy.PACK;
            popUpChest.EtypeChest = TypeChest.ChestNormal;
            QuantityNormalChest--;
            DataPlayer.SetQuantityChestNormalPack(QuantityNormalChest);
            popUpChest.gameObject.SetActive(true);
            InitView();
        }
    }
    private void RewadsChestNormal()
    {
        base.BUY_CHEST_X1();
        popUpChest.gameObject.SetActive(true);
        popUpChest.EtypeChest = TypeChest.ChestNormal;
        typeChest = TypeChest.ChestNormal;
        popUpChest.ChestSpine.Skeleton.SetSkin("Skin_blue");
        popUpChest.ChestSpine.LateUpdate();
        countClickAdsChestNormal = DataPlayer.GetCountAdsNormal();
        countClickAdsChestNormal++;
        DataPlayer.SetCountAdsNormal(countClickAdsChestNormal);
        InitView();
        /*if (countClickAdsChestNormal < maxCountClickAdsChestNormal && typeStatBuy == E_TypeBuy.ADS)
        {
            nextTime = System.DateTime.Now.AddSeconds(30);
            DataPlayer.SetTimeOutChestNormal(nextTime);
        }*/
        if (countClickAdsChestNormal == maxCountClickAdsChestNormal && typeStatBuy == E_TypeBuy.ADS)
        {
            nextTime = System.DateTime.Now.AddDays(1);
            DataPlayer.SetTimeOutChestNormal(nextTime);
        }
    }
    private void ShowAdsRewardsChetsNormal()
    {
        AdStatus adstatus = SDKDGManager.Instance.AdsManager.ShowRewardedAdsStatus(() =>
        {
            RewadsChestNormal();
            Controller.Instance.CountTime = 0;
            Debug.Log("Open Chest Normal");
        }, "Open Chest Normal");
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
    public override void BUY_CHEST_X10()
    {
        if (DataPlayer.GetCoin() >= Coin_x10)
        {
            base.BUY_CHEST_X10();
            popUpChest.gameObject.SetActive(true);
            popUpChest.EtypeChest = TypeChest.ChestNormal;
            typeChest = TypeChest.ChestNormal;
            popUpChest.ChestSpine.Skeleton.SetSkin("Skin_blue");
            popUpChest.ChestSpine.LateUpdate();
            InitView();
            SubCurrency(Coin_x10, E_TypeBuy.COIN);
            typeStatBuy = E_TypeBuy.COIN;
            popUpChest.EstatBuy = E_TypeBuy.COIN;
        }
        else
        {
            popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.COIN;
            popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
        }
    }
    public override void InitView()
    {
        base.InitView();
        QuantityNormalChest = DataPlayer.GetQuantityChestNormalPack();
        Coin_x1 = Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestNormal;
        Coin_x10 = (int)((Coin_x1 * 10) * 0.9f);

        Price_x1_txt.text = Coin_x1.ToString();
        Price_x10_txt.text = Coin_x10.ToString();
        if (QuantityNormalChest <= 0)
        {
            NotiObj.SetActive(false);
            if (!isCanBuyAds)
            {
                CoinObj.gameObject.SetActive(true);
                AdsObj.gameObject.SetActive(false);
            }
            else
            {
                AdsObj.gameObject.SetActive(true);
                CoinObj.gameObject.SetActive(false);
            }
        }
        else
        {
            NotiTxt.text = DataPlayer.GetQuantityChestNormalPack().ToString();
            NotiObj.gameObject.SetActive(true);
            AdsObj.gameObject.SetActive(false);
            CoinObj.gameObject.SetActive(false);
        }
    }
    public void RandItem()
    {
        chestRw = Controller.Instance.dataChest.ChestRewardIndex(TypeChest.ChestNormal);
    }
    public void LoadItem()
    {
        popUpChest.ResetContent();
        L_enemy.Clear();
        switch (QuantityBuy)
        {
            case E_QuantityBuy.x1:
                RandItem();
                if (chestRw.typeReward == TypeReward.Coin || chestRw.typeReward == TypeReward.Gem)
                {
                    var obj = Instantiate(itemCurrency);
                    ItemCurrentcy item = obj.GetComponent<ItemCurrentcy>();
                    if (chestRw.typeReward == TypeReward.Coin)
                    {
                        item.icon.sprite = popUpChest.SP_Coin_Card;
                        int coin = chestRw.QuantityOrStar;
                        UI_Home.Instance.m_UICoinManager.SetTextCoin(coin);
                    }
                    else
                    {
                        item.icon.sprite = popUpChest.SP_Gem;
                        int Gem = chestRw.QuantityOrStar;
                        UI_Home.Instance.m_UIGemManager.SetTextGem(Gem);
                    }
                    item.description.text = "x" + chestRw.QuantityOrStar.ToString();
                    popUpChest.ContentList.Add(obj.gameObject);
                    obj.transform.position = popUpChest.ChestSpine.transform.position + new Vector3(0, 140, 0);
                    obj.transform.SetParent(popUpChest.transform);
                    obj.GetComponent<RectTransform>().sizeDelta = new Vector2(140, 170);
                    obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                }
                else
                {
                    var obj = Instantiate(itemCritter);
                    popUpChest.ContentList.Add(obj.gameObject);
                    ItemCritter item = obj.GetComponent<ItemCritter>();

                    for (int i = 0; i < Controller.Instance.enemyData.enemies.Count; i++)
                    {
                        if (Controller.Instance.enemyData.enemies[i].Rarity == chestRw.QuantityOrStar)
                        {
                            L_enemy.Add(Controller.Instance.enemyData.enemies[i]);
                        }
                    }
                    int rand = Random.Range(0, L_enemy.Count);
                    EnemyStat stat = L_enemy[rand];
                    item.characterType = stat.Type;
                    item.Avatar.sprite = stat.Avatar;
                    item.Star_Txt.text = stat.Rarity.ToString();
                    item.Hp_Txt.text = stat.HP.ToString();
                    item.Damage_Txt.text = stat.Damage.ToString();
                    DataPlayer.Add(item.characterType);
                    DataPlayer.AddCritter(item.characterType);
                    Controller.Instance.L_TypeNewUICritter.Add(item.characterType);

                    obj.transform.SetParent(popUpChest.transform);
                    obj.transform.position = popUpChest.ChestSpine.transform.position + new Vector3(0, 140, 0);
                    obj.GetComponent<RectTransform>().sizeDelta = new Vector2(140, 170);
                    obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

                }
                popUpChest.ContentList[0].transform.DOMove(popUpChest.PosActiveFlyCards[2].transform.position + new Vector3(0, -80, 0), 0.2f).OnStart(() =>
                {
                    popUpChest.ContentList[0].transform.DOScale(1.5f, 0.3f).OnStart(() =>
                    {
                    //    AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectReward);
                    }).OnComplete(() =>
                    {
                        popUpChest.ActiveButton();
                    });
                }).SetEase(curveAnimMove);
                break;
            case E_QuantityBuy.x10:
                for (int i = 0; i < 10; i++)
                {
                    RandItem();
                    if (chestRw.typeReward == TypeReward.Coin || chestRw.typeReward == TypeReward.Gem)
                    {
                        var obj = Instantiate(itemCurrency);
                        ItemCurrentcy item = obj.GetComponent<ItemCurrentcy>();
                        item.transform.SetParent(popUpChest.Content.transform);
                        item.transform.localScale = Vector3.zero;
                        if (chestRw.typeReward == TypeReward.Coin)
                        {
                            item.icon.sprite = popUpChest.SP_Coin_Card;
                            int coin = chestRw.QuantityOrStar;
                            UI_Home.Instance.m_UICoinManager.SetTextCoin(coin);
                        }
                        else
                        {
                            item.icon.sprite = popUpChest.SP_Gem;
                            int Gem = chestRw.QuantityOrStar;
                            UI_Home.Instance.m_UIGemManager.SetTextGem(Gem);
                        }
                        item.description.text = "x" + chestRw.QuantityOrStar.ToString();
                        popUpChest.ContentList.Add(obj.gameObject);
                    }
                    else
                    {
                        var obj = Instantiate(itemCritter);
                        popUpChest.ContentList.Add(obj.gameObject);
                        ItemCritter item = obj.GetComponent<ItemCritter>();
                        item.transform.SetParent(popUpChest.Content.transform);
                        item.transform.localScale = Vector3.zero;

                        for (int j = 0; j < Controller.Instance.enemyData.enemies.Count; j++)
                        {
                            if (Controller.Instance.enemyData.enemies[j].Rarity == chestRw.QuantityOrStar)
                            {
                                L_enemy.Add(Controller.Instance.enemyData.enemies[j]);
                            }
                        }
                        int rand = Random.Range(0, L_enemy.Count);
                        EnemyStat stat = L_enemy[rand];
                        item.characterType = stat.Type;
                        item.Avatar.sprite = stat.Avatar;
                        item.Star_Txt.text = stat.Rarity.ToString();
                        item.Hp_Txt.text = stat.HP.ToString();
                        item.Damage_Txt.text = stat.Damage.ToString();
                        DataPlayer.Add(item.characterType);
                        DataPlayer.AddCritter(item.characterType);
                        Controller.Instance.L_TypeNewUICritter.Add(item.characterType);

                    }
                }
                break;
        }
        if (popUpChest.ContentList.Count > 1)
        {
            StartCoroutine(IE_delayFlyCard(0));
        }
    }
    IEnumerator IE_delayFlyCard(int index)
    {
        if (index < popUpChest.ContentList.Count)
        {
            var obj = Instantiate(popUpChest.ContentList[index].gameObject);
            obj.transform.position = popUpChest.ChestSpine.transform.position + new Vector3(0, 140, 0);
            obj.transform.SetParent(popUpChest.transform);
            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(140, 170);
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            obj.transform.DOMove(popUpChest.PosActiveFlyCards[index].transform.position, 0.4f).OnStart(() =>
            {
             //   AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectReward);
                obj.transform.DOScale(1, 0.3f);
            }).SetEase(curveAnimMove);
            yield return new WaitForSeconds(0.05f);
            index++;
            StartCoroutine(IE_delayFlyCard(index));
            popUpChest.ItemCloneList.Add(obj.gameObject);
            if(index + 1 >= popUpChest.ContentList.Count)
            {
                popUpChest.ActiveButton();
            }
        }

    }
    public override void SubCurrency(int value, E_TypeBuy _typeStatBuy)
    {
        base.SubCurrency(value, _typeStatBuy);
    }
}
