using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;
using System;
public class UI_Battle : MonoBehaviour
{
    public Sprite[] ArrBG;
    public Image BG;

    public Transform PosIntroEnemyBase;
    public Transform PosIntroAllidBase;
    public Transform PosMergeCard;
    public RectTransform PosIntroGroupEnemyIntro;

    public GameObject Eventory; // 3 allid (bottom)
    public GameObject TeamEnemyBased;
    public GameObject GroupEnemyIntro;

    public GameObject PosAllidBase;
    public GameObject PosEnemyBase;
    public GameObject PosEnemyWhenPartEnemy;

    public GameObject AllidBasedItem;
    public GameObject SLOT_ALLID;
    public GameObject Soldier;

    public GameObject PopUpVictory;
    public GameObject PopUpLose;
    public GameObject PopUpCatched;
    public GameObject UI_Catch_Pending;
    public GameObject UI_Catch;
    public GameObject EffectActive;
    public GameObject TimeCoolDownBeforeLose;
    public GameObject TextContent;
    public GameObject EffectHidden;
    public GameObject Test;

    public SkeletonGraphic AllidBase;
    public SkeletonGraphic EnemyBase;

    public AllidELement SLOT1;
    public AllidELement SLOT2;
    public AllidELement SLOT3;
    public AllidELement SLOT4;

    public Button OutUIBattleBtn;

    public Button SLOT1_BTN;
    public Button SLOT2_BTN;
    public Button SLOT3_BTN;
    public Button SLOT4_BTN;

    public EnemyBased enemyBased;
    public RuleController m_RuleController;

    public ToastManager m_ToastManager;
    public Character m_character;

    public PopUpCatchPending m_PopUpCatchPending;
    public PopUpCatchDone m_PopUpCatchDone;

    public int Catch_Chance;
    public int Catch_Chance_Plus = 0;
    public int Sum_Catch;
    public bool IsCatchDone = false;

    Dictionary<EnemyBased, float> keyValuePairs = new Dictionary<EnemyBased, float>();
    public AutoManager m_Auto;

    public Action A_unlockLevel;

    public List<AllidELement> L_AllidELements = new List<AllidELement>();
    public List<EnemyBaseElement> L_EnemyBaseElement = new List<EnemyBaseElement>();
    public List<EnemyBased> L_EnemyBase = new List<EnemyBased>();

    public Transform EnemyMoveToPosAllidBase;
    public Transform AllidMoveToPosEnemyBase;

    public GameObject BG_Shake;
    public GameObject SpinWheel;

    public AnimationCurve CurveMoveCardCritter;
    public string isLose;

    public GameObject Hand;

    public int CoinWhenEnableUiBattle;

    public GameObject ParentAllid1;
    public GameObject ParentAllid2;
    public GameObject ParentAllid3;
    public GameObject ParentAllid4;

