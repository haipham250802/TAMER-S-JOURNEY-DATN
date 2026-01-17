using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System;
using Dragon.SDK;
public class PickerWheelManager : Singleton<PickerWheelManager>
{
    public Transform _wheel;
    public const float TIME_ROTATE = 7;
    public int MAX_REWARD = 8;
    public List<PieceElement> ListWheelPieces = new List<PieceElement>(8);

    public DateTime TimeAds;
    public DateTime TimeFree;
    public DateTime TimeCoolNextDay;

    public TimeSpan timcoolfree;
    public TimeSpan timcoolfreeWithAds;
    public TimeSpan timeCoolNextDay;

    public Button PurchaseButtonFree;
    public Button PurchaseButtonFreeWithAds;
    public Button TapToExitBtn;

    public Image BG_PurchaseButtonFree;
    public Image BG_PurchaseButtonfreeWithAds;

    public Text TimeCoolDownFreeTxt;
    public Text TimeCoolDownFreeWithAdsTxt;

    public int TimeCoolAdsWithMinus;
    public int TimeCoolWithHours;

    public int CountSpinFree;

    public GameObject FreeObj;
    public GameObject GemObj;

    public float AngleItem01;
    public float AngleItem02;
    public float AngleItem03;
    public float AngleItem04;
    public float AngleItem05;
    public float AngleItem06;
    public float AngleItem07;
    public float AngleItem08;

    public bool Spinning;
    private void Awake()
    {
        PurchaseButtonFree.onClick.AddListener(OnCLickPurchaseButtonFree);
        PurchaseButtonFreeWithAds.onClick.AddListener(ShowAdsReward);
        TapToExitBtn.onClick.AddListener(onCLickTapToSkip);
    }
    private void OnEnable()
    {
        player.Instance.gameObject.GetComponent<Collider2D>().enabled = false;
        BagManager.Instance.Setting_Button.gameObject.SetActive(false);
        AudioManager.Instance.PlaySound(AudioManager.instance.SoundEffectWosh);
    }

