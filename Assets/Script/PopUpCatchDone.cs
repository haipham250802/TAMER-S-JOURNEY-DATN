using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
using Spine;
using DG.Tweening;
using Sirenix.OdinInspector;
public class PopUpCatchDone : MonoBehaviour
{
    public CanvasGroup canvas;

    public SkeletonGraphic skeletonWin_Or_Lose;
    public SkeletonGraphic skeletonCritterCatchDone;

    public SkeletonDataAsset SkeletonLose;
    public SkeletonDataAsset SkeletonWin;
    public SkeletonDataAsset Magnet;

    public Button NextToMerge_Btn;
    public Button NextToTeam_Btn;
    public Button SkipBtn;

    public Text NameCritter;
    public Text CoinEarnTxt;
    public Text CoinEarndTxt;
    public Text DescriptionTxt;

    public Image ParentTextNameCritter;

    public GameObject ContainerCenter;
    public GameObject ContainerBottom;
    public GameObject ButtonWatchAds;
    public GameObject CoinParent;
    public GameObject Effect;

    public AnimationCurve animCurveEarnCoin;
    public AnimationCurve animCurveScaleSkeleton;
    public AnimationCurve animaCurveScaleCoinParent;
    public AnimationCurve animaCurveScalePopUp;

    public TextCoinAnimation txtCoinAnim;
    public MachineReward machineReward;
    public int CoinWhenEnablePopUpCatchDone;

    public Animator RewardAnm;
    public int CoinEarn;

    public bool isCheckButtonAds;

    public Image NewImg;

