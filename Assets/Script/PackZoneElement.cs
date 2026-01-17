using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackZoneElement : MonoBehaviour
{
    public int ID;
    public TypeRewardPack EtypePackNofree1;
    public TypeRewardPack EtypePackNofree2;
    public TypeZone EtypeZone;

    public Image IconFreeImg;
    public Image IconNofree1Img;
    public Image IconNofree2Img;
    public Image LoginImg;
    public Image LockImgNoFree1;
    public Image LockImgNoFree2;

    public Image DoneFree;
    public Image DoneNoFree1;
    public Image DoneNoFree2;

    public GameObject ClaimObjFree;
    public GameObject ClaimObjNoFree1;
    public GameObject ClaimObjNoFree2;

    public Button PurchaseButtonFree;
    public Button PurchaseButtonNoFree1;
    public Button PurchaseButtonNoFree2;

    public Text CountCurMapTxt;
    public Text QuantityRewardFreeTxt;
    public Text QuantityRewardNoFree1Txt;
    public Text QuantityRewardNoFree2Txt;

    public int QuantityRewardFree;
    public int QuantityRewardNoFree1;
    public int QuantityRewardNoFree2;

    float time;

    private void Awake()
    {
        StartCoroutine(IE_delayActive());
        LoadVieww();
    }
    private void Update()
    {
        if (time >= 0)
        {
            time -= Time.deltaTime;
        }
    }
    void OnEnable()
    {
        StartCoroutine(IE_delay());
    }
    IEnumerator IE_delay()
    {
        yield return new WaitForSeconds(0.2f);
        LoadVieww();
    }
    public void LoadVieww()
    {
        if (DataPlayer.GetListDoneGemFreePackZone().Contains(ID))
        {
            ClaimObjFree.SetActive(false);
            PurchaseButtonFree.interactable = false;
            DoneFree.gameObject.SetActive(true);
        }
        else if (DataPlayer.GetListDoneGemFreePackZone().Contains(ID) && ID <= DataPlayer.GetCurZonepackZone())
        {
            ClaimObjFree.SetActive(true);
            PurchaseButtonFree.interactable = true;
            DoneFree.gameObject.SetActive(false);
        }
        //
        if (DataPlayer.GetListDoneGemNoFree1PackZone().Contains(ID))
        {
            ClaimObjNoFree1.SetActive(false);
            PurchaseButtonNoFree1.interactable = false;
            DoneNoFree1.gameObject.SetActive(true);
        }
        else if (DataPlayer.GetListDoneGemNoFree1PackZone().Contains(ID) && ID <= DataPlayer.GetCurZonepackZone() && DataPlayer.GetUnLockRewardNoFreePackZone())
        {
            ClaimObjNoFree1.SetActive(true);
            PurchaseButtonNoFree1.interactable = false;
            DoneNoFree1.gameObject.SetActive(false);
        }
        //
        if (DataPlayer.GetListDoneGemNoFree2PackZone().Contains(ID) && DataPlayer.GetUnLockRewardNoFreePackZone())
        {
            ClaimObjNoFree2.SetActive(false);
            PurchaseButtonNoFree2.interactable = false;
            DoneNoFree2.gameObject.SetActive(true);
        }
        else if (DataPlayer.GetListDoneGemNoFree2PackZone().Contains(ID) && ID <= DataPlayer.GetCurZonepackZone() && DataPlayer.GetUnLockRewardNoFreePackZone())
        {
            ClaimObjNoFree2.SetActive(true);
            PurchaseButtonNoFree2.interactable = false;
            DoneNoFree2.gameObject.SetActive(false);
        }
    }
    IEnumerator IE_delayActive()
    {
        yield return null;
        PurchaseButtonFree.onClick.AddListener(OnClickPurchaseButtonFree);
        PurchaseButtonNoFree1.onClick.AddListener(OnclickPurchaseButtonNofree1);
        PurchaseButtonNoFree2.onClick.AddListener(OnclickButtonPurchaseNoFree2);

        /* if(!DataPlayer.GetListDoneGemFreePackZone().Contains(ID) && ID <= DataPlayer.GetCurZonepackZone())
         {
             ClaimObjFree.gameObject.SetActive(true);
         }*/
    }
    private void Start()
    {
        StartCoroutine(IE_DelayLoadView());
    }
    IEnumerator IE_DelayLoadView()
    {
        yield return null;
        LoadView();
    }
    void LoadView()
    {
        /*  for (int i = 0; i < DataPlayer.GetListDoneGemFreePackZone().Count; i++)
          {
              if (ID == DataPlayer.GetListDoneGemFreePackZone()[i])
              {
                  PurchaseButtonFree.interactable = false;
                  DoneFree.gameObject.SetActive(true);
                  ClaimObjFree.gameObject.SetActive(false);
                  break;
              }
          }
  */
        for (int i = 0; i < DataPlayer.GetListDoneGemNoFree1PackZone().Count; i++)
        {
            if (ID == DataPlayer.GetListDoneGemNoFree1PackZone()[i])
            {
                PurchaseButtonNoFree1.interactable = false;
                DoneNoFree1.gameObject.SetActive(true);
                ClaimObjNoFree1.gameObject.SetActive(false);
                break;
            }
        }

        for (int i = 0; i < DataPlayer.GetListDoneGemNoFree2PackZone().Count; i++)
        {
            if (ID == DataPlayer.GetListDoneGemNoFree2PackZone()[i])
            {
                PurchaseButtonNoFree2.interactable = false;
                DoneNoFree2.gameObject.SetActive(true);
                ClaimObjNoFree2.gameObject.SetActive(false);
                break;
            }
        }
    }
    void SetTextCoin()
    {
        UI_Home.Instance.m_UICoinManager.SetTextCoin(QuantityRewardFree);
    }
    public List<GameObject> L_Rewads = new List<GameObject>();
    bool isCoin = false;
    bool isSoundScaleCoin;
    bool isSoundMoveCoin;

    void SpawnEffectRewardsFly(GameObject GamObj, Vector3 PosEnd)
    {
      //  L_Rewads = new List<GameObject>();
        PopUpPackZone m_PopZone = popUpManager.Instance.m_PopUpPackZone;
        for (int i = 0; i < 10; i++)
        {
            Vector3 rand = Random.insideUnitCircle * 70;

            GameObject obj = Instantiate(GamObj);
            obj.transform.SetParent(UI_Home.Instance.m_UiCurrency.transform);
            obj.transform.position = Input.mousePosition + rand;
            obj.transform.localScale = Vector3.zero;
            if (GamObj == m_PopZone.CoinPrefabs.gameObject)
            {
                DOTween.To(() => 0f, _ =>
                {
                    obj.transform.localScale = new Vector3(_, _, _);
                }, 0.7f, 0.1f).OnStart(() =>
                {
                    if (!isSoundScaleCoin)
                    {
                        isSoundScaleCoin = true;
                        AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectScaleCoin);
                    }
                });
            }
            else
            {
                DOTween.To(() => 0f, _ =>
                {
                    obj.transform.localScale = new Vector3(_, _, _);
                }, 1.5f, 0.1f).OnStart(() =>
                {
                    if (!isSoundScaleCoin)
                    {
                        isSoundScaleCoin = true;
                        AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectScaleCoin);
                    }
                }); ;
            }

            L_Rewads.Add(obj);
        }
        StartCoroutine(IE_DelayFlyRewadsToUICurrentcy(PosEnd));
        StartCoroutine(IE_DelayRemove());
    }
    int Coin;
    int Gem;
    bool isSetCurrentcy;

    IEnumerator IE_DelayFlyRewadsToUICurrentcy(Vector3 POsEnd)
    {
        yield return new WaitForSeconds(0.3f);
        for (int i = 0; i < L_Rewads.Count; i++)
        {
            float randDuration = Random.Range(0.5f, 0.8f);
            if (L_Rewads[i] != null)
            {
                L_Rewads[i].transform.DOKill();
                L_Rewads[i].transform.DOMove(POsEnd, randDuration).OnStart(() =>
                {
                    if (!isSoundMoveCoin)
                    {
                        isSoundMoveCoin = true;
                        AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectGetCoin);
                    }
                    if (!isSetCurrentcy)
                    {
                        if (isCoin)
                        {
                            /*UI_Home.Instance.m_UICoinManager.SetTextCoin(Coin);*/
                            TextCoinAnimation.Instance.ActionAnimationText(UI_Home.Instance.m_UICoinManager.CoinTxt,
                                DataPlayer.GetCoin(), DataPlayer.GetCoin() + Coin, 0.5f);

                            DataPlayer.SetCoin(DataPlayer.GetCoin() + Coin);
                        }
                        else
                        {
                            TextCoinAnimation.Instance.ActionAnimationText(UI_Home.Instance.m_UIGemManager.Gemtxt,
                               DataPlayer.GetGem(), DataPlayer.GetGem() + Gem, 0.5f);
                            DataPlayer.SetGem(DataPlayer.GetGem() + Gem);
                            /*UI_Home.Instance.m_UIGemManager.SetTextGem(Gem);*/
                        }
                        isSetCurrentcy = true;
                    }

                });
            }
        }
        //   StartCoroutine(IE_DelayRemove());

    }
    IEnumerator IE_DelayRemove()
    {
        yield return new WaitForSeconds(2f);
        popUpManager.Instance.m_PopUpPackZone.ExitButton.interactable = true;
        for (int i = 0; i < L_Rewads.Count; i++)
        {
            Destroy(L_Rewads[i]);
        }
        L_Rewads.Clear();
        isSetCurrentcy = false;
        isCoin = false;
        isSoundMoveCoin = false;
        isSoundScaleCoin = false;
    }
    void OnClickPurchaseButtonFree()
    {
        if (time <= 0)
        {
            PopUpPackZone m_PopZone = popUpManager.Instance.m_PopUpPackZone;
            m_PopZone.ExitButton.interactable = false;
            Debug.Log("da bam free: " + ID);
            isCoin = true;
            Coin = 0;
            Coin = QuantityRewardFree;
            Debug.Log("COIN");

            if (DataPlayer.GetListDoneGemFreePackZone().Count == 0)
            {
                PurchaseButtonFree.interactable = false;
                PopUpPackZone m_POp = popUpManager.Instance.m_PopUpPackZone;
                DoneFree.gameObject.SetActive(true);
                DataPlayer.AddListDoneGemfreePackZone(ID);
                ClaimObjFree.gameObject.SetActive(false);
                SpawnEffectRewardsFly(m_PopZone.CoinPrefabs.gameObject, m_PopZone.PosEndCoin.position);
            }
            for (int i = 0; i < DataPlayer.GetListDoneGemFreePackZone().Count; i++)
            {
                if (ID != DataPlayer.GetListDoneGemFreePackZone()[i])
                {
                    PurchaseButtonFree.interactable = false;
                    DoneFree.gameObject.SetActive(true);
                    DataPlayer.AddListDoneGemfreePackZone(ID);
                    ClaimObjFree.gameObject.SetActive(false);
                    SpawnEffectRewardsFly(m_PopZone.CoinPrefabs.gameObject, m_PopZone.PosEndCoin.position);
                    return;
                }
            }
            time = 2;
        }
    }
    void OnclickPurchaseButtonNofree1()
    {
        if (time <= 0)
        {
            Debug.Log("da bam Nofree1: " + ID);
            PopUpPackZone m_PopZone = popUpManager.Instance.m_PopUpPackZone;
            if (EtypePackNofree1 == TypeRewardPack.Coin || EtypePackNofree1 == TypeRewardPack.Gem)
            {
                m_PopZone.ExitButton.interactable = false;
            }
            if (DataPlayer.GetListDoneGemNoFree1PackZone().Count == 0)
            {
                PurchaseButtonNoFree1.interactable = false;
                DoneNoFree1.gameObject.SetActive(true);
                SetRewards1(EtypePackNofree1);
                DataPlayer.AddListDoneGemNofree1PackZone(ID);
                ClaimObjNoFree1.gameObject.SetActive(false);

            }
            for (int i = 0; i < DataPlayer.GetListDoneGemNoFree1PackZone().Count; i++)
            {
                if (ID != DataPlayer.GetListDoneGemNoFree1PackZone()[i])
                {
                    PurchaseButtonNoFree1.interactable = false;
                    DoneNoFree1.gameObject.SetActive(true);
                    DataPlayer.AddListDoneGemNofree1PackZone(ID);
                    SetRewards1(EtypePackNofree1);
                    ClaimObjNoFree1.gameObject.SetActive(false);
                    return;
                }
            }
            time = 2;
        }
    }
    void OnclickButtonPurchaseNoFree2()
    {
        if (time <= 0)
        {
            Debug.Log("da bam Nofree1: " + ID);
            PopUpPackZone m_PopZone = popUpManager.Instance.m_PopUpPackZone;
            if(EtypePackNofree2 == TypeRewardPack.Coin || EtypePackNofree2 == TypeRewardPack.Gem)
            {
                m_PopZone.ExitButton.interactable = false;
            }
            if (DataPlayer.GetListDoneGemNoFree2PackZone().Count == 0)
            {
                PurchaseButtonNoFree2.interactable = false;
                DoneNoFree2.gameObject.SetActive(true);
                DataPlayer.AddListDoneGemNofree2PackZone(ID);
                SetRewards2(EtypePackNofree2);
                ClaimObjNoFree2.gameObject.SetActive(false);

            }
            for (int i = 0; i < DataPlayer.GetListDoneGemNoFree2PackZone().Count; i++)
            {
                if (ID != DataPlayer.GetListDoneGemNoFree2PackZone()[i])
                {
                    PurchaseButtonNoFree2.interactable = false;
                    DoneNoFree2.gameObject.SetActive(true);
                    DataPlayer.AddListDoneGemNofree2PackZone(ID);
                    SetRewards2(EtypePackNofree2);
                    ClaimObjNoFree2.gameObject.SetActive(false);
                    return;
                }
            }
            time = 2;
        }
    }
    void SetRewards1(TypeRewardPack typeRewards)
    {
         
        switch (typeRewards)
        {
            case TypeRewardPack.Coin:
                isCoin = true;
                Coin = 0;
                Coin = QuantityRewardNoFree1;
                // UI_Home.Instance.m_UICoinManager.SetTextCoin(QuantityRewardNoFree1);
                SpawnEffectRewardsFly(popUpManager.Instance.m_PopUpPackZone.CoinPrefabs.gameObject, popUpManager.Instance.m_PopUpPackZone.PosEndCoin.position);
                break;
            case TypeRewardPack.Gem:
                Gem = 0;
                Gem = QuantityRewardNoFree1;
                SpawnEffectRewardsFly(popUpManager.Instance.m_PopUpPackZone.GemPrefabs.gameObject, popUpManager.Instance.m_PopUpPackZone.PosEndGem.position);
                //      UI_Home.Instance.m_UIGemManager.SetTextGem(QuantityRewardNoFree1);
                break;
            case TypeRewardPack.Chest_Normal:
                int quantityNormal = DataPlayer.GetQuantityChestNormalPack();
                quantityNormal += QuantityRewardNoFree1;
                DataPlayer.SetQuantityChestNormalPack(quantityNormal);
                break;
            case TypeRewardPack.Chest_Epic:
                int quantityEpic = DataPlayer.GetQuantityChestEpicPack();
                quantityEpic += QuantityRewardNoFree1;
                DataPlayer.SetQuantityChestEpicPack(quantityEpic);
                break;
            case TypeRewardPack.Chest_Legend:
                int quantityLegend = DataPlayer.GetQuantityChestLegendPack();
                quantityLegend += QuantityRewardNoFree1;
                DataPlayer.SetQuantityChestLegendPack(quantityLegend);
                break;
        }
    }
    void SetRewards2(TypeRewardPack typeRewards)
    {
        switch (typeRewards)
        {
            case TypeRewardPack.Coin:
                isCoin = true;
                Coin = 0;
                Coin = QuantityRewardNoFree2;
                SpawnEffectRewardsFly(popUpManager.Instance.m_PopUpPackZone.CoinPrefabs.gameObject, popUpManager.Instance.m_PopUpPackZone.PosEndCoin.position);
                //    UI_Home.Instance.m_UICoinManager.SetTextCoin(QuantityRewardNoFree2);
                break;
            case TypeRewardPack.Gem:
                Gem = 0;
                Gem = QuantityRewardNoFree2;
                SpawnEffectRewardsFly(popUpManager.Instance.m_PopUpPackZone.GemPrefabs.gameObject, popUpManager.Instance.m_PopUpPackZone.PosEndGem.position);

                //   UI_Home.Instance.m_UIGemManager.SetTextGem(QuantityRewardNoFree2);
                break;
            case TypeRewardPack.Chest_Normal:
                int quantityNormal = DataPlayer.GetQuantityChestNormalPack();
                quantityNormal += QuantityRewardNoFree2;
                DataPlayer.SetQuantityChestNormalPack(quantityNormal);
                break;
            case TypeRewardPack.Chest_Epic:
                int quantityEpic = DataPlayer.GetQuantityChestEpicPack();
                quantityEpic += QuantityRewardNoFree2;
                DataPlayer.SetQuantityChestEpicPack(quantityEpic);
                break;
            case TypeRewardPack.Chest_Legend:
                int quantityLegend = DataPlayer.GetQuantityChestLegendPack();
                quantityLegend += QuantityRewardNoFree2;
                DataPlayer.SetQuantityChestLegendPack(quantityLegend);
                break;
        }
    }


}
