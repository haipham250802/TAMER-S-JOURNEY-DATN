using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
public class UI_Team : MonoBehaviour
{
    public GameObject HandPrefabs;
    public Vector3 offset;

    public GameObject Eventory;
    public GameObject prefabsItemEventory;

    GameObject Item1Render, Item2Render, Item3Render, Item4Render;
    public GameObject ItemBased;

    public ElementBase Slot1;
    public ElementBase Slot2;
    public ElementBase Slot3;
    public ElementBase Slot4;

    public GameObject SlotParent01, SlotParent02, SlotParent03, SlotParent04, SLOT, MoveParent;
    public GameObject BGMove;
    public GameObject ShopObj;

    public Transform startPos, endpos, MovePos, PosOutTeam;
    public float Duration;

    public Button DEL_SLOT1, DEL_SLOT2, DEL_SLOT3, DEL_SLOT4, ActiveSlot4, OutButton;

    public Text UnlockSot4txt;
    public ToastManager m_ToastManager;
    public GameObject Unlock;
    public GameObject UnlockInUIHome;

    public List<GameObject> L_G_Render = new List<GameObject>();
    public List<Element> L_elementEventory = new List<Element>();
    public List<Element> L_SLot = new List<Element>();
    public List<GameObject> L_BGClone = new List<GameObject>();
    public int GemActiveSlot4;
    public List<ElementBase> L_elementBase;

    public Button LinkMergeButton;
    public ScrollRect Scroll;

