using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using Spine;
using Dragon.SDK;

public class TimeCoolDownBeforeLose : MonoBehaviour
{
    public UI_Coin m_UiCoin;
    public Text TimeCoolDownTxt;

    public Button Test_Btn;
    public Button TapToSkip_Btn;
    public Image Filled_Radius;

    public CritterPopUpBeforeLose slot_01;
    public CritterPopUpBeforeLose slot_02;
    public CritterPopUpBeforeLose slot_03;
    public CritterPopUpBeforeLose slot_04;

    public Button Heal_Hp_With_Coin_Slot_01;
    public Button Heal_Hp_With_Coin_Slot_02;
    public Button Heal_Hp_With_Coin_Slot_03;
    public Button Heal_Hp_With_Coin_Slot_04;

    public Button Heal_Hp_With_Ads_Slot_01;
    public Button Heal_Hp_With_Ads_Slot_02;
    public Button Heal_Hp_With_Ads_Slot_03;
    public Button Heal_Hp_With_Ads_Slot_04;

    public List<CritterPopUpBeforeLose> List_Critter_PopUp_Before_Lose;

    public int Coin;
    public UI_Battle m_UI_Battle;

    public GameObject PopUpLose;

    public int CoinHealHP;
    public int DeviceHP;

    public int Time;
    public int CurTime = 0;
    public int StartTime = 0;

