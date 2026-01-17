using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NotifiController : Singleton<NotifiController>
{
    public Action<bool> NotiWheel;
    public Action<bool> NotiPackOnline;
    public Action<bool> NotiPackZone;
    public Action<bool> NotiBag;
    public Action<bool> NotiShop;
    public Action<bool> NotiMergebtn;
    public Action<bool> NotiTeamBtn;

    // PickerWheel
    public DateTime TimeAds;
    public DateTime TimeFree;
    public DateTime TimeCoolNextDay;
    public TimeSpan timcoolfree;
    public TimeSpan timcoolfreeWithAds;
    public TimeSpan timeCoolNextDay;
    //

    //PackOnine
    public TimeSpan timeSpan;
    public DateTime nextTime;
    public int CurCountDay;
    //

    //PackZone
    public int Curzone;
    //

    private void OnEnable()
    {
        // NotiWheel?.Invoke(false);
    }
    private void Start()
    {
        StartLuckyWheel();
        StartPackOnline();
        StartPackZone();
    }
    private void Update()
    {
        CheckLuckyWheel();
        CheckPackOnline();
        CheckPackZone();
        CheckBagManager();
        CheckShop();
        CheckMergeButton();
        CheckTeamButton();
    }
    void StartPackZone()
    {
        Curzone = DataPlayer.GetCurZonepackZone();
    }
    void StartLuckyWheel()
    {
        TimeFree = DataPlayer.GetTimeFreePickerWheel();
        TimeAds = DataPlayer.GetTimeFreeWithAdsPickerWheel();
        TimeCoolNextDay = DataPlayer.GetTimeFreeNextDayPickerWheel();
    }

    TimeSpan timeChestNormal;
    TimeSpan timeChestEpic;
    TimeSpan timeChestLegend;
    void CheckTeamButton()
    {
        if (UI_Home.Instance.m_UIMerge.L_newElement.Count > 0)
        {
            NotiTeamBtn?.Invoke(true);
        }
        else
        {
            NotiTeamBtn?.Invoke(false);
        }
    }
    void CheckMergeButton()
    {
        if (UI_Home.Instance.m_UIMerge.L_newElement.Count > 0)
        {
            NotiMergebtn?.Invoke(true);
        }
        else
        {
            NotiMergebtn?.Invoke(false);
        }
    }
    void CheckShop()
    {
        timeChestNormal = DataPlayer.GetTimeOutChestNormal() - DateTime.Now;
        timeChestEpic = DataPlayer.GetTimeOutChestEpic() - DateTime.Now;
        timeChestLegend = DataPlayer.GetTimeOutChestLegend() - DateTime.Now;
        if (DataPlayer.GetQuantityChestEpicPack() > 0 ||
            DataPlayer.GetQuantityChestNormalPack() > 0 ||
            DataPlayer.GetQuantityChestLegendPack() > 0 ||
            timeChestNormal.Ticks <= 0 || timeChestEpic.Ticks <= 0 || timeChestLegend.Ticks <= 0)
        {
            NotiShop?.Invoke(true);
        }
        else
        {
            NotiShop?.Invoke(false);
        }
    }
    void CheckBagManager()
    {
        if (DataPlayer.GetQuantityChestEpicPack() > 0 ||
            DataPlayer.GetQuantityChestNormalPack() > 0 ||
            DataPlayer.GetQuantityChestLegendPack() > 0 ||
                UI_Home.Instance.m_UIMerge.L_newElement.Count > 0 ||
                timeChestNormal.Ticks <= 0 || timeChestEpic.Ticks <= 0 || timeChestLegend.Ticks <= 0)
        {
            NotiBag?.Invoke(true);
        }
        else
        {
            NotiBag?.Invoke(false);
        }
    }
    void CheckLuckyWheel()
    {
        TimeFree = DataPlayer.GetTimeFreePickerWheel();
        TimeAds = DataPlayer.GetTimeFreeWithAdsPickerWheel();
        TimeCoolNextDay = DataPlayer.GetTimeFreeNextDayPickerWheel();
        timcoolfree = TimeFree - DateTime.Now;
        timcoolfreeWithAds = TimeAds - DateTime.Now;
        timeCoolNextDay = TimeCoolNextDay - DateTime.Now;

        if (timcoolfree.Ticks < 0 || timcoolfreeWithAds.Ticks < 0)
        {
            NotiWheel?.Invoke(true);
        }
        else
        {
            NotiWheel?.Invoke(false);
        }
    }
    void CheckPackZone()
    {
        Curzone = DataPlayer.GetCurZonepackZone();
        if (Curzone < DataPlayer.getCurLevel())
        {
            Curzone = DataPlayer.getCurLevel();
            DataPlayer.SetCurZonePackZone(Curzone);
        }
        else
        {
            if (DataPlayer.GetListDoneGemFreePackZone().Count < Curzone)
            {
                NotiPackZone?.Invoke(true);
            }
            else
            {
                NotiPackZone?.Invoke(false);
            }
            if (DataPlayer.GetUnLockRewardNoFreePackZone())
            {
                if (DataPlayer.GetListDoneGemNoFree1PackZone().Count < Curzone || DataPlayer.GetListDoneGemNoFree2PackZone().Count < Curzone)
                {
                    NotiPackZone?.Invoke(true);
                }
            }

            if (DataPlayer.GetListDoneGemFreePackZone().Count >= Curzone && DataPlayer.GetListDoneGemNoFree1PackZone().Count >= Curzone && DataPlayer.GetListDoneGemNoFree2PackZone().Count >= Curzone)
            {
                NotiPackZone?.Invoke(false);
            }
        }
    }
    void StartPackOnline()
    {
        nextTime = DataPlayer.GetTimePackLogin();
        CurCountDay = DataPlayer.GetCountDayPackLogin();
    }
    void CheckPackOnline()
    {
        nextTime = DataPlayer.GetTimePackLogin();
        timeSpan = nextTime - DateTime.Now;
        CurCountDay = DataPlayer.GetCountDayPackLogin();
        if (timeSpan.Ticks <= 0 && CurCountDay <= 29)
        {
            NotiPackOnline?.Invoke(true);
        }
        if (timeSpan.Ticks > 0)
        {
            if (CurCountDay >= 0 && CurCountDay <= 29)
            {
                for (int i = 0; i <= CurCountDay; i++)
                {
                    if (!DataPlayer.GetListDoneGemFree()
                        .Contains(popUpManager.Instance.m_PopUPpackOnline.LoginElements[i].CountDays)
                        )
                    {
                        NotiPackOnline?.Invoke(true);
                        break;
                    }
                    else
                    {
                        NotiPackOnline.Invoke(false);
                    }
                }

                for (int i = 0; i <= CurCountDay; i++)
                {
                    if (DataPlayer.GetUnLockGemPackLogin())
                    {
                        if (!DataPlayer.GetListDoneGemNoFree()
                            .Contains(popUpManager.Instance.m_PopUPpackOnline.LoginElements[i].CountDays))
                        {
                            NotiPackOnline?.Invoke(true);
                            break;
                        }
                        else
                        {
                            NotiPackOnline.Invoke(false);
                        }
                    }

                }
            }
            else
            {
                NotiPackOnline?.Invoke(false);
            }
        }

    }
    void CheckPacKUnlockZone()
    {

    }
}