    public GameObject MaskObj;
    private void Awake()
    {
        LinkMergeButton.onClick.AddListener(OnclickButtonLinkMerge);
    }
    public void OnclickButtonLinkMerge()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            this.gameObject.SetActive(false);
            UI_Home.Instance.m_UIMerge.gameObject.SetActive(true);
            UI_Home.Instance.m_UICollection.ActiveUIMerge();
            UI_Home.Instance.m_UIMerge.ResetItemMerge();
            UI_Home.Instance.m_UIMerge.ResetAllid();
            UI_Home.Instance.m_UIMerge.LoadEventory(UI_Home.Instance.m_UIMerge.Eventory.gameObject);
        }
    }

    private void Start()
    {
        OutButton.onClick.AddListener(OnClickOutBtn);
        gameObject.SetActive(false);
        if (!DataPlayer.GetIsActiveSLot4())
        {
            DEL_SLOT4.enabled = false;
            ActiveSlot4.onClick.AddListener(OnClickSlot4);
        }
        if (DataPlayer.GetIsActiveSLot4())
        {
            DEL_SLOT4.enabled = true;
            ActiveSlot4.gameObject.SetActive(false);
            UnlockInUIHome.SetActive(false);
            Unlock.SetActive(false);
        }
        if (!DataPlayer.GetIsActiveSLot4())
        {
            UnlockSot4txt.text = GemActiveSlot4.ToString();
        }
        if (DataPlayer.GetIsTapToBackUITeam())
        {
            Destroy(OutButton.GetComponent<GraphicRaycaster>());
            Destroy(OutButton.GetComponent<Canvas>());
        }
    }
    public void OnClickSlot4()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (DataPlayer.GetGem() < GemActiveSlot4)
            {
                popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.GEM;
                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
            }
            else
            {
                /*  int Curgem = DataPlayer.GetGem();
                  Curgem -= GemActiveSlot4;
                  DataPlayer.SetGem(Curgem);
                  UI_Home.Instance.m_UIGemManager.SetTextGem(Curgem);
                  Unlock.gameObject.SetActive(false);
                  DataPlayer.SetIsActiveSlot4(true);
                  UnlockInUIHome.SetActive(false);
                  ActiveSlot4.gameObject.SetActive(false);
                  DEL_SLOT4.enabled = true;*/
                popUpManager.Instance.m_POpUpConFirm.gameObject.SetActive(true);
            }
        }
    }
    public void LoadView()
    {
        Unlock.gameObject.SetActive(false);
        UnlockInUIHome.SetActive(false);
        ActiveSlot4.gameObject.SetActive(false);
        DEL_SLOT4.enabled = true;
    }
    public void OnClickOutBtn()
    {
      
        if (player.Instance.BagBtn.GetComponent<Button>().interactable == false)
        {
            player.Instance.BagBtn.GetComponent<Button>().interactable = true;
        }
        UI_Home.Instance.ActiveUIHome();

        m_ToastManager.Toast.SetActive(false);
        //   UI_Home.Instance.ActiveBag();
        UI_Home.Instance.m_UICollection.UnPauseGame();
       /* if (!UI_Home.Instance.uI_Battle.gameObject.activeInHierarchy)
            player.Instance.Option.gameObject.SetActive(true);*/
        DataPlayer.SetIsTapToBackUITeam(true);
        DataPlayer.SetIsCheckTapButtonteam(true);

        Destroy(UI_Home.Instance.m_Bag.UITeamBtn.GetComponent<GraphicRaycaster>());
        Destroy(UI_Home.Instance.m_Bag.UITeamBtn.GetComponent<Canvas>());
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
        /*  if (!DataPlayer.GetIsCheckDoneTutorial())
          {
              *//* LoadSceneManager.Instance.LOAD_MAP_01();
               DataPlayer.AddListLevel(1);
               DataPlayer.SetCurLevel(1);
               UI_Home.Instance.m_UIselectMap.CurrentLevel();
               DataPlayer.SetIsTapToBackUITeam(true);
               OutButton.GetComponent<Canvas>().sortingOrder = 8;
               DataPlayer.SetCoin(100);
               UI_Home.Instance.m_UICoinManager.SetTextCoin();*//*
              if (!DataPlayer.GetIsCheckTapButtonTeam())
              {

              }

              if (!DataPlayer.GetIsCheckTapCloseBag() && !DataPlayer.GetIsCheckDoneTutorial())
              {
                  UI_Home.Instance.m_Bag.GetComponent<Canvas>().sortingOrder = 10002;
                  TutorialManager.Instance.SpawnHandUIHome(UI_Home.Instance.m_Player.Menu.transform, new Vector3(325, -70));
                  DataPlayer.SetIsCheckTapCloseBag(true);
              }
              //   DataPlayer.SetIsCheckDoneTutorial(true);
          }*/
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            DataPlayer.SetIsCheckDoneTutorial(true);
            LoadSceneManager.Instance.LOAD_MAP_01();
            DataPlayer.AddListLevel(1);
            DataPlayer.SetCurLevel(1);
            UI_Home.Instance.m_UIselectMap.CurrentLevel();
            TutorialManager.Instance.DestroyAllObjHandClone();

            player.Instance.transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
            player.Instance.isFacingRight = true;
        }
    }
    Tweener twenner;
    void KillTween()
    {
        twenner.Kill();
    }
    private void OnDisable()
    {
        KillTween();
        isMoveBGDone = true;
        for (int i = 0; i < L_CardAnonymous.Count; i++)
        {
            Destroy(L_CardAnonymous[i].gameObject);
        }
        L_CardAnonymous.Clear();
        /* if (player.Instance.gameObject)
             player.Instance.EnableCollider();*/
    }
    private void OnEnable()
    {
        isMoveBGDone = false;
        BG_Parent.transform.position = StartPosBG.position;
        if (this.gameObject.activeInHierarchy)
            StartCoroutine(IE_delayAnonymousCard(Eventory.transform));
        StartCoroutine(IEdelay());
    }
    IEnumerator IEdelay()
    {
        yield return null;
        player.Instance.gameObject.GetComponent<Collider2D>().enabled = false;
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            MaskObj.SetActive(false);
        }
        else
        {
            Controller.Instance.exampObj.gameObject.SetActive(true);
            string str = I2.Loc.LocalizationManager.GetTranslation("KEY_TUT_ADD_SLOT_TEAM");
            ExampleStoryTut.Instance.SetText(str);
            MaskObj.SetActive(true);
        }
    }
    public GameObject BG_Parent;

    public Transform StartPosBG;
    public Transform EndPosBG;
    public bool isMoveBGDone = true;

    public void MoveBackGround(Transform BG)
    {
        twenner = BG.DOMove(EndPosBG.position, 60).SetEase(Ease.Linear).OnComplete(() =>
        {
            BG.position = StartPosBG.position;
            isMoveBGDone = false;
        });
    }
    private void Update()
    {
        for (int i = 0; i < L_elementEventory.Count; i++)
        {
            if (L_elementEventory[i] == null)
            {
                L_elementEventory.RemoveAt(i);
            }
        }
        for (int i = 0; i < L_SLot.Count; i++)
        {
            if (L_SLot[i] == null)
            {
                L_SLot.RemoveAt(i);
            }
        }
        if (!isMoveBGDone)
        {
            MoveBackGround(BG_Parent.transform);
            isMoveBGDone = true;
        }
    }
    public void Test()
    {

    }
    public void ResetAllidInEventory()
    {
        SetDataElementInEventory();
        for (int i = 0; i < L_elementEventory.Count; i++)
        {
            Debug.Log("da den");
            if (L_elementEventory[i] != null)
                Destroy(L_elementEventory[i].gameObject);
        }
    }
    public void ResetAllid()
    {
        Slot1 = null;
        Slot2 = null;
        Slot3 = null;
        Slot4 = null;

        var arr = SLOT.GetComponentsInChildren<Element>();
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i].transform.GetChild(0) != null)
            {
                Destroy(arr[i].GetComponentInChildren<Element>().gameObject);
            }
        }
        Debug.Log("da reset");
    }
    public void IsActive(int index)
    {
        Debug.Log("da bam 5");

        for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
        {
            for (int j = 0; j < L_elementEventory.Count; j++)
            {
                if (i == index)
                {
                    if (L_elementEventory[j] != null)
                    {
                        if (DataPlayer.GetListAllid()[i].ID == L_elementEventory[j].ID &&
                            DataPlayer.GetListAllid()[i].Type == L_elementEventory[j].Type)
                        {
                            L_elementEventory[j].HP = DataPlayer.GetListAllid()[i].HP;
                            DataPlayer.SetHP(L_elementEventory[j].Type, L_elementEventory[j].ID, L_elementEventory[j].HP);
                            L_elementEventory[j].TxtHP.text = L_elementEventory[j].HP.ToString();
                            L_elementEventory[j].HP_Bar.value = DataPlayer.GetListAllid()[i].HP;
                            L_elementEventory[j].gameObject.SetActive(true);
                            if (L_SLot.Count > 1)
                            {
                                for (int k = 0; k < L_SLot.Count; k++)
                                {
                                    if (L_SLot[k] != null)
                                    {
                                        if (DataPlayer.GetListAllid()[i].ID == L_SLot[k].ID &&
                                            DataPlayer.GetListAllid()[i].Type == L_SLot[k].Type)
                                        {
                                            Destroy(L_SLot[k].gameObject);
                                            DataPlayer.RemoveItem(DataPlayer.GetListAllid()[i]);

                                            return;
                                        }
                                    }
                                }
                            }
                            else if (L_SLot.Count == 1)
                            {
                                if (L_SLot[0] != null)
                                {
                                    if (DataPlayer.GetListAllid()[i].ID == L_SLot[0].ID &&
                                        DataPlayer.GetListAllid()[i].Type == L_SLot[0].Type)
                                    {
                                        DataPlayer.GetListAllid()[i].HP = L_SLot[0].HP;
                                        Destroy(L_SLot[0].gameObject);
                                        DataPlayer.RemoveItem(DataPlayer.GetListAllid()[i]);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public void LoadAllied()
    {
        for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
        {
            if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
            {
                //   LoadAllidElement(i, element);

                if (Slot1 == null)
                {
                    GameObject tempItem = Instantiate(ItemBased);
                    tempItem.transform.SetParent(SLOT.transform.GetChild(i));
                    tempItem.transform.localScale = Vector3.one;
                    tempItem.transform.localPosition = Vector3.zero;
                    tempItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 195.3026f);
                    ElementData element = new ElementData();
                    element.Type = DataPlayer.GetListAllid()[i].Type;
                    element.ID = DataPlayer.GetListAllid()[i].ID;
                    element.HP = DataPlayer.GetListAllid()[i].HP;
                    element.Rarity = DataPlayer.GetListAllid()[i].Rarity;

                    tempItem.GetComponent<Element>().PurchaseBtn.enabled = false;
                    tempItem.GetComponent<Element>().Init(element);
                    Debug.Log("a " + i);

                    switch (i)
                    {
                        case 0:
                            Slot1 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 1:
                            Slot2 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 2:
                            Slot3 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 3:
                            Slot4 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                    }
                }
                else if (Slot2 == null)
                {
                    GameObject tempItem = Instantiate(ItemBased);
                    tempItem.transform.SetParent(SLOT.transform.GetChild(i));
                    tempItem.transform.localScale = Vector3.one;
                    tempItem.transform.localPosition = Vector3.zero;
                    tempItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 195.3026f);
                    ElementData element = new ElementData();
                    element.Type = DataPlayer.GetListAllid()[i].Type;
                    element.ID = DataPlayer.GetListAllid()[i].ID;
                    element.HP = DataPlayer.GetListAllid()[i].HP;
                    element.Rarity = DataPlayer.GetListAllid()[i].Rarity;

                    tempItem.GetComponent<Element>().PurchaseBtn.enabled = false;
                    tempItem.GetComponent<Element>().Init(element);
                    switch (i)
                    {
                        case 0:
                            Slot1 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 1:
                            Slot2 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 2:
                            Slot3 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 3:
                            Slot4 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                    }

                }
                else if (Slot3 == null)
                {
                    GameObject tempItem = Instantiate(ItemBased);
                    tempItem.transform.SetParent(SLOT.transform.GetChild(i));
                    tempItem.transform.localScale = Vector3.one;
                    tempItem.transform.localPosition = Vector3.zero;
                    tempItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 195.3026f);
                    ElementData element = new ElementData();
                    element.Type = DataPlayer.GetListAllid()[i].Type;
                    element.ID = DataPlayer.GetListAllid()[i].ID;
                    element.HP = DataPlayer.GetListAllid()[i].HP;
                    element.Rarity = DataPlayer.GetListAllid()[i].Rarity;

                    tempItem.GetComponent<Element>().PurchaseBtn.enabled = false;
                    tempItem.GetComponent<Element>().Init(element);
                    switch (i)
                    {
                        case 0:
                            Slot1 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 1:
                            Slot2 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 2:
                            Slot3 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                        case 3:
                            Slot4 = tempItem.GetComponent<ElementBase>();
                            L_SLot.Add(tempItem.GetComponent<Element>());
                            break;
                    }

                }
                else if (Slot4 == null)
                {
                    if (DataPlayer.GetIsActiveSLot4())
                    {

                        GameObject tempItem = Instantiate(ItemBased);
                        tempItem.transform.SetParent(SLOT.transform.GetChild(i));
                        tempItem.transform.localScale = Vector3.one;
                        tempItem.transform.localPosition = Vector3.zero;
                        tempItem.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 195.3026f);
                        ElementData element = new ElementData();
                        element.Type = DataPlayer.GetListAllid()[i].Type;
                        element.ID = DataPlayer.GetListAllid()[i].ID;
                        element.HP = DataPlayer.GetListAllid()[i].HP;
                        element.Rarity = DataPlayer.GetListAllid()[i].Rarity;

                        tempItem.GetComponent<Element>().PurchaseBtn.enabled = false;
                        tempItem.GetComponent<Element>().Init(element);
                        switch (i)
                        {
                            case 0:
                                Slot1 = tempItem.GetComponent<ElementBase>();
                                L_SLot.Add(tempItem.GetComponent<Element>());
                                break;
                            case 1:
                                Slot2 = tempItem.GetComponent<ElementBase>();
                                L_SLot.Add(tempItem.GetComponent<Element>());
                                break;
                            case 2:
                                Slot3 = tempItem.GetComponent<ElementBase>();
                                L_SLot.Add(tempItem.GetComponent<Element>());
                                break;
                            case 3:
                                Slot4 = tempItem.GetComponent<ElementBase>();
                                L_SLot.Add(tempItem.GetComponent<Element>());
                                break;
                        }

                    }
                }


                if (L_elementEventory.Count > 0)
                {
                    for (int k = 0; k < L_elementEventory.Count; k++)
                    {
                        if (DataPlayer.GetListAllid()[i].Type == L_elementEventory[k].Type &&
                            DataPlayer.GetListAllid()[i].ID == L_elementEventory[k].ID)
                        {
                            if (L_elementEventory[k] != null)
                            {
                                L_elementEventory[k].gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }

    }
    public void Spawn(Transform _Eventory)
    {
        // TutorialManager.Instance.DeSpawn();
        foreach (var kv in DataPlayer.GetDictionary())
        {
            for (int i = 0; i < kv.Value.Count; i++)
            {
                GameObject tempItem = Instantiate(prefabsItemEventory);
                tempItem.transform.SetParent(_Eventory.transform);
                tempItem.transform.localScale = Vector3.one;
                tempItem.transform.localPosition = Vector3.zero;
                ElementData element = new ElementData();
                element.Type = kv.Key;
                element.ID = kv.Value[i].ID;
                element.HP = kv.Value[i].HP;
                element.Rarity = kv.Value[i].Rarity;

                tempItem.GetComponent<Element>().Init(element);
                L_elementEventory.Add(tempItem.GetComponent<Element>());

                for (int j = 0; j < UI_Home.Instance.m_UIMerge.L_newElement.Count; j++)
                {
                  //  Element e = tempItem.GetComponent<Element>();
                    if (tempItem.GetComponent<Element>().ID == UI_Home.Instance.m_UIMerge.L_newElement[j].ID && tempItem.GetComponent<Element>().Type == UI_Home.Instance.m_UIMerge.L_newElement[j].Type)
                    {
                        tempItem.GetComponent<Element>().NewImg.gameObject.SetActive(true);
                    }
                }
            }
        }
        if (this.gameObject.activeInHierarchy)
        {
            StartCoroutine(IE_delayAnonymousCard(_Eventory));
            StartCoroutine(IESpawnHand(_Eventory));
        }

        if (!DataPlayer.getisCheckTapSLotMerge() && UI_Home.Instance.m_UIMerge.gameObject.activeInHierarchy && !DataPlayer.GetIsCheckDoneTutorial())
        {
            TutorialManager.Instance.SpawnHandUIHome(_Eventory.transform.GetChild(0), new Vector3(30, -70));
        }
        if (UI_Home.Instance.m_UIMerge.isNew)
        {
            /*for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
            {
                for (int j = 0; j < L_elementEventory.Count; j++)
                {
                    if (DataPlayer.GetListAllid()[i].Type == L_elementEventory[j].Type &&
                        DataPlayer.GetListAllid()[i].ID == L_elementEventory[j].ID)
                    {
                        L_elementEventory[j].NewImg.gameObject.SetActive(true);
                    }
                }
            }
            UI_Home.Instance.m_UIMerge.isNew = false;*/
        }
        if (DataPlayer.GetisCheckTapMergeBtn())
            SortEventory();
        if (!DataPlayer.GetIsTapToBackUITeam() && !DataPlayer.GetIsCheckDoneTutorial() && DataPlayer.GetIsCheckOutUiMerge())
        {
            Debug.LogError("TYPE: " + UI_Home.Instance.m_UITeam.L_elementEventory[1].Type);
            this.Eventory.transform.GetChild(1).GetComponent<Element>().PurchaseBtn.enabled = false;
            Scroll.vertical = false;
        }
        else if (DataPlayer.GetIsCheckDoneTutorial())
        {
            //  UI_Home.Instance.m_UITeam.L_elementEventory[1].PurchaseBtn.gameObject.SetActive(true);
            Scroll.vertical = true;
        }

    }
    public void SortEventory()
    {
        for (int i = 0; i < L_elementEventory.Count; i++)
        {
            for (int j = 1; j < L_elementEventory.Count; j++)
            {
                if (L_elementEventory[i].Rarity > L_elementEventory[j].Rarity)
                {
                    var temp = L_elementEventory[i];
                    L_elementEventory[i] = L_elementEventory[j];
                    L_elementEventory[j] = temp;
                }
            }
        }
        for (int i = 0; i < L_elementEventory.Count; i++)
        {
            if (L_elementEventory[i])
                L_elementEventory[i].gameObject.transform.SetAsLastSibling();
        }
    }
    IEnumerator IESpawnHand(Transform e)
    {
        yield return new WaitForSeconds(0.1f);
        if (!DataPlayer.GetIsCheckAddToSlot() && this.gameObject.activeInHierarchy && !DataPlayer.GetIsCheckDoneTutorial())
        {
            TutorialManager.Instance.SpawnHandUIHome(Eventory.transform.GetChild(0).transform, new Vector3(30, -70));
               for (int i = 0; i < TutorialManager.Instance.l_obj.Count; i++)
        {
            if (TutorialManager.Instance.l_obj[i].gameObject != null)
                TutorialManager.Instance.l_obj[i].transform.localScale = Vector3.zero;
        }
        }
    }
    IEnumerator IE_delayAnonymousCard(Transform parent)
    {
        yield return null;
        AnonymousCard(parent);
    }
    public void SetDataElementInEventory()
    {
        foreach (var kv in DataPlayer.GetDictionary())
        {
            for (int i = 0; i < kv.Value.Count; i++)
            {
                for (int j = 0; j < DataPlayer.GetListAllid().Count; j++)
                {

                    if (kv.Value[i].ID == DataPlayer.GetListAllid()[j].ID &&
                       kv.Value[i].Type == DataPlayer.GetListAllid()[j].Type)
                    {
                        kv.Value[i].HP = DataPlayer.GetListAllid()[j].HP;
                        DataPlayer.SetHP(kv.Value[i].Type, kv.Value[i].ID, kv.Value[i].HP);
                    }
                }
            }
        }
    }
    public void HiddenAllidEventory()
    {
        Debug.Log("hidden");
        for (int i = 0; i < L_elementEventory.Count; i++)
        {
            for (int j = 0; j < DataPlayer.GetListAllid().Count; j++)
            {
                if (L_elementEventory[i] != null)
                {
                    if (L_elementEventory[i].Type == DataPlayer.GetListAllid()[j].Type &&
                        L_elementEventory[i].ID == DataPlayer.GetListAllid()[j].ID)
                    {
                        L_elementEventory[i].gameObject.SetActive(false);
                        DataPlayer.SetHP(DataPlayer.GetListAllid()[j].Type, DataPlayer.GetListAllid()[j].ID, DataPlayer.GetListAllid()[j].HP);
                    }
                }
            }
        }
    }
    public void ADD_SLOT_ELEMENT_TEAM(Element CrtterItem, Action<bool> successed)
    {
        TutorialManager.Instance.DeSpawn();
        if (Slot1 != null && Slot2 != null && Slot3 != null)
        {
            if (!DataPlayer.GetIsActiveSLot4())
            {
                switch(DataPlayer.GetCurrentLanguage())
                {
                    case "vi":
                        m_ToastManager.SetText("Đã hết chỗ trống , vui lòng mở khóa ô trống thứ 4 !");
                        break;
                    case "en":
                        m_ToastManager.SetText("Slot Is Full , You Can Unlock Slot 4 !");
                        break;
                }
                MoveToast();
            }
        }
        if (Slot1 != null && Slot2 != null && Slot3 != null && Slot4 != null)
        {
            switch (DataPlayer.GetCurrentLanguage())
            {
                case "vi":
                    m_ToastManager.SetText("Chỗ trống đã hết !");
                    break;
                case "en":
                    m_ToastManager.SetText("Slot Is Full !");
                    break;
            }
            MoveToast();
        }
        if (Slot1 == null)
        {
            Slot1 = CrtterItem;
            LoadDataElement(1, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT1.gameObject.SetActive(true);
            if (this.gameObject.activeInHierarchy)
                /*StartCoroutine(IE_delayAnonymousCard(Eventory.transform));*/
                AnonymousCard(Eventory.transform);

        }
        else if (Slot2 == null)
        {
            Slot2 = CrtterItem;
            LoadDataElement(2, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT2.gameObject.SetActive(true);
            if (this.gameObject.activeInHierarchy)
                /*                StartCoroutine(IE_delayAnonymousCard(Eventory.transform));
                */
                AnonymousCard(Eventory.transform);

        }
        else if (Slot3 == null)
        {
            Slot3 = CrtterItem;
            LoadDataElement(3, CrtterItem.ThisElementData);
            successed?.Invoke(true);
            DEL_SLOT3.gameObject.SetActive(true);
            if (this.gameObject.activeInHierarchy)
                /*StartCoroutine(IE_delayAnonymousCard(Eventory.transform));*/
                AnonymousCard(Eventory.transform);
        }
        else if (Slot4 == null)
        {
            if (DataPlayer.GetIsActiveSLot4())
            {
                Slot4 = CrtterItem;
                LoadDataElement(4, CrtterItem.ThisElementData);
                successed?.Invoke(true);
                DEL_SLOT4.gameObject.SetActive(true);
                if (this.gameObject.activeInHierarchy)
                    /*                    StartCoroutine(IE_delayAnonymousCard(Eventory.transform));
                    */
                    AnonymousCard(Eventory.transform);
            }
        }

        else
        {
            Debug.Log("alo alo");
            successed?.Invoke(false);
        }
        if (Slot1 != null && Slot2 == null && !DataPlayer.GetIsCheckAddToSlot())
        {
            Eventory.transform.GetChild(1).GetComponent<Element>().PurchaseBtn.enabled = true;
           
            TutorialManager.Instance.SpawnHandUIHome(Eventory.transform.GetChild(1).transform, new Vector3(30, -70));
            for (int i = 0; i < TutorialManager.Instance.l_obj.Count; i++)
            {
                if (TutorialManager.Instance.l_obj[i].gameObject != null)
                    TutorialManager.Instance.l_obj[i].transform.localScale = Vector3.one;
            }
        }
        if (Slot1 != null && Slot2 != null)
        {
            if (this.gameObject.activeInHierarchy)
            {
               
                if (!DataPlayer.GetIsTapToBackUITeam() && !DataPlayer.GetIsCheckDoneTutorial())
                {
                    OutButton.GetComponent<Canvas>().sortingOrder = 10002;
                    //   DataPlayer.SetIsCheckDoneTutorial(true);
                    //   OnClickOutBtn();
                    Controller.Instance.exampObj.gameObject.SetActive(true);
                    string str = I2.Loc.LocalizationManager.GetTranslation("KEY_TUT_DONE");
                    TutorialManager.Instance.DeSpawn();
                    DataPlayer.SetIsCheckAddToSlot(true);
                    ExampleStoryTut.Instance.SetText(str);
                    TutorialManager.Instance.SpawnHandUIHome(PosOutTeam.transform, new Vector3(60, -110, 0));

                 /*   for (int i = 0; i < TutorialManager.Instance.l_obj.Count; i++)
                    {
                        if (TutorialManager.Instance.l_obj[i].gameObject != null)
                            TutorialManager.Instance.l_obj[i].transform.localScale = Vector3.one;
                    }*/
                }

            }
        }
    }
    public void MoveToast()
    {
        m_ToastManager.ResetToast();
        m_ToastManager.MoveToastDontRepeat();
    }
    public void LoadDataElement(int index, ElementData elemendata)
    {
        if (index == 1)
        {
            Item1Render = Instantiate(ItemBased);

            Item1Render.GetComponent<Element>().Type = Slot1.Type;
            Item1Render.GetComponent<Element>().Init(elemendata);

            Item1Render.transform.SetParent(SlotParent01.transform);
            Item1Render.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 195.3026f);
            Item1Render.transform.position = Input.mousePosition;
            Item1Render.transform.DOLocalMove(Vector3.zero, 0.29f).OnStart(() =>
            {
                AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectWosh);
                Item1Render.AddComponent<Canvas>();
                Item1Render.GetComponent<Canvas>().overrideSorting = true;
                Item1Render.GetComponent<Canvas>().sortingOrder = 10000;
            }).OnComplete(() =>
            {
                Item1Render.GetComponent<Element>().Efect.SetActive(true);
                Destroy(Item1Render.GetComponent<Canvas>());
                AudioManager.instance.PlaySound(AudioManager.instance.Sound_Effect_Card_UITeam_And_Merge_To_Slot);
            });
            Item1Render.transform.localScale = Vector3.one;
            DataPlayer.AddAlliedIteam(elemendata);
        }
        else if (index == 2)
        {
            Item2Render = Instantiate(ItemBased);

            Item2Render.GetComponent<Element>().Type = Slot2.Type;
            Item2Render.GetComponent<Element>().Init(elemendata);
            Item2Render.transform.SetParent(SlotParent02.transform);
            Item2Render.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 195.3026f);
            Item2Render.transform.position = Input.mousePosition;
            Item2Render.transform.DOLocalMove(Vector3.zero, 0.2f).OnStart(() =>
            {
                AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectWosh);
                Item2Render.AddComponent<Canvas>();
                Item2Render.GetComponent<Canvas>().overrideSorting = true;
                Item2Render.GetComponent<Canvas>().sortingOrder = 10000;
            }).OnComplete(() =>
            {
                Item2Render.GetComponent<Element>().Efect.SetActive(true);
                Destroy(Item2Render.GetComponent<Canvas>());
                AudioManager.instance.PlaySound(AudioManager.instance.Sound_Effect_Card_UITeam_And_Merge_To_Slot);
            });
            Item2Render.transform.localScale = Vector3.one;

            DataPlayer.AddAlliedIteam(elemendata);
        }
        else if (index == 3)
        {
            Item3Render = Instantiate(ItemBased);

            Item3Render.GetComponent<Element>().Type = Slot3.Type;
            Item3Render.GetComponent<Element>().Init(elemendata);
            Item3Render.transform.SetParent(SlotParent03.transform);
            Item3Render.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 195.3026f);
            Item3Render.transform.position = Input.mousePosition;
            Item3Render.transform.DOLocalMove(Vector3.zero, 0.2f).OnStart(() =>
            {
                AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectWosh);
                Item3Render.AddComponent<Canvas>();
                Item3Render.GetComponent<Canvas>().overrideSorting = true;
                Item3Render.GetComponent<Canvas>().sortingOrder = 10000;
            }).OnComplete(() =>
            {
                Item3Render.GetComponent<Element>().Efect.SetActive(true);
                Destroy(Item3Render.GetComponent<Canvas>());
                AudioManager.instance.PlaySound(AudioManager.instance.Sound_Effect_Card_UITeam_And_Merge_To_Slot);
            });
            Item3Render.transform.localScale = Vector3.one;
            DataPlayer.AddAlliedIteam(elemendata);
        }
        if (DataPlayer.GetIsActiveSLot4())
        {
            if (index == 4)
            {
                Item4Render = Instantiate(ItemBased);
                Item4Render.GetComponent<Element>().Type = Slot4.Type;
                Item4Render.GetComponent<Element>().Init(elemendata);
                Item4Render.transform.SetParent(SlotParent04.transform);
                Item4Render.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 195.3026f);
                Item4Render.transform.position = Input.mousePosition;
                Item4Render.transform.DOLocalMove(Vector3.zero, 0.2f).OnStart(() =>
                {
                    AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectWosh);
                    Item4Render.AddComponent<Canvas>();
                    Item4Render.GetComponent<Canvas>().overrideSorting = true;
                    Item4Render.GetComponent<Canvas>().sortingOrder = 10000;
                }).OnComplete(() =>
                {
                    Item4Render.GetComponent<Element>().Efect.SetActive(true);
                    Destroy(Item4Render.GetComponent<Canvas>());
                    AudioManager.instance.PlaySound(AudioManager.instance.Sound_Effect_Card_UITeam_And_Merge_To_Slot);
                });
                Item4Render.transform.localScale = Vector3.one;

                DataPlayer.AddAlliedIteam(elemendata);
            }
        }
    }
    public void RemoveItem(int index)
    {
        ElementData elementdata = new ElementData();
        if (index == 1)
        {
            if (Item1Render == null) return;

            Destroy(Item1Render);
            Debug.Log(elementdata);
            DataPlayer.RemoveItem(Item1Render.GetComponent<Element>().ThisElementData);

            DEL_SLOT1.gameObject.SetActive(false);
            Slot1.gameObject.transform.SetAsFirstSibling();
            Slot1.gameObject.SetActive(true);
            Slot1 = null;
            if (this.gameObject.activeInHierarchy)
                StartCoroutine(IE_delayAnonymousCard(Eventory.transform));
        }
        else if (index == 2)
        {
            if (Item2Render == null) return;

            Destroy(Item2Render);
            DataPlayer.RemoveItem(Item2Render.GetComponent<Element>().ThisElementData);

            DEL_SLOT2.gameObject.SetActive(false);
            Slot2.gameObject.transform.SetAsFirstSibling();
            Slot2.gameObject.SetActive(true);
            Slot2 = null;
            if (this.gameObject.activeInHierarchy)
                StartCoroutine(IE_delayAnonymousCard(Eventory.transform));
        }
        else if (index == 3)
        {
            if (Item3Render == null) return;

            Destroy(Item3Render);
            DataPlayer.RemoveItem(Item3Render.GetComponent<Element>().ThisElementData);

            DEL_SLOT3.gameObject.SetActive(false);
            Slot3.gameObject.transform.SetAsFirstSibling();
            Slot3.gameObject.SetActive(true);
            Slot3 = null;
            if (this.gameObject.activeInHierarchy)
                StartCoroutine(IE_delayAnonymousCard(Eventory.transform));
        }
        else if (index == 4)
        {
            if (DataPlayer.GetIsActiveSLot4())
            {
                if (Item4Render == null) return;
                Destroy(Item4Render);
                DataPlayer.RemoveItem(Item4Render.GetComponent<Element>().ThisElementData);
                DEL_SLOT4.gameObject.SetActive(false);
                Slot4.gameObject.transform.SetAsFirstSibling();
                Slot4.gameObject.SetActive(true);
                Slot4 = null;
                if (this.gameObject.activeInHierarchy)
                    StartCoroutine(IE_delayAnonymousCard(Eventory.transform));
            }
        }
    }

    public GameObject AnonymousCardPrefabs;
    public List<GameObject> L_CardAnonymous = new List<GameObject>();
    public int countElementActive = 0;

    public void AnonymousCard(Transform parent)
    {
        countElementActive = 0;

        if (L_CardAnonymous.Count > 0)
        {
            for (int i = 0; i < L_CardAnonymous.Count; i++)
            {
                Destroy(L_CardAnonymous[i].gameObject);
            }
        }
        L_CardAnonymous.Clear();
        for (int i = 0; i < L_elementEventory.Count; i++)
        {
            if (L_elementEventory[i] != null)
            {
                if (L_elementEventory[i].gameObject.activeInHierarchy)
                {
                    countElementActive++;
                }
            }
        }
        while (countElementActive < 16)
        {
            var obj = Instantiate(AnonymousCardPrefabs);
            obj.transform.SetParent(parent.transform);
            obj.transform.SetAsLastSibling();
            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 195.3026f);
            obj.GetComponent<RectTransform>().localScale = Vector3.one;
            L_CardAnonymous.Add(obj);
            countElementActive++;
        }
    }
}