    public bool isGameOver = false;
    public bool isHealing = false;
    private void Awake()
    {
        Test_Btn.onClick.AddListener(ShowPopUpTimeCoolDown);
        TapToSkip_Btn.onClick.AddListener(OnClickTapToSkip);

        Heal_Hp_With_Coin_Slot_01.onClick.AddListener(OnclickHealBtn_01);
        Heal_Hp_With_Coin_Slot_02.onClick.AddListener(OnclickHealBtn_02);
        Heal_Hp_With_Coin_Slot_03.onClick.AddListener(OnclickHealBtn_03);
        Heal_Hp_With_Coin_Slot_04.onClick.AddListener(OnclickHealBtn_04);

        Heal_Hp_With_Ads_Slot_01.onClick.AddListener(ShowAdsReward1);
        Heal_Hp_With_Ads_Slot_02.onClick.AddListener(ShowAdsReward2);
        Heal_Hp_With_Ads_Slot_03.onClick.AddListener(ShowAdsReward3);
        Heal_Hp_With_Ads_Slot_04.onClick.AddListener(ShowAdsReward4);
    }
    private void Start()
    {
        StartTime = Time;
    }
    public void ShowPopUpTimeCoolDown()
    {
        if (!isHealing)
        {
            TutorialManager.Instance.DeSpawn();
            if (!DataPlayer.GetIsHealHP())
            {
             //   TutorialManager.Instance.SpawnHandUIBattle(transform, new Vector3(20, -170));
               /* Heal_Hp_With_Coin_Slot_01.gameObject.AddComponent<Canvas>();
                Heal_Hp_With_Coin_Slot_01.gameObject.GetComponent<Canvas>().overrideSorting = true;
                Heal_Hp_With_Coin_Slot_01.gameObject.AddComponent<GraphicRaycaster>();
                Heal_Hp_With_Coin_Slot_01.gameObject.GetComponent<Canvas>().sortingLayerName = "UI";
                Heal_Hp_With_Coin_Slot_01.gameObject.GetComponent<Canvas>().sortingOrder = 9999;*/
            }

            Time = StartTime;
            isHealing = true;   

            TimeCoolDownTxt.text = Time.ToString();
            this.gameObject.SetActive(true);
            Filled_Radius.fillAmount = 1;
            StartCoroutine(IE_TimeCoolDown());
            LoadCritterPopUpBeforeLose();

            for (int j = 0; j < Controller.Instance.healingData.E_Blood.Count; j++)
            {
                for (int i = 0; i < List_Critter_PopUp_Before_Lose.Count; i++)
                {
                    if (List_Critter_PopUp_Before_Lose[i].type != ECharacterType.NONE)
                    {
                        if (List_Critter_PopUp_Before_Lose[i].Rarity - 1 == j)
                        {
                            CoinHealHP = Controller.Instance.healingData.E_Blood[j].Cost_HP_Lowest;
                            CoinHealHP += CoinHealHP / 2;
                            List_Critter_PopUp_Before_Lose[i].TxtCoinHealing.text = CoinHealHP.ToString();
                            List_Critter_PopUp_Before_Lose[i].skeleton.AnimationState.SetAnimation(0, "Idle", true);
                        }
                    }
                }
            }
        }
        else
        {
            isGameOver = true;
        }
    }
    IEnumerator IE_TimeCoolDown()
    {
        while (Time > CurTime)
        {
            yield return new WaitForSeconds(1);
            Time--;
            TimeCoolDownTxt.text = "0" + Time.ToString();
            Filled_Radius.fillAmount -= 0.1f;
        }
        if (CurTime >= Time)
        {

            //     ActivePopUpLose();

            if (DataPlayer.GetIsCheckDoneTutorial())
            {
                isGameOver = true;
                this.gameObject.SetActive(false);
                UI_Home.Instance.uI_Battle.isLose = "Thua";
                UI_Home.Instance.uI_Battle.IntroNextPopUp();
                Time = StartTime;
            }

        }
    }
    public void ActivePopUpLose()
    {
        PopUpLose.gameObject.SetActive(true);
        // AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music, AudioManager.instance.SoundEffectPopUpLose);
        AudioManager.instance.BG_In_Game_Music.loop = false;
        PlayAnimation("Appear", false, DoSomethingWhenAppear);
        if (this.gameObject.activeInHierarchy)
        {
            StartCoroutine(IE_AnimPopUpLose());
        }
    }
    public void LoadCritterPopUpBeforeLose()
    {
        Debug.Log("da load crt");
        ResetCritterPopUp();
        initCritter();

        if (m_UI_Battle.L_AllidELements.Count >= 1)
        {
            if (slot_01 != null)
            {
                if (m_UI_Battle.L_AllidELements[0].Type != ECharacterType.NONE)
                    slot_01.SetData(m_UI_Battle.L_AllidELements[0]);
            }
        }
        if (m_UI_Battle.L_AllidELements.Count >= 2)
        {
            if (slot_02 != null)
            {
                if (m_UI_Battle.L_AllidELements[1].Type != ECharacterType.NONE)
                    slot_02.SetData(m_UI_Battle.L_AllidELements[1]);
            }
        }

        if (m_UI_Battle.L_AllidELements.Count >= 3)
        {
            if (slot_03 != null)
            {
                if (m_UI_Battle.L_AllidELements[2].Type != ECharacterType.NONE)
                    slot_03.SetData(m_UI_Battle.L_AllidELements[2]);
            }
        }

        if (m_UI_Battle.L_AllidELements.Count >= 4)
        {
            if (slot_04 != null)
            {
                if (m_UI_Battle.L_AllidELements[3].Type != ECharacterType.NONE)
                    slot_04.SetData(m_UI_Battle.L_AllidELements[3]);
            }
        }


        for (int i = 0; i < List_Critter_PopUp_Before_Lose.Count; i++)
        {
            if (List_Critter_PopUp_Before_Lose[i].type == ECharacterType.NONE)
            {
                Debug.Log(i);
                List_Critter_PopUp_Before_Lose[i].gameObject.SetActive(false);
            }
            else
            {
                List_Critter_PopUp_Before_Lose[i].gameObject.SetActive(true);
            }
        }
    }
    public void ResetCritterPopUp()
    {
        if (slot_01 != null)
            slot_01 = null;
        if (slot_02 != null)
            slot_02 = null;
        if (slot_03 != null)
            slot_03 = null;
        if (slot_04 != null)
            slot_04 = null;

        for (int i = 0; i < List_Critter_PopUp_Before_Lose.Count; i++)
        {
            List_Critter_PopUp_Before_Lose[i].type = ECharacterType.NONE;
        }
    }
    private void initCritter()
    {
        if (slot_01 == null)
            slot_01 = List_Critter_PopUp_Before_Lose[0];
        if (slot_02 == null)
            slot_02 = List_Critter_PopUp_Before_Lose[1];
        if (slot_03 == null)
            slot_03 = List_Critter_PopUp_Before_Lose[2];
        if (slot_04 == null)
            slot_04 = List_Critter_PopUp_Before_Lose[3];
    }
    public void OnClickTapToSkip()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            this.gameObject.SetActive(false);
            ActivePopUpLose();
            LoadEnemyBasedElement();
            LoadAllidBasedElement();
            isHealing = false;
            Time = StartTime;
            isGameOver = true;

