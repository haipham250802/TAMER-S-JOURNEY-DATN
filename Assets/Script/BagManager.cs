using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class BagManager : Singleton<BagManager>
{
    public GameObject Option;
    public GameObject Noti;

    [FoldoutGroup("SETTING")]
    public GameObject PopUp_Setting;
    [FoldoutGroup("SETTING")]
    public Button Setting_Button;
    [FoldoutGroup("SETTING")]
    public Button OutSetting_Button;
    [FoldoutGroup("SETTING")]
    public bool IsPressSettingButton = false;

    [FoldoutGroup("SHOP")]
    public GameObject UI_Shop;
    [FoldoutGroup("SHOP")]
    public Button Shop_Button;
    [FoldoutGroup("SHOP")]
    public Button OutShopButton;
    [FoldoutGroup("SHOP")]
    public bool IsPressShopButton = false;

    [FoldoutGroup("MONSTER")]
    public Button Monster_Btn;

    [FoldoutGroup("SELECT MAP")]
    public Button SelectMap_Btn;

    public RuleController m_RuleController;
    public Transform UIMergeBtn;
    public Transform UITeamBtn;

    public Button btnTeam;
    public Button btnMerge;
    private void Awake()
    {
        btnTeam.onClick.AddListener(OnclickButtonTeam);
        UIMergeBtn.gameObject.GetComponent<Button>().onClick.AddListener(OnclickButtonMerge);
        Shop_Button.onClick.AddListener(OnClickShopButton);
        Setting_Button.onClick.AddListener(OnClickSettingButton);

        OutShopButton.onClick.AddListener(OnClickOutShopButton);
        OutSetting_Button.onClick.AddListener(OnclickOutSettingButton);

        Monster_Btn.onClick.AddListener(OnClickMonsterButton);

        SelectMap_Btn.onClick.AddListener(OnclickSelectMap);
        NotifiController.Instance.NotiBag += SetIcon;
    }
    void SetIcon(bool check)
    {
        if (Noti != null)
            Noti.SetActive(check);
    }
  
   
    private void OnEnable()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            //   gameObject.GetComponent<Canvas>().sortingOrder = 2;
            if (btnMerge.GetComponent<GraphicRaycaster>())
                Destroy(btnMerge.GetComponent<GraphicRaycaster>());
            if (btnMerge.GetComponent<Canvas>())
                Destroy(btnMerge.GetComponent<Canvas>());

        }
    }
    private void Start()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            if (gameObject.GetComponent<GraphicRaycaster>())
            {
                Destroy(gameObject.GetComponent<GraphicRaycaster>());
            }
            if (gameObject.GetComponent<Canvas>())
            {
                Destroy(gameObject.GetComponent<Canvas>());
            }
        }
    }
    public void DisableButtonInBagManager()
    {
        SelectMap_Btn.interactable = false;
        Monster_Btn.interactable = false;
        Shop_Button.interactable = false;
        Setting_Button.interactable = false;
    }
    public void EnableButtonInBagManager()
    {
        SelectMap_Btn.interactable = true;
        Monster_Btn.interactable = true;
        Shop_Button.interactable = true;
        Setting_Button.interactable = true;
    }
    public void OnclickButtonTeam()
    {
        TutorialManager.Instance.DeSpawn();
        DataPlayer.SetIsCheckTapButtonteam(true);
        //  TutorialManager.Instance.DeSpawn();
    }
    public void OnclickButtonMerge()
    {
        DataPlayer.SetIsCheckTapButtonMerge(true);
        TutorialManager.Instance.DeSpawn();
    }
    public void OnClickMonsterButton()
    {
        UI_Home.Instance.m_UiCritter.LoadCritterInUI();
        UI_Home.Instance.m_UiCritter.ActiveCritter();
        StopEnemy();
    }
    public void OnClickSettingButton()
    {
        if (!IsPressSettingButton)
        {
            popUpManager.Instance.m_PopUpSeting.gameObject.SetActive(true);
            IsPressSettingButton = true;
            StopEnemy();
            return;
        }
    }
    public void OnclickOutSettingButton()
    {
        if (IsPressSettingButton)
        {
            popUpManager.Instance.m_PopUpSeting.gameObject.SetActive(false);
            /*  Option.SetActive(true);
              IsPressSettingButton = false;*/
            IsPressSettingButton = false;
            SetSpeedEnemy();
            return;
        }
    }
    public void OnClickShopButton()
    {
        if (!IsPressShopButton)
        {
            UI_Shop.SetActive(true);
            UI_Home.Instance.m_UIShop.ResetPosShop();
            IsPressShopButton = true;
            StopEnemy();
            return;
        }
    }
    public void OnClickOutShopButton()
    {
        if (IsPressShopButton)
        {
            UI_Shop.SetActive(false);
            Option.SetActive(true);
            IsPressShopButton = false;
            SetSpeedEnemy();
            return;
        }
    }
    public void OnclickSelectMap()
    {
        UI_Home.Instance.m_UIselectMap.HiddenLockImg();
        UI_Home.Instance.m_UIselectMap.gameObject.SetActive(true);
        StopEnemy();

    }

    public void StopEnemy()
    {
        if (m_RuleController.L_enemy.Count > 0)
        {
            for (int i = 0; i < m_RuleController.L_enemy.Count; i++)
            {
                m_RuleController.L_enemy[i].GetComponent<PolyNavAgent>().maxSpeed = 0;
            }
        }
        if (m_RuleController.L_boss.Count > 0)
        {
            m_RuleController.L_boss[0].GetComponent<PolyNavAgent>().maxSpeed = 0f;
        }
    }
    public void SetSpeedEnemy()
    {
        if (m_RuleController.L_enemy.Count > 0)
        {
            for (int i = 0; i < m_RuleController.L_enemy.Count; i++)
            {
                m_RuleController.L_enemy[i].GetComponent<PolyNavAgent>().maxSpeed = 3.5f;
            }
        }

        if (m_RuleController.L_boss.Count > 0)
        {
            m_RuleController.L_boss[0].GetComponent<PolyNavAgent>().maxSpeed = 3.5f;
        }
    }
}
