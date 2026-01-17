using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Dragon.SDK;

public class PopUpPackOnline : MonoBehaviour
{
    public List<PackLoginElement> LoginElements = new List<PackLoginElement>();

    public int CurCountDay;

    public GameObject NotiBar;
    public Text TimeCoolDownTxt;
    public Text CostTxt;

    public Sprite BG_LockCountDays;
    public Sprite BG_UnLockCountDays;

    public DateTime nextTime;
    public TimeSpan timeSpan;

    public Button PurchaseButtonUnLockGem;
    public Button ExitButton;

    public float CountDay;
    public GameObject objNotUnlock;
    public GameObject objUnlocked;
    public GameObject Content;
    public GameObject Slash;

    public Currentcy_Fly m_CurrencyFly;
    public Transform PosEndCoin;
    //public Transform PosActiveCoin;

    private void Awake()
    {
        InitPackLoginElement();
        PurchaseButtonUnLockGem.onClick.AddListener(Purchase);
        ExitButton.onClick.AddListener(OnClickButtonExit);
        CostTxt.text = SDKDGManager.Instance.IAPManager.GetPrice(ProduckID.PACK_ONLINE_VIP);
    }
    private void OnEnable()
    {
       
        StartCoroutine(IE_delay());
        AudioManager.Instance.PlaySound(AudioManager.instance.SoundEffectWosh);
    }
    IEnumerator IE_delay()
    {
        yield return null;
        if (DataPlayer.GetUnLockGemPackLogin())
        {
            objUnlocked.SetActive(true);
            objNotUnlock.SetActive(false);
            Slash.SetActive(false);

        }
        else
        {
            objUnlocked.SetActive(false);
            objNotUnlock.SetActive(true);
            Slash.SetActive(true);

        }
        Content.gameObject.transform.localPosition = new Vector3(Content.transform.localPosition.x, 0, 0);
        player.Instance.GetComponent<Collider2D>().enabled = false;
    }
    void CheckUnLockGem()
    {
        if (DataPlayer.GetUnLockGemPackLogin())
        {
            for (int i = 0; i < LoginElements.Count; i++)
            {
                if (LoginElements[i].LockGemImage)
                {
                    LoginElements[i].LockGemImage.gameObject.SetActive(false);
                }
            }
        }
    }
    private void Start()
    {
        nextTime = DataPlayer.GetTimePackLogin();
        CurCountDay = DataPlayer.GetCountDayPackLogin();
        CheckUnLockGem();

        if (CurCountDay >= 29)
        {
            Destroy(NotiBar);
        }
        if (CurCountDay >= 0 && CurCountDay <= 29)
        {
            NotiBar.transform.localPosition = Vector3.zero;
            NotiBar.transform.SetParent(LoginElements[CurCountDay].transform);
            NotiBar.transform.localPosition = new Vector3(0, -85, 0);
        }

        if (CurCountDay == 0)
        {
            LoginElements[0].BG_LoginCountImage.sprite = BG_UnLockCountDays;
            if (!DataPlayer.GetListDoneGemFree().Contains(LoginElements[0].CountDays))
            {
                LoginElements[0].PurchaseTakeGemFree.interactable = true;
                LoginElements[0].NotiClaim.SetActive(true);
            }
            else
            {
                LoginElements[0].PurchaseTakeGemFree.interactable = false;
                LoginElements[0].NotiClaim.SetActive(false);

            }
            if (!DataPlayer.GetListDoneGemNoFree().Contains(LoginElements[0].CountDays))
            {
                LoginElements[0].PurchaseButtonGemNoFree.interactable = true;
                if (DataPlayer.GetUnLockGemPackLogin())
                {
                    LoginElements[0].NotiClaimNoFree.SetActive(true);
                }
            }
            else
            {
                LoginElements[0].PurchaseButtonGemNoFree.interactable = false;
                if (DataPlayer.GetUnLockGemPackLogin())
                {
                    LoginElements[0].NotiClaimNoFree.SetActive(false);
                }
            }
        }
        if (CurCountDay > 0)
        {
            for (int i = 0; i <= CurCountDay; i++)
            {
                LoginElements[i].BG_LoginCountImage.sprite = BG_UnLockCountDays;
                if (!DataPlayer.GetListDoneGemFree().Contains(LoginElements[i].CountDays))
                {
                    LoginElements[i].PurchaseTakeGemFree.interactable = true;
                    LoginElements[i].NotiClaim.SetActive(true);
                }
                else
                {
                    LoginElements[i].NotiClaim.SetActive(false);
                    LoginElements[i].PurchaseTakeGemFree.interactable = false;
                }
                if (!DataPlayer.GetListDoneGemNoFree().Contains(LoginElements[i].CountDays))
                {
                    LoginElements[i].PurchaseButtonGemNoFree.interactable = true;
                    if (DataPlayer.GetUnLockGemPackLogin())
                    {
                        LoginElements[i].NotiClaimNoFree.SetActive(true);
                    }
                }
                else
                {
                    LoginElements[i].NotiClaimNoFree.SetActive(false);
                    LoginElements[i].PurchaseButtonGemNoFree.interactable = false;
                }
            }
        }
        StartCoroutine(IE_DelayLoad());
    }
    IEnumerator IE_DelayLoad()
    {
        yield return new WaitForSeconds(0.3f);
        LoadView();
    }
    private void Update()
    {
        timeSpan = nextTime - DateTime.Now;
        CurCountDay = DataPlayer.GetCountDayPackLogin();
        if (timeSpan.Ticks <= 0 && CurCountDay <= 29)
        {
            CurCountDay++;
            if (CurCountDay == 29)
            {
                Destroy(NotiBar);
            }
            DataPlayer.SetCountDayPackLogin(CurCountDay);
            if (NotiBar)
            {
                NotiBar.transform.localPosition = Vector3.zero;
                NotiBar.transform.SetParent(LoginElements[CurCountDay].transform);
                NotiBar.transform.localPosition = new Vector3(0, -85, 0);
            }
            if (CurCountDay == 0)
            {
                LoginElements[0].BG_LoginCountImage.sprite = BG_UnLockCountDays;
                if (!DataPlayer.GetListDoneGemFree().Contains(LoginElements[0].CountDays))
                {
                    LoginElements[0].PurchaseTakeGemFree.interactable = true;
                    LoginElements[0].NotiClaim.SetActive(true);
                }
                else
                {
                    LoginElements[0].PurchaseTakeGemFree.interactable = false;
                    LoginElements[0].NotiClaim.SetActive(false);

                }
                if (!DataPlayer.GetListDoneGemNoFree().Contains(LoginElements[0].CountDays))
                {
                    LoginElements[0].PurchaseButtonGemNoFree.interactable = true;
                    if (DataPlayer.GetUnLockGemPackLogin())
                    {
                        LoginElements[0].NotiClaimNoFree.SetActive(true);
                    }
                }
                else
                {
                    LoginElements[0].PurchaseButtonGemNoFree.interactable = false;
                    if (DataPlayer.GetUnLockGemPackLogin())
                    {
                        LoginElements[0].NotiClaimNoFree.SetActive(false);
                    }
                }
            }

            if (CurCountDay > 0 && CurCountDay <= 29)
            {
                for (int i = 0; i <= CurCountDay; i++)
                {
                    LoginElements[i].BG_LoginCountImage.sprite = BG_UnLockCountDays;
                    if (!DataPlayer.GetListDoneGemFree().Contains(LoginElements[i].CountDays))
                    {
                        LoginElements[i].PurchaseTakeGemFree.interactable = true;
                        LoginElements[i].NotiClaim.SetActive(true);

                    }
                    else
                    {
                        LoginElements[i].PurchaseTakeGemFree.interactable = false;
                        LoginElements[i].NotiClaim.SetActive(false);

                    }
                    if (!DataPlayer.GetListDoneGemNoFree().Contains(LoginElements[i].CountDays))
                    {
                        LoginElements[i].PurchaseButtonGemNoFree.interactable = true;
                        if (DataPlayer.GetUnLockGemPackLogin())
                        {
                            LoginElements[i].NotiClaimNoFree.SetActive(true);
                        }
                    }
                    else
                    {
                        LoginElements[i].PurchaseButtonGemNoFree.interactable = false;
                        if (DataPlayer.GetUnLockGemPackLogin())
                        {
                            LoginElements[i].NotiClaimNoFree.SetActive(false);
                        }
                    }
                }
            }

            nextTime = DateTime.Now.AddDays(CountDay);
            DataPlayer.SetTimePackLogin(nextTime);
            if (TimeCoolDownTxt)
                TimeCoolDownTxt.text = "0h00m";
            Debug.Log("timeSpan: " + timeSpan.Ticks);
        }
        else if (timeSpan.Ticks > 0 && CurCountDay <= 29)
        {
            if (TimeCoolDownTxt)
                TimeCoolDownTxt.text = DateTimeHelper.TimeToString_HM(timeSpan);
        }
    }
    public void InitPackLoginElement()
    {
        for (int i = 0; i < LoginElements.Count; i++)
        {
            LoginElements[i].TypeDay = (E_LogInDays)i + 1;
            LoginElements[i].CountDays = i + 1;
            LoginElements[i].PurchaseTakeGemFree.interactable = false;
            LoginElements[i].PurchaseButtonGemNoFree.interactable = false;
            LoginElements[i].LockGemImage.gameObject.SetActive(true);
            LoginElements[i].BG_LoginCountImage.sprite = BG_LockCountDays;
            LoginElements[i].NotiClaim.gameObject.SetActive(false);
            LoginElements[i].NotiClaimNoFree.gameObject.SetActive(false);
        }
    }
    public void LoadView()
    {
        for (int i = 0; i < LoginElements.Count; i++)
        {
            for (int j = 0; j < DataPlayer.GetListDoneGemFree().Count; j++)
            {
                if (LoginElements[i].CountDays == DataPlayer.GetListDoneGemFree()[j])
                {
                    LoginElements[i].CheckDoneFreeGem.gameObject.SetActive(true);
                    LoginElements[i].NotiClaim.SetActive(false);
                    LoginElements[i].PurchaseTakeGemFree.interactable = false;
                }
            }
            for (int k = 0; k < DataPlayer.GetListDoneGemNoFree().Count; k++)
            {
                if (LoginElements[i].CountDays == DataPlayer.GetListDoneGemNoFree()[k])
                {
                    LoginElements[i].CheckDoneNoFreeGem.gameObject.SetActive(true);
                    LoginElements[i].NotiClaimNoFree.SetActive(false);

                    LoginElements[i].PurchaseButtonGemNoFree.interactable = false;
                }
            }
        }
        if (DataPlayer.GetUnLockGemPackLogin())
        {
            PurchaseButtonUnLockGem.interactable = false;
        }
    }
    public void Purchase()
    {
        SDKDGManager.Instance.IAPManager.Purchase(ProduckID.PACK_ONLINE_VIP, () =>
        {
            // BuyGem();
            Debug.Log(ProduckID.PACK_ONLINE_VIP + "$");
            Debug.Log("Pruchase Success Update UI");
            OnClickPurchaseUnLockGem();
        });

    }
    void OnClickPurchaseUnLockGem()
    {
        Slash.SetActive(false);
        CurCountDay = DataPlayer.GetCountDayPackLogin();
        if (!DataPlayer.GetUnLockGemPackLogin())
        {
            DataPlayer.SetUnLockGemPackLogin(true);

            for (int i = 0; i < LoginElements.Count; i++)
            {
                if (LoginElements[i].LockGemImage)
                {
                    LoginElements[i].LockGemImage.gameObject.SetActive(false);
                }
                if (!DataPlayer.GetListDoneGemNoFree().Contains(LoginElements[i].CountDays) && i <= CurCountDay)
                {
                    LoginElements[i].PurchaseButtonGemNoFree.interactable = true;
                    if (DataPlayer.GetUnLockGemPackLogin())
                    {
                        LoginElements[i].NotiClaimNoFree.SetActive(true);
                    }
                }
            }

            objUnlocked.SetActive(true);
            objNotUnlock.SetActive(false);

        }
        else
        {
            PurchaseButtonUnLockGem.interactable = false;
        }
    }
    void OnClickButtonExit()
    {
        gameObject.SetActive(false);
    }
}
