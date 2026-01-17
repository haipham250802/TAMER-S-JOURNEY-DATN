using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Dragon.SDK;
public class AllidsBaseShow : MonoBehaviour
{
    public ECharacterType Type;
    public int ID;
    public int HP;
    public int Damage;
    public int Rarity;
    public int CoinHealing;
    public int SLotIndex;

    public Button PurchaseBtn;
    public Button PurchaseBtnAds;

    public Text TxtHP;
    public Text TxtDamage;
    public Text TxtCoinHealing;
    public SkeletonGraphic skeleton;

    public Slider HP_Bar;
    public Button PurChaseBtn;

    public ElementData ThisElementData;
    public int MaxHP;

    public int CoinStart;

    int Lowest_HP = 0;
    int Low_HP = 0;
    int Medium_HP = 0;
    int High_HP = 0;

    public GameObject EffectBuff;
    private void Awake()
    {
        PurchaseBtnAds.onClick.AddListener(ShowAdsReward);
    }

    private void Start()
    {

    }

    private void Update()
    {

    }

    public void Init(ElementData elemendata = null)
    {
        if (elemendata != null)
        {
            ID = elemendata.ID;
            Type = elemendata.Type;
            ThisElementData = elemendata;
            EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
            HP = elemendata.HP;
            HP_Bar.value = HP;
            Damage = Controller.Instance.enemyData.GetDamageEnemy(Type);
            Rarity = stat.Rarity;
            MaxHP = Controller.Instance.enemyData.GetHPEmemy(Type);
            HP_Bar.maxValue = MaxHP;

            skeleton.skeletonDataAsset = null;
            skeleton.skeletonDataAsset = stat.ICON;
            skeleton.Initialize(true);

            skeleton.AnimationState.SetAnimation(0, "Idle", true);
            //  ICON = stat.ICON;
            //  Avatar.sprite = stat.Avatar;
        }
        else
        {
            EnemyStat stat = Controller.Instance.GetStatEnemy(Type);
            HP = Controller.Instance.enemyData.GetHPEmemy(Type);
            Damage = Controller.Instance.enemyData.GetDamageEnemy(Type);
            Rarity = stat.Rarity;
        }
        SetView();
    }
    public void SetView()
    {
        if (ThisElementData != null)
            TxtHP.text = ThisElementData.HP.ToString();

        else TxtHP.text = HP.ToString();
        TxtDamage.text = Damage.ToString();
    }
    public void SubCoin()
    {
        if (DataPlayer.GetCoin() >= CoinHealing)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.SoundEffecrHealing);
            UI_Home.Instance.m_UICoinManager.Subcoin(CoinHealing);
            TxtCoinHealing.text = 0.ToString();
            HP = MaxHP;
            TxtHP.text = HP.ToString();
            HP_Bar.value = HP;
            EffectBuff.SetActive(true);
            for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
            {
                if (Type == DataPlayer.GetListAllid()[i].Type && ID == DataPlayer.GetListAllid()[i].ID)
                {
                    DataPlayer.SetHP(HP, DataPlayer.GetListAllid()[i]);
                    break;
                }
            }
            if (HP == MaxHP)
            {
                PurchaseBtn.gameObject.SetActive(false);
                PurchaseBtnAds.gameObject.SetActive(false);
            }
        }
        else
        {
            popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.COIN;
            popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
            popUpManager.Instance.m_PopUpNotmoney.isCoin = true;
        }
    }
    public void ShowAdsReward()
    {
        AdStatus adstatus = SDKDGManager.Instance.AdsManager.ShowRewardedAdsStatus(() =>
        {
            HealHPAds();
            Controller.Instance.CountTime = 0;
            Debug.Log("Healing Critter At CheckPoint");
        }, "Healing Critter At CheckPoint");
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
    public void HealHPAds()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.SoundEffecrHealing);
        for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
        {
            if (Type == DataPlayer.GetListAllid()[i].Type && ID == DataPlayer.GetListAllid()[i].ID)
            {
                HP = MaxHP;
                TxtHP.text = HP.ToString();
                HP_Bar.value = HP;
                EffectBuff.SetActive(true);
                DataPlayer.SetHP(HP, DataPlayer.GetListAllid()[i]);
                break;
            }
        }
        if (HP == MaxHP)
        {
            PurchaseBtnAds.transform.GetChild(0).transform.localScale = Vector3.one;
            PurchaseBtnAds.gameObject.SetActive(false);
            PurchaseBtn.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(IEdelay());
    }
    IEnumerator IEdelay()
    {
        yield return null;
        EffectBuff.SetActive(false);
        EffectBuff.SetActive(false);
        CoinStart = DataPlayer.GetCoin();

        if (Type != ECharacterType.NONE)
        {
            EnemyStat enemyStat = Controller.Instance.GetStatEnemy(Type);
            MaxHP = Controller.Instance.enemyData.GetHPEmemy(Type);
        }
        Lowest_HP = (MaxHP * 0 / 100);
        Low_HP = (MaxHP * 25 / 100);
        Medium_HP = (MaxHP * 50 / 100);
        High_HP = (MaxHP * 75 / 100);

        HP_Bar.maxValue = MaxHP;
        PurChaseBtn.gameObject.SetActive(false);
        if (HP < MaxHP)
        {
            PurChaseBtn.gameObject.SetActive(true);
            PurchaseBtnAds.gameObject.SetActive(true);
            for (int i = 0; i < Controller.Instance.healingData.E_Blood.Count; i++)
            {
                if (Rarity - 1 == i)
                {
                    if (HP >= Lowest_HP && HP < Low_HP)
                    {
                        CoinHealing = Controller.Instance.healingData.E_Blood[i].Cost_HP_Lowest;
                        Debug.LogError("Healing: " + CoinHealing);
                        TxtCoinHealing.text = CoinHealing.ToString();
                        break;
                    }
                    else if (HP >= Low_HP && HP < Medium_HP)
                    {
                        CoinHealing = Controller.Instance.healingData.E_Blood[i].Cost_HP_Low;
                        TxtCoinHealing.text = CoinHealing.ToString();
                    }
                    else if (HP >= Medium_HP && HP < High_HP)
                    {
                        CoinHealing = Controller.Instance.healingData.E_Blood[i].Cost_HP_Medium;
                        TxtCoinHealing.text = CoinHealing.ToString();
                        break;
                    }
                    else if (HP >= High_HP && HP < MaxHP)
                    {
                        CoinHealing = Controller.Instance.healingData.E_Blood[i].Cost_HP_Hight;
                        TxtCoinHealing.text = CoinHealing.ToString();
                        break;
                    }
                    else
                        TxtCoinHealing.text = 0.ToString();
                    break;
                }
            }
        }
        else
        {
            PurChaseBtn.gameObject.SetActive(false);
            PurchaseBtnAds.gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        Lowest_HP = 0;
        Low_HP = 0;
        Medium_HP = 0;
        High_HP = 0;
        CoinHealing = 0;
    }
}
