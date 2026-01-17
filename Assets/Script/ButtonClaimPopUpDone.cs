using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Dragon.SDK;
public class ButtonClaimPopUpDone : MonoBehaviour
{
    Tween tween;
    public ArrowReward arrowRewards;
    public GameObject CoinPrefabs;

    public Transform Parent;
    public Transform ContainerBottom;

    public Currentcy_Fly m_CurrentCy;
    private void Awake()
    {
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            this.GetComponent<Button>().onClick.AddListener(OnCickButtonNoAds);
        }
        else
        {
            this.GetComponent<Button>().onClick.AddListener(ShowAdsRewards);
        }
    }
    private void OnEnable()
    {
        ActiveScaleButtonWithEvenanim();
        if (!DataPlayer.GetIsTapButtonClaim())
        {
            Controller.Instance.exampObj.gameObject.SetActive(true);
            string str = I2.Loc.LocalizationManager.GetTranslation("KEY_TUT_CATCH_DONE");
            TutorialManager.Instance.SpawnHandUIBattle(Parent, new Vector3(30, -400));
            ExampleStoryTut.Instance.SetText(str);
        }
    }

    public void ActiveScaleButtonWithEvenanim()
    {
        ScaleButtonClaim(transform);
    }
    public void Kill()
    {
        tween.Kill(true);
    }
    public void ScaleButtonClaim(Transform transform)
    {
        tween = transform.DOScale(1.2f, 0.5f).OnComplete(() =>
        {
            tween = transform.DOScale(1f, 0.5f).OnComplete(() =>
             {
                 ScaleButtonClaim(transform);
             });
        });
    }
    public int numcoin;
    int mincoin;
    int maxCoin;

#if UNITY_EDITOR
    [Button("Test Move Coin")]
    public void test()
    {
        this.GetComponent<Button>().interactable = false;
        arrowRewards.StopArrow();
        tween.Pause();
        mincoin = DataPlayer.GetCoin();
        maxCoin = mincoin + arrowRewards.CoinClaim;
        // MoveCoin.Instance.Action(CoinPrefabs, numcoin, transform.position, Parent, ActionAnimTxt);
        /*
                m_NewCoinFly.L_coin.Clear();
                m_NewCoinFly.ACtionCoin(0, ActionAnimTxt);
                Debug.Log("da bam ads");
                DataPlayer.SetCoin(maxCoin);*/
    }
#endif

    public void ShowAdsRewards()
    {
        Debug.Log("da show ads");
        AdStatus adstatus = SDKDGManager.Instance.AdsManager.ShowRewardedAdsStatus(() =>
        {
            OnCickButtonWithAds();
            Controller.Instance.CountTime = 0;
            Debug.Log("Claim PopUp End Game");
        },"Claim PopUp End Game");

        switch(adstatus)
        {
            case AdStatus.NoInternet:
                popUpManager.Instance.m_PopUpNointernet.gameObject.SetActive(true);
                break;

            case AdStatus.NoVideo:
                popUpManager.Instance.m_PopUPNovideo.gameObject.SetActive(true);
                break;
        }
    }

    public void OnCickButtonNoAds()
    {
        Debug.Log("khong show ads");
        popUpManager.Instance.m_PopUpCatchDone.isCheckButtonAds = true;
        /* this.GetComponent<Button>().interactable = false;
         arrowRewards.StopArrow();
         tween.Pause();
         mincoin = DataPlayer.GetCoin();
         maxCoin = mincoin + arrowRewards.CoinClaim;
         MoveCoin.Instance.Action(CoinPrefabs, numcoin, transform.position, Parent, ActionAnimTxt);
         Debug.Log("da bam no ads");
         DataPlayer.SetCoin(maxCoin);*/
        this.GetComponent<Button>().interactable = false;
        arrowRewards.StopArrow();
        tween.Pause();
        m_CurrentCy.ActiveCurrency(0);
        maxCoin = arrowRewards.CoinClaim;
        StartCoroutine(IE_delay());
        DataPlayer.SetIsTapButtonClaim(true);
        TutorialManager.Instance.DeSpawn();
        SpawnHandToSkipButton();
        this.GetComponent<Button>().onClick.RemoveListener(OnCickButtonNoAds);
        this.GetComponent<Button>().onClick.AddListener(ShowAdsRewards);
    }

    IEnumerator IE_delay()
    {
        yield return new WaitForSeconds(m_CurrentCy.MaxTimeMoveCoin);
        UI_Home.Instance.m_UICoinManager.SetTextCoin(maxCoin);
    }
    void SpawnHandToSkipButton()
    {
        if (!DataPlayer.GetIsCheckTapButtonSkip())
        {
            TutorialManager.Instance.SpawnHandUIBattle(ContainerBottom, new Vector3(300, -10));
            UI_Home.Instance.uI_Battle.m_PopUpCatchDone.SkipBtn.GetComponent<Canvas>().sortingOrder = 1000;
        }
    }
    public void OnCickButtonWithAds()
    {
        popUpManager.Instance.m_PopUpCatchDone.isCheckButtonAds = true;
        this.GetComponent<Button>().interactable = false;
        arrowRewards.StopArrow();
        tween.Pause();

        maxCoin = arrowRewards.CoinClaim;
        m_CurrentCy.ActiveCurrency(0);
        StartCoroutine(IE_delay());
    }

    public void ActionAnimTxt()
    {
        Text txt = UI_Home.Instance.m_UICoinManager.CoinTxt;
        TextCoinAnimation.Instance.ActionAnimationText(txt, mincoin, maxCoin, 0.5f);
    }
    private void OnDisable()
    {
        transform.localScale = Vector3.one;
        MoveCoin.Instance.num = 0;
        for (int i = 0; i < MoveCoin.Instance.L_obj.Count; i++)
        {
            Destroy(MoveCoin.Instance.L_obj[i]);
        }
        MoveCoin.Instance.L_obj.Clear();
        this.GetComponent<Button>().interactable = true;
        for (int i = 0; i < m_CurrentCy.CurrentcyList.Count; i++)
        {
            Destroy(m_CurrentCy.CurrentcyList[i]);
        }
        m_CurrentCy.CurrentcyList.Clear();
    }
}
