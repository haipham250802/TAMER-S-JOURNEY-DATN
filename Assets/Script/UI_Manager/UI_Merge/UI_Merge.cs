using CodeStage.AntiCheat.ObscuredTypes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Dragon.SDK;

public class UI_Merge : MonoBehaviour
{
    public int Coin;
    public int CoinMerge;
    public int CurCoin;

    public Button DEL_SLOT1;
    public Button DEL_SLOT2;
    public Button MERGE;
    public Button OutButton;
    public Button MergeFreeButton;
    public Text ButtonMergeTxt;

    public Image Slot_01;
    public Image Slot_02;
    public Image Slot_03;

    public Element slot1, slot2;
    public GameObject SLOT;
    public MergeElement SlotMerge;
    public MergeElement SlotMergeClone;
    public UI_Team m_UITeam;

    public GameObject PopUpSuccess;

    public Transform startPos, endpos, MovePos, PosBackButton;
    public float duration;

    public Transform SlotParent1, SlotParent2, SlotParentMerge, Eventory, EventoryTeam, BG_MovementParent;
    public GameObject BG_Move;
    public GameObject prefabsItemMerge;
    public GameObject prefabsItemEventory;
    public GameObject IntroMerge;
    GameObject Item1Render, Item2Render, ItemMergeRender;

    public GameObject PopUpDontEnoughtMoneyMerge;
    public GameObject UI_Shop;

    Dictionary<ECharacterType, int> keyValuePairs = new Dictionary<ECharacterType, int>();
    [SerializeField]
    public List<ElementData> L_elementData = new List<ElementData>();
    public List<GameObject> L_BGClone = new List<GameObject>();

    public bool isMergeWithAds;
    public bool isMergeWithCoin;
    public bool isEnoughtCoinMerge;