            Coin = 0;
            UI_Home.Instance.uI_Battle.isLose = "Thua";
            UI_Home.Instance.uI_Battle.IntroNextPopUp();
        }

    }
    void DoSomethingWhenAppear()
    {
        PopUpLose.GetComponent<PopUpLose>().skeleton.AnimationState.SetAnimation(0, "Idle", true);
    }
    IEnumerator IE_AnimPopUpLose()
    {
        yield return new WaitForSeconds(1f);

    }
    public void LoadEnemyBasedElement()
    {
        for (int i = 0; i < m_UI_Battle.L_EnemyBaseElement.Count; i++)
        {
            m_UI_Battle.L_EnemyBaseElement[i].Init();
            m_UI_Battle.L_EnemyBaseElement[i].DeadImg.gameObject.SetActive(false);
        }
    }
    public void LoadAllidBasedElement()
    {
        for (int i = 0; i < m_UI_Battle.L_AllidELements.Count; i++)
        {
            for (int j = 0; j < DataPlayer.GetListAllid().Count; j++)
            {
                if (m_UI_Battle.L_AllidELements[i].Type == DataPlayer.GetListAllid()[j].Type &&
                    m_UI_Battle.L_AllidELements[i].ID == DataPlayer.GetListAllid()[j].ID)
                {
                    m_UI_Battle.L_AllidELements[i].Init(DataPlayer.GetListAllid()[j]);
                }
            }
        }
    }

    private void ShowAdsReward1()
    {
        AdStatus adstatus = SDKDGManager.Instance.AdsManager.ShowRewardedAdsStatus(() =>
        {
            HealHpWithAds(slot_01);
            Controller.Instance.CountTime = 0;
            Debug.Log("Healing at time cooldown");
        }, "Healing at time cooldown");
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
    private void ShowAdsReward2()
    {
        AdStatus adstatus = SDKDGManager.Instance.AdsManager.ShowRewardedAdsStatus(() =>
        {
            HealHpWithAds(slot_02);
            Controller.Instance.CountTime = 0;
            Debug.Log("Healing at time cooldown");
        }, "Healing at time cooldown");
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
    private void ShowAdsReward3()
    {
        AdStatus adstatus = SDKDGManager.Instance.AdsManager.ShowRewardedAdsStatus(() =>
        {
            HealHpWithAds(slot_03);
            Controller.Instance.CountTime = 0;
            Debug.Log("Healing at time cooldown");
        }, "Healing at time cooldown");
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
    private void ShowAdsReward4()
    {
        AdStatus adstatus = SDKDGManager.Instance.AdsManager.ShowRewardedAdsStatus(() =>
        {
            HealHpWithAds(slot_04);
            Controller.Instance.CountTime = 0;
            Debug.Log("Healing at time cooldown");
        }, "Healing at time cooldown");
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








    public void OnclickHealBtn_01()
    {
        Heal_HP(slot_01);
        TutorialManager.Instance.DeSpawn();
        DataPlayer.SetIsHealHP(true);
    }
    public void OnclickHealBtn_02()
    {
        Heal_HP(slot_02);
        DataPlayer.SetIsHealHP(true);
    }
    public void OnclickHealBtn_03()
    {
        Heal_HP(slot_03);
        DataPlayer.SetIsHealHP(true);
    }
    public void OnclickHealBtn_04()
    {
        Heal_HP(slot_04);
        DataPlayer.SetIsHealHP(true);
    }
    public void OnclickHealBtnAds_01()
    {
        HealHpWithAds(slot_01);
    }
    public void OnclickHealBtnAds_02()
    {
        HealHpWithAds(slot_02);
    }
    public void OnclickHealBtnAds_03()
    {
        HealHpWithAds(slot_03);
    }
    public void OnclickHealBtnAds_04()
    {
        HealHpWithAds(slot_04);
    }

    int curCoin;
    public void Heal_HP(CritterPopUpBeforeLose critterPopUpBeforeLose)
    {
        curCoin = DataPlayer.GetCoin();
        if (curCoin < CoinHealHP)
        {
            gameObject.SetActive(false);
            OnClickTapToSkip();
            popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.COIN;
            popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
            UI_Home.Instance.uI_Battle.isLose = "Thua";
            UI_Home.Instance.uI_Battle.IntroNextPopUp();
            return;
        }
        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(critterPopUpBeforeLose.type);
        int hp = Controller.Instance.enemyData.GetHPEmemy(critterPopUpBeforeLose.type) / DeviceHP;
        if (critterPopUpBeforeLose != null)
        {
            critterPopUpBeforeLose.Hp_Bar.value = hp;
        }
        for (int i = 0; i < m_UI_Battle.L_AllidELements.Count; i++)
        {
            if (m_UI_Battle.L_AllidELements[i].Type == critterPopUpBeforeLose.type && m_UI_Battle.L_AllidELements[i].ID == critterPopUpBeforeLose.ID)
            {
                m_UI_Battle.L_AllidELements[i].HP = hp;
                m_UI_Battle.L_AllidELements[i].TxtHP.text = hp.ToString();
                m_UI_Battle.L_AllidELements[i].HP_Bar.value = hp;
                m_UI_Battle.L_AllidELements[i].DeadImage.gameObject.SetActive(false);
                m_UI_Battle.L_AllidELements[i].PurchaseBtn.enabled = true;
                DataPlayer.SetHP(hp, m_UI_Battle.L_AllidELements[i].ThisElementData);
            }
        }

        Coin += CoinHealHP;
        curCoin -= CoinHealHP;
        Debug.Log(curCoin);
        DataPlayer.SetCoin(curCoin);
        UI_Home.Instance.m_UICoinManager.SetTextCoin();
        m_UI_Battle.switchAllidbase();

        this.gameObject.SetActive(false);
        Time = 10;
        Filled_Radius.fillAmount = 1;
    }

    public void HealHpWithAds(CritterPopUpBeforeLose critterPopUpBeforeLose)
    {
        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(critterPopUpBeforeLose.type);
        int hp = Controller.Instance.enemyData.GetHPEmemy(critterPopUpBeforeLose.type) / DeviceHP;
        if (critterPopUpBeforeLose != null)
        {
            critterPopUpBeforeLose.Hp_Bar.value = hp;
        }
        for (int i = 0; i < m_UI_Battle.L_AllidELements.Count; i++)
        {
            if (m_UI_Battle.L_AllidELements[i].Type == critterPopUpBeforeLose.type && m_UI_Battle.L_AllidELements[i].ID == critterPopUpBeforeLose.ID)
            {
                m_UI_Battle.L_AllidELements[i].HP = hp;
                m_UI_Battle.L_AllidELements[i].TxtHP.text = hp.ToString();
                m_UI_Battle.L_AllidELements[i].HP_Bar.value = hp;
                m_UI_Battle.L_AllidELements[i].DeadImage.gameObject.SetActive(false);
                m_UI_Battle.L_AllidELements[i].PurchaseBtn.enabled = true;
                DataPlayer.SetHP(hp, m_UI_Battle.L_AllidELements[i].ThisElementData);
            }
        }
        m_UI_Battle.switchAllidbase();
        this.gameObject.SetActive(false);
        Time = 10;
        Filled_Radius.fillAmount = 1;
    }
    private System.Action EventCallbackAnimationComplete;

    public void PlayAnimation(string _animationName, bool _Loop, System.Action _animationCallback)
    {
        EventCallbackAnimationComplete = null;
        PopUpLose.GetComponent<PopUpLose>().skeleton.AnimationState.SetAnimation(0, _animationName, _Loop);

        if (_animationCallback == null) return;
        if (PopUpLose.GetComponent<PopUpLose>().skeleton.AnimationState.GetCurrent(0).Animation.Name == "Appear")
        {
            EventCallbackAnimationComplete = _animationCallback;
            PopUpLose.GetComponent<PopUpLose>().skeleton.AnimationState.Complete += Animation_Oncomplete;
        }
        else
        {
            PopUpLose.GetComponent<PopUpLose>().skeleton.AnimationState.Complete -= Animation_Oncomplete;
        }
    }
    private void Animation_Oncomplete(TrackEntry _trackEntry)
    {

        if (_trackEntry.Animation.Name == "Appear")
        {
            EventCallbackAnimationComplete?.Invoke();
            PopUpLose.GetComponent<PopUpLose>().skeleton.AnimationState.Complete -= Animation_Oncomplete;
        }
    }
    private void OnDisable()
    {
    }
}
