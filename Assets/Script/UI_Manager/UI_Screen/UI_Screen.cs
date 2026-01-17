using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Screen : MonoBehaviour
{
    public Button AdsBtn;
    public Button Bag;
    public Button AddCoin;
    public Button AddGem;
    public Button PackLogin;
    public Button PackZone;
    public Button SpinWheel;

    public Text NotificationBoss;

    public GameObject hand;
    public GameObject slideHand;
    public GameObject UI_Shop;

    public player m_player;

    public GameObject NoAdsObj;
    public GameObject ZoneObj;
    public GameObject OnlineObj;
    public GameObject LuckyWheel;
    public GameObject InfoObj;

    private void Awake()
    {
        AdsBtn.onClick.AddListener(onClickAdsButton);
        AddCoin.onClick.AddListener(onClickAddCoin);
        AddGem.onClick.AddListener(Onclickaddgem);
        SpinWheel.onClick.AddListener(OnClickButtonSpinWheel);
        PackLogin.onClick.AddListener(OnClickPackLogin);
        PackZone.onClick.AddListener(OnClickPackZone);
        Bag.onClick.AddListener(onclickBag);

        if (DataPlayer.GetEnemyCatched() >= 1)
        {
            InfoObj.SetActive(true);
        }
        if (DataPlayer.GetEnemyCatched() >= 2)
        {
            OnlineObj.SetActive(true);
        }
        if (DataPlayer.GetEnemyCatched() >= 4 && !DataPlayer.GetNoAds())
        {
            NoAdsObj.SetActive(true);
        }
        if (DataPlayer.GetEnemyCatched() >= 6)
        {
            LuckyWheel.SetActive(true);
        }
        if (DataPlayer.GetBossCatched() >= 1)
        {
            ZoneObj.SetActive(true);
        }
    }
    private void Onclickaddgem()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (Controller.Instance.isShowInter)
            {
                Controller.Instance.ShowInterstitial();
            }
        }
    }
    private void onclickBag()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (Controller.Instance.isShowInter)
            {
                Controller.Instance.ShowInterstitial();
            }
        }
    }
    private void OnClickPackZone()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (Controller.Instance.isShowInter)
            {
                Controller.Instance.ShowInterstitial();
            }
        }
    }
    private void OnClickPackLogin()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (Controller.Instance.isShowInter)
            {
                Controller.Instance.ShowInterstitial();
            }
        }
    }
    private void Start()
    {
        disableButton();
    }

    public void HiddenObjScreen()
    {
        InfoObj.SetActive(false);
        OnlineObj.SetActive(false);
        NoAdsObj.SetActive(false);
        LuckyWheel.SetActive(false);
        ZoneObj.SetActive(false);
    }
    public void ActiveObjScreen()
    {
        if (DataPlayer.GetEnemyCatched() >= 1)
        {
            InfoObj.SetActive(true);
        }
        if (DataPlayer.GetEnemyCatched() >= 2)
        {
            OnlineObj.SetActive(true);
        }
        if (DataPlayer.GetEnemyCatched() >= 4)
        {
            NoAdsObj.SetActive(true);
        }
        if (DataPlayer.GetEnemyCatched() >= 6)
        {
            LuckyWheel.SetActive(true);
        }
        if (DataPlayer.GetBossCatched() >= 1)
        {
            ZoneObj.SetActive(true);
        }
    }


    private void OnEnable()
    {
        StartCoroutine(IE_delay());
    }
    IEnumerator IE_delay()
    {
        yield return new WaitForSeconds(0.1f);
        if (DataPlayer.GetIsCheckTapButtonSkip())
            Bag.interactable = true;

        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            BagManager.Instance.DisableButtonInBagManager();
        }
        else
        {
            BagManager.Instance.EnableButtonInBagManager();
        }
    }
    public void disableButton()
    {
        AddCoin.interactable = false;
        AdsBtn.interactable = false;
        Bag.interactable = false;
        SpinWheel.interactable = false;
        AddGem.interactable = false;
        PackLogin.interactable = false;
        PackZone.interactable = false;
    }
    public void EnableButton()
    {
        AddCoin.interactable = true;
        AdsBtn.interactable = true;
        Bag.interactable = true;
        SpinWheel.interactable = true;
        AddGem.interactable = true;
        PackLogin.interactable = true;
        PackZone.interactable = true;
    }
    public void onClickAdsButton()
    {
        //   UI_Shop.SetActive(true);
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (Controller.Instance.isShowInter)
            {
                Controller.Instance.ShowInterstitial();
            }
        }
        popUpManager.Instance.m_PopUpNoAds.gameObject.SetActive(true);

    }

    public void onClickAddCoin()
    {
        Debug.Log("da bam");
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (Controller.Instance.isShowInter)
            {
                Controller.Instance.ShowInterstitial();
            }
        }
        UI_Shop.SetActive(true);
    }
    void OnClickButtonSpinWheel()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (Controller.Instance.isShowInter)
            {
                Controller.Instance.ShowInterstitial();
            }
        }
        if (!UI_Home.Instance.uI_Battle.gameObject.activeInHierarchy)
            popUpManager.Instance.m_PickerWheel.gameObject.SetActive(true);
    }
    private void OnMouseDown()
    {
        Debug.Log("a");
    }
}
