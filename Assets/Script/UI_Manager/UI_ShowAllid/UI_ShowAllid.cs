using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShowAllid : MonoBehaviour
{
    public GameObject Hand;

    public AllidsBaseShow Slot1;
    public AllidsBaseShow Slot2;
    public AllidsBaseShow Slot3;
    public AllidsBaseShow Slot4;

    public GameObject SlotParent_01;
    public GameObject SlotParent_02;
    public GameObject SlotParent_03;
    public GameObject SlotParent_04;


    public GameObject Effect_Slot1;
    public GameObject Effect_Slot2;
    public GameObject Effect_Slot3;
    public GameObject Effect_Slot4;


    public Button PurchaseSlot4;
    public int GemActiveSlot4;


    public GameObject ActiveInUiTeam;
    public GameObject UI_Shop;
    public GameObject UI_Team;

    public GameObject AddCritterAllidSlotShowAllid1;
    public GameObject AddCritterAllidSlotShowAllid2;
    public GameObject AddCritterAllidSlotShowAllid3;
    public GameObject AddCritterAllidSlotShowAllid4;

    public GameObject BG_SHadow1;
    public GameObject BG_SHadow2;
    public GameObject BG_SHadow3;
    public GameObject BG_SHadow4;

    public Button AddCritterAllidSlotShowAllid1_Btn;
    public Button AddCritterAllidSlotShowAllid2_Btn;
    public Button AddCritterAllidSlotShowAllid3_Btn;
    public Button AddCritterAllidSlotShowAllid4_Btn;

    public GameObject AddSlot4;

    public Text GemUnlockTxt;
    public int GemUnlock;

    public List<AllidsBaseShow> L_SlotAllidBaseShowDone = new List<AllidsBaseShow>();
    private void Start()
    {
        var arr = GetComponentsInChildren<AllidsBaseShow>();
        LoadAllidBaseShow();

        GemUnlockTxt.text = GemActiveSlot4.ToString();
    }

    public void LoadShowButtonUnlock()
    {
        if (!DataPlayer.GetIsActiveSLot4())
        {
            PurchaseSlot4.gameObject.SetActive(true);
            AddCritterAllidSlotShowAllid4.SetActive(false);
        }
        else
        {
            PurchaseSlot4.gameObject.SetActive(false);
            AddCritterAllidSlotShowAllid4.SetActive(true);
        }
    }
    public void DisableButtonAdd()
    {
        AddCritterAllidSlotShowAllid1_Btn.interactable = false;
        AddCritterAllidSlotShowAllid2_Btn.interactable = false;
        AddCritterAllidSlotShowAllid3_Btn.interactable = false;
        AddCritterAllidSlotShowAllid4_Btn.interactable = false;
    }
    public void EnableButton()
    {
        AddCritterAllidSlotShowAllid1_Btn.interactable = true;
        AddCritterAllidSlotShowAllid2_Btn.interactable = true;
        AddCritterAllidSlotShowAllid3_Btn.interactable = true;
        AddCritterAllidSlotShowAllid4_Btn.interactable = true;



    }
    private void Awake()
    {
        LoadShowButtonUnlock();

        PurchaseSlot4.onClick.AddListener(OnClickPurchaseSlot4);

        AddCritterAllidSlotShowAllid1_Btn.onClick.AddListener(OnClickAddCritterAllidSlotShowAllidBtn_1);
        AddCritterAllidSlotShowAllid2_Btn.onClick.AddListener(OnClickAddCritterAllidSlotShowAllidBtn_2);
        AddCritterAllidSlotShowAllid3_Btn.onClick.AddListener(OnClickAddCritterAllidSlotShowAllidBtn_3);
        AddCritterAllidSlotShowAllid4_Btn.onClick.AddListener(OnClickAddCritterAllidSlotShowAllidBtn_4);
    }


    public void OnClickAddCritterAllidSlotShowAllidBtn_1()
    {
        TutorialManager.Instance.DeSpawn();
        DataPlayer.SetIsTapToAddSlot(true);
        UI_Team.SetActive(true);
        UI_Home.Instance.m_UICollection.ActiveUIteam();
        UI_Team.GetComponent<UI_Team>().ResetAllid();
        UI_Team.GetComponent<UI_Team>().ResetAllidInEventory();
        UI_Team.GetComponent<UI_Team>().Spawn(UI_Team.GetComponent<UI_Team>().Eventory.transform);
        UI_Team.GetComponent<UI_Team>().HiddenAllidEventory();
        UI_Team.GetComponent<UI_Team>().LoadAllied();
        UI_Home.Instance.UI_HomeObj.SetActive(false);
    }
    public void OnClickAddCritterAllidSlotShowAllidBtn_2()
    {
        TutorialManager.Instance.DeSpawn();
        DataPlayer.SetIsTapToAddSlot(true);
        UI_Team.SetActive(true);
        UI_Home.Instance.m_UICollection.ActiveUIteam();
        UI_Team.GetComponent<UI_Team>().ResetAllid();
        UI_Team.GetComponent<UI_Team>().ResetAllidInEventory();
        UI_Team.GetComponent<UI_Team>().Spawn(UI_Team.GetComponent<UI_Team>().Eventory.transform);
        UI_Team.GetComponent<UI_Team>().HiddenAllidEventory();
        UI_Team.GetComponent<UI_Team>().LoadAllied();
        UI_Home.Instance.UI_HomeObj.SetActive(false);

    }
    public void OnClickAddCritterAllidSlotShowAllidBtn_3()
    {
        TutorialManager.Instance.DeSpawn();
        DataPlayer.SetIsTapToAddSlot(true);

        UI_Team.SetActive(true);
        UI_Home.Instance.m_UICollection.ActiveUIteam();
        UI_Team.GetComponent<UI_Team>().ResetAllid();
        UI_Team.GetComponent<UI_Team>().ResetAllidInEventory();
        UI_Team.GetComponent<UI_Team>().Spawn(UI_Team.GetComponent<UI_Team>().Eventory.transform);
        UI_Team.GetComponent<UI_Team>().HiddenAllidEventory();
        UI_Team.GetComponent<UI_Team>().LoadAllied();
        UI_Home.Instance.UI_HomeObj.SetActive(false);

    }
    public void OnClickAddCritterAllidSlotShowAllidBtn_4()
    {
        TutorialManager.Instance.DeSpawn();
        DataPlayer.SetIsTapToAddSlot(true);

        UI_Team.SetActive(true);
        UI_Home.Instance.m_UICollection.ActiveUIteam();
        UI_Team.GetComponent<UI_Team>().ResetAllid();
        UI_Team.GetComponent<UI_Team>().ResetAllidInEventory();
        UI_Team.GetComponent<UI_Team>().Spawn(UI_Team.GetComponent<UI_Team>().Eventory.transform);
        UI_Team.GetComponent<UI_Team>().HiddenAllidEventory();
        UI_Team.GetComponent<UI_Team>().LoadAllied();
        UI_Home.Instance.UI_HomeObj.SetActive(false);

    }

    private void OnEnable()
    {
        LoadAllidBaseShow();
        /* if (!DataPlayer.GetIsCheckTapToAdd())
         {
             *//*Hand.SetActive(true);
             Hand.transform.SetParent(AddCritterAllidSlotShowAllid1_Btn.transform);
             Hand.transform.localPosition = Vector3.zero;*//*

             TutorialManager.Instance.SpawnHandUIHome(AddCritterAllidSlotShowAllid1_Btn.transform, new Vector3(30, -70, 0));
         }*/
        if (DataPlayer.GetIsCheckAddToSlot())
        {
            EnableButton();
        }
        else
        {
            DisableButtonAdd();
        }
        StartCoroutine(IE_delayBGShadow());
    }
    IEnumerator IE_delayBGShadow()
    {
        yield return null;
        BG_Shadow();
    }
    void BG_Shadow()
    {
        if (Slot1.gameObject.activeInHierarchy)
        {
            BG_SHadow1.SetActive(false);
        }
        else
        {
            BG_SHadow1.SetActive(true);
        }

        if (Slot2.gameObject.activeInHierarchy)
        {
            BG_SHadow2.SetActive(false);
        }
        else
        {
            BG_SHadow2.SetActive(true);
        }

        if (Slot3.gameObject.activeInHierarchy)
        {
            BG_SHadow3.SetActive(false);
        }
        else
        {
            BG_SHadow3.SetActive(true);
        }

        if (Slot4.gameObject.activeInHierarchy)
        {
            BG_SHadow4.SetActive(false);
        }
        else
        {
            BG_SHadow4.SetActive(true);
        }
    }
    IEnumerator IE_DelayLoadAllidBaseShow()
    {
        yield return null;
        LoadAllidBaseShow();
    }
    public void LoadAllidBaseShow()
    {
        for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
        {
            switch (i)
            {
                case 0:
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        Effect_Slot1.SetActive(true);
                        Slot1.Type = DataPlayer.GetListAllid()[i].Type;
                        Slot1.Init(DataPlayer.GetListAllid()[i]);
                        Slot1.gameObject.SetActive(true);
                        SlotParent_01.SetActive(true);
                        AddCritterAllidSlotShowAllid1.SetActive(false);
                        if (Effect_Slot1.activeInHierarchy)
                        {
                            StartCoroutine(IE_HiddenEffectShow(Effect_Slot1));
                        }
                    }
                    else
                    {
                        Slot1.gameObject.SetActive(false);
                        SlotParent_01.SetActive(false);
                        AddCritterAllidSlotShowAllid1.SetActive(true);
                    }
                    break;
                case 1:
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        Effect_Slot2.SetActive(true);
                        Slot2.Type = DataPlayer.GetListAllid()[i].Type;
                        Slot2.Init(DataPlayer.GetListAllid()[i]);
                        Slot2.gameObject.SetActive(true);
                        SlotParent_02.SetActive(true);
                        AddCritterAllidSlotShowAllid2.SetActive(false);
                        if (Effect_Slot2.activeInHierarchy)
                        {
                            StartCoroutine(IE_HiddenEffectShow(Effect_Slot2));
                        }
                    }
                    else
                    {
                        Slot2.gameObject.SetActive(false);
                        SlotParent_02.SetActive(false);
                        AddCritterAllidSlotShowAllid2.SetActive(true);
                    }
                    break;
                case 2:
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        Effect_Slot3.SetActive(true);
                        Slot3.Type = DataPlayer.GetListAllid()[i].Type;
                        Slot3.Init(DataPlayer.GetListAllid()[i]);
                        Slot3.gameObject.SetActive(true);
                        SlotParent_03.SetActive(true);
                        AddCritterAllidSlotShowAllid3.SetActive(false);
                        if (Effect_Slot3.activeInHierarchy)
                        {
                            StartCoroutine(IE_HiddenEffectShow(Effect_Slot3));
                        }
                    }
                    else
                    {
                        Slot3.gameObject.SetActive(false);
                        SlotParent_03.SetActive(false);
                        AddCritterAllidSlotShowAllid3.SetActive(true);
                    }
                    break;
                case 3:
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        Effect_Slot4.SetActive(true);
                        Slot4.Type = DataPlayer.GetListAllid()[i].Type;
                        Slot4.Init(DataPlayer.GetListAllid()[i]);
                        Slot4.gameObject.SetActive(true);
                        SlotParent_04.SetActive(true);
                        AddCritterAllidSlotShowAllid4.SetActive(false);
                        if (Effect_Slot4.activeInHierarchy)
                        {
                            StartCoroutine(IE_HiddenEffectShow(Effect_Slot4));
                        }
                    }
                    else
                    {
                        Slot4.gameObject.SetActive(false);
                        SlotParent_04.SetActive(false);
                        LoadShowButtonUnlock();
                    }
                    break;
            }
        }
        if (gameObject.activeSelf)
            StartCoroutine(IE_delayBGShadow());
    }
    int CurGem;

    public void OnClickPurchaseSlot4()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            CurGem = DataPlayer.GetGem();
            if (CurGem > GemActiveSlot4)
            {
                /* DataPlayer.SetIsActiveSlot4(true);
                 if (DataPlayer.GetIsActiveSLot4())
                 {
                     PurchaseSlot4.gameObject.SetActive(false);
                     ActiveInUiTeam.SetActive(false);
                     AddCritterAllidSlotShowAllid4_Btn.gameObject.SetActive(true);
                 }*/
                popUpManager.Instance.m_POpUpConFirm.gameObject.SetActive(true);
            }
            else
            {
                popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.GEM;
                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
            }
            LoadShowButtonUnlock();
        }

    }
    IEnumerator IE_HiddenEffectShow(GameObject EffectShow)
    {
        yield return new WaitForSeconds(0.3f);
        if (EffectShow.activeInHierarchy)
        {
            EffectShow.SetActive(false);
        }
    }
}