    private void Awake()
    {
        SkipBtn.onClick.AddListener(OnClickSkipBtn);
        NextToMerge_Btn.onClick.AddListener(ActiveUIMerge);
        NextToTeam_Btn.onClick.AddListener(ActiveUITeam);
    }
    private void OnEnable()
    {
        if (UI_Home.Instance.uI_Battle.isLose == "Win")
        {
            skeletonWin_Or_Lose.skeletonDataAsset = null;
            skeletonWin_Or_Lose.skeletonDataAsset = SkeletonWin;
            skeletonWin_Or_Lose.Initialize(true);
            AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music, AudioManager.instance.SoundEffectPopUpVictory);
            AudioManager.instance.BG_In_Game_Music.loop = false;
            Effect.SetActive(true);
        }
        else if (UI_Home.Instance.uI_Battle.isLose == "Thua")
        {
            skeletonWin_Or_Lose.skeletonDataAsset = null;
            skeletonWin_Or_Lose.skeletonDataAsset = SkeletonLose;
            skeletonWin_Or_Lose.Initialize(true);
            AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music, AudioManager.instance.SoundEffectPopUpLose);
            AudioManager.instance.BG_In_Game_Music.loop = false;
        }
        else if (UI_Home.Instance.uI_Battle.isLose == "Khong")
        {
            skeletonWin_Or_Lose.skeletonDataAsset = null;
            skeletonWin_Or_Lose.skeletonDataAsset = SkeletonWin;
            skeletonWin_Or_Lose.Initialize(true);
            AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music, AudioManager.instance.SoundEffectPopUpVictory);
            AudioManager.instance.BG_In_Game_Music.loop = false;
            Effect.SetActive(true);
        }
        ActiveSkeletonWinLose();

        SetName(UI_Home.Instance.uI_Battle.enemyBased.Type);

        if (UI_Home.Instance.uI_Battle.m_character is BossPatrol && !DataPlayer.GetIsCheckDoneTutorial())
        {
            NextToTeam_Btn.interactable = false;
            NextToMerge_Btn.interactable = false;
        }
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            NextToTeam_Btn.interactable = true;
            NextToMerge_Btn.interactable = true;
        }
    }

    public void ActiveSkeletonWinLose()
    {
        skeletonWin_Or_Lose.gameObject.SetActive(true);

        skeletonWin_Or_Lose.AnimationState.SetAnimation(0, "Appear", false);

        skeletonWin_Or_Lose.AnimationState.Complete += _ =>
        {
            if (_.Animation.Name == "Appear")
            {
                skeletonWin_Or_Lose.AnimationState.SetAnimation(0, "Idle", true);
            }
        };

        StartCoroutine(ShowEnd());
        StartCoroutine(IE_DelayActiveSkeleton());
    }
    public void ActiveUITeam()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (Controller.Instance.isShowInter)
            {
                Controller.Instance.ShowInterstitial();
            }
        }
        UI_Home.Instance.m_Player.GetComponent<Collider2D>().enabled = true;
        UI_Home.Instance.ActiveUIHome();
        AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music, AudioManager.instance.MusicUIHome);
        if (!UI_Home.Instance.m_UITeam.OutButton.gameObject.activeInHierarchy)
        {
            UI_Home.Instance.m_UITeam.OutButton.gameObject.SetActive(true);
        }
        /*  if (UI_Home.Instance.uI_Battle.m_character is Enemy)
          {
              UI_Home.Instance.uI_Battle.RemoveEnemy();
          }*/
        this.gameObject.SetActive(false);
        UI_Home.Instance.m_UITeam.gameObject.SetActive(true);
        UI_Home.Instance.m_UICollection.ActiveUIteam();
        UI_Home.Instance.m_UITeam.ResetAllid();
        UI_Home.Instance.m_UITeam.ResetAllidInEventory();
        UI_Home.Instance.m_UITeam.Spawn(UI_Home.Instance.m_UITeam.Eventory.transform);
        UI_Home.Instance.m_UITeam.HiddenAllidEventory();
        UI_Home.Instance.m_UITeam.LoadAllied();
        UI_Home.Instance.ActiveBag();

        if (UI_Home.Instance.uI_Battle.isLose == "Win")
        {
            if (UI_Home.Instance.uI_Battle.m_character is BossPatrol)
            {
                UI_Home.Instance.uI_Battle.UnlockLevel();
                UI_Home.Instance.uI_Battle.RemoveBoss();
                if (UI_Home.Instance.uI_Battle.isFocusSelectMap)
                {
                    UI_Home.Instance.m_UIselectMap.HiddenLockImg();
                    //  UI_Home.Instance.m_UIselectMap.gameObject.SetActive(true);
                    //   BagManager.Instance.m_RuleController.DoSomethingKillDoneBoss();
                    UI_Home.Instance.uI_Battle.isFocusSelectMap = false;
                }
                Controller.Instance.isMoveDoor = true;
            }
            else
                UI_Home.Instance.uI_Battle.RemoveEnemy();
        }
        else if (UI_Home.Instance.uI_Battle.isLose == "Thua")
        {
            UI_Home.Instance.ActiveUIHome();
        }
        else if (UI_Home.Instance.uI_Battle.isLose == "Khong")
        {
            if (UI_Home.Instance.uI_Battle.m_character is BossPatrol)
            {
                UI_Home.Instance.uI_Battle.UnlockLevel();
                UI_Home.Instance.uI_Battle.RemoveBoss();
                if (UI_Home.Instance.uI_Battle.isFocusSelectMap)
                {
                    UI_Home.Instance.m_UIselectMap.HiddenLockImg();
                    //   UI_Home.Instance.m_UIselectMap.gameObject.SetActive(true);
                    //  BagManager.Instance.m_RuleController.DoSomethingKillDoneBoss();
                    UI_Home.Instance.uI_Battle.isFocusSelectMap = false;
                }
                Controller.Instance.isMoveDoor = true;
            }
            else
                UI_Home.Instance.ActiveUIHome();
        }
        UI_Home.Instance.uI_Battle.gameObject.SetActive(false);
        /*  }
          else
          {
              NextToMerge_Btn.GetComponent<Image>().color = Color.gray;
              NextToTeam_Btn.GetComponent<Image>().color = Color.gray;
          }*/
        if (BagManager.Instance.m_RuleController.Count == BagManager.Instance.m_RuleController.QuantityEnemy &&
           DataPlayer.GetIsCheckDoneTutorial())
        {
            Controller.Instance.isMoveBoss = true;
            // BagManager.Instance.m_RuleController.MoveToBoss();
        }
    }

    public void ActiveNameCritter(System.Action callback = null)
    {
        DOTween.To(() => 0f, _ =>
        {
            ParentTextNameCritter.fillAmount = _;
        }, 1, 0.6f).OnComplete(() =>
        {
            callback?.Invoke();
        });
    }
    public void ActiveUIMerge()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (Controller.Instance.isShowInter)
            {
                Controller.Instance.ShowInterstitial();
            }
        }
        AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music, AudioManager.instance.MusicUIHome);
        if (!UI_Home.Instance.m_UIMerge.OutButton.gameObject.activeInHierarchy)
        {
            UI_Home.Instance.m_UIMerge.OutButton.gameObject.SetActive(true);
        }
        UI_Home.Instance.m_Player.GetComponent<Collider2D>().enabled = true;
        /* if (UI_Home.Instance.uI_Battle.m_character is Enemy)
         {
             UI_Home.Instance.uI_Battle.RemoveEnemy();
         }*/
        this.gameObject.SetActive(false);
        UI_Home.Instance.m_UITeam.ResetAllidInEventory();
        UI_Home.Instance.m_UIMerge.gameObject.SetActive(true);
        UI_Home.Instance.m_UICollection.ActiveUIMerge();
        UI_Home.Instance.m_UIMerge.ResetItemMerge();
        UI_Home.Instance.m_UIMerge.ResetAllid();
        UI_Home.Instance.m_UIMerge.LoadEventory(UI_Home.Instance.m_UIMerge.Eventory.gameObject);
        UI_Home.Instance.ActiveBag();
        UI_Home.Instance.ActiveUIHome();

        if (UI_Home.Instance.uI_Battle.isLose == "Win")
        {
            if (UI_Home.Instance.uI_Battle.m_character is BossPatrol)
            {
                UI_Home.Instance.uI_Battle.UnlockLevel();
                UI_Home.Instance.uI_Battle.RemoveBoss();
                if (UI_Home.Instance.uI_Battle.isFocusSelectMap)
                {
                    UI_Home.Instance.m_UIselectMap.HiddenLockImg();
                    //  UI_Home.Instance.m_UIselectMap.gameObject.SetActive(true);
                    // BagManager.Instance.m_RuleController.DoSomethingKillDoneBoss();
                    UI_Home.Instance.uI_Battle.isFocusSelectMap = false;
                }
                Controller.Instance.isMoveDoor = true;
            }
            else
                UI_Home.Instance.uI_Battle.RemoveEnemy();
        }
        else if (UI_Home.Instance.uI_Battle.isLose == "Thua")
        {
            UI_Home.Instance.ActiveUIHome();
        }
        else if (UI_Home.Instance.uI_Battle.isLose == "Khong")
        {
            if (UI_Home.Instance.uI_Battle.m_character is BossPatrol)
            {
                UI_Home.Instance.uI_Battle.UnlockLevel();
                UI_Home.Instance.uI_Battle.RemoveBoss();
                if (UI_Home.Instance.uI_Battle.isFocusSelectMap)
                {
                    UI_Home.Instance.m_UIselectMap.HiddenLockImg();
                    //   UI_Home.Instance.m_UIselectMap.gameObject.SetActive(true);
                    //   BagManager.Instance.m_RuleController.DoSomethingKillDoneBoss();
                    UI_Home.Instance.uI_Battle.isFocusSelectMap = false;
                }
                Controller.Instance.isMoveDoor = true;
            }
            else
                UI_Home.Instance.ActiveUIHome();
        }
        UI_Home.Instance.uI_Battle.gameObject.SetActive(false);
        if (BagManager.Instance.m_RuleController.Count == BagManager.Instance.m_RuleController.QuantityEnemy &&
          DataPlayer.GetIsCheckDoneTutorial())
        {
            Controller.Instance.isMoveBoss = true;
            // BagManager.Instance.m_RuleController.MoveToBoss();
        }
        /* }
         else
         {
             NextToMerge_Btn.GetComponent<Image>().color = Color.gray;
             NextToTeam_Btn.GetComponent<Image>().color = Color.gray;
         }*/
    }

    public void ActiveContainerCenter()
    {
        DOTween.To(() => 0f, x =>
        {
            ContainerCenter.GetComponent<CanvasGroup>().alpha = x;
        }, 1, 1f).OnComplete(() =>
        {
            ActiveButtonWatchAds();
        });
    }
    public void SetName(ECharacterType NameCritter)
    {
        if (UI_Home.Instance.uI_Battle.isLose == "Win")
        {
            this.NameCritter.text = NameCritter.ToString();
        }
        else if (UI_Home.Instance.uI_Battle.isLose == "Thua")
        {
            this.NameCritter.text = null;
        }
    }

    public void OnClickSkipBtn()
    {
        this.gameObject.SetActive(false);
        TutorialManager.Instance.DeSpawn();
        if(DataPlayer.GetIsCheckDoneTutorial())
        {
            if(Controller.Instance.isShowInter)
            {
                Controller.Instance.ShowInterstitial();
            }
        }

        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            UI_Home.Instance.m_Player.transform.position = DataPlayer.GetStartPosNewUser();
            DataPlayer.SetisCheckTapButtonSkip(true);
            if (DataPlayer.GetIsCheckTapButtonSkip() && !DataPlayer.GetIsCheckTapBag() && !DataPlayer.GetIsCheckDoneTutorial())
            {
                TutorialManager.Instance.SpawnHandUIHome(UI_Home.Instance.m_Player.Menu.transform, new Vector3(320, -70));

                for (int i = 0; i < TutorialManager.Instance.l_obj.Count; i++)
                {
                    TutorialManager.Instance.l_obj[i].transform.localScale = Vector3.one;
                }
                UI_Home.Instance.m_Player.BagBtn.GetComponent<Canvas>().sortingOrder = 10002;
            }
        }
        player.Instance.ShadowBag.gameObject.SetActive(true);


        if (DataPlayer.GetIsCheckDoneTutorial() && DataPlayer.GetEnemyCatched() >= 2 && !DataPlayer.GetOutpopUPRate())
        {
            if (!DataPlayer.GetRate())
            {
                popUpManager.Instance.m_PopUpRate.gameObject.SetActive(true);
            }
        }
        if (DataPlayer.GetIsCheckDoneTutorial() && DataPlayer.GetEnemyCatched() >= 8
            && DataPlayer.GetOutpopUPRate() && !DataPlayer.GetOffPopUpRate())
        {
            if (!DataPlayer.GetRate())
            {
                popUpManager.Instance.m_PopUpRate.gameObject.SetActive(true);
                DataPlayer.SetOffUPRate(true);
            }
        }

        if (UI_Home.Instance.uI_Battle.isLose == "Win")
        {
            if (UI_Home.Instance.uI_Battle.m_character is BossPatrol)
            {
                UI_Home.Instance.uI_Battle.UnlockLevel();
                UI_Home.Instance.uI_Battle.RemoveBoss();
                if (UI_Home.Instance.uI_Battle.isFocusSelectMap)
                {
                    UI_Home.Instance.m_UIselectMap.HiddenLockImg();
                    //  UI_Home.Instance.m_UIselectMap.gameObject.SetActive(true);
                    BagManager.Instance.m_RuleController.DoSomethingKillDoneBoss();
                    UI_Home.Instance.uI_Battle.isFocusSelectMap = false;
                }
            }
            else
                UI_Home.Instance.uI_Battle.RemoveEnemy();

            if (BagManager.Instance.m_RuleController.Count == BagManager.Instance.m_RuleController.QuantityEnemy &&
           DataPlayer.GetIsCheckDoneTutorial())
            {
                Controller.Instance.isMoveBoss = true;
                BagManager.Instance.m_RuleController.MoveToBoss();
            }
        }
        else if (UI_Home.Instance.uI_Battle.isLose == "Thua")
        {
            UI_Home.Instance.ActiveUIHome();
            if (UI_Home.Instance.uI_Battle.m_character is BossPatrol)
            {
                //   BagManager.Instance.m_RuleController.L_boss[0].ActiveCollider();
                BagManager.Instance.m_RuleController.L_boss[0].SetSpeed();
                BagManager.Instance.m_RuleController.L_boss[0].isCanAI = false;
            }
        }
        else if (UI_Home.Instance.uI_Battle.isLose == "Khong")
        {
            if (UI_Home.Instance.uI_Battle.m_character is BossPatrol)
            {
                UI_Home.Instance.uI_Battle.UnlockLevel();
                UI_Home.Instance.uI_Battle.RemoveBoss();
                if (UI_Home.Instance.uI_Battle.isFocusSelectMap)
                {
                    UI_Home.Instance.m_UIselectMap.HiddenLockImg();
                    //   UI_Home.Instance.m_UIselectMap.gameObject.SetActive(true);
                    BagManager.Instance.m_RuleController.DoSomethingKillDoneBoss();
                    UI_Home.Instance.uI_Battle.isFocusSelectMap = false;
                }
            }
            else
                UI_Home.Instance.ActiveUIHome();
        }

        UI_Home.Instance.uI_Battle.gameObject.SetActive(false);
        UI_Home.Instance.ActiveUIHome();

    }
    public bool ChechNewCritter()
    {
        for (int i = 0; i < DataPlayer.GetListCritters().Count; i++)
        {
            if (DataPlayer.GetListCritters()[i] != UI_Home.Instance.uI_Battle.enemyBased.Type)
            {
                return true;
            }
        }
        return false;
    }

    public void ActiveButtonWatchAds()
    {
        ButtonWatchAds.SetActive(true);
    }
    private void ActiveContainerBottom()
    {
        ContainerBottom.SetActive(true);
    }
    IEnumerator IE_DelayActiveSkeleton()
    {
        yield return new WaitForSeconds(0.7f);
        CoinWhenEnablePopUpCatchDone = DataPlayer.GetCoin();
        CoinEarn = /*CoinWhenEnablePopUpCatchDone - UI_Home.Instance.uI_Battle.CoinWhenEnableUiBattle +
            UI_Home.Instance.uI_Battle.TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().Coin +
            UI_Home.Instance.uI_Battle.m_PopUpCatchPending.CoinCatchPlusCatchDone;*/UI_Home.Instance.uI_Battle.enemyBased.CoinDrop;

        if (UI_Home.Instance.uI_Battle.isLose == "Win")
        {
            EnemyStat enemy = Controller.Instance.GetStatEnemy(UI_Home.Instance.uI_Battle.enemyBased.Type);

            skeletonCritterCatchDone.skeletonDataAsset = null;
            skeletonCritterCatchDone.skeletonDataAsset = enemy.ICON;
            skeletonCritterCatchDone.Initialize(true);
            skeletonCritterCatchDone.AnimationState.SetAnimation(0, "Idle", true);
            SetName(UI_Home.Instance.uI_Battle.enemyBased.Type);
            if (enemy.Rarity <= 5)
            {
                skeletonCritterCatchDone.transform.DOScale(0.7f, 0.5f).SetEase(animCurveScaleSkeleton).OnComplete(() =>
                {
                    StartCoroutine(IE_delayActiveName());
                    if (ChechNewCritter())
                    {
                        NewImg.gameObject.SetActive(true);
                    }
                    /*   else
                       {
                           NewImg.gameObject.SetActive(false);
                       }*/
                });
            }
            else if (enemy.Rarity >= 5)
            {
                skeletonCritterCatchDone.transform.DOScale(0.55f, 0.5f).SetEase(animCurveScaleSkeleton).OnComplete(() =>
                {
                    StartCoroutine(IE_delayActiveName());
                    if (ChechNewCritter())
                    {
                        NewImg.gameObject.SetActive(true);
                    }
                });
            }
        }
        else if (UI_Home.Instance.uI_Battle.isLose == "Thua")
        {
            skeletonCritterCatchDone.transform.localScale = Vector3.one;
            skeletonCritterCatchDone.skeletonDataAsset = null;
            skeletonCritterCatchDone.skeletonDataAsset = Magnet;
            skeletonCritterCatchDone.Initialize(true);
            skeletonCritterCatchDone.AnimationState.SetAnimation(0, "Break", false);


            skeletonCritterCatchDone.transform.DOScale(0.7f, 0.5f).SetEase(animCurveScaleSkeleton).OnComplete(() =>
            {
                skeletonCritterCatchDone.AnimationState.Complete += _ =>
                {
                    if (_.Animation.Name == "Break")
                    {
                        skeletonCritterCatchDone.AnimationState.SetAnimation(0, "Idle", true);
                        //  StartCoroutine(IE_delayActiveName());
                        NameCritter.text = null;
                        ActiveCoinErantext();
                        ActiveNameCritter();
                    }
                };
            });
        }
        else if (UI_Home.Instance.uI_Battle.isLose == "Khong")
        {
            skeletonCritterCatchDone.transform.localScale = Vector3.one;
            skeletonCritterCatchDone.skeletonDataAsset = null;
            skeletonCritterCatchDone.skeletonDataAsset = Magnet;
            skeletonCritterCatchDone.Initialize(true);
            skeletonCritterCatchDone.AnimationState.SetAnimation(0, "Break", false);


            skeletonCritterCatchDone.transform.DOScale(0.7f, 0.5f).SetEase(animCurveScaleSkeleton).OnComplete(() =>
            {
                skeletonCritterCatchDone.AnimationState.Complete += _ =>
                {
                    if (_.Animation.Name == "Break")
                    {
                        skeletonCritterCatchDone.AnimationState.SetAnimation(0, "Idle", true);
                        NameCritter.text = null;
                        ActiveNameCritter();
                        ActiveCoinErantext();
                    }
                };
            });
        }
    }

    public void EventAnimation()
    {

    }
    IEnumerator IE_delayActiveName()
    {
        yield return new WaitForSeconds(0.2f);
        ActiveNameCritter();
        ActiveCoinErantext();
    }
    void ActiveCoinErantext()
    {
        StartCoroutine(IE_DelayTextEarn());
    }
    IEnumerator IE_DelayTextEarn()
    {
        yield return null;
        ScaleCoinEarnText();
    }
    private void ScaleCoinEarnText()
    {
        CoinEarndTxt.transform.DOScale(1, 0.2f).SetEase(animCurveEarnCoin).OnComplete(() =>
        {
            ScaleCoinParent();
        });
    }
    private void ScaleCoinParent()
    {
        CoinParent.transform.DOScale(1, 0.3f).SetEase(animaCurveScaleCoinParent).OnComplete(() =>
        {
            StartCoroutine(IE_delayAnimtext());
        });
    }
    IEnumerator IE_delayAnimtext()
    {
        yield return new WaitForSeconds(0.2f);
        txtCoinAnim.ActionAnimationText(CoinEarnTxt, 0, CoinEarn, 0.6f, StartAnimRewards);
    }
    private void StartAnimRewards()
    {
        RewardAnm.Play("PopUpCatchDoneAnimation");
    }

    private IEnumerator ShowEnd()
    {
        yield return null;
        canvas.alpha = 1;
    }
    IEnumerator IE_delayActiveContainerCenter()
    {
        yield return new WaitForSeconds(1f);
        ActiveContainerCenter();
    }

    public GameObject Arrowred;
    private void OnDisable()
    {
        skeletonWin_Or_Lose.gameObject.SetActive(false);
        //  skeletonCritterCatchDone.gameObject.SetActive(false);
        NameCritter.text = null;

        CoinEarnTxt.text = null;
        CoinEarnTxt.text = null;

        CoinEarndTxt.transform.localScale = Vector3.zero;

        skeletonCritterCatchDone.transform.localScale = Vector3.zero;
        skeletonCritterCatchDone.skeletonDataAsset = null;
        canvas.alpha = 0;

        ContainerCenter.GetComponent<CanvasGroup>().alpha = 0;
        ContainerBottom.gameObject.SetActive(false);

        ButtonWatchAds.gameObject.SetActive(false);

        machineReward.GetComponent<Image>().fillAmount = 0;
        Arrowred.SetActive(false);
        NewImg.gameObject.SetActive(false);

        if (UI_Home.Instance.uI_Battle)
        {
            UI_Home.Instance.uI_Battle.OutBattle();
            UI_Home.Instance.uI_Battle.m_PopUpCatchPending.gameObject.SetActive(false);
        }

        ParentTextNameCritter.fillAmount = 0;
        CoinParent.transform.localScale = Vector3.zero;

        UI_Home.Instance.uI_Battle.TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().Coin = 0;

        RewardAnm.Play("Idle");

        NextToMerge_Btn.interactable = true;
        NextToTeam_Btn.interactable = true;

        DataPlayer.SetIsCheckWin(true);
        CoinEarn = 0;

        Effect.SetActive(false);

        StopAllCoroutines();
    }
}
