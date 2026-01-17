using Dragon.SDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpPackZone : MonoBehaviour
{
    public List<PackZoneElement> L_PackZone = new List<PackZoneElement>();
    public bool isUnLockRewardsNoFree;
    public int CurZone;

    public Sprite UnLoginImg;
    public Sprite LoginImg;

    public GameObject NotiObj;
    public GameObject Content;

    public Button PurchaseButtonUnLockNoFree;
    public Button PurchaseButtonUnLockZone;
    public Button ExitButton;

    public GameObject objNotUnlock;
    public GameObject objUnlocked;

    public Image GemPrefabs;
    public Image CoinPrefabs;

    public Transform PosEndCoin;
    public Transform PosEndGem;
    public Transform PosActiveCoin;

    public Currentcy_Fly m_CurrentcyFly;
    public GameObject Slash;

    public Text CostTxt;
    private void Awake()
    {
        PurchaseButtonUnLockNoFree.onClick.AddListener(Purchase);
        PurchaseButtonUnLockZone.onClick.AddListener(OnclickButtonUnLockZone);
        ExitButton.onClick.AddListener(OnClickButtonExit);
        CostTxt.text = SDKDGManager.Instance.IAPManager.GetPrice(ProduckID.PACK_ZONE_VIP);
    }
    private void OnEnable()
    {
        StartCoroutine(IE_Delay());
        AudioManager.Instance.PlaySound(AudioManager.instance.SoundEffectWosh);
    }
    IEnumerator IE_Delay()
    {
        yield return null;
        Content.transform.localPosition = new Vector3(Content.transform.localPosition.x, 0, 0);
        int max = 0;
        for (int i = 0; i < DataPlayer.GetListLevel().Count; i++)
        {
            if (max < DataPlayer.GetListLevel()[i])
            {
                max = DataPlayer.GetListLevel()[i];
            }
        }
    
        CurZone = DataPlayer.GetCurZonepackZone();
        if (CurZone < max)
        {
            CurZone = max;
            DataPlayer.SetCurZonePackZone(CurZone);
            NotiObj.transform.SetParent(L_PackZone[CurZone - 1].transform);
            NotiObj.transform.localPosition = new Vector3(0, -90, 0);
        }

        if (DataPlayer.GetUnLockRewardNoFreePackZone())
        {
            objNotUnlock.SetActive(false);
            objUnlocked.SetActive(true);
            Slash.SetActive(false);

        }
        else
        {
            objNotUnlock.SetActive(true);
            objUnlocked.SetActive(false);
            Slash.SetActive(true);

        }
        for (int i = 0; i < L_PackZone.Count; i++)
        {
            L_PackZone[i].LoadVieww();
        }
        player.Instance.gameObject.GetComponent<Collider2D>().enabled = false;
        LoadOnenable();
    }
    private void Start()
    {
        InitView();
        StartCoroutine(IE_DelayLoadView());
        //  LoadView();
    }
    IEnumerator IE_DelayLoadView()
    {
        yield return null;
        LoadView();
    }
    public void InitView()
    {
        for (int i = 0; i < L_PackZone.Count; i++)
        {
            L_PackZone[i].DoneFree.gameObject.SetActive(false);
            L_PackZone[i].DoneNoFree1.gameObject.SetActive(false);
            L_PackZone[i].DoneNoFree2.gameObject.SetActive(false);

            L_PackZone[i].PurchaseButtonFree.interactable = false;
            L_PackZone[i].PurchaseButtonNoFree1.interactable = false;
            L_PackZone[i].PurchaseButtonNoFree2.interactable = false;

            L_PackZone[i].ClaimObjFree.SetActive(false);
            L_PackZone[i].ClaimObjNoFree1.SetActive(false);
            L_PackZone[i].ClaimObjNoFree2.SetActive(false);
        }
        for (int i = 0; i < Controller.Instance.dataPackZone.L_PackZoneStat.Count; i++)
        {
            L_PackZone[i].EtypeZone = Controller.Instance.dataPackZone.L_PackZoneStat[i].typeZone;
            L_PackZone[i].EtypePackNofree1 = Controller.Instance.dataPackZone.L_PackZoneStat[i].TypeRewardsNofree1;
            L_PackZone[i].EtypePackNofree2 = Controller.Instance.dataPackZone.L_PackZoneStat[i].TypeRewardsNofree2;
            L_PackZone[i].CountCurMapTxt.text = string.Format("{0:00}", i + 1);

            L_PackZone[i].QuantityRewardFree = Controller.Instance.dataPackZone.L_PackZoneStat[i].QuantityRewarsFree;
            L_PackZone[i].QuantityRewardNoFree1 = Controller.Instance.dataPackZone.L_PackZoneStat[i].QuantityRewarsNoFree1;
            L_PackZone[i].QuantityRewardNoFree2 = Controller.Instance.dataPackZone.L_PackZoneStat[i].QuantityRewarsNoFree2;

            L_PackZone[i].QuantityRewardFreeTxt.text = L_PackZone[i].QuantityRewardFree.ToString();
            L_PackZone[i].QuantityRewardNoFree1Txt.text = L_PackZone[i].QuantityRewardNoFree1.ToString();
            L_PackZone[i].QuantityRewardNoFree2Txt.text = L_PackZone[i].QuantityRewardNoFree2.ToString();
            L_PackZone[i].IconNofree1Img.sprite = Controller.Instance.dataPackZone.L_PackZoneStat[i].SP_NoFreeIcon1;
            L_PackZone[i].IconNofree2Img.sprite = Controller.Instance.dataPackZone.L_PackZoneStat[i].SP_NoFreeIcon2;
            L_PackZone[i].LoginImg.sprite = LoginImg;
        }
    }
    void LoadView()
    {
        isUnLockRewardsNoFree = DataPlayer.GetUnLockRewardNoFreePackZone();
        if (isUnLockRewardsNoFree)
        {
            for (int i = 0; i < L_PackZone.Count; i++)
            {
                L_PackZone[i].LockImgNoFree1.gameObject.SetActive(false);
                L_PackZone[i].LockImgNoFree2.gameObject.SetActive(false);
            }
            PurchaseButtonUnLockNoFree.interactable = false;
        }

        if (CurZone == 0)
        {
            L_PackZone[CurZone].PurchaseButtonFree.interactable = true;
            L_PackZone[CurZone].PurchaseButtonNoFree1.interactable = true;
            L_PackZone[CurZone].PurchaseButtonNoFree2.interactable = true;
            L_PackZone[CurZone].LoginImg.sprite = UnLoginImg;
            if (!DataPlayer.GetListDoneGemFreePackZone().Contains(CurZone))
                L_PackZone[CurZone].ClaimObjFree.gameObject.SetActive(true);
            //     NotiObj.transform.SetParent(L_PackZone[CurZone].transform);
        }
        else if (CurZone > 0)
        {
            for (int i = 0; i < CurZone; i++)
            {
                Debug.Log("i: " + i);
                L_PackZone[i].PurchaseButtonFree.interactable = true;
                L_PackZone[i].PurchaseButtonNoFree1.interactable = true;
                L_PackZone[i].PurchaseButtonNoFree2.interactable = true;
                if (!DataPlayer.GetListDoneGemFreePackZone().Contains(i))
                {
                    L_PackZone[i].ClaimObjFree.gameObject.SetActive(true);
                }
                L_PackZone[i].LoginImg.sprite = UnLoginImg;

                if (DataPlayer.GetUnLockRewardNoFreePackZone())
                {
                    L_PackZone[i].ClaimObjNoFree1.gameObject.SetActive(true);
                    L_PackZone[i].ClaimObjNoFree2.gameObject.SetActive(true);
                }
            }
        }
        CurZone = DataPlayer.GetCurZonepackZone();
        if (NotiObj)
        {
            Debug.Log("a: " + (CurZone - 1));
            NotiObj.transform.SetParent(L_PackZone[CurZone - 1].transform);
            NotiObj.transform.localPosition = new Vector3(0, -90, 0);
        }

        if (CurZone == L_PackZone.Count)
        {
            Destroy(NotiObj);
        }
    }
    public void Purchase()
    {
        SDKDGManager.Instance.IAPManager.Purchase(ProduckID.PACK_ZONE_VIP, () =>
        {
            // BuyGem();
            Debug.Log(ProduckID.PACK_ZONE_VIP + "$");
            Debug.Log("Pruchase Success Update UI");
            OnClickButtonUnLockNoFree();
        });

    }
    public void OnClickButtonUnLockNoFree()
    {
        if (!isUnLockRewardsNoFree)
        {
            isUnLockRewardsNoFree = true;
            DataPlayer.SetUnLockRewardsFreePackZone(isUnLockRewardsNoFree);
            PurchaseButtonUnLockNoFree.interactable = false;
            CurZone = DataPlayer.GetCurZonepackZone();
            /*  for (int i = 0; i < L_PackZone.Count; i++)
              {
                  L_PackZone[i].LockImgNoFree1.gameObject.SetActive(false);
                  L_PackZone[i].LockImgNoFree2.gameObject.SetActive(false);
                  *//*if (!DataPlayer.GetListDoneGemFreePackZone().Contains(L_PackZone[i].ID) && L_PackZone[i].ID <= CurZone)
                  {
                      *//* L_PackZone[i].ClaimObjFree.gameObject.SetActive(true);
                       *//*  L_PackZone[i].ClaimObjNoFree1.gameObject.SetActive(true);
                         L_PackZone[i].ClaimObjNoFree2.gameObject.SetActive(true);*//*
                      if (DataPlayer.GetUnLockRewardNoFreePackZone())
                      {
                          L_PackZone[i].ClaimObjNoFree1.gameObject.SetActive(true);
                          L_PackZone[i].ClaimObjNoFree2.gameObject.SetActive(true);
                      }
                  }*//*
              }*/
            LoadView();
            objNotUnlock.gameObject.SetActive(false);
            objUnlocked.gameObject.SetActive(true);
        }
    }
    void LoadOnenable()
    {
        if (DataPlayer.GetCurZonepackZone() < CurZone)
        {
            L_PackZone[CurZone - 1].PurchaseButtonFree.interactable = true;
            L_PackZone[CurZone - 1].PurchaseButtonNoFree1.interactable = true;
            L_PackZone[CurZone - 1].PurchaseButtonNoFree2.interactable = true;
            L_PackZone[CurZone - 1].LoginImg.sprite = UnLoginImg;
            L_PackZone[CurZone - 1].ClaimObjFree.SetActive(true);
            if (DataPlayer.GetUnLockRewardNoFreePackZone())
            {
                L_PackZone[CurZone - 1].ClaimObjNoFree1.SetActive(true);
                L_PackZone[CurZone - 1].ClaimObjNoFree2.SetActive(true);
            }
            UI_Home.Instance.m_UIGemManager.SubGem(30);
            if (CurZone == L_PackZone.Count)
            {
                Destroy(NotiObj);
            }
        }
       /* for (int i = 0; i < CurZone; i++)
        {
            for (int j = 0; j < DataPlayer.GetListDoneGemFreePackZone().Count; j++)
            {
                if (i != DataPlayer.GetListDoneGemFreePackZone()[j])
                {
                    L_PackZone[i - 1].PurchaseButtonFree.interactable = true;
                    L_PackZone[i - 1].PurchaseButtonNoFree1.interactable = true;
                    L_PackZone[i - 1].PurchaseButtonNoFree2.interactable = true;
                    L_PackZone[i - 1].LoginImg.sprite = UnLoginImg;
                    L_PackZone[i - 1].ClaimObjFree.SetActive(true);
                }
            }
        }*/
        LoadView();

    }
    public void OnclickButtonUnLockZone()
    {
        if (DataPlayer.GetGem() >= 30)
        {
            for (int i = 0; i < DataPlayer.GetListLevel().Count; i++)
            {
                if (DataPlayer.GetListLevel()[i] > CurZone)
                {
                    CurZone = DataPlayer.GetListLevel()[i];
                    DataPlayer.SetCurZonePackZone(CurZone);
                }
            }
            CurZone = DataPlayer.GetCurZonepackZone();
            CurZone++;
            DataPlayer.SetCurZonePackZone(CurZone);

            NotiObj.transform.SetParent(L_PackZone[CurZone - 1].transform);
            NotiObj.transform.localPosition = new Vector3(0, -90, 0);

            L_PackZone[CurZone - 1].PurchaseButtonFree.interactable = true;
            L_PackZone[CurZone - 1].PurchaseButtonNoFree1.interactable = true;
            L_PackZone[CurZone - 1].PurchaseButtonNoFree2.interactable = true;
            L_PackZone[CurZone - 1].LoginImg.sprite = UnLoginImg;
            L_PackZone[CurZone - 1].ClaimObjFree.SetActive(true);
            if (DataPlayer.GetUnLockRewardNoFreePackZone())
            {
                L_PackZone[CurZone - 1].ClaimObjNoFree1.SetActive(true);
                L_PackZone[CurZone - 1].ClaimObjNoFree2.SetActive(true);
            }
            UI_Home.Instance.m_UIGemManager.SubGem(30);
            if (CurZone == L_PackZone.Count)
            {
                Destroy(NotiObj);
            }
            /* for (int i = 0; i < L_PackZone.Count; i++)
             {
                 L_PackZone[i].LoadVieww();
             }*/
        }
        else
        {
            popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.GEM;
            popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
        }
    }
    public void OnClickButtonExit()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        for (int i = 0; i < L_PackZone.Count; i++)
        {

        }
    }
}
