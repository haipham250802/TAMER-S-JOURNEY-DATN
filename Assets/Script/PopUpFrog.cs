using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;
using Dragon.SDK;

public class PopUpFrog : MonoBehaviour
{
    public int ID;
    int StartCoin;
    public int SumCoin;

    public Button CoinNoAds;
    public Button CoinWithAds;
    public Button NoThanksBtn;

    public Text QuantityCoinNoAdsTxt;
    public Text QuantityCoinWithAdsTxt;
    public Text ContentTxt;
    public GameObject CoinPrefab;
    public AnimationCurve animCurve;
    public Transform DesPos;

    public Currentcy_Fly m_Currency;

    public List<a_Frog> FrogList = new List<a_Frog>();
    public List<GameObject> L_objFrogCry = new List<GameObject>();

    public SkeletonGraphic Icon;
    public GameObject FrogCry;
    private void Awake()
    {
        NoThanksBtn.onClick.AddListener(OnClickButtonNoThanks);
        CoinNoAds.onClick.AddListener(OnClickButtonCoinNoAds);
        CoinWithAds.onClick.AddListener(ShowAdsReward);
    }
    private void OnEnable()
    {
        StartCoroutine(IEdelay());
        AudioManager.Instance.PlaySound(AudioManager.instance.SoundEffectWosh);
    }
    IEnumerator IEdelay()
    {
        yield return null;
        for (int i = 0; i < BagManager.Instance.m_RuleController.L_enemy2.Count; i++)
        {
            BagManager.Instance.m_RuleController.L_enemy2[i].isCanAI = true;
        }
    }
    private void Start()
    {
        Icon.AnimationState.SetAnimation(0, "Idle", true);
    }
    void OnClickButtonCoinNoAds()
    {
        if (FrogList.Count > 0)
        {
            for (int i = 0; i < FrogList.Count; i++)
            {
                if (ID == FrogList[i].ID)
                {
                    SetCoin(FrogList[i].QuantityCoinNoAds);
                    Destroy(FrogList[i].gameObject);
                    Destroy(FrogList[i].FrogChildGroup);
                    SumCoin = FrogList[i].QuantityCoinNoAds;
                    m_Currency.ActiveCurrency(0);
                    FrogList.RemoveAt(i);
                    StartCoroutine(IE_delaySetTextCoin());
                    break;
                }
            }
        }
        else if (FrogList.Count == 1)
        {
            if (ID == FrogList[0].ID)
            {
                SetCoin(FrogList[0].QuantityCoinNoAds);
                Destroy(FrogList[0].gameObject);
                Destroy(FrogList[0].FrogChildGroup);
                SumCoin = FrogList[0].QuantityCoinWithAds;
                m_Currency.ActiveCurrency(0);
                FrogList.RemoveAt(0);
                StartCoroutine(IE_delaySetTextCoin());
            }
        }
    }
    private void ShowAdsReward()
    {
        AdStatus adstatus = SDKDGManager.Instance.AdsManager.ShowRewardedAdsStatus(() =>
        {
            OnClickButtonCoinWithAds();
            Controller.Instance.CountTime = 0;
            Debug.Log("Coin - PopUp Frog");
        }, "Coin - PopUp Frog");
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
    void OnClickButtonCoinWithAds()
    {
        NoThanksBtn.interactable = false;
        if (FrogList.Count > 0)
        {
            for (int i = 0; i < FrogList.Count; i++)
            {
                if (ID == FrogList[i].ID)
                {
                    //  SetCoin(FrogList[i].QuantityCoinWithAds);
                    SumCoin = FrogList[i].QuantityCoinWithAds;
                    Debug.LogError("Sumcoin: " + SumCoin);
                    StartCoroutine(AnimCoin());
                    m_Currency.A_CallBack2 += SetTextCOin;
                    m_Currency.ActiveCurrency(0);
                    // StartCoroutine(IE_delaySetTextCoin());
                    Destroy(FrogList[i].gameObject);
                    Destroy(FrogList[i].FrogChildGroup);
                    FrogList.RemoveAt(i);
                    GameObject obj = Instantiate(FrogCry, player.Instance.transform.position, Quaternion.identity);
                    L_objFrogCry.Add(obj);
                    return;
                }
            }
        }
        else if (FrogList.Count == 1)
        {
            if (ID == FrogList[0].ID)
            {
                // SetCoin(FrogList[0].QuantityCoinWithAds);
                SumCoin = FrogList[0].QuantityCoinWithAds;
                StartCoroutine(AnimCoin());
                m_Currency.A_CallBack2 += SetTextCOin;
                m_Currency.ActiveCurrency(0);
                Destroy(FrogList[0].gameObject);
                Destroy(FrogList[0].FrogChildGroup);
                FrogList.RemoveAt(0);
                GameObject obj = Instantiate(FrogCry, player.Instance.transform.position, Quaternion.identity);
                L_objFrogCry.Add(obj);
            }
        }
    }
    public void ClearFrogCry()
    {
        for (int i = 0; i < L_objFrogCry.Count; i++)
        {
            Destroy(L_objFrogCry[i].gameObject);
        }
        L_objFrogCry.Clear();
    }
    IEnumerator AnimCoin()
    {
        yield return new WaitForSeconds(m_Currency.MaxTimeMoveCoin);
        UI_Home.Instance.m_UICoinManager.SetTextCoin(SumCoin);
    }
    void SetTextCOin()
    {
        StartCoroutine(DelayDisable());
    }
    IEnumerator IE_delaySetTextCoin()
    {
        yield return new WaitForSeconds(m_Currency.MaxTimeMoveCoin);
        //gameObject.SetActive(false);
        StartCoroutine(DelayDisable());
    }
    IEnumerator DelayDisable()
    {
        yield return new WaitForSeconds(0.5f);
        {
            gameObject.SetActive(false);
        }
    }
    void SetCoin()
    {
        UI_Home.Instance.m_UICoinManager.SetTextCoin(SumCoin);
    }
    public void OnClickButtonNoThanks()
    {
        if (FrogList.Count > 0)
        {
            for (int i = 0; i < FrogList.Count; i++)
            {
                if (ID == FrogList[i].ID)
                {
                    Destroy(FrogList[i].gameObject);
                    Destroy(FrogList[i].FrogChildGroup);
                    FrogList.RemoveAt(i);
                    break;
                }
            }
        }
        else if (FrogList.Count == 1)
        {
            if (ID == FrogList[0].ID)
            {
                Destroy(FrogList[0].gameObject);
                Destroy(FrogList[0].FrogChildGroup);
                FrogList.RemoveAt(0);
            }
        }
        this.gameObject.SetActive(false);
    }
    int CoinTmp;
    public void SetCoin(int value)
    {
        CoinTmp = 0;
        CoinTmp = value;
    }
    bool isPlaySoundCoin = false;
    public void SetTextConTent(string txt)
    {
        /* ContentTxt.text = "Watch The Video To Get Coin Up To " + txt + "$"; */// nhận được <color=\"#FFC600\">" + txt + "</color>
       // ContentTxt.text = "Bạn đã bắt được 1 chú cóc vàng";
    }
    private void OnDisable()
    {
        m_Currency.A_CallBack2 -= SetTextCOin;
        NoThanksBtn.interactable = true;
        player.Instance.GetComponent<Collider2D>().enabled = true;
        if (BagManager.Instance.m_RuleController)
        {
            for (int i = 0; i < BagManager.Instance.m_RuleController.L_enemy.Count; i++)
            {
                if (BagManager.Instance.m_RuleController.L_enemy[i].gameObject.activeInHierarchy)
                {
                    if (UI_Home.Instance.CanShowUIBattle())
                        BagManager.Instance.m_RuleController.L_enemy[i].isCanAI = false;
                }
            }
        }
        StopAllCoroutines();
    }
}
