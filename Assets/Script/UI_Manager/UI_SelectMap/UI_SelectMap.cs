using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UI_SelectMap : MonoBehaviour
{
    public Button OutButton;
    public Button Map1_Btn;
    public Button Map2_Btn;
    public Button Map3_Btn;
    public Button Map4_Btn;
    public Button Map5_Btn;
    public Button Map6_Btn;
    public Button Map7_Btn;
    public Button Map8_Btn;
    public Button Map9_Btn;
    public Button Map10_Btn;
    public Button Map11_Btn;

    public Image LockMap1;
    public Image LockMap2;
    public Image LockMap3;
    public Image LockMap4;
    public Image LockMap5;
    public Image LockMap6;
    public Image LockMap7;
    public Image LockMap8;
    public Image LockMap9;
    public Image LockMap10;
    public Image LockMap11;

    public Image UnlockImg;
    public Image LockImg;
    public Sprite HereImg;
    public Image HeadBoy;

    public GameObject _ViewFinderFocus;
    public PopUpSelectMap m_popUpSelectMap;
    public PopUpRewardCritter m_popUpReward;

    public LoadSceneManager m_LoadSceneManager;
    public GameObject BG;

    public float LimitTop;
    public float LimitBottom;
    public float Offset1;
    public float Offset2;
    public float Offset3;
    public ScrollRect scroll;

    public int LevelSelect;
    public int MaxLevel;
    private void Awake()
    {
        OutButton.onClick.AddListener(OnClickOutButton);

        Map1_Btn.onClick.AddListener(OnClickLevel1);
        Map2_Btn.onClick.AddListener(OnClickLevel2);
        Map3_Btn.onClick.AddListener(OnClickLevel3);
        Map4_Btn.onClick.AddListener(OnClickLevel4);
        Map5_Btn.onClick.AddListener(OnClickLevel5);
        Map6_Btn.onClick.AddListener(OnClickLevel6);
        Map7_Btn.onClick.AddListener(OnClickLevel7);
        Map8_Btn.onClick.AddListener(OnClickLevel8);
        Map9_Btn.onClick.AddListener(OnClickLevel9);
        Map10_Btn.onClick.AddListener(OnClickLevel10);
        Map11_Btn.onClick.AddListener(OnClickLevel11);
    }
    void FollowPosCurrentMap(int index)
    {
        if (index >= 0 && index <= 3)
        {
            BG.transform.DOLocalMoveY(Offset1, 0.5f);
        }
        else if (index >= 4 && index <= 7)
        {
            BG.transform.DOLocalMoveY(Offset2, 0.5f);
        }
        else if (index >= 8 && index <= 11)
        {
            BG.transform.DOLocalMoveY(Offset3, 0.5f);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(IE_DelaytOneanble());
    }
    IEnumerator IE_DelaytOneanble()
    {
        yield return null;
        BagManager.Instance.m_RuleController.StopEnemy();
        BG.transform.localPosition = new Vector3(BG.transform.localPosition.x, 800, 0);

        if (!DataPlayer.GetIsCheckDoneTutorial() && DataPlayer.GetIsHealHP())
        {
            UI_Home.Instance.m_ShowAllid.L_SlotAllidBaseShowDone.Clear();
            HeadBoy.transform.position = LockMap1.transform.position + new Vector3(1000, 1000, 0);
            DataPlayer.AddListLevel(1);
            HiddenLockImg();
            // ViewFinderFocus(1);
            UI_Home.Instance.uI_Battle.m_PopUpCatchDone.OnClickSkipBtn();
            ArrowDirection.Instance.gameObject.SetActive(false);
            UI_Home.Instance.m_UIScreen.NotificationBoss.gameObject.SetActive(false);
            DataPlayer.ClearAlldata();
            DataPlayer.ClearDataAllid();
            DataPlayer.ReLoadDataCritter();
            // bat pop up o cho nay
            /*  if (!DataPlayer.GetIsNewUser())
              {
                  m_popUpReward.gameObject.SetActive(true);
                  DataPlayer.SetCoin(0);
                  DataPlayer.SetGem(0);
                  UI_Home.Instance.m_UIGemManager.SetTextGem();
              }*/

            DataPlayer.SetIsCheckSelectMap1(true);
            CritterFollowController.Instance.LoadCritterFollow();
            BagManager.Instance.EnableButtonInBagManager();

            OutButton.interactable = false;
        }
        if (DataPlayer.GetIsCheckSelectMap1())
        {
            ArrowDirection.Instance.gameObject.SetActive(false);
        }
        FollowPosCurrentMap(BagManager.Instance.m_RuleController.CurLevel);

    }
    public void Start()
    {
        HiddenLockImg();

    }
    public float OffsetHeadBoy;
    public void CurrentLevel()
    {
        switch (DataPlayer.getCurLevel())
        {
            case 0:
                break;
            case 1:
                LockMap1.sprite = HereImg;
                HeadBoy.transform.SetParent(LockMap1.transform, false);
                //    HeadBoy.transform.localPosition = Vector3.zero;
                HeadBoy.transform.localPosition = new Vector3(0, OffsetHeadBoy, 0);
                AnimUpdownHeadBoy(HeadBoy.transform);
                break;
            case 2:
                LockMap2.sprite = HereImg;
                HeadBoy.transform.SetParent(LockMap2.transform, false);
                // HeadBoy.transform.localPosition = Vector3.zero;
                //HeadBoy.transform.localPosition = LockMap2.transform.position + new Vector3(0, OffsetHeadBoy, 0);
                HeadBoy.transform.localPosition = new Vector3(0, OffsetHeadBoy, 0);
                AnimUpdownHeadBoy(HeadBoy.transform);
                break;
            case 3:
                LockMap3.sprite = HereImg;
                HeadBoy.transform.SetParent(LockMap3.transform, false);
                //   HeadBoy.transform.localPosition = Vector3.zero;
                HeadBoy.transform.localPosition = new Vector3(0, OffsetHeadBoy, 0);

                //  HeadBoy.transform.localPosition = LockMap3.transform.position + new Vector3(0, OffsetHeadBoy, 0);
                AnimUpdownHeadBoy(HeadBoy.transform);
                break;
            case 4:
                LockMap4.sprite = HereImg;
                HeadBoy.transform.SetParent(LockMap4.transform, false);
                // HeadBoy.transform.localPosition = Vector3.zero;
                HeadBoy.transform.localPosition = new Vector3(0, OffsetHeadBoy, 0);
                AnimUpdownHeadBoy(HeadBoy.transform);
                break;
            case 5:
                LockMap5.sprite = HereImg;
                HeadBoy.transform.SetParent(LockMap5.transform, false);
                //  HeadBoy.transform.position = Vector3.zero;
                HeadBoy.transform.localPosition = new Vector3(0, OffsetHeadBoy, 0);
                /*                HeadBoy.transform.SetParent(LockMap5.transform);
                */
                AnimUpdownHeadBoy(HeadBoy.transform);
                break;
            case 6:
                LockMap6.sprite = HereImg;
                HeadBoy.transform.SetParent(LockMap6.transform, false);
                //  HeadBoy.transform.position = Vector3.zero;
                HeadBoy.transform.localPosition = new Vector3(0, OffsetHeadBoy, 0);
                /*                HeadBoy.transform.SetParent(LockMap5.transform);
                */
                AnimUpdownHeadBoy(HeadBoy.transform);
                break;
            case 7:
                LockMap7.sprite = HereImg;
                HeadBoy.transform.SetParent(LockMap7.transform, false);
                //  HeadBoy.transform.position = Vector3.zero;
                HeadBoy.transform.localPosition = new Vector3(0, OffsetHeadBoy, 0);
                AnimUpdownHeadBoy(HeadBoy.transform);
                break;
            case 8:
                HeadBoy.transform.SetParent(LockMap8.transform, false);
                //  HeadBoy.transform.localPosition = Vector3.zero;
                LockMap8.sprite = HereImg;
                HeadBoy.transform.localPosition = new Vector3(0, OffsetHeadBoy, 0);
                AnimUpdownHeadBoy(HeadBoy.transform);

                break;
            case 9:
                LockMap9.sprite = HereImg;
                HeadBoy.transform.SetParent(LockMap9.transform, false);
                //  HeadBoy.transform.position = Vector3.zero;
                HeadBoy.transform.localPosition = new Vector3(0, OffsetHeadBoy, 0);
                AnimUpdownHeadBoy(HeadBoy.transform);

                break;
            case 10:
                LockMap10.sprite = HereImg;
                HeadBoy.transform.SetParent(LockMap10.transform, false);
                //  HeadBoy.transform.position = Vector3.zero;
                HeadBoy.transform.localPosition = new Vector3(0, OffsetHeadBoy, 0);
                AnimUpdownHeadBoy(HeadBoy.transform);

                break;
            case 11:
                LockMap11.sprite = HereImg;
                HeadBoy.transform.SetParent(LockMap11.transform, false);
                //  HeadBoy.transform.position = Vector3.zero;
                HeadBoy.transform.localPosition = new Vector3(0, OffsetHeadBoy, 0);
                AnimUpdownHeadBoy(HeadBoy.transform);

                break;
        }
    }
    IEnumerator IE_DelayAnimHead(Transform _Trans)
    {
        yield return new WaitForSeconds(1f);
        AnimUpdownHeadBoy(_Trans);
    }
    void AnimUpdownHeadBoy(Transform trans)
    {
        DOTween.KillAll();
        if (gameObject.activeInHierarchy)
        {
            trans.transform.DOLocalMove(new Vector3(trans.localPosition.x, trans.localPosition.y + 5, 0), 0.5f)
                .OnComplete(() =>
                {
                    trans.transform.DOLocalMove(new Vector3(trans.localPosition.x, trans.localPosition.y - 5, 0), 0.5f)
                                   .OnComplete(() =>
                                   {
                                       AnimUpdownHeadBoy(trans);
                                   });
                });
        }
    }
    private void Update()
    {
        if (scroll)
        {
            if (BG.transform.localPosition.y <= LimitTop)
            {
                scroll.StopMovement();
                BG.transform.localPosition = new Vector3(0, LimitTop + 1, 0);
            }
            if (BG.transform.localPosition.y >= LimitBottom)
            {
                scroll.StopMovement();
                BG.transform.localPosition = new Vector3(0, LimitBottom - 1, 0);
            }
            else
            {
                scroll.enabled = true;
            }
        }
    }
    public void HiddenLockImg()
    {
        for (int i = 1; i <= 11; i++)
        {
            if (DataPlayer.GetListLevel().Contains(i))
            {
                switch (i)
                {
                    case 1:
                        LockMap1.sprite = UnlockImg.sprite;
                        CurrentLevel();
                        break;
                    case 2:
                        LockMap2.sprite = UnlockImg.sprite;
                        CurrentLevel();
                        break;
                    case 3:
                        LockMap3.sprite = UnlockImg.sprite;
                        CurrentLevel();
                        break;
                    case 4:
                        LockMap4.sprite = UnlockImg.sprite;
                        CurrentLevel();
                        break;
                    case 5:
                        LockMap5.sprite = UnlockImg.sprite;
                        CurrentLevel();
                        break;
                    case 6:
                        LockMap6.sprite = UnlockImg.sprite;
                        CurrentLevel();
                        break;
                    case 7:
                        LockMap7.sprite = UnlockImg.sprite;
                        CurrentLevel();
                        break;
                    case 8:
                        LockMap8.sprite = UnlockImg.sprite;
                        CurrentLevel();
                        break;
                    case 9:
                        LockMap9.sprite = UnlockImg.sprite;
                        CurrentLevel();
                        break;
                    case 10:
                        LockMap10.sprite = UnlockImg.sprite;
                        CurrentLevel();
                        break;
                    case 11:
                        LockMap11.sprite = UnlockImg.sprite;
                        CurrentLevel();
                        break;

                }
            }
        }
    }

    public void OnClickLevel1()
    {
        LevelSelect = 1;
        if (LevelSelect < MaxLevel)
        {
            m_popUpSelectMap.gameObject.SetActive(true);
            m_popUpSelectMap.ID = 1;
            m_popUpSelectMap.loadCritterMap1();
            m_popUpSelectMap.LoadTextNumCritter();
        }
        else
        {
            popUpManager.Instance.m_PopUpComingSoon.gameObject.SetActive(true);
        }

    }
    public void OnClickLevel2()
    {
        LevelSelect = 2;
        if (LevelSelect < MaxLevel)
        {
            m_popUpSelectMap.gameObject.SetActive(true);
            m_popUpSelectMap.ID = 2;
            m_popUpSelectMap.loadCritterMap2();
            m_popUpSelectMap.LoadTextNumCritter();
        }
        else
        {
            popUpManager.Instance.m_PopUpComingSoon.gameObject.SetActive(true);
        }
    }
    public void OnClickLevel3()
    {
        LevelSelect = 3;
        if (LevelSelect < MaxLevel)
        {
            m_popUpSelectMap.gameObject.SetActive(true);
            m_popUpSelectMap.ID = 3;
            m_popUpSelectMap.loadCritterMap3();
            m_popUpSelectMap.LoadTextNumCritter();
        }
        else
        {
            popUpManager.Instance.m_PopUpComingSoon.gameObject.SetActive(true);

        }
    }
    public void OnClickLevel4()
    {
        LevelSelect = 4;
        if (LevelSelect < MaxLevel)
        {
            m_popUpSelectMap.gameObject.SetActive(true);
            m_popUpSelectMap.ID = 4;
            m_popUpSelectMap.loadCritterMap4();
            m_popUpSelectMap.LoadTextNumCritter();
        }
        else
        {
            popUpManager.Instance.m_PopUpComingSoon.gameObject.SetActive(true);

        }
    }
    public void OnClickLevel5()
    {
        LevelSelect = 5;
        if (LevelSelect < MaxLevel)
        {
            m_popUpSelectMap.gameObject.SetActive(true);
            m_popUpSelectMap.ID = 5;
            m_popUpSelectMap.loadCritterMap5();
            m_popUpSelectMap.LoadTextNumCritter();
        }
        else
        {
            popUpManager.Instance.m_PopUpComingSoon.gameObject.SetActive(true);

        }
    }
    public void OnClickLevel6()
    {
        LevelSelect = 6;
        if (LevelSelect < MaxLevel)
        {
            m_popUpSelectMap.gameObject.SetActive(true);
            m_popUpSelectMap.ID = 6;
            m_popUpSelectMap.loadCritterMap6();
            m_popUpSelectMap.LoadTextNumCritter();
        }
        else
        {
            popUpManager.Instance.m_PopUpComingSoon.gameObject.SetActive(true);

        }
    }
    public void OnClickLevel7()
    {
        LevelSelect = 7;
        if (LevelSelect < MaxLevel)
        {
            m_popUpSelectMap.gameObject.SetActive(true);
            m_popUpSelectMap.ID = 7;
            m_popUpSelectMap.loadCritterMap7();
            m_popUpSelectMap.LoadTextNumCritter();
        }
        else
        {
            popUpManager.Instance.m_PopUpComingSoon.gameObject.SetActive(true);

        }
    }
    public void OnClickLevel8()
    {
        LevelSelect = 8;
        if (LevelSelect < MaxLevel)
        {
            m_popUpSelectMap.gameObject.SetActive(true);
            m_popUpSelectMap.ID = 8;
            m_popUpSelectMap.loadCritterMap8();
            m_popUpSelectMap.LoadTextNumCritter();
        }
        else
        {
            popUpManager.Instance.m_PopUpComingSoon.gameObject.SetActive(true);

        }
    }
    public void OnClickLevel9()
    {
        LevelSelect = 9;
        if (LevelSelect < MaxLevel)
        {
            m_popUpSelectMap.gameObject.SetActive(true);
            m_popUpSelectMap.ID = 9;
            m_popUpSelectMap.loadCritterMap9();
            m_popUpSelectMap.LoadTextNumCritter();
        }
        else
        {
            popUpManager.Instance.m_PopUpComingSoon.gameObject.SetActive(true);

        }
    }
    public void OnClickLevel10()
    {
        LevelSelect = 10;
        if (LevelSelect < MaxLevel)
        {
            m_popUpSelectMap.gameObject.SetActive(true);
            m_popUpSelectMap.ID = 10;
            m_popUpSelectMap.loadCritterMap10();
            m_popUpSelectMap.LoadTextNumCritter();
        }
        else
        {
            popUpManager.Instance.m_PopUpComingSoon.gameObject.SetActive(true);

        }
    }
    public void OnClickLevel11()
    {
        LevelSelect = 11;
        if (LevelSelect < MaxLevel)
        {
            m_popUpSelectMap.gameObject.SetActive(true);
            m_popUpSelectMap.ID = 11;
            m_popUpSelectMap.loadCritterMap11();
            m_popUpSelectMap.LoadTextNumCritter();
        }
        else
        {
            popUpManager.Instance.m_PopUpComingSoon.gameObject.SetActive(true);

        }
    }
    public void OnClickOutButton()
    {
        this.gameObject.SetActive(false);
        //  UI_Home.Instance.UI_HomeObj.SetActive(true);
        UI_Home.Instance.ActiveUIHome();
        Controller.Instance.m_Player.gameObject.SetActive(true);
        _ViewFinderFocus.gameObject.SetActive(false);
    }
    public void LoadMap1()
    {
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            DataPlayer.SetIsCheckDoneTutorial(true);
            UI_Home.Instance.m_UIScreen.EnableButton();
        }

        Debug.LogError("1. true");
        if (DataPlayer.GetListLevel().Contains(1))
        {
            player.Instance.BagBtn.GetComponent<Button>().interactable = true;
            m_LoadSceneManager.LOAD_MAP_01();
            this.gameObject.SetActive(false);
            UI_Home.Instance.UI_HomeObj.SetActive(true);
            Controller.Instance.m_Player.gameObject.SetActive(true);
            _ViewFinderFocus.gameObject.SetActive(false);
            player.Instance.BagBtn.GetComponent<Image>().sprite = player.Instance.bagClose.sprite;
            player.Instance.GetComponent<Collider2D>().enabled = true;
            player.Instance.Option.SetActive(false);
            player.Instance.ShadowBag.gameObject.SetActive(true);
            DataPlayer.SetCurLevel(1);
            if (!DataPlayer.GetIsCheckDoneTutorial())
            {
                DataPlayer.SetIsCheckDoneTutorial(true);
                UI_Home.Instance.m_UIScreen.EnableButton();
            }

            if (DataPlayer.GetIsCheckDoneTutorial() || DataPlayer.GetIsCheckSelectMap1())
            {
                OutButton.interactable = true;

            }
        }
        Debug.LogError("2. true");

    }
    public void LoadMap2()
    {
        if (DataPlayer.GetListLevel().Contains(2))
        {
            m_LoadSceneManager.LOAD_MAP_02();
            this.gameObject.SetActive(false);
            UI_Home.Instance.UI_HomeObj.SetActive(true);
            Controller.Instance.m_Player.gameObject.SetActive(true);
            _ViewFinderFocus.gameObject.SetActive(false);

            player.Instance.BagBtn.GetComponent<Image>().sprite = player.Instance.bagClose.sprite;
            player.Instance.GetComponent<Collider2D>().enabled = true;
            player.Instance.Option.SetActive(false);
            player.Instance.ShadowBag.gameObject.SetActive(true);
            DataPlayer.SetCurLevel(2);

        }
    }
    public void LoadMap3()
    {
        if (DataPlayer.GetListLevel().Contains(3))
        {
            m_LoadSceneManager.LOAD_MAP_03();
            this.gameObject.SetActive(false);
            UI_Home.Instance.UI_HomeObj.SetActive(true);
            Controller.Instance.m_Player.gameObject.SetActive(true);
            _ViewFinderFocus.gameObject.SetActive(false);

            player.Instance.BagBtn.GetComponent<Image>().sprite = player.Instance.bagClose.sprite;
            player.Instance.GetComponent<Collider2D>().enabled = true;
            player.Instance.Option.SetActive(false);
            player.Instance.ShadowBag.gameObject.SetActive(true);
            DataPlayer.SetCurLevel(3);

        }
    }
    public void LoadMap4()
    {
        if (DataPlayer.GetListLevel().Contains(4))
        {

            m_LoadSceneManager.LOAD_MAP_04();
            this.gameObject.SetActive(false);
            UI_Home.Instance.UI_HomeObj.SetActive(true);
            Controller.Instance.m_Player.gameObject.SetActive(true);
            _ViewFinderFocus.gameObject.SetActive(false);

            player.Instance.BagBtn.GetComponent<Image>().sprite = player.Instance.bagClose.sprite;
            player.Instance.GetComponent<Collider2D>().enabled = true;
            player.Instance.Option.SetActive(false);
            player.Instance.ShadowBag.gameObject.SetActive(true);
            DataPlayer.SetCurLevel(4);

        }
    }
    public void LoadMap5()
    {
        if (DataPlayer.GetListLevel().Contains(5))
        {
            m_LoadSceneManager.LOAD_MAP_05();
            this.gameObject.SetActive(false);
            UI_Home.Instance.UI_HomeObj.SetActive(true);
            Controller.Instance.m_Player.gameObject.SetActive(true);
            _ViewFinderFocus.gameObject.SetActive(false);

            player.Instance.BagBtn.GetComponent<Image>().sprite = player.Instance.bagClose.sprite;
            player.Instance.GetComponent<Collider2D>().enabled = true;
            player.Instance.Option.SetActive(false);
            player.Instance.ShadowBag.gameObject.SetActive(true);
            DataPlayer.SetCurLevel(5);
        }
    }
    public void LoadMap6()
    {
        if (DataPlayer.GetListLevel().Contains(6))
        {
            m_LoadSceneManager.LOAD_MAP_06();
            this.gameObject.SetActive(false);
            UI_Home.Instance.UI_HomeObj.SetActive(true);
            Controller.Instance.m_Player.gameObject.SetActive(true);
            _ViewFinderFocus.gameObject.SetActive(false);

            player.Instance.BagBtn.GetComponent<Image>().sprite = player.Instance.bagClose.sprite;
            player.Instance.GetComponent<Collider2D>().enabled = true;
            player.Instance.Option.SetActive(false);
            player.Instance.ShadowBag.gameObject.SetActive(true);
            DataPlayer.SetCurLevel(6);
        }
    }
    public void LoadMap7()
    {
        if (DataPlayer.GetListLevel().Contains(7))
        {
            m_LoadSceneManager.LOAD_MAP_07();
            this.gameObject.SetActive(false);
            UI_Home.Instance.UI_HomeObj.SetActive(true);
            Controller.Instance.m_Player.gameObject.SetActive(true);
            _ViewFinderFocus.gameObject.SetActive(false);

            player.Instance.BagBtn.GetComponent<Image>().sprite = player.Instance.bagClose.sprite;
            player.Instance.GetComponent<Collider2D>().enabled = true;
            player.Instance.Option.SetActive(false);
            player.Instance.ShadowBag.gameObject.SetActive(true);
            DataPlayer.SetCurLevel(7);

        }
    }
    public void LoadMap8()
    {
        if (DataPlayer.GetListLevel().Contains(8))
        {
            m_LoadSceneManager.LOAD_MAP_08();
            this.gameObject.SetActive(false);
            UI_Home.Instance.UI_HomeObj.SetActive(true);
            Controller.Instance.m_Player.gameObject.SetActive(true);
            _ViewFinderFocus.gameObject.SetActive(false);

            player.Instance.BagBtn.GetComponent<Image>().sprite = player.Instance.bagClose.sprite;
            player.Instance.GetComponent<Collider2D>().enabled = true;
            player.Instance.Option.SetActive(false);
            player.Instance.ShadowBag.gameObject.SetActive(true);
            DataPlayer.SetCurLevel(8);

        }
    }
    public void LoadMap9()
    {
        if (DataPlayer.GetListLevel().Contains(9))
        {
            m_LoadSceneManager.LOAD_MAP_09();
            this.gameObject.SetActive(false);
            UI_Home.Instance.UI_HomeObj.SetActive(true);
            Controller.Instance.m_Player.gameObject.SetActive(true);
            _ViewFinderFocus.gameObject.SetActive(false);

            player.Instance.BagBtn.GetComponent<Image>().sprite = player.Instance.bagClose.sprite;
            player.Instance.GetComponent<Collider2D>().enabled = true;
            player.Instance.Option.SetActive(false);
            player.Instance.ShadowBag.gameObject.SetActive(true);
            DataPlayer.SetCurLevel(9);

        }
    }
    public void LoadMap10()
    {
        if (DataPlayer.GetListLevel().Contains(10))
        {
            m_LoadSceneManager.LOAD_MAP_10();
            this.gameObject.SetActive(false);
            UI_Home.Instance.UI_HomeObj.SetActive(true);
            Controller.Instance.m_Player.gameObject.SetActive(true);
            _ViewFinderFocus.gameObject.SetActive(false);

            player.Instance.BagBtn.GetComponent<Image>().sprite = player.Instance.bagClose.sprite;
            player.Instance.GetComponent<Collider2D>().enabled = true;
            player.Instance.Option.SetActive(false);
            player.Instance.ShadowBag.gameObject.SetActive(true);
            DataPlayer.SetCurLevel(10);
        }
    }
    public void LoadMap11()
    {
        if (DataPlayer.GetListLevel().Contains(11))
        {
            m_LoadSceneManager.LOAD_MAP_11();
            this.gameObject.SetActive(false);
            UI_Home.Instance.UI_HomeObj.SetActive(true);
            Controller.Instance.m_Player.gameObject.SetActive(true);
            _ViewFinderFocus.gameObject.SetActive(false);

            player.Instance.BagBtn.GetComponent<Image>().sprite = player.Instance.bagClose.sprite;
            player.Instance.GetComponent<Collider2D>().enabled = true;
            player.Instance.Option.SetActive(false);
            player.Instance.ShadowBag.gameObject.SetActive(true);
            DataPlayer.SetCurLevel(11);
        }
    }

    private void OnDisable()
    {
        m_popUpSelectMap.gameObject.SetActive(false);
        BagManager.Instance.m_RuleController.UnStopEnemy();
    }
}