    public AnimationCurve AnimCurveMerge;
    public GameObject objpopUp;
    private void OnEnable()
    {
        A_unlockLevel += UnlockLevel;
        StartCoroutine(IE_delayIntroAllid());
        CoinWhenEnableUiBattle = DataPlayer.GetCoin();
        Test.gameObject.SetActive(true);
        TextContent.SetActive(true);

        if (!DataPlayer.getIsCheckTapToAllid())
        {
            OutUIBattleBtn.interactable = false;
            m_Auto.Auto.interactable = false;
        }
        else
        {
            OutUIBattleBtn.interactable = true;
            m_Auto.Auto.interactable = true;
        }
        UpdateBG();
        //objpopUp.SetActive(false);
        StartCoroutine(IE_delay());
        if (UI_Home.Instance.m_Toast)
        {
            if (UI_Home.Instance.m_Toast.Toast.gameObject.activeInHierarchy)
            {
                UI_Home.Instance.m_Toast.Toast.gameObject.SetActive(false);
            }
        }
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            DisableTut();
            //  L_AllidELements[1].GetComponent<Canvas>().sortingOrder = 100;
        }
        else
        {
            m_Auto.Auto.interactable = true;
        }
        VirtualCamera.Instance.transform.position = new Vector3(VirtualCamera.Instance.transform.position.x, VirtualCamera.Instance.transform.position.y, -10);
    }
    int time = 6;
    public float curtime = 0;
    private void Update()
    {
        if (curtime < time)
        {
            curtime += Time.deltaTime;
        }

        if (curtime >= time)
        {
            for (int i = 0; i < L_AllidELements.Count; i++)
            {
                if (L_AllidELements[i].Type != ECharacterType.NONE && L_AllidELements[i].gameObject.activeInHierarchy)
                {
                    L_AllidELements[i].SetVibrate();
                }
            }
            curtime = 0;
        }

        /* for (int i = 0; i < L_AllidELements.Count; i++)
         {
             if (L_AllidELements[i].gameObject.activeInHierarchy)
             {
                 L_AllidELements[i].Container.transform.DOShakePosition(0.5f, 90, 10, 90, true);
             }
         }*/
    }
    void DisableTut()
    {
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            SLOT2_BTN.interactable = false;
            SLOT3_BTN.interactable = false;
            SLOT4_BTN.interactable = false;
            m_Auto.Auto.interactable = false;
        }
    }
    IEnumerator IE_delay()
    {
        yield return new WaitForSeconds(0.5f);
        player.Instance.gameObject.GetComponent<Collider2D>().enabled = false;
        GetCurrencyWithAds.Instance.obj.SetActive(false);
        for (int i = 0; i < BagManager.Instance.m_RuleController.L_enemy2.Count; i++)
        {
            BagManager.Instance.m_RuleController.L_enemy2[i].isCanAI = true;
        }

        player.Instance.ShadowBag.SetActive(true);
        player.Instance.isPurchaseBagBtn = false;
        player.Instance.Option.SetActive(false);
        player.Instance.CloseBag();
        //   player.Instance.Menu.SetActive(false);
        /* if(DataPlayer.GetIsCheckDoneTutorial())
         {
             m_Auto.Auto.interactable = true;
             OutUIBattleBtn.interactable = true;
         }*/
    }
    public void resetTeamenemy()
    {
        var obj = TeamEnemyBased.GetComponentsInChildren<EnemyBaseElement>();
        for (int i = 0; i < obj.Length; i++)
        {
            Destroy(obj[i].gameObject);
        }
        L_EnemyBaseElement.Clear();
    }
    void UpdateBG()
    {
        BG.transform.localPosition = Vector3.zero;
        switch (DataPlayer.getCurLevel())
        {
            case 0:
                BG.sprite = ArrBG[0];
                break;
            case 1:
                BG.sprite = ArrBG[0];
                break;
            case 2:
                BG.sprite = ArrBG[1];
                break;
            case 3:
                BG.sprite = ArrBG[2];
                break;
            case 4:
                BG.sprite = ArrBG[3];
                break;
            case 5:
                BG.sprite = ArrBG[4];
                break;
            case 6:
                BG.sprite = ArrBG[5];
                break;
            case 7:
                BG.sprite = ArrBG[6];
                break;
            case 8:
                BG.sprite = ArrBG[7];
                break;
        }
    }
    public void ActiveTutorial()
    {
        if (!DataPlayer.getIsCheckTapToAllid())
        {
            Controller.Instance.exampObj.gameObject.SetActive(true);
            string str = I2.Loc.LocalizationManager.GetTranslation("KEY_TUT_ATTACK");
            TutorialManager.Instance.SpawnHandUIBattle(SLOT_ALLID.transform.GetChild(0), new Vector3(30, -70));
            ExampleStoryTut.Instance.SetText(str);
        }
    }
    public void InitEnemy(List<EnemyBaseElement> lstEnemy)
    {
        L_EnemyBaseElement = new List<EnemyBaseElement>();
        startPosShake = BG_Shake.transform.position;
        for (int i = 0; i < lstEnemy.Count; i++)
        {
            GameObject obj = Instantiate(lstEnemy[i].gameObject, TeamEnemyBased.transform);
            obj.GetComponent<RectTransform>().localScale = Vector3.one;
            L_EnemyBaseElement.Add(obj.GetComponent<EnemyBaseElement>());
        }
        SpawnEnemyBase();
    }

    private void OnDisable()
    {
        if (GetCurrencyWithAds.Instance.CurTimeActive <= 0)
        {
            GetCurrencyWithAds.Instance.obj.SetActive(true);
        }
        //  player.Instance.BagBtn.SetActive(true);

        ResetEnemyBasedELement();
        resetTeamenemy();
        A_unlockLevel -= UnlockLevel;
        L_EnemyBaseElement.Clear();

        OutUIBattleBtn.gameObject.SetActive(true);
        m_Auto.Auto.gameObject.SetActive(true);
        CheckAttack = false;
        enemyBased.CoinDrop = 0;
        UI_Home.Instance.m_UIScreen.EnableButton();

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
        m_Auto.AutoOff = true;
        Test.gameObject.SetActive(false);
        enemyBased.TextDamageAnim.SetActive(false);
        //      objpopUp.SetActive(true);
    }
    private void Awake()
    {
        SLOT1_BTN.onClick.AddListener(onclickButtonSlot1);
        SLOT2_BTN.onClick.AddListener(onclickButtonSlot2);
        SLOT3_BTN.onClick.AddListener(onclickButtonSlot3);
        SLOT4_BTN.onClick.AddListener(onclickButtonSlot4);

        OutUIBattleBtn.onClick.AddListener(Out_UI_Battle_Btn);
        DisableButton();

        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            Destroy(SLOT1_BTN.GetComponent<GraphicRaycaster>());
            Destroy(SLOT1_BTN.GetComponent<Canvas>());
        }
    }
    void onclickButtonSlot1()
    {
        DisableButton();
        TutorialManager.Instance.DeSpawn();
        DataPlayer.SetIsCheckTapToAllid(true);
        isTap = true;
    }
    void onclickButtonSlot2()
    {
        DisableButton();
        isTap = true;
    }
    void onclickButtonSlot3()
    {
        DisableButton();
        isTap = true;
    }
    void onclickButtonSlot4()
    {
        DisableButton();
        isTap = true;
    }

    void Out_UI_Battle_Btn()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (m_RuleController)
                m_RuleController.UnStopEnemy();
            ResetEnemyBasedELement();
            ResetAllidBaseElement();
            TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().isHealing = false;
            TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().LoadEnemyBasedElement();
            this.gameObject.SetActive(false);
            PopUpVictory.gameObject.SetActive(false);
            PopUpLose.gameObject.SetActive(false);
            m_Auto.AutoOn = false;
            m_Auto.ButtonAuto_Txt.text = "Auto: Off";
            if (Controller.Instance.isShowInter)
            {
                Controller.Instance.ShowInterstitial();
            }
            OutBattle();
            isLose = null;
        }

    }
    public void OutBattle()
    {
        gameObject.SetActive(false);
        UI_Home.Instance.ActiveUIHome();
        GridLayoutGroup a = SLOT_ALLID.GetComponent<GridLayoutGroup>();
        GridLayoutGroup b = TeamEnemyBased.GetComponent<GridLayoutGroup>();
        a.spacing = new Vector2(-160, 0);
        b.spacing = new Vector2(-160, 0);

        AllidBase.transform.localScale = new Vector3(-2.5f, 2.5f, 2.5f);

        SLOT_ALLID.transform.localScale = Vector3.one;
        TeamEnemyBased.transform.localScale = Vector3.one;

        //  TeamEnemyBased.transform.localPosition = new Vector3(0, -370, 0);
        //  SLOT_ALLID.transform.localPosition = new Vector3(0, -390, 0);

        for (int i = 0; i < enemyBased.L_Coin.Count; i++)
        {
            Destroy(enemyBased.L_Coin[i].gameObject);
        }
        enemyBased.L_Coin.Clear();
        if (!m_RuleController)
        {
            return;
        }
        for (int i = 0; i < m_RuleController.L_enemy.Count; i++)
        {
            m_RuleController.L_enemy[i].m_MoveEnemy.agent.maxSpeed = 3.5f;
        }
    }

    public void DestroyCanvas()
    {
        if (m_character is Enemy)
        {
            //    m_RuleController.Remove(m_character);
            //Destroy(m_character.GetComponent<Enemy>().canvas.gameObject);
        }
        else if (m_character is BossPatrol)
        {
            // m_RuleController.Remove(m_character);
        }
    }
    public void ResetPosAllidBase_And_EnemyBase()
    {
        AllidBase.transform.position = PosIntroAllidBase.transform.position;
        EnemyBase.transform.position = PosIntroEnemyBase.transform.position;
        /*        GroupEnemyIntro.GetComponent<RectTransform>().rect.position
        */
    }
    public void resetPosGroupEnemyIntro()
    {

    }
    public void ShakeBackGround()
    {
        BG_Shake.transform.DOShakePosition(0.3f, 500, 600, 90f);
    }
    Vector3 startPosShake;
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void UpdateViewEnemyBaseElement()
    {
        for (int i = 0; i < L_EnemyBaseElement.Count; i++)
        {
            if (enemyBased.ID == L_EnemyBaseElement[i].ID &&
                enemyBased.Type == L_EnemyBaseElement[i].Type)
            {
                L_EnemyBaseElement[i].HP = enemyBased.HP;
                if (m_character is Enemy)
                {
                    L_EnemyBaseElement[i].SetHP(L_EnemyBaseElement[i].HP, Controller.Instance.enemyData.GetHPEmemy(enemyBased.Type));
                }
                else if (m_character is BossPatrol)
                {
                    L_EnemyBaseElement[i].SetHP(L_EnemyBaseElement[i].HP, Controller.Instance.enemyData.GetHPBoss(enemyBased.Type));
                }
                L_EnemyBaseElement[i].TxtHP.text = enemyBased.HP.ToString();
                if (L_EnemyBaseElement[i].HP < 1)
                {
                    L_EnemyBaseElement[i].HP = 0;
                    L_EnemyBaseElement[i].DeadImg.gameObject.SetActive(true);
                    L_EnemyBaseElement[i].TxtHP.text = 0.ToString();
                    L_EnemyBaseElement[i].SetHP(0, Controller.Instance.enemyData.GetHPEmemy(L_EnemyBaseElement[i].Type));
                }

                StartCoroutine(IE_HiddenEnemyTakeDamageEffect(L_EnemyBaseElement[i]));
            }
        }
    }

    public void ShowEffectAvatar()
    {
        for (int i = 0; i < L_EnemyBaseElement.Count; i++)
        {
            if (enemyBased.ID == L_EnemyBaseElement[i].ID &&
                enemyBased.Type == L_EnemyBaseElement[i].Type)
            {
                L_EnemyBaseElement[i].Effect.gameObject.SetActive(true);
                AudioManager.instance.PlaySound(AudioManager.instance.Slash);
            }
        }
    }

    public void UpdateViewAllidBaseElement()
    {
        for (int i = 0; i < L_AllidELements.Count; i++)
        {
            if (AllidBase.GetComponent<AllidBase>().Type == L_AllidELements[i].Type &&
                AllidBase.GetComponent<AllidBase>().ID == L_AllidELements[i].ID)
            {
                L_AllidELements[i].Effect.SetActive(true);
                StartCoroutine(IE_HiddenAllidTakeDamageEffect(L_AllidELements[i]));
                if (L_AllidELements[i].HP < 1)
                {
                    L_AllidELements[i].PurchaseBtn.gameObject.SetActive(false);
                    L_AllidELements[i].PurchaseBtn.enabled = false;
                    L_AllidELements[i].DeadImage.gameObject.SetActive(true);
                    DisableButton(i + 1);
                }
            }
        }
    }

    IEnumerator IE_HiddenEnemyTakeDamageEffect(EnemyBaseElement enemyBaseElement)
    {
        yield return new WaitForSeconds(0.3f);
        enemyBaseElement.Effect.SetActive(false);
    }

    IEnumerator IE_HiddenAllidTakeDamageEffect(AllidELement allidELement)
    {
        yield return new WaitForSeconds(0.3f);
        allidELement.Effect.SetActive(false);
    }

    public void MoveToPosEnemyWhenPartEnemy()
    {
        enemyBased.MoveToPosCatchedCritter();
    }

    public bool isbuttonNothankWithLose = false;
    public bool isbuttonNothankWithWin = false;
    public void GameOver()
    {
        int dem = 0;
        for (int i = 0; i < L_AllidELements.Count; i++)
        {
            if (L_AllidELements[i].HP == 0)
            {
                dem++;
                Debug.Log("da chet");
            }
        }
        if (dem >= L_AllidELements.Count)
        {
            TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().ShowPopUpTimeCoolDown();
            if (TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().isGameOver)
            {
                isLose = "Thua";
                Debug.Log("Thua roi !!!!");
                m_Auto.AutoOn = false;
                m_Auto.ButtonAuto_Txt.text = "Auto: Off";
                DisableButton();
                isbuttonNothankWithLose = true;
                for (int i = 0; i < L_AllidELements.Count; i++)
                {
                    L_AllidELements[i].PurchaseBtn.enabled = false;
                }

                TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().ActivePopUpLose();

                if (DataPlayer.GetIsCheckDoneTutorial())
                {
                    IntroNextPopUp();
                }
                /* else
                 {
                     OutBattle();
                     DataPlayer.SetIsCheckDoneTutorial(true);
                     LoadSceneManager.Instance.LOAD_MAP_01();
                     DataPlayer.AddListLevel(1);
                     DataPlayer.SetCurLevel(1);
                     UI_Home.Instance.m_UIselectMap.CurrentLevel();
                     DataPlayer.SetIsTapToBackUITeam(true);
                     Debug.LogError("4. true");

                     *//*  DataPlayer.SetCoin(100);
                       UI_Home.Instance.m_UICoinManager.SetTextCoin();*/
                /*UI_Home.Instance.m_UIselectMap.gameObject.SetActive(true);*//*
            }*/
                /*                AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music, AudioManager.instance.SoundEffectPopUpLose);
                                AudioManager.instance.BG_In_Game_Music.loop = false;
                                TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().PopUpLose.GetComponent<PopUpLose>()
                                    .skeleton.AnimationState.SetAnimation(0, "Appear", false);
                                StartCoroutine(IE_ChangeAnim());*/

                TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().isGameOver = false;
            }
        }
    }

    int count = 0;
    public bool isPartEnemy = false;
    public GameObject fakescale;

    IEnumerator IE_delayIntroAllid()
    {
        yield return new WaitForSeconds(1f);
        IntroAllidAndEnemyBased();
    }
    private void IntroAllidAndEnemyBased()
    {
        GridLayoutGroup a = SLOT_ALLID.GetComponent<GridLayoutGroup>();
        GridLayoutGroup b = TeamEnemyBased.GetComponent<GridLayoutGroup>();
        DOTween.To(() => -160, x =>
        {
            b.spacing = new Vector2(x, 0);
        }, 10, 0.3f).OnComplete(() =>
        {
            DOTween.To(() => -160, x =>
            {
                a.spacing = new Vector2(x, 0);
            }, 10, 0.3f).OnComplete(() =>
            {
            });
        });
    }
    public void GameWin()
    {
        count = 0;
        Debug.Log("Count List Enemy: " + L_EnemyBaseElement.Count);
        for (int i = 0; i < L_EnemyBaseElement.Count; i++)
        {
            if (L_EnemyBaseElement[i].HP < 1)
            {
                count++;
                Debug.Log(count);
            }
        }
        if (count == L_EnemyBaseElement.Count)
        {
            Debug.Log("Part Enemy");
            isPartEnemy = true;
            A_unlockLevel?.Invoke();
            //  m_PopUpCatchPending.gameObject.SetActive(true);
            IntroNextPopUp();
            isLose = "Win";
        }
    }
    public void IntroNextPopUp()
    {
        OutUIBattleBtn.gameObject.SetActive(false);
        AllidBase.transform.DOJump(AllidBase.GetComponent<AllidBase>().PosDead.position, 300, 1, 0.2f).OnUpdate(() =>
        {
            AllidBase.GetComponent<AllidBase>().UpdateViewShadow();
        }).OnComplete(() =>
        {
            TextContent.SetActive(false);
            GridLayoutGroup a = SLOT_ALLID.GetComponent<GridLayoutGroup>();
            GridLayoutGroup b = TeamEnemyBased.GetComponent<GridLayoutGroup>();
            DOTween.To(() => 10, x =>
            {
                b.spacing = new Vector2(x, 0);

            }, -160, 0.2f).OnStart(() =>
            {
                DOTween.To(() => 10, x =>
                {
                    a.spacing = new Vector2(x, 0);
                }, -160, 0.2f).SetEase(CurveMoveCardCritter);
            }).SetEase(AnimCurveMerge).OnComplete(() =>
            {

            });
            EffectHidden.SetActive(true);
            StartCoroutine(IE_delayActivePopUpPending());
        });
    }
    IEnumerator IE_delayActivePopUpPending()
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(IE_hiddenEffectHidden());
        SLOT_ALLID.transform.DOScale(0, 0.3f)
        .OnStart(() =>
        {
            /* TeamEnemyBased.transform.DOMove(PosMergeCard.position, 0.5f).OnComplete(() =>
             {*/
            TeamEnemyBased.transform.DOScale(0, 0.3f).OnStart(() =>
            {
                SLOT_ALLID.transform.DOScale(0, 0.3f).OnComplete(() => { });
                OutUIBattleBtn.gameObject.SetActive(false);
                m_Auto.Auto.gameObject.SetActive(false);
            });
            StartCoroutine(IE_delayActivePopUp());/*.OnComplete(() =>
            {
              
            });*/
            /*  });*/
        });
    }
    IEnumerator IE_delayActivePopUp()
    {
        yield return new WaitForSeconds(0.1f);
        // bat pop up
        if (isLose == "Win")
        {
            m_PopUpCatchPending.gameObject.SetActive(true);
        }
        else if (isLose == "Thua")
        {
            ActivePopUpCatchDone();
        }
    }
    IEnumerator IE_hiddenEffectHidden()
    {
        yield return new WaitForSeconds(0.7f);
        EffectHidden.SetActive(false);
    }
    public void ActivePopUpCatchDone()
    {
        enemyBased.transform.DOJump(enemyBased.PosDead.position, 300, 1, 0.3f).OnUpdate(() =>
        {
            enemyBased.UpdatePosShadow();
        }).OnComplete(() =>
        {
            m_PopUpCatchDone.gameObject.SetActive(true);
        });
    }
    public void MergeCardEnemyBaseElement_And_ALlidELement()
    {

    }
    public bool isFocusSelectMap;
    public void UnlockLevel()
    {
        if (m_character is BossPatrol)
        {
            if (m_character.GetComponent<BossPatrol>().levelboss == LevelBoss.LV1)
            {
                if (!DataPlayer.GetListLevel().Contains(2))
                {
                    //  UI_Home.Instance.m_UIselectMap.ViewFinderFocus(2);
                    isFocusSelectMap = true;
                }
                DataPlayer.AddListLevel(2);
                Debug.Log("da unlock 2");
            }
            else if (m_character.GetComponent<BossPatrol>().levelboss == LevelBoss.LV2)
            {
                if (!DataPlayer.GetListLevel().Contains(3))
                {
                    //   UI_Home.Instance.m_UIselectMap.ViewFinderFocus(3);
                    isFocusSelectMap = true;
                }
                Debug.Log("da unlock 3");
                DataPlayer.AddListLevel(3);
            }
            else if (m_character.GetComponent<BossPatrol>().levelboss == LevelBoss.LV3)
            {
                if (!DataPlayer.GetListLevel().Contains(4))
                {
                    //   UI_Home.Instance.m_UIselectMap.ViewFinderFocus(3);
                    isFocusSelectMap = true;
                }
                Debug.Log("da unlock 3");
                DataPlayer.AddListLevel(4);
            }
            else if (m_character.GetComponent<BossPatrol>().levelboss == LevelBoss.LV4)
            {
                if (!DataPlayer.GetListLevel().Contains(5))
                {
                    //   UI_Home.Instance.m_UIselectMap.ViewFinderFocus(3);
                    isFocusSelectMap = true;
                }
                Debug.Log("da unlock 3");
                DataPlayer.AddListLevel(5);
            }
            else if (m_character.GetComponent<BossPatrol>().levelboss == LevelBoss.LV5)
            {
                if (!DataPlayer.GetListLevel().Contains(6))
                {
                    //   UI_Home.Instance.m_UIselectMap.ViewFinderFocus(3);
                    isFocusSelectMap = true;
                }
                Debug.Log("da unlock 3");
                DataPlayer.AddListLevel(6);
            }
            else if (m_character.GetComponent<BossPatrol>().levelboss == LevelBoss.LV6)
            {
                if (!DataPlayer.GetListLevel().Contains(7))
                {
                    //   UI_Home.Instance.m_UIselectMap.ViewFinderFocus(3);
                    isFocusSelectMap = true;
                }
                Debug.Log("da unlock 3");
                DataPlayer.AddListLevel(7);
            }
            else if (m_character.GetComponent<BossPatrol>().levelboss == LevelBoss.LV7)
            {
                if (!DataPlayer.GetListLevel().Contains(8))
                {
                    //   UI_Home.Instance.m_UIselectMap.ViewFinderFocus(3);
                    isFocusSelectMap = true;
                }
                Debug.Log("da unlock 3");
                DataPlayer.AddListLevel(8);
            }
        }
    }
    bool isTap;
    public void RemoveEnemy()
    {
        if (IsCatchDone)
        {
            //  m_RuleController.Remove(((Enemy)m_character));
            m_RuleController.HiddenEnemy(m_character);
            //Destroy(((Enemy)m_character).canvas.gameObject);
            //  Destroy(((Enemy)m_character));
            //    m_RuleController.ActiveBoss();
            Debug.Log("da remove");
            IsCatchDone = false;
        }

        // if (m_character is Enemy)
    }

    public void RemoveBoss()
    {
        if (m_character is BossPatrol)
        {
            m_RuleController.RemoveBoss(m_character);
        }
    }
    public void DestroyEnemy()
    {
        Destroy(Soldier);
    }
    public void switchAllidbase()
    {
        if (AllidBase.GetComponent<AllidBase>().HP < 1)
        {
            for (int i = 0; i < L_AllidELements.Count; i++)
            {
                if (AllidBase.GetComponent<AllidBase>().Type == L_AllidELements[i].Type)
                {
                    L_AllidELements[i].PurchaseBtn.enabled = false;
                    break;
                }
            }
            if (gameObject.activeInHierarchy)
                StartCoroutine(IE_ChangeAllidWhenJumdead());
        }
    }

    IEnumerator IE_ChangeAllidWhenJumdead()
    {
        AllidBase.GetComponent<AllidBase>().JumDead();
        yield return 0.3f;
        for (int i = 0; i < L_AllidELements.Count; i++)
        {
            if (L_AllidELements[i].HP > 0)
            {
                EffectActive.SetActive(true);
                StartCoroutine(IE_HiddenEffectActive());
                AllidBase.GetComponent<AllidBase>().SetData(L_AllidELements[i]);
                AllidBase.GetComponent<AllidBase>().skeletonGraphic.AnimationState.SetAnimation(0, "Idle", true);
                break;
            }
        }
        AllidBase.GetComponent<AllidBase>().JumpToStartPosWhenJumpDead();
    }

    IEnumerator IE_HiddenEffectActive()
    {
        yield return new WaitForSeconds(0.2f);
        EffectActive.gameObject.SetActive(false);
    }
    int dem = 0;
    public void switchEnemy()
    {
        for (int i = 0; i < L_EnemyBaseElement.Count; i++)
        {
            for (int j = i + 1; j < L_EnemyBaseElement.Count; j++)
            {
                if (L_EnemyBaseElement[i].HP < 1)
                {
                    dem++;
                    //   enemyBased.JumDead();
                    L_EnemyBaseElement[i].HP = 0;
                    enemyBased.SetData(L_EnemyBaseElement[j]);
                    break;
                }
            }
        }
        if (dem == L_EnemyBaseElement.Count)
        {
            enemyBased.isJumStartPosWhenDieEnemy = true;
        }

    }
    public void SetHPEnemyBase()
    {
        for (int i = 0; i < L_EnemyBaseElement.Count; i++)
        {
            if (enemyBased.ID == L_EnemyBaseElement[i].ID &&
                enemyBased.Type == L_EnemyBaseElement[i].Type)
            {
                L_EnemyBaseElement[i].HP = enemyBased.HP;
                L_EnemyBaseElement[i].TxtHP.text = enemyBased.HP.ToString();

            }
        }
    }


    public void SpawnEnemyBase()
    {
        for (int i = 0; i < L_EnemyBaseElement.Count; i++)
        {
            if (L_EnemyBaseElement[i].HP > 0)
            {
                enemyBased.SetData(L_EnemyBaseElement[i].ICON);
                enemyBased.init(L_EnemyBaseElement[i]);
                enemyBased.PlayAnimation("Idle", true, null);
                break;
            }
        }
    }

    public void ResetEnemyBasedELement()
    {
        if (L_EnemyBaseElement.Count > 1)
        {
            for (int i = 0; i < L_EnemyBaseElement.Count; i++)
            {
                if (L_EnemyBaseElement[i])
                    Destroy(L_EnemyBaseElement[i].gameObject);
            }
        }
        L_EnemyBaseElement.Clear();
    }

    public void SpawnAllidBased(SkeletonDataAsset ICON)
    {
        AllidBase.GetComponent<AllidBase>().m_UIBattle = this;
        AllidBase.skeletonDataAsset = null;
        AllidBase.skeletonDataAsset = ICON;
        AllidBase.Initialize(true);
    }

    public void LoadAlliedBasedElement()
    {
        Debug.Log("da load allid base element");

        for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
        {
            if (DataPlayer.GetListAllid()[i].HP > 0)
            {
                if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                {
                    if (SLOT1 == null)
                    {
                        GameObject tempItem = Instantiate(AllidBasedItem);
                        tempItem.transform.SetParent(SLOT_ALLID.transform.GetChild(i).GetChild(0).GetChild(0));
                        tempItem.transform.localScale = Vector3.one;
                        tempItem.transform.localPosition = Vector3.zero;
                        tempItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 195.3026f);
                        ElementData element = new ElementData();
                        element.Type = DataPlayer.GetListAllid()[i].Type;
                        element.ID = DataPlayer.GetListAllid()[i].ID;
                        element.HP = DataPlayer.GetListAllid()[i].HP;
                        element.Rarity = DataPlayer.GetListAllid()[i].Rarity;
                        element.RangeAttack = DataPlayer.GetListAllid()[i].RangeAttack;

                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(element.Type);

                        tempItem.GetComponent<AllidELement>().PurchaseBtn.enabled = true;
                        tempItem.GetComponent<AllidELement>().PurchaseBtn.gameObject.SetActive(true);


                        tempItem.GetComponent<AllidELement>().Init(element);
                        tempItem.GetComponent<AllidELement>().SetHP(element.HP, Controller.Instance.enemyData.GetHPEmemy(element.Type));
                        switch (i)
                        {
                            case 0:
                                SLOT1 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                            case 1:
                                SLOT2 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                            case 2:
                                SLOT3 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                            case 3:
                                SLOT4 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                        }
                    }
                    else if (SLOT2 == null)
                    {
                        GameObject tempItem = Instantiate(AllidBasedItem);
                        tempItem.transform.SetParent(SLOT_ALLID.transform.GetChild(i).GetChild(0).GetChild(0));
                        tempItem.transform.localScale = Vector3.one;
                        tempItem.transform.localPosition = Vector3.zero;
                        tempItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 195.3026f);
                        ElementData element = new ElementData();
                        element.Type = DataPlayer.GetListAllid()[i].Type;
                        element.ID = DataPlayer.GetListAllid()[i].ID;
                        element.HP = DataPlayer.GetListAllid()[i].HP;
                        element.Rarity = DataPlayer.GetListAllid()[i].Rarity;

                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(element.Type);

                        tempItem.GetComponent<AllidELement>().PurchaseBtn.enabled = true;
                        tempItem.GetComponent<AllidELement>().PurchaseBtn.gameObject.SetActive(true);


                        tempItem.GetComponent<AllidELement>().Init(element);
                        tempItem.GetComponent<AllidELement>().SetHP(element.HP, Controller.Instance.enemyData.GetHPEmemy(element.Type));


                        switch (i)
                        {
                            case 0:
                                SLOT1 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                            case 1:
                                SLOT2 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                            case 2:
                                SLOT3 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                            case 3:
                                SLOT4 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                        }
                    }
                    else if (SLOT3 == null)
                    {

                        GameObject tempItem = Instantiate(AllidBasedItem);
                        tempItem.transform.SetParent(SLOT_ALLID.transform.GetChild(i).GetChild(0).GetChild(0));
                        tempItem.transform.localScale = Vector3.one;
                        tempItem.transform.localPosition = Vector3.zero;
                        tempItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 195.3026f);
                        ElementData element = new ElementData();
                        element.Type = DataPlayer.GetListAllid()[i].Type;
                        element.ID = DataPlayer.GetListAllid()[i].ID;
                        element.HP = DataPlayer.GetListAllid()[i].HP;
                        element.Rarity = DataPlayer.GetListAllid()[i].Rarity;

                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(element.Type);

                        tempItem.GetComponent<AllidELement>().PurchaseBtn.enabled = true;
                        tempItem.GetComponent<AllidELement>().PurchaseBtn.gameObject.SetActive(true);


                        tempItem.GetComponent<AllidELement>().Init(element);
                        tempItem.GetComponent<AllidELement>().SetHP(element.HP, Controller.Instance.enemyData.GetHPEmemy(element.Type));

                        switch (i)
                        {
                            case 0:
                                SLOT1 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                            case 1:
                                SLOT2 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                            case 2:
                                SLOT3 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                            case 3:
                                SLOT4 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                        }
                    }
                    else if (SLOT4 == null)
                    {

                        GameObject tempItem = Instantiate(AllidBasedItem);
                        tempItem.transform.SetParent(SLOT_ALLID.transform.GetChild(i).GetChild(0).GetChild(0));
                        tempItem.transform.localScale = Vector3.one;
                        tempItem.transform.localPosition = Vector3.zero;
                        tempItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 195.3026f);
                        ElementData element = new ElementData();
                        element.Type = DataPlayer.GetListAllid()[i].Type;
                        element.ID = DataPlayer.GetListAllid()[i].ID;
                        element.HP = DataPlayer.GetListAllid()[i].HP;
                        element.Rarity = DataPlayer.GetListAllid()[i].Rarity;

                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(element.Type);

                        tempItem.GetComponent<AllidELement>().PurchaseBtn.enabled = true;
                        tempItem.GetComponent<AllidELement>().PurchaseBtn.gameObject.SetActive(true);


                        tempItem.GetComponent<AllidELement>().Init(element);
                        tempItem.GetComponent<AllidELement>().SetHP(element.HP, Controller.Instance.enemyData.GetHPEmemy(element.Type));

                        switch (i)
                        {
                            case 0:
                                SLOT1 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                            case 1:
                                SLOT2 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                            case 2:
                                SLOT3 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                            case 3:
                                SLOT4 = tempItem.GetComponent<AllidELement>();
                                L_AllidELements.Add(tempItem.GetComponent<AllidELement>());
                                break;
                        }
                    }
                }
            }
        }
        if (SLOT1 == null)
            SLOT_ALLID.transform.GetChild(0).gameObject.SetActive(false);
        if (SLOT2 == null)
            SLOT_ALLID.transform.GetChild(1).gameObject.SetActive(false);
        if (SLOT3 == null)
            SLOT_ALLID.transform.GetChild(2).gameObject.SetActive(false);
        if (SLOT4 == null)
            SLOT_ALLID.transform.GetChild(3).gameObject.SetActive(false);


        if (SLOT1 != null)
            SLOT_ALLID.transform.GetChild(0).gameObject.SetActive(true);
        if (SLOT2 != null)
            SLOT_ALLID.transform.GetChild(1).gameObject.SetActive(true);
        if (SLOT3 != null)
            SLOT_ALLID.transform.GetChild(2).gameObject.SetActive(true);
        if (SLOT4 != null)
            SLOT_ALLID.transform.GetChild(3).gameObject.SetActive(true);
    }
    public bool isCanFight = false;
    public void CheckCanFight()
    {
        int Count = 0;
        if (L_AllidELements.Count > 0)
        {
            for (int i = 0; i < L_AllidELements.Count; i++)
            {
                if (L_AllidELements[i].HP < 1)
                {
                    Count++;
                }
            }
            if (Count == L_AllidELements.Count)
            {
                m_ToastManager.Toast.gameObject.SetActive(true);
                m_ToastManager.MoveUpToast();
                isCanFight = true;
            }
            else
            {
                isCanFight = false;
            }
        }
        if (L_AllidELements.Count == 0)
        {
            m_ToastManager.Toast.gameObject.SetActive(true);
            m_ToastManager.MoveUpToast();
            isCanFight = true;
        }
        else
        {
            isCanFight = false;
        }

        for (int i = 0; i < L_AllidELements.Count; i++)
        {
            if (L_AllidELements[i].HP > 0)
            {
                Debug.Log("Co");
                L_AllidELements[i].gameObject.SetActive(true);
                StartCoroutine(IE_HiddenToast());
            }
        }

    }
    IEnumerator IE_HiddenToast()
    {
        yield return null;
        m_ToastManager.Toast.gameObject.SetActive(false);
    }
    public void ResetAllidBaseElement()
    {
        Debug.Log("da reset");

        if (SLOT1 != null)
        {
            Destroy(SLOT1.gameObject);
            SLOT1 = null;
        }

        if (SLOT2 != null)
        {
            Destroy(SLOT2.gameObject);
            SLOT2 = null;
        }

        if (SLOT3 != null)
        {
            Destroy(SLOT3.gameObject);
            SLOT3 = null;
        }

        if (SLOT4 != null)
        {
            Destroy(SLOT4.gameObject);
        }
        L_AllidELements.Clear();
    }
    public void ActiveButtonAllidElement()
    {
        Debug.Log("da active");
        for (int i = 0; i < L_AllidELements.Count; i++)
        {
            if (L_AllidELements[i].HP > 0)
            {
                L_AllidELements[i].PurchaseBtn.enabled = true;
            }
        }
    }

    public void LoadStartALliBaseElement()
    {
        DisableButton();
        Debug.Log("da load start");
        for (int i = 0; i < L_AllidELements.Count; i++)
        {
            if (L_AllidELements[i].HP > 0)
            {
                AllidBase allidBase = AllidBase.GetComponent<AllidBase>();

                allidBase.Type = L_AllidELements[i].Type;
                AllidBase.skeletonDataAsset = null;
                allidBase.ICON = L_AllidELements[i].ICON;
                AllidBase.Initialize(true);
                allidBase.HP = L_AllidELements[i].HP;
                allidBase.ID = L_AllidELements[i].ID;
                allidBase.Damage = L_AllidELements[i].Damage;
                break;
            }
            else
            {
                L_AllidELements[i].gameObject.SetActive(false);
            }
        }
    }
    public void LoadStartAllidBase()
    {
        if (SLOT1 != null)
        {
            if (SLOT1.HP > 0)
            {
                AllidBase alid = AllidBase.GetComponent<AllidBase>();
                alid.Type = SLOT1.Type;
                alid.ICON = SLOT1.ICON;
                AllidBase.skeletonDataAsset = null;
                AllidBase.skeletonDataAsset = SLOT1.ICON;
                AllidBase.Initialize(true);
                alid.HP = SLOT1.HP;
                alid.ID = SLOT1.ID;
                alid.Damage = SLOT1.Damage;
                alid.RangeAttack = SLOT1.RangeAttack;

                alid.PlayAnimation("Idle", true, null);
                alid.MoveToStartPos();
            }
        }
        else if (SLOT2 != null)
        {
            if (SLOT2.HP > 0)
            {
                AllidBase alid = AllidBase.GetComponent<AllidBase>();
                alid.Type = SLOT2.Type;
                alid.ICON = SLOT2.ICON;
                AllidBase.skeletonDataAsset = null;
                AllidBase.skeletonDataAsset = SLOT2.ICON;
                AllidBase.Initialize(true);
                alid.HP = SLOT2.HP;
                alid.ID = SLOT2.ID;
                alid.Damage = SLOT2.Damage;
                alid.RangeAttack = SLOT2.RangeAttack;

                alid.PlayAnimation("Idle", true, null);
                alid.MoveToStartPos();

            }
        }
        else if (SLOT3 != null)
        {
            if (SLOT3.HP > 0)
            {
                AllidBase alid = AllidBase.GetComponent<AllidBase>();
                alid.Type = SLOT3.Type;
                alid.ICON = SLOT3.ICON;
                AllidBase.skeletonDataAsset = null;
                AllidBase.skeletonDataAsset = SLOT3.ICON;
                AllidBase.Initialize(true);
                alid.HP = SLOT3.HP;
                alid.ID = SLOT3.ID;
                alid.Damage = SLOT3.Damage;
                alid.RangeAttack = SLOT3.RangeAttack;


                alid.PlayAnimation("Idle", true, null);
                alid.MoveToStartPos();
            }
        }
        else if (SLOT4 != null)
        {
            if (SLOT4.HP > 0)
            {
                AllidBase alid = AllidBase.GetComponent<AllidBase>();
                alid.Type = SLOT4.Type;
                alid.ICON = SLOT4.ICON;
                AllidBase.skeletonDataAsset = null;
                AllidBase.skeletonDataAsset = SLOT4.ICON;
                AllidBase.Initialize(true);
                alid.HP = SLOT4.HP;
                alid.ID = SLOT4.ID;
                alid.Damage = SLOT4.Damage;
                alid.RangeAttack = SLOT4.RangeAttack;


                alid.PlayAnimation("Idle", true, null);
                alid.MoveToStartPos();
            }
        }
    }
    public bool CheckAttack = false;

    public void SpawnAllidBase(int index)
    {
        if (CheckAttack) return;

        CheckAttack = true;
        DisableButton();
        m_Auto.isTurnAutoDone = true;
        if (index == 1)
        {
            AllidBase allibase = AllidBase.GetComponent<AllidBase>();

            allibase.Type = SLOT1.Type;
            allibase.HP = SLOT1.HP;
            allibase.ID = SLOT1.ID;
            allibase.Damage = SLOT1.Damage;
            allibase.RangeAttack = SLOT1.RangeAttack;
            AllidBase.skeletonDataAsset = null;
            AllidBase.skeletonDataAsset = SLOT1.ICON;
            AllidBase.Initialize(true);

            if (allibase.HP > 0)
                allibase.ATTACK_ALLID();
        }
        if (index == 2)
        {
            AllidBase allibase = AllidBase.GetComponent<AllidBase>();

            allibase.Type = SLOT2.Type;
            allibase.ICON = SLOT2.ICON;
            AllidBase.skeletonDataAsset = null;
            AllidBase.skeletonDataAsset = SLOT2.ICON;
            AllidBase.Initialize(true);
            AllidBase.GetComponent<SkeletonGraphic>().skeletonDataAsset = SLOT2.ICON;
            allibase.HP = SLOT2.HP;
            allibase.ID = SLOT2.ID;
            allibase.Damage = SLOT2.Damage;
            allibase.RangeAttack = SLOT2.RangeAttack;


            if (AllidBase.GetComponent<AllidBase>().HP > 0)
                allibase.ATTACK_ALLID();
        }
        if (index == 3)
        {
            AllidBase allidbase = AllidBase.GetComponent<AllidBase>();

            allidbase.Type = SLOT3.Type;
            allidbase.ICON = SLOT3.ICON;
            AllidBase.skeletonDataAsset = null;
            AllidBase.skeletonDataAsset = SLOT3.ICON;
            AllidBase.Initialize(true);
            AllidBase.GetComponent<SkeletonGraphic>().skeletonDataAsset = SLOT3.ICON;
            allidbase.HP = SLOT3.HP;
            allidbase.ID = SLOT3.ID;
            allidbase.Damage = SLOT3.Damage;
            allidbase.RangeAttack = SLOT3.RangeAttack;

            if (allidbase.HP > 0)
                allidbase.ATTACK_ALLID();

        }
        if (index == 4)
        {
            AllidBase allidbase = AllidBase.GetComponent<AllidBase>();

            allidbase.Type = SLOT4.Type;
            allidbase.ICON = SLOT4.ICON;
            AllidBase.skeletonDataAsset = null;
            AllidBase.skeletonDataAsset = SLOT4.ICON;
            AllidBase.Initialize(true);
            AllidBase.GetComponent<SkeletonGraphic>().skeletonDataAsset = SLOT4.ICON;
            allidbase.HP = SLOT4.HP;
            allidbase.ID = SLOT4.ID;
            allidbase.Damage = SLOT4.Damage;
            allidbase.RangeAttack = SLOT4.RangeAttack;

            if (allidbase.HP > 0)
                allidbase.ATTACK_ALLID();
        }
    }

    public void TakeDamageAllid(int damage, AllidBase allidBase)
    {
        for (int i = 0; i < L_AllidELements.Count; i++)
        {
            if (allidBase.GetComponent<AllidBase>().ID == L_AllidELements[i].ID
                && allidBase.GetComponent<AllidBase>().Type == L_AllidELements[i].Type)
            {
                allidBase.HP -= damage;
                DataPlayer.SetHP(allidBase.HP, L_AllidELements[i].ThisElementData);
                AudioManager.instance.PlaySound(AudioManager.instance.Slash);
                if (allidBase.HP <= 0)
                {
                    allidBase.HP = 0;
                }
                L_AllidELements[i].Init(L_AllidELements[i].ThisElementData);
            }
        }
    }

    public void TakeDamageEnemy(int damage, EnemyBased enemyBase)
    {
        enemyBase.HP -= damage;
        ShowEffectAvatar();
    }
    public void EnableButton()
    {
        CheckAttack = false;
        EnableSlotButton();
        /*for (int i = 0; i < L_AllidELements.Count; i++)
        {
            if (L_AllidELements[i] != null)
            {
                if (L_AllidELements[i].HP > 0)
                {
                    EnableButton(i + 1);
                }
            }
        }*/
        /*  if (L_AllidELements[1] != null && L_AllidELements[1].HP > 0)
              SLOT2_BTN.enabled = true;
          if (L_AllidELements[2] != null && L_AllidELements[2].HP > 0)
              SLOT3_BTN.enabled = true;
          if (L_AllidELements[3] != null && L_AllidELements[3].HP > 0)
              SLOT4_BTN.enabled = true;*/
    }
    public List<Button> L_SlotButtons = new List<Button>();

    public void EnableSlotButton()
    {
        /* for (int i = 0; i < L_AllidELements.Count; i++)
         {
             if (L_AllidELements[i].HP > 0)
             {
                 L_SlotButtons[i].enabled = true;
             }
         }*/
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            L_AllidELements[0].GetComponentInParent<Button>().enabled = true;
            L_AllidELements[0].GetComponentInParent<Button>().interactable = true;
            return;
        }


        for (int i = 0; i < L_AllidELements.Count; i++)
        {
            if (L_AllidELements[i].HP > 0)
            {
                L_AllidELements[i].GetComponentInParent<Button>().enabled = true;
                L_AllidELements[i].GetComponentInParent<Button>().interactable = true;

            }
        }
        m_Auto.isTurnAutoDone = false;
    }
    public void EnableButton(int intdex)
    {
        switch (intdex)
        {
            case 1:
                SLOT1_BTN.enabled = true;
                SLOT1_BTN.interactable = true;
                break;
            case 2:
                SLOT2_BTN.enabled = true;
                if (DataPlayer.GetIsCheckDoneTutorial())
                    SLOT2_BTN.interactable = true;
                break;
            case 3:
                SLOT3_BTN.enabled = true;
                SLOT3_BTN.interactable = true;
                break;
            case 4:
                SLOT4_BTN.enabled = true;
                SLOT4_BTN.interactable = true;
                break;
        }
    }
    public void DisableButton(int Index)
    {

    }
    public void DisableButton()
    {
        SLOT1_BTN.enabled = false;
        SLOT2_BTN.enabled = false;
        SLOT3_BTN.enabled = false;
        SLOT4_BTN.enabled = false;
        m_Auto.isTurnAutoDone = true;
    }
    public void LoadCatchChance(EnemyBased _enemyBased)
    {
        Debug.Log("Load Catch Chance");
        EnemyBased enemy = new EnemyBased();
        /* for (int i = 0; i < Controller.Instance.enemyData.enemies.Count; i++)
         {
             if (_enemyBased.Type == Controller.Instance.enemyData.enemies[i].Type)
             {
                 keyValuePairs.Remove(_enemyBased);
                 keyValuePairs.Remove(enemy);
                 break;
             }
         }*/
        keyValuePairs.Clear();

        for (int i = 0; i < Controller.Instance.enemyData.enemies.Count; i++)
        {
            if (_enemyBased.Type == Controller.Instance.enemyData.enemies[i].Type)
            {
                if (!DataPlayer.GetIsCheckDoneTutorial())
                {
                    Catch_Chance = 100;
                }
                else
                    Catch_Chance = Controller.Instance.enemyData.enemies[i].CatchChance;
                Sum_Catch = Catch_Chance + Catch_Chance_Plus;
                Debug.Log("Ti le phan tram la: " + Sum_Catch);
                keyValuePairs.Add(_enemyBased, Sum_Catch);
                keyValuePairs.Add(enemy, 100 - (Sum_Catch));
                break;
            }
        }
    }
    public void CatchEnemy(EnemyBased enemyBased)
    {
        if (Catch_Chance_Controller.GetRandomByPercent(keyValuePairs) == enemyBased)
        {
            IsCatchDone = true;
            foreach (var kv in DataPlayer.GetDictionary())
            {
                DataPlayer.Add(enemyBased.Type);
                DataPlayer.AddCritter(enemyBased.Type);
                Controller.Instance.L_TypeNewUICritter.Add(enemyBased.Type);

                PopUpCatched.GetComponent<PopUpCatched>().SetName(enemyBased.Type);

                EnemyStat enemyStat = Controller.Instance.GetStatEnemy(enemyBased.Type);
                PopUpCatched.GetComponent<PopUpCatched>().SetAvatar(enemyStat.Type);

                Debug.Log("da bat thanh cong");

                m_PopUpCatchDone.gameObject.SetActive(true);
                //  PopUpCatched.SetActive(true);
            }
        }
        else
        {
            Debug.Log("khong bat dc");
            if (m_character is BossPatrol)
            {
                IsCatchDone = true;
            }
            StartCoroutine(IE_FallPosCritter());
        }
    }
    IEnumerator IE_FallPosCritter()
    {
        yield return new WaitForSeconds(1f);
        //   EffectHidden.gameObject.SetActive(false);
        m_PopUpCatchPending.MoveSkeletonCritterToStarPosWhenCatchFalse();
        m_PopUpCatchPending.ResetBar();
        m_PopUpCatchPending.SetValueChanceBar();
        m_PopUpCatchPending.LoadRateChance();
        m_PopUpCatchPending.ResetCoinCatch();
        m_PopUpCatchPending.ResetGemCatch();
        m_PopUpCatchPending.ResetCatchAds();
        m_PopUpCatchPending.resetCatchfree();
        LoadCatchChance(enemyBased);
        Catch_Chance_Plus = 0;
    }
}
