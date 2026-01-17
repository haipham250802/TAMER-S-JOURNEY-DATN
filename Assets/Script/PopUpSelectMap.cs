using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSelectMap : MonoBehaviour
{
    public int ID;

    public Button NextBtn;
    public Button ExitBtn;

    public Text NameMap;
    public Text NumMonster;
    public Text Content;

    public int CurrentNumMonster;

    public GameObject ListCritter;
    public CritterSelectMapElement critter;

    public ListCritterManager L_critterManager = new ListCritterManager();
    public List<GameObject> CritterList = new List<GameObject>();

    public UI_SelectMap m_UiSelectMap;

    bool Ischeck;

    private void OnEnable()
    {
        StartCoroutine(IE_delayActive());
    }
    IEnumerator IE_delayActive()
    {
        yield return null;

        if (!DataPlayer.GetListLevel().Contains(ID))
        {
            Content.gameObject.SetActive(true);
            string str = I2.Loc.LocalizationManager.GetTranslation("KEY_UNLOCK_LEVEL_SELECT_MAP");
            Content.text = str + " " + (ID - 1);
            NextBtn.gameObject.SetActive(false);
            AudioManager.instance.PlaySound(AudioManager.instance.Sound_Efect_MisNoti);
        }
        else
        {
            Content.gameObject.SetActive(false);
            NextBtn.gameObject.SetActive(true);
        }
    }

    private void Awake()
    {
        NextBtn.onClick.AddListener(OnClickNextButton);
        ExitBtn.onClick.AddListener(onclickExit);
        NextBtn.gameObject.SetActive(false);
    }

    public void LoadTextNumCritter()
    {
        for (int i = 0; i < CritterList.Count; i++)
        {
            if (DataPlayer.GetListCritters().Contains(CritterList[i].GetComponent<CritterSelectMapElement>().CharacterType))
            {
                CurrentNumMonster++;
            }
        }
        switch (ID)
        {
            case 1:
                NameMap.text = NameMap.text = I2.Loc.LocalizationManager.GetTranslation("KEY_TILE_MAP1");
                break;
            case 2:
                NameMap.text = NameMap.text = I2.Loc.LocalizationManager.GetTranslation("KEY_TILE_MAP2");
                break;
            case 3:
                NameMap.text = NameMap.text = I2.Loc.LocalizationManager.GetTranslation("KEY_TILE_MAP3");
                break;
            case 4:
                NameMap.text = NameMap.text = I2.Loc.LocalizationManager.GetTranslation("KEY_TILE_MAP4");
                break;
            case 5:
                NameMap.text = NameMap.text = I2.Loc.LocalizationManager.GetTranslation("KEY_TILE_MAP5");
                break;
            case 6:
                NameMap.text = NameMap.text = I2.Loc.LocalizationManager.GetTranslation("KEY_TILE_MAP7");
                break;
            case 7:
                NameMap.text = NameMap.text = I2.Loc.LocalizationManager.GetTranslation("KEY_TILE_MAP6");
                break;
            case 8:
                NameMap.text = NameMap.text = I2.Loc.LocalizationManager.GetTranslation("KEY_TILE_MAP8");
                break;
        }
        if (CurrentNumMonster < CritterList.Count)
        {
            NumMonster.text = I2.Loc.LocalizationManager.GetTranslation("KEY_TILE_INFO_CRITTER") + " : <color=\"#FF0000\">" + CurrentNumMonster + "</color>/" + CritterList.Count;
        }
        else
        {
            NumMonster.text = I2.Loc.LocalizationManager.GetTranslation("KEY_TILE_INFO_CRITTER") + " : " + CurrentNumMonster + "/" + CritterList.Count;
        }
    }

    public void LoadCritter(List<ECharacterType> type)
    {
        for (int i = 0; i < type.Count; i++)
        {
            var obj = Instantiate(critter);
            obj.transform.SetParent(ListCritter.transform);
            CritterSelectMapElement critterSelect = obj.GetComponent<CritterSelectMapElement>();

            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(160, 195.3026f);
            obj.GetComponent<RectTransform>().localScale = Vector3.one;

            critterSelect.CharacterType = type[i];
            critterSelect.SetData(critterSelect.CharacterType);

            if (DataPlayer.GetListCritters().Contains(type[i]))
            {
                obj.GetComponent<CritterSelectMapElement>().ActiveAvatar();
            }
            else
            {
                obj.GetComponent<CritterSelectMapElement>().HiddenAvatar();
            }

            CritterList.Add(obj.gameObject);
        }
    }

    public void OnClickNextButton()
    {
        switch (ID)
        {
            case 1:
                if (!DataPlayer.GetIsCheckDoneTutorial())
                {
                    m_UiSelectMap.m_popUpReward.GotIt.GetComponent<Canvas>().sortingOrder = 10002;
                }
                m_UiSelectMap.LoadMap1();
                UI_Home.Instance.m_Player.transform.position = CheckPointManager.Instance.L_CheckPoint[0].transform.localPosition;
                break;
            case 2:
                m_UiSelectMap.LoadMap2();
                UI_Home.Instance.m_Player.transform.position = CheckPointManager.Instance.L_CheckPoint[0].transform.localPosition;
                break;
            case 3:
                m_UiSelectMap.LoadMap3();
                UI_Home.Instance.m_Player.transform.position = CheckPointManager.Instance.L_CheckPoint[0].transform.localPosition;
                break;
            case 4:
                m_UiSelectMap.LoadMap4();
                UI_Home.Instance.m_Player.transform.position = CheckPointManager.Instance.L_CheckPoint[0].transform.localPosition;
                break;
            case 5:
                m_UiSelectMap.LoadMap5();
                UI_Home.Instance.m_Player.transform.position = CheckPointManager.Instance.L_CheckPoint[0].transform.localPosition;
                break;
            case 6:
                m_UiSelectMap.LoadMap6();
                UI_Home.Instance.m_Player.transform.position = CheckPointManager.Instance.L_CheckPoint[0].transform.localPosition;
                break;
            case 7:
                m_UiSelectMap.LoadMap7();
                UI_Home.Instance.m_Player.transform.position = CheckPointManager.Instance.L_CheckPoint[0].transform.localPosition;
                break;
            case 8:
                m_UiSelectMap.LoadMap8();
                UI_Home.Instance.m_Player.transform.position = CheckPointManager.Instance.L_CheckPoint[0].transform.localPosition;
                break;
            case 9:
                m_UiSelectMap.LoadMap9();
                UI_Home.Instance.m_Player.transform.position = CheckPointManager.Instance.L_CheckPoint[0].transform.localPosition;
                break;
            case 10:
                m_UiSelectMap.LoadMap10();
                UI_Home.Instance.m_Player.transform.position = CheckPointManager.Instance.L_CheckPoint[0].transform.localPosition;
                break;
            case 11:
                m_UiSelectMap.LoadMap11();
                UI_Home.Instance.m_Player.transform.position = CheckPointManager.Instance.L_CheckPoint[0].transform.localPosition;
                break;
        }
    }

    public void loadCritterMap1()
    {
        LoadCritter(L_critterManager.characterTypesLV1);
    }
    public void loadCritterMap2()
    {
        LoadCritter(L_critterManager.characterTypesLV2);
    }
    public void loadCritterMap3()
    {
        LoadCritter(L_critterManager.characterTypesLV3);
    }
    public void loadCritterMap4()
    {
        LoadCritter(L_critterManager.characterTypesLV4);
    }
    public void loadCritterMap5()
    {
        LoadCritter(L_critterManager.characterTypesLV5);
    }
    public void loadCritterMap6()
    {
        LoadCritter(L_critterManager.characterTypesLV6);
    }
    public void loadCritterMap7()
    {
        LoadCritter(L_critterManager.characterTypesLV7);
    }
    public void loadCritterMap8()
    {
        LoadCritter(L_critterManager.characterTypesLV8);
    }
    public void loadCritterMap9()
    {
        LoadCritter(L_critterManager.characterTypesLV9);
    }
    public void loadCritterMap10()
    {
        LoadCritter(L_critterManager.characterTypesLV10);
    }
    public void loadCritterMap11()
    {
        LoadCritter(L_critterManager.characterTypesLV11);
    }
    void onclickExit()
    {
        this.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        for (int i = 0; i < CritterList.Count; i++)
        {
            Destroy(CritterList[i]);
        }
        CritterList.Clear();
        CurrentNumMonster = 0;
        NextBtn.gameObject.SetActive(false);
    }
}
