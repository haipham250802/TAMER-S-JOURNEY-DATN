using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class UI_Home : MonoBehaviour
{
    private static UI_Home instance;

    public UI_Team m_UITeam;
    public UI_Merge m_UIMerge;
    public UI_ShowAllid m_ShowAllid;
    public UIPopUp m_UIPopUp;
    public UI_Collection m_UICollection;
    public UI_Coin m_UICoin;
    public UI_CoinManager m_UICoinManager;
    public UI_Gem_Manager m_UIGemManager;
    public UI_Critter m_UiCritter;
    public UI_Shop m_UIShop;
    public UI_SelectMap m_UIselectMap;
    public TaskManager m_TaskManager;
    public UI_Battle uI_Battle;
    public GameObject ChangeScreenObj;
    public GroupEnemyIntro m_GroupEnemyIntro;
    public BagManager m_Bag;
    public UI_Screen m_UIScreen;
    public UI_Shop_Gem m_SHopGem;
    public ToastManager m_Toast;
    public ui_Currrency m_UiCurrency;

    string ALLID_1 = "ALLID_1";
    string ALLID_2 = "ALLID_2";
    string ALLID_3 = "ALLID_3";


    ECharacterType E_TypeAllid_1;
    ECharacterType E_TypeAllid_2;
    ECharacterType E_TypeAllid_3;

    public player m_Player;
    public GameObject UI_HomeObj;
    [SerializeField] List<GameObject> m_List;
    public bool CanShowUIBattle()
    {
        for (int i = 0; i < m_List.Count; i++)
        {
            if (m_List[i].activeSelf)
            {
                return false;
            }
        }
        return true;
    }
    private void Update()
    {

    }
    private void OnEnable()
    {
        StartCoroutine(iedelay());
    }

    public void DestroyCanvasbag()
    {
        if (DataPlayer.GetIsCheckTapCloseBag())
        {
            Destroy(m_Bag.GetComponent<Canvas>());
            Destroy(m_Bag.GetComponent<GraphicRaycaster>());

        }
    }

    IEnumerator iedelay()
    {
        yield return 0.5f;

        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            m_UIScreen.EnableButton();
        }
    }
    public void TatBatUI(bool _active)
    {
        UI_HomeObj.SetActive(_active);
    }
    public void ActiveBag()
    {
        if (uI_Battle.gameObject.activeInHierarchy)
        {
            if (m_Player)
            {
                if (!m_Player.gameObject.activeInHierarchy)
                {
                    m_Player.gameObject.SetActive(true);
                }
                if (m_Player.ShadowBag.activeInHierarchy && m_Player.Option.activeInHierarchy)
                {
                    m_Player.ShadowBag.gameObject.SetActive(false);
                }
                if (!m_Player.ShadowBag.activeInHierarchy && !m_Player.Option.activeInHierarchy)
                {
                    m_Player.ShadowBag.gameObject.SetActive(true);
                }
            }
        }
    }
    public void ActiveShowAllid()
    {
        m_ShowAllid.gameObject.SetActive(true);
    }
    public static UI_Home Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UI_Home();
            }
            return instance;
        }
    }
    public void LoadData()
    {
        if (ObscuredPrefs.HasKey(ALLID_1))
        {
            E_TypeAllid_1 = Utils.ToEnum<ECharacterType>(ObscuredPrefs.GetString(ALLID_1));
        }
        else
        {
            E_TypeAllid_1 = ECharacterType.NONE;
            ObscuredPrefs.SetString(ALLID_1, E_TypeAllid_1.ToString());
        }

        if (ObscuredPrefs.HasKey(ALLID_2))
        {
            E_TypeAllid_2 = Utils.ToEnum<ECharacterType>(ObscuredPrefs.GetString(ALLID_2));
        }
        else
        {
            E_TypeAllid_2 = ECharacterType.NONE;
            for (int i = 0; i < 3; i++)
            {
                ObscuredPrefs.SetString(ALLID_2, E_TypeAllid_2.ToString());
            }
        }

        if (ObscuredPrefs.HasKey(ALLID_3))
        {
            E_TypeAllid_3 = Utils.ToEnum<ECharacterType>(ObscuredPrefs.GetString(ALLID_3));
        }
        else
        {
            E_TypeAllid_3 = ECharacterType.NONE;
            ObscuredPrefs.SetString(ALLID_3, E_TypeAllid_3.ToString());
        }
    }

    public void ActiveUIHome()
    {
        if (uI_Battle.gameObject.activeInHierarchy)
        {
            return;
        }
        else
        {
            /* // ActiveBag();
              if (!m_Player.ShadowBag.activeInHierarchy)
              {
                  m_Player.ShadowBag.SetActive(true);
              }
  */
            UI_HomeObj.SetActive(true);

            uI_Battle.gameObject.SetActive(false);

            uI_Battle.UI_Catch.SetActive(false);
            uI_Battle.PopUpCatched.SetActive(false);

            m_Player.skeleton.AnimationState.SetAnimation(1, "Idle", true);

            m_Player.joystick.gameObject.SetActive(true);
            m_Player.joystick.background.gameObject.SetActive(false);

            if (m_Player)
                m_Player.GetComponent<Collider2D>().enabled = true;

            if (AudioManager.Instance.gameObject && AudioManager.Instance.BG_In_Game_Music.GetComponent<AudioSource>().clip != AudioManager.instance.MusicUIHome)
                AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music, AudioManager.instance.MusicUIHome);
        }

        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            player.Instance.CloseBag();
            player.Instance.Option.SetActive(false);
            player.Instance.isPurchaseBagBtn = false;
        }
    }
    private void Awake()
    {
        instance = this;
        LoadData();
    }
    private void OnDisable()
    {
        Destroy(gameObject);
    }
}