    public Button LinkToTeamBtn;
    private void Awake()
    {
        MergeFreeButton.onClick.AddListener(ShowAdsReward);
        LinkToTeamBtn.onClick.AddListener(OnclickButtonLinkUiTeam);
    }
    public void OnclickButtonLinkUiTeam()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            this.gameObject.SetActive(false);
            UI_Home.Instance.m_UITeam.gameObject.SetActive(true);
            UI_Home.Instance.m_UICollection.ActiveUIteam();
            UI_Home.Instance.m_UITeam.ResetAllid();
            UI_Home.Instance.m_UITeam.ResetAllidInEventory();
            UI_Home.Instance.m_UITeam.Spawn(UI_Home.Instance.m_UITeam.Eventory.transform);
            UI_Home.Instance.m_UITeam.HiddenAllidEventory();
            UI_Home.Instance.m_UITeam.LoadAllied();
        }
    }
    string str;
    private void Start()
    {
        str = I2.Loc.LocalizationManager.GetTranslation("KEY_MERGE");
        isEnoughtCoinMerge = false;
        Coin = DataPlayer.GetCoin();
        ButtonMergeTxt.text = str;
        MERGE.onClick.AddListener(OnMergeBtn);
        OutButton.onClick.AddListener(OnClickOutButton);
        DEL_SLOT1.gameObject.SetActive(false);
        DEL_SLOT2.gameObject.SetActive(false);
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            OutButton.GetComponent<Canvas>().sortingOrder = 10;
        }
    }
    Tweener tween;
    private void KillTween()
    {
        tween.Kill();
    }
    public void OnEnable()
    {
        StartCoroutine(IE_DelayOnEnable());
        DataPlayer.SetIsCheckTapButtonMerge(true);
    }
    IEnumerator IE_DelayOnEnable()
    {
        yield return null;
        isMoveBGDone = false;
        BG_Parent.transform.position = StartPosBG.position;
        if (!OutButton.gameObject.activeInHierarchy)
        {
            OutButton.gameObject.SetActive(true);
        }
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            OutButton.GetComponent<Canvas>().sortingOrder = 8;
        }
        player.Instance.gameObject.GetComponent<Collider2D>().enabled = false;

        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            Controller.Instance.exampObj.gameObject.SetActive(true);
            string str = I2.Loc.LocalizationManager.GetTranslation("KEY_TUT_ADD_SLOT_MERGE");
            ExampleStoryTut.Instance.SetText(str);
        }
    }

    public GameObject BG_Parent;
    public Transform StartPosBG;
    public Transform EndPosBG;
    public bool isMoveBGDone;

    private void Update()
    {
        if (!isMoveBGDone)
        {
            MoveBackGround(BG_Parent.transform);
            isMoveBGDone = true;
        }
    }
    public void MoveBackGround(Transform BG)
    {
        tween = BG.DOMove(EndPosBG.position, 60).SetEase(Ease.Linear).OnComplete(() =>
        {
            BG.position = StartPosBG.position;
            isMoveBGDone = false;
        });
    }
    List<GameObject> L_CardAnonymous = new List<GameObject>();
    private void OnDisable()
    {
        KillTween();
        isMoveBGDone = true;
        for (int i = 0; i < L_CardAnonymous.Count; i++)
        {
            L_CardAnonymous.Remove(L_CardAnonymous[i]);
        }
        // player.Instance.gameObject.GetComponent<Collider2D>().enabled = true;
    }

    public void OnClickOutButton()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (BagManager.Instance.m_RuleController.L_enemy.Count == BagManager.Instance.m_RuleController.QuantityEnemy)
            {
                Controller.Instance.isMoveBoss = true;
            }
        }
        TutorialManager.Instance.DeSpawn();
        DataPlayer.SetisCheckOutUIMerge(true);
        UI_Home.Instance.ActiveUIHome();

        if (!DataPlayer.GetIsCheckTapButtonTeam() && !DataPlayer.GetIsCheckDoneTutorial())
        {
            TutorialManager.Instance.SpawnHandUIHome(UI_Home.Instance.m_Player.Menu.transform, new Vector3(80, -158, 0));
            UI_Home.Instance.m_Bag.UITeamBtn.GetComponent<Canvas>().sortingOrder = 10002;
            for (int i = 0; i < TutorialManager.Instance.l_obj.Count; i++)
            {
                if (TutorialManager.Instance.l_obj[i].gameObject != null)
                    TutorialManager.Instance.l_obj[i].transform.localScale = Vector3.one;
            }
            Destroy(UI_Home.Instance.m_Bag.UIMergeBtn.GetComponent<GraphicRaycaster>());
            Destroy(UI_Home.Instance.m_Bag.UIMergeBtn.GetComponent<Canvas>());
        }
        // UI_Home.Instance.ActiveBag();
        if (UI_Home.Instance.m_Bag.UIMergeBtn.GetComponent<Canvas>())
            UI_Home.Instance.m_Bag.UIMergeBtn.GetComponent<Canvas>().sortingOrder = 200;

        if (!DataPlayer.GetIsCheckDoneTutorial() && DataPlayer.GetIsCheckOutUiMerge())
        {
            BagManager.Instance.GetComponent<Canvas>().sortingOrder = 200;
        }
        /*if (!UI_Home.Instance.uI_Battle.gameObject.activeInHierarchy)
            player.Instance.Option.gameObject.SetActive(true);*/
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (Controller.Instance.isMoveDoor)
            {
                BagManager.Instance.m_RuleController.MoveToDoor();
            }
            if (Controller.Instance.isMoveBoss)
            {
                BagManager.Instance.m_RuleController.MoveToBoss();
            }
        }
    }
    public void ResetAllid()
    {
        slot1 = null;
        slot2 = null;
        var arr = SLOT.GetComponentsInChildren<MergeElement>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].transform.GetChild(0) != null)
            {
                Destroy(arr[i].GetComponentInChildren<MergeElement>().gameObject);
            }
        }
    }
    public void ADD_SLOT(Element CrtterItem, Action<bool> successed) //, 
    {
        if (slot1 == null)
        {
            slot1 = CrtterItem;
            LoadDataElement(1, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT1.gameObject.SetActive(true);
            m_UITeam.AnonymousCard(Eventory);
            if (!DataPlayer.getisCheckTapSLotMerge() && !DataPlayer.GetIsCheckDoneTutorial())
            {

                TutorialManager.Instance.SpawnHandUIHome(Eventory.transform.GetChild(1), new Vector3(30, -70));

                for (int i = 0; i < TutorialManager.Instance.l_obj.Count; i++)
                {
                    TutorialManager.Instance.l_obj[i].transform.localScale = Vector3.one;
                }
            }

            MergeCritter();
        }
        else if (slot2 == null)
        {
            slot2 = CrtterItem;
            LoadDataElement(2, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT2.gameObject.SetActive(true);
            m_UITeam.AnonymousCard(Eventory);
            if (!DataPlayer.getisCheckTapSLotMerge() && !DataPlayer.GetIsCheckDoneTutorial())
            {
                TutorialManager.Instance.SpawnHandUIHome(Eventory.transform.GetChild(0), new Vector3(30, -70));

            }

            MergeCritter();

        }
    }
    public Transform Top;
    public void MergeCritter()
    {
        if (slot1 != null && slot2 != null)
        {
            if (!DataPlayer.GetIsCheckDoneTutorial())
            {
                MERGE.GetComponent<Canvas>().sortingOrder = 10002;
            }
            TutorialManager.Instance.DeSpawn();
            DataPlayer.SetIsCheckTapSlotMerge(true);
            if (!DataPlayer.GetIsCheckButtonOnMerge())
            {
                TutorialManager.Instance.SpawnHandUIHome(Top, new Vector3(150, -330));

                for (int i = 0; i < TutorialManager.Instance.l_obj.Count; i++)
                {
                    TutorialManager.Instance.l_obj[i].transform.localScale = Vector3.one;
                }
                MERGE.GetComponent<Canvas>().sortingOrder = 10002;
            }
            ItemMergeRender = Instantiate(prefabsItemMerge);
            SlotMerge = ItemMergeRender.GetComponent<MergeElement>();

            if (SlotMerge != null)
            {
                SlotMerge.Type = Controller.Instance.mergeElementData.Child(slot1.Type, slot2.Type);

                if (SlotMerge.Type == ECharacterType.NONE)
                {
                    return;
                }
                if (SlotMerge.Type != ECharacterType.NONE)
                {
                    IntroMerge.GetComponent<MergeIntro>().SetData();

                    for (int i = 0; i < Controller.Instance.enemyData.enemies.Count; i++)
                    {
                        if (Controller.Instance.enemyData.enemies[i].Type == SlotMerge.Type)
                        {
                            CoinMerge = Controller.Instance.enemyData.enemies[i].CombineCost;
                            ButtonMergeTxt.text = CoinMerge.ToString();
                            break;
                        }
                    }
                    ItemMergeRender.transform.SetParent(SlotParentMerge);
                    ItemMergeRender.transform.localPosition = new Vector3(0, 0, 0);
                    ItemMergeRender.transform.localScale = Vector3.one;
                    SlotMerge.SetViewMergeDone(SlotMerge.Type);
                    int max = 0;
                    for (int i = 0; i < DataPlayer.GetListLevel().Count; i++)
                    {
                        if (max < DataPlayer.GetListLevel()[i])
                        {
                            max = DataPlayer.GetListLevel()[i];
                        }
                    }

                    Debug.LogError("da vao day");

                    for (int i = 0; i < DataPlayer.GetListCritters().Count; i++)
                    {
                        if (DataPlayer.GetListCritters()[i] == ItemMergeRender.GetComponent<MergeElement>().Type)
                        {
                            ItemMergeRender.GetComponent<MergeElement>().Avatar.color = Color.white;
                            ItemMergeRender.GetComponent<MergeElement>().MaskImg.gameObject.SetActive(false);
                            return;
                            //  return;
                        }
                        if (DataPlayer.GetListCritters()[i] != ItemMergeRender.GetComponent<MergeElement>().Type &&
                            ItemMergeRender.GetComponent<MergeElement>().Rarity <= max + 1)
                        {
                            ItemMergeRender.GetComponent<MergeElement>().Avatar.color = Color.black;
                            ItemMergeRender.GetComponent<MergeElement>().MaskImg.gameObject.SetActive(false);
                            //  return;
                        }
                        else if (DataPlayer.GetListCritters()[i] != ItemMergeRender.GetComponent<MergeElement>().Type &&
                            ItemMergeRender.GetComponent<MergeElement>().Rarity > max + 1)
                        {
                            ItemMergeRender.GetComponent<MergeElement>().Avatar.color = Color.black;
                            ItemMergeRender.GetComponent<MergeElement>().MaskImg.gameObject.SetActive(true);
                            //    return;
                        }


                        /*   if (DataPlayer.GetListCritters()[i] != ItemMergeRender.GetComponent<MergeElement>().Type &&
                               ItemMergeRender.GetComponent<MergeElement>().Rarity == max + 1)
                           {
                               ItemMergeRender.GetComponent<MergeElement>().Avatar.color = Color.black;
                               ItemMergeRender.GetComponent<MergeElement>().MaskImg.gameObject.SetActive(false);

                               return;
                           }
                           else if (DataPlayer.GetListCritters()[i] != ItemMergeRender.GetComponent<MergeElement>().Type &&
                               ItemMergeRender.GetComponent<MergeElement>().Rarity > max + 1)
                           {
                               ItemMergeRender.GetComponent<MergeElement>().Avatar.color = Color.black;
                               ItemMergeRender.GetComponent<MergeElement>().MaskImg.gameObject.SetActive(true);
                               return;
                           }
                           else if(DataPlayer.GetListCritters()[i] == ItemMergeRender.GetComponent<MergeElement>().Type)
                           {
                               ItemMergeRender.GetComponent<MergeElement>().Avatar.color = Color.white;
                               ItemMergeRender.GetComponent<MergeElement>().MaskImg.gameObject.SetActive(false);
                               return;
                           }
                           if (DataPlayer.GetListCritters()[i] != ItemMergeRender.GetComponent<MergeElement>().Type)
                           {
                               ItemMergeRender.GetComponent<MergeElement>().Avatar.color = Color.black;
                               ItemMergeRender.GetComponent<MergeElement>().MaskImg.gameObject.SetActive(false);

                               return;
                           }*/

                    }
                }
            }
        }
    }
    public void LoadDataElement(int index, ElementData elementData)
    {
        if (index == 1)
        {
            Item1Render = Instantiate(prefabsItemMerge);
            Item1Render.GetComponent<MergeElement>().Type = slot1.Type;
            Item1Render.GetComponent<MergeElement>().Init(elementData);

            Item1Render.transform.SetParent(SlotParent1.transform);
            Item1Render.transform.position = Input.mousePosition;
            Item1Render.transform.DOLocalMove(Vector3.zero, 0.2f).OnStart(() =>
            {
                AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectWosh);
                Item1Render.AddComponent<Canvas>();
                Item1Render.GetComponent<Canvas>().overrideSorting = true;
                Item1Render.GetComponent<Canvas>().sortingOrder = 10000;
            }).OnComplete(() =>
            {
                Item1Render.GetComponent<MergeElement>().Effect.SetActive(true);
                Destroy(Item1Render.GetComponent<Canvas>());
                AudioManager.instance.PlaySound(AudioManager.instance.Sound_Effect_Card_UITeam_And_Merge_To_Slot);
            });
            Item1Render.transform.localScale = Vector3.one;

            /**//* DataPlayer.AddAlliedIteam(elementData);*/
            DataPlayer.AddAlliedIteamMerge(elementData);
            // DataPlayer.Remove(elementData.Type, elementData.ID);

            L_elementData.Add(elementData);
        }
        else
        {
            Item2Render = Instantiate(prefabsItemMerge);

            Item2Render.GetComponent<MergeElement>().Type = slot2.Type;
            Item2Render.GetComponent<MergeElement>().Init(elementData);
            Item2Render.transform.SetParent(SlotParent2);
            Item2Render.transform.position = Input.mousePosition;
            Item2Render.transform.DOLocalMove(Vector3.zero, 0.2f).OnStart(() =>
            {
                AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectWosh);
                Item2Render.AddComponent<Canvas>();
                Item2Render.GetComponent<Canvas>().overrideSorting = true;
                Item2Render.GetComponent<Canvas>().sortingOrder = 10000;
            }).OnComplete(() =>
            {
                Item2Render.GetComponent<MergeElement>().Effect.SetActive(true);
                Destroy(Item2Render.GetComponent<Canvas>());
                AudioManager.instance.PlaySound(AudioManager.instance.Sound_Effect_Card_UITeam_And_Merge_To_Slot);
            });
            Item2Render.transform.localScale = Vector3.one;

            /*DataPlayer.AddAlliedIteam(elementData);*/
            DataPlayer.AddAlliedIteamMerge(elementData);

            // DataPlayer.Remove(elementData.Type, elementData.ID);
            L_elementData.Add(elementData);
        }
    }
    public void RemoveItem(int index)
    {
        ElementData elementdata = new ElementData();
        if (index == 1)
        {
            DataPlayer.RemoveItemMerge(Item1Render.GetComponent<MergeElement>().ThisElementData);
            Destroy(ItemMergeRender);
            if (Item1Render == null) return;

            Destroy(Item1Render);

            //DataPlayer.Add(elementdata);
            DEL_SLOT1.gameObject.SetActive(false);
            slot1.gameObject.transform.SetAsLastSibling();
            slot1.gameObject.SetActive(true);
            //  LoadEventory(Eventory.gameObject);

            slot1 = null;
            ButtonMergeTxt.text = str;

            m_UITeam.AnonymousCard(Eventory);
            if (SlotMerge)
                SlotMerge.Type = ECharacterType.NONE;
            SlotMerge = null;
        }
        else if (index == 2)
        {
            DataPlayer.RemoveItemMerge(Item2Render.GetComponent<MergeElement>().ThisElementData);

            Destroy(ItemMergeRender);
            if (Item2Render == null) return;

            Destroy(Item2Render);
            ButtonMergeTxt.text = str;
            //    DataPlayer.Add(elementdata);
            DEL_SLOT2.gameObject.SetActive(false);
            slot2.gameObject.transform.SetAsLastSibling();
            slot2.gameObject.SetActive(true);
            //   LoadEventory(Eventory.gameObject);
            if (SlotMerge)
                SlotMerge.Type = ECharacterType.NONE;

            slot2 = null;
            m_UITeam.AnonymousCard(Eventory);
            SlotMerge = null;
        }
    }
    public bool isMerge = false;

    public void ShowIntroMerge()
    {
        IntroMerge.gameObject.SetActive(true);
        IntroMerge.GetComponent<MergeIntro>().ActionMerge();
    }
    public void OnMergeBtn()
    {
        /*if (SlotMerge.GetComponent<MergeElement>().Rarity >= 8)
        {
            popUpManager.Instance.m_POpUPCommingsoonAll.gameObject.SetActive(true);
            return;
        }*/
        int max = 0;
        for (int i = 0; i < DataPlayer.GetListLevel().Count; i++)
        {
            if (max < DataPlayer.GetListLevel()[i])
            {
                max = DataPlayer.GetListLevel()[i];
            }
        }
        if (SlotMerge.GetComponent<MergeElement>().Rarity > max + 1 && DataPlayer.GetIsCheckDoneTutorial() && SlotMerge.GetComponent<MergeElement>().Rarity < 9)
        {
            if (DataPlayer.GetCurrentLanguage() == "vi")
            {
                popUpManager.Instance.m_POpUpCommingSoon.COntent.text = "Hãy mở khóa vùng " + (SlotMerge.GetComponent<MergeElement>().Rarity - 1);
                popUpManager.Instance.m_POpUpCommingSoon.gameObject.SetActive(true);
                return;
            }
            else if (DataPlayer.GetCurrentLanguage() == "en")
            {
                popUpManager.Instance.m_POpUpCommingSoon.COntent.text = "Please unlock zone " + (SlotMerge.GetComponent<MergeElement>().Rarity - 1);
                popUpManager.Instance.m_POpUpCommingSoon.gameObject.SetActive(true);
                return;
            }
        }
        Coin = DataPlayer.GetCoin();
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            isEnoughtCoinMerge = true;
            isMergeWithCoin = true;
            isMergeWithAds = false;
            //     CurCoin = Coin - CoinMerge;
            CurCoin = Coin - CoinMerge;

            TutorialManager.Instance.DeSpawn();
            DataPlayer.SetIsCheckButtonOnMerge(true);
            MERGE.GetComponent<Canvas>().sortingOrder = 3;
            isMerge = true;
            if (slot1 != null && slot2 != null && SlotMerge.GetComponent<MergeElement>().Type != ECharacterType.NONE)
            {
                if (isEnoughtCoinMerge)
                {
                    ShowIntroMerge();
                    ButtonMergeTxt.text = str;
                    Debug.Log(SlotMerge.GetComponent<MergeElement>().Type);
                }
            }
            return;
        }

        if (CoinMerge <= Coin && SlotMerge.GetComponent<MergeElement>().Type != ECharacterType.NONE)
        {
            Debug.Log("du tien");
            isEnoughtCoinMerge = true;
            isMergeWithCoin = true;
            isMergeWithAds = false;
            CurCoin = Coin - CoinMerge;

            Debug.LogError("CurCoin: " + CurCoin);
            TutorialManager.Instance.DeSpawn();
            DataPlayer.SetIsCheckButtonOnMerge(true);
            MERGE.GetComponent<Canvas>().sortingOrder = 3;
            isMerge = true;
        }
        if (slot1 != null && slot2 != null && SlotMerge.GetComponent<MergeElement>().Type != ECharacterType.NONE)
        {
            if (isEnoughtCoinMerge)
            {
                ShowIntroMerge();
                ButtonMergeTxt.text = str;
                Debug.Log(SlotMerge.GetComponent<MergeElement>().Type);
            }
            else
            {
                Debug.LogWarning("ban khong du tien !"); // show pop UP cho nay !
                popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.COIN;
                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
            }
        }
    }
    private void ShowAdsReward()
    {
        if (SlotMerge.GetComponent<MergeElement>().Type == ECharacterType.NONE)
        {
            return;
        }
        AdStatus adstatus = SDKDGManager.Instance.AdsManager.ShowRewardedAdsStatus(() =>
        {
            OnMergeButtonFree();
            Controller.Instance.CountTime = 0;
            Debug.Log("Merge Free (ads)");
        }, "Merge");
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
    public void OnMergeButtonFree()
    {
        /*if (SlotMerge.GetComponent<MergeElement>().Type == ECharacterType.NONE)
        {
            return;
        }*/
        /*

                if (SlotMerge.GetComponent<MergeElement>().Rarity >= 8 && SlotMerge.Type != ECharacterType.NONE)
                {
                    popUpManager.Instance.m_POpUPCommingsoonAll.gameObject.SetActive(true);
                    return;
                }*/
        int max = 0;
        for (int i = 0; i < DataPlayer.GetListLevel().Count; i++)
        {
            if (max < DataPlayer.GetListLevel()[i])
            {
                max = DataPlayer.GetListLevel()[i];
            }
        }
        if (SlotMerge.GetComponent<MergeElement>().Rarity > max + 1 && DataPlayer.GetIsCheckDoneTutorial() && SlotMerge.GetComponent<MergeElement>().Rarity < 9)
        {
            /* popUpManager.Instance.m_POpUpCommingSoon.COntent.text = "Please unlock zone " + ((SlotMerge.GetComponent<MergeElement>().Rarity - 1));
             popUpManager.Instance.m_POpUpCommingSoon.gameObject.SetActive(true);*//* popUpManager.Instance.m_POpUpCommingSoon.COntent.text = "Please unlock zone " + ((SlotMerge.GetComponent<MergeElement>().Rarity - 1));
             popUpManager.Instance.m_POpUpCommingSoon.gameObject.SetActive(true);*/

            if (DataPlayer.GetCurrentLanguage() == "vi")
            {
                popUpManager.Instance.m_POpUpCommingSoon.COntent.text = "Hãy mở khóa vùng " + (SlotMerge.GetComponent<MergeElement>().Rarity - 1);
                popUpManager.Instance.m_POpUpCommingSoon.gameObject.SetActive(true);
                return;
            }
            else if (DataPlayer.GetCurrentLanguage() == "en")
            {
                popUpManager.Instance.m_POpUpCommingSoon.COntent.text = "Please unlock zone " + (SlotMerge.GetComponent<MergeElement>().Rarity - 1);
                popUpManager.Instance.m_POpUpCommingSoon.gameObject.SetActive(true);
                return;
            }
            return;
        }
        isMergeWithCoin = false;
        isMergeWithAds = true;
        if (slot1 != null && slot2 != null)
        {
            ShowIntroMerge();
        }
        isMerge = true;
    }
    public void SubCoin()
    {
        if (isMergeWithCoin)
        {
            /* if (DataPlayer.GetIsCheckDoneTutorial())
             {

             }*/
            Debug.LogError("CurCoin2: " + CurCoin);

            UI_Home.Instance.m_UICoin.CoinText.text = CurCoin.ToString();
            DataPlayer.SetCoin(CurCoin);
            UI_Home.Instance.m_UICoinManager.SetTextCoin();
        }
    }
    public bool isNew;
    public List<Element> L_newElement = new List<Element>();
    public bool isNewCritter;
    public void Merge()
    {
        isNew = true;
        SubCoin();
        DEL_SLOT1.gameObject.SetActive(false);
        DEL_SLOT2.gameObject.SetActive(false);
        GameObject MergeDone = Instantiate(prefabsItemEventory);
        // MergeDone.transform.SetParent(Eventory);
        MergeDone.GetComponent<Element>().Init();
        MergeDone.transform.localPosition = new Vector3(0, 7, 0);
        MergeDone.transform.localScale = Vector3.one;
        MergeDone.GetComponent<Element>().Type = SlotMerge.Type;
        L_newElement.Add(MergeDone.GetComponent<Element>());
       /* for (int i = 0; i < DataPlayer.GetListCritters().Count; i++)
        {
            if (DataPlayer.GetListCritters()[i] != MergeDone.GetComponent<Element>().Type)
            {
                isNewCritter = true;
                PopUpSuccess.GetComponent<UIPopUp>().New_Img.SetActive(true);
                Controller.Instance.L_TypeNewUICritter.Add(MergeDone.GetComponent<Element>().Type);
            }
            else
            {
                isNewCritter = false;
                PopUpSuccess.GetComponent<UIPopUp>().New_Img.SetActive(false);
            }
        }*/
        if (!DataPlayer.GetListCritters().Contains(MergeDone.GetComponent<Element>().Type))
        {
            isNewCritter = true;
            PopUpSuccess.GetComponent<UIPopUp>().New_Img.SetActive(true);
            Controller.Instance.L_TypeNewUICritter.Add(MergeDone.GetComponent<Element>().Type);
        }
        else
        {
            isNewCritter = false;
            PopUpSuccess.GetComponent<UIPopUp>().New_Img.SetActive(false);
        }
        DataPlayer.Add(SlotMerge.Type);
        DataPlayer.AddCritter(SlotMerge.Type);
        Controller.Instance.L_TypeNewUICritter.Add(SlotMerge.Type);

        UI_Home.Instance.m_UiCritter.UpdateView(MergeDone.GetComponent<Element>().Rarity);
        //  UI_Home.Instance.m_UIPopUp._ShowPopUpSuccess(SlotMerge);
        Destroy(SlotMerge.gameObject);
        SlotMerge = null;
        Destroy(Item1Render);
        Destroy(Item2Render);
        Destroy(slot1.gameObject);
        Destroy(slot2.gameObject);
        Debug.Log("List: " + JsonConvert.SerializeObject(L_elementData));
        StartCoroutine(IE_Delay(MergeDone));
    }
    IEnumerator IE_Delay(GameObject obj)
    {
        yield return null;
        for (int i = 0; i < L_elementData.Count; i++)
        {
            for (int j = 0; j < DataPlayer.GetListAllidMerge().Count; j++)
            {
                if (L_elementData[i].Type == DataPlayer.GetListAllidMerge()[j].Type &&
                    L_elementData[i].ID == DataPlayer.GetListAllidMerge()[j].ID)
                {
                    DataPlayer.Remove(L_elementData[i].Type, L_elementData[i].ID);
                    DataPlayer.RemoveItem(L_elementData[i]);
                    DataPlayer.RemoveItemMerge(DataPlayer.GetListAllidMerge()[j]);
                    break;
                }
            }
        }
        L_elementData.Clear();
        isEnoughtCoinMerge = false;

        Destroy(obj);
    }
    public void LoadEventory(GameObject eventory)
    {
        var arr = eventory.GetComponentsInChildren<Element>();
        for (int i = 0; i < arr.Length; i++)
        {
            Destroy(arr[i].gameObject);
        }
        StartCoroutine(IE_SpawnEventory());
    }
    IEnumerator IE_SpawnEventory()
    {
        yield return null;
        m_UITeam.Spawn(Eventory);
        m_UITeam.AnonymousCard(Eventory);
        if (!DataPlayer.GetIsCheckButtonOnMerge())
        {
            Eventory.transform.GetChild(1).GetComponent<Element>().PurchaseBtn.interactable = false;
            Eventory.transform.GetChild(2).GetComponent<Element>().PurchaseBtn.interactable = false;
        }
        if (DataPlayer.GetIsCheckButtonOnMerge() && !DataPlayer.GetIsCheckDoneTutorial())
        {
            for (int i = 0; i < m_UITeam.L_elementEventory.Count; i++)
            {
                if (m_UITeam.L_elementEventory[i] != null)
                {
                    m_UITeam.L_elementEventory[i].PurchaseBtn.interactable = false;
                }
            }
        }
    }
    public void ResetItemMerge()
    {
        DataPlayer.ResetItemMerge();
        if (ItemMergeRender != null)
        {
            Destroy(ItemMergeRender.GetComponent<MergeElement>().gameObject);
        }
        else
            return;
        Debug.Log(ItemMergeRender);
    }
}
