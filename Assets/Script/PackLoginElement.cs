using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class PackLoginElement : MonoBehaviour
{
    public E_LogInDays TypeDay;
    public int CountDays;

    public Text CountDaysTxt;
    public Text RewardsGemFreeTxt;
    public Text RewardsGemNoFreeTxt;

    public int RewardsGemFree;
    public int RewardsGemNoFree;

    public Image BG_LoginCountImage;
    public Image LockGemImage;

    public Image CheckDoneFreeGem;
    public Image CheckDoneNoFreeGem;

    public Button PurchaseTakeGemFree;
    public Button PurchaseButtonGemNoFree;

    public GameObject NotiClaim;
    public GameObject NotiClaimNoFree;

    private void Awake()
    {
        PurchaseTakeGemFree.onClick.AddListener(onClickPurchaseButtonFreeGem);
        PurchaseButtonGemNoFree.onClick.AddListener(onClickPurchaseButtonNoFreeGem);
    }

    private void Start()
    {
        for (int i = 0; i < Controller.Instance.dataPackOnline.L_PackOnline.Count; i++)
        {
            if (TypeDay == Controller.Instance.dataPackOnline.L_PackOnline[i].L_pack.typeLogInDay)
            {

                RewardsGemFree = Controller.Instance.dataPackOnline.L_PackOnline[i].L_pack.GemFree;
                RewardsGemNoFree = Controller.Instance.dataPackOnline.L_PackOnline[i].L_pack.GemNoFree;

                RewardsGemFreeTxt.text = RewardsGemFree.ToString();
                RewardsGemNoFreeTxt.text = RewardsGemNoFree.ToString();
            }
        }
        LoadTextLogin(CountDays);
    }
    void LoadTextLogin(int _num)
    {
        if (CountDays <= 9)
        {
            CountDaysTxt.text = string.Format("{0}{1}", 0, _num);
        }
        else
        {
            CountDaysTxt.text = _num.ToString();
        }
    }
    void onClickPurchaseButtonFreeGem()
    {
        CheckDoneFreeGem.gameObject.SetActive(true);
        PurchaseTakeGemFree.interactable = false;
        NotiClaim.SetActive(false);
        DataPlayer.AddListDoneGemFree(CountDays);
        popUpManager.Instance.m_PopUPpackOnline.PosEndCoin.position = Input.mousePosition;
        popUpManager.Instance.m_PopUPpackOnline.m_CurrencyFly.A_CallBack -= SetTextGemFree;
        popUpManager.Instance.m_PopUPpackOnline.m_CurrencyFly.A_CallBack += SetTextGemFree;
        popUpManager.Instance.m_PopUPpackOnline.m_CurrencyFly.ActiveCurrency(0);
    }
    void SetTextGemFree()
    {
        UI_Home.Instance.m_UIGemManager.SetTextGem(RewardsGemFree);
    }
    void SetTextGemNOFree()
    {
        UI_Home.Instance.m_UIGemManager.SetTextGem(RewardsGemNoFree);
    }

    void onClickPurchaseButtonNoFreeGem()
    {
        CheckDoneNoFreeGem.gameObject.SetActive(true);
        PurchaseButtonGemNoFree.interactable = false;
        NotiClaimNoFree.SetActive(false);
        DataPlayer.AddListDoneGemNoFree(CountDays);
        popUpManager.Instance.m_PopUPpackOnline.PosEndCoin.position = Input.mousePosition;
        popUpManager.Instance.m_PopUPpackOnline.m_CurrencyFly.A_CallBack -= SetTextGemNOFree;
        popUpManager.Instance.m_PopUPpackOnline.m_CurrencyFly.A_CallBack += SetTextGemNOFree;
        popUpManager.Instance.m_PopUPpackOnline.m_CurrencyFly.ActiveCurrency(0);
    }
}
public enum E_LogInDays
{
    NONE = 0,
    D_01,
    D_02,
    D_03,
    D_04,
    D_05,
    D_06,
    D_07,
    D_08,
    D_09,
    D_10,
    D_11,
    D_12,
    D_13,
    D_14,
    D_15,
    D_16,
    D_17,
    D_18,
    D_19,
    D_20,
    D_21,
    D_22,
    D_23,
    D_24,
    D_25,
    D_26,
    D_27,
    D_28,
    D_29,
    D_30
}
public enum StateBuy
{
    Free,
    NoFree
}