    private void Start()
    {
        TimeFree = DataPlayer.GetTimeFreePickerWheel();
        TimeAds = DataPlayer.GetTimeFreeWithAdsPickerWheel();
        TimeCoolNextDay = DataPlayer.GetTimeFreeNextDayPickerWheel();
    }
    private void Update()
    {
        timcoolfree = TimeFree - DateTime.Now;
        timcoolfreeWithAds = TimeAds - DateTime.Now;
        timeCoolNextDay = TimeCoolNextDay - DateTime.Now;

        if (timcoolfree.Ticks > 0)
        {
            TimeCoolDownFreeTxt.text = DateTimeHelper.TimeToString_HMS(timcoolfree);
            FreeObj.SetActive(false);
            GemObj.SetActive(true);
        }
        else
        {
            TimeCoolDownFreeTxt.text = "0m:00s";
            FreeObj.SetActive(true);
            GemObj.SetActive(false);

        }
        if (timcoolfreeWithAds.Ticks > 0)
        {
            TimeCoolDownFreeWithAdsTxt.text = DateTimeHelper.TimeToString_HMS(timcoolfreeWithAds);
        }
        else
        {
            TimeCoolDownFreeWithAdsTxt.text = "0m:00s";
        }
        if (timeCoolNextDay.Ticks <= 0)
        {
            CountSpinFree = 0;
            DataPlayer.SetCountSpinFree(CountSpinFree);
        }
        if (!Spinning)
        {
            if (timcoolfree.Ticks <= 0 && CountSpinFree < 3)
            {
                FreeObj.SetActive(true);
                GemObj.SetActive(false);
            }
            if (timcoolfreeWithAds.Ticks <= 0)
            {
                PurchaseButtonFreeWithAds.interactable = true;
                BG_PurchaseButtonfreeWithAds.color = Color.white;
            }
        }
        if (Spinning)
        {
            BG_PurchaseButtonfreeWithAds.color = Color.gray;
            BG_PurchaseButtonFree.color = Color.gray;
        }
    }
    private void OnCLickPurchaseButtonFree()
    {
        if (!Spinning)
        {
            FreeObj.SetActive(false);
            GemObj.SetActive(true);
            if (timeCoolNextDay.Ticks <= 0)
            {
                TimeCoolNextDay = DateTime.Now.AddMinutes(1);
                DataPlayer.SetTimeNextDayPickerWheel(TimeCoolNextDay);
            }
            if (timcoolfree.Ticks <= 0 && CountSpinFree < 3)
            {
                CountSpinFree++;
                DataPlayer.SetCountSpinFree(CountSpinFree);
                TimeFree = DateTime.Now.AddHours(8);
                DataPlayer.SetTimeFreePickerWheel(TimeFree);
                SpinFree();
            }
            else
            {
                if (DataPlayer.GetGem() >= 10)
                {
                    UI_Home.Instance.m_UIGemManager.SubGem(10);
                    SpinFree();
                }
                else
                {
                    popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.GEM;
                    popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                }
            }
        }
    }
    private void ShowAdsReward()
    {
        if (!Spinning)
        {
            AdStatus adstatus = SDKDGManager.Instance.AdsManager.ShowRewardedAdsStatus(() =>
            {
                Rewards();
                Controller.Instance.CountTime = 0;
            }, "Spine Wheel");
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
    }
    private void Rewards()
    {
        SpinFree();
        TimeAds = DateTime.Now.AddMinutes(1);
        DataPlayer.SetTimeFreeWithAdsPickerWheel(TimeAds);
        PurchaseButtonFreeWithAds.interactable = false;
        BG_PurchaseButtonfreeWithAds.color = Color.gray;
    }
    int _indexReward;
    Vector3 angleTarget;
    public AnimationCurve Curve;
    private void SpinFree()
    {
        Spinning = true;
        _indexReward = GetRandomRewards(ListWheelPieces);
        angleTarget = GetAngle(_indexReward, MAX_REWARD);
        _wheel.DOLocalRotate(angleTarget, TIME_ROTATE)
            .OnStart(onStartFree)
              .SetEase(Curve)
              .OnComplete(OnCompleteFree);

        Debug.LogError(ListWheelPieces[_indexReward].ToString());
    }
    private Vector3 GetAngle(int indexReward, int loop)
    {
        return new Vector3(0, 0, -360 * loop + (indexReward) * (360 / MAX_REWARD));
    }
    private int GetRandomRewards(List<PieceElement> L_WheelPieces)
    {
        Dictionary<int, float> DicRandomRewardsSpin = new Dictionary<int, float>();
        for (int i = 0; i < L_WheelPieces.Count; i++)
        {
            if (!DicRandomRewardsSpin.ContainsKey(L_WheelPieces[i].ID))
            {
                DicRandomRewardsSpin.Add(L_WheelPieces[i].ID, (float)L_WheelPieces[i].ratechance);
            }
        }
        int Randone = Catch_Chance_Controller.GetRandomByPercent<int>(DicRandomRewardsSpin);
        return Randone;
    }
    private void onStartFree()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectLuckyWheel);
        TapToExitBtn.gameObject.SetActive(false);
    }
    private void OnCompleteFree()
    {
        TapToExitBtn.gameObject.SetActive(true);

        Spinning = false;
        BG_PurchaseButtonFree.color = Color.white;
        PopUpRewardSpin m_pop = popUpManager.Instance.m_PopUpRewardSpin;
        m_pop.Icon.sprite = ListWheelPieces[_indexReward].Icon.sprite;
        m_pop.QuantityTxt.text = "x" + ListWheelPieces[_indexReward].Quantity.ToString();
        m_pop.gameObject.SetActive(true);
        switch (ListWheelPieces[_indexReward].typeRewards)
        {
            case TypeRewardPickerWheel.Coin:
                UI_Home.Instance.m_UICoinManager.SetTextCoin(ListWheelPieces[_indexReward].Quantity);
                break;
            case TypeRewardPickerWheel.Gem:
                UI_Home.Instance.m_UIGemManager.SetTextGem(ListWheelPieces[_indexReward].Quantity);
                break;
            case TypeRewardPickerWheel.Chest_Epic:
                int num = DataPlayer.GetQuantityChestEpicPack();
                num++;
                DataPlayer.SetQuantityChestEpicPack(num);
                break;
        }
    }
    private void OnDisable()
    {
        BagManager.Instance.Setting_Button.gameObject.SetActive(true);
    }
    private void onCLickTapToSkip()
    {
        gameObject.SetActive(false);
    }
#if UNITY_EDITOR
    [Button("Render")]
    void Reder()
    {
    }
#endif
}
