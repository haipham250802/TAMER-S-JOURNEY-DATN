using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Dragon.SDK;

public class PopUpHealingWhenTriggerLake : MonoBehaviour
{
    public int ID;
    public Text contentxt;
    public Text HpTxt;
    public Text QuantityHpHealingNoAdsTxt;
    public Text QuantityHpHealingWithAdsTxt;

    public Button HpHealingNoAdsBtn;
    public Button HpHealingWithAds;
    public Button OutPopUpButton;

    public List<Lake> LakeList = new List<Lake>();
    public List<GameObject> L_objectCloneLake = new List<GameObject>();
    public List<GameObject> L_HpBuffPrefabs = new List<GameObject>();

    public GameObject BuffHPPrefab;
    public GameObject PoolPrefabs;

    public CritterFollowController m_CritterFollowController;
    public SkeletonGraphic Icon;
    private void Awake()
    {
        HpHealingNoAdsBtn.onClick.AddListener(onclickButtonHpHealingNoAds);
        HpHealingWithAds.onClick.AddListener(ShowAdsReward);
        OutPopUpButton.onClick.AddListener(OnClickOutButton);
    }
    private void Start()
    {
        Icon.AnimationState.SetAnimation(0, "animation", true);
    }

    private void OnEnable()
    {
        StartCoroutine(IEdelay());
        AudioManager.Instance.PlaySound(AudioManager.instance.SoundEffectWosh);
    }
    IEnumerator IEdelay()
    {
        yield return null;
        for (int i = 0; i < BagManager.Instance.m_RuleController.L_enemy2.Count; i++)
        {
            BagManager.Instance.m_RuleController.L_enemy2[i].isCanAI = true;
        }
    }
    public void onclickButtonHpHealingNoAds()
    {
        Spawn_Effect_Buff_At_Pos_CritterFollow();
        AudioManager.instance.PlaySound(AudioManager.instance.SoundEffecrHealing);
        for (int x = 0; x < LakeList.Count; x++)
        {
            if (ID == LakeList[x].ID)
            {
                for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
                {
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(DataPlayer.GetListAllid()[i].Type);
                        int MaxHp = enemyStat.HP;
                        Debug.Log(MaxHp);
                        if (DataPlayer.GetListAllid()[i].HP < MaxHp)
                        {
                            DataPlayer.GetListAllid()[i].HP += LakeList[x].QuantityHpHealingNoAds;
                            if (DataPlayer.GetListAllid()[i].HP > MaxHp)
                            {
                                DataPlayer.GetListAllid()[i].HP = MaxHp;
                            }
                            DataPlayer.SetHP(DataPlayer.GetListAllid()[i].HP, DataPlayer.GetListAllid()[i]);
                        }
                    }
                }
                Destroy(LakeList[x].gameObject);
                var obj = Instantiate(PoolPrefabs, LakeList[x].gameObject.transform.localPosition + new Vector3(0, 1, 0), Quaternion.identity);
                L_objectCloneLake.Add(obj);
                LakeList.RemoveAt(x);
                break;
            }
        }
        this.gameObject.SetActive(false);
    }
    private void ShowAdsReward()
    {
        AdStatus adstatus = SDKDGManager.Instance.AdsManager.ShowRewardedAdsStatus(() =>
        {
            onclickButtonHpHealingWithAds();
            Controller.Instance.CountTime = 0;
            Debug.Log("Healing - Pooling");
        }, "Healing - Pooling");
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
    public void onclickButtonHpHealingWithAds()
    {
        Spawn_Effect_Buff_At_Pos_CritterFollow();
        AudioManager.instance.PlaySound(AudioManager.instance.SoundEffecrHealing);
        for (int x = 0; x < LakeList.Count; x++)
        {
            if (ID == LakeList[x].ID)
            {
                for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
                {
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(DataPlayer.GetListAllid()[i].Type);
                        int MaxHp = enemyStat.HP;

                        if (DataPlayer.GetListAllid()[i].HP < MaxHp)
                        {
                            DataPlayer.GetListAllid()[i].HP += LakeList[x].QuantityHpHealingWithAds;
                            if (DataPlayer.GetListAllid()[i].HP > MaxHp)
                            {
                                DataPlayer.GetListAllid()[i].HP = MaxHp;
                            }
                            DataPlayer.SetHP(DataPlayer.GetListAllid()[i].HP, DataPlayer.GetListAllid()[i]);
                        }
                    }
                }
                Destroy(LakeList[x].gameObject);
                var obj = Instantiate(PoolPrefabs, LakeList[x].gameObject.transform.localPosition + new Vector3(0, 1, 0), Quaternion.identity);
                L_objectCloneLake.Add(obj);

                LakeList.RemoveAt(x);
                break;
            }
        }
        this.gameObject.SetActive(false);
    }
    public void OnClickOutButton()
    {
        this.gameObject.SetActive(false);
        LakeList.Clear();
    }

    public void Spawn_Effect_Buff_At_Pos_CritterFollow()
    {
        if (m_CritterFollowController.Critter_Follow_Element_01.CritterFollowType != ECharacterType.NONE && m_CritterFollowController.Critter_Follow_Element_01.gameObject.activeInHierarchy)
        {
            GameObject obj = Instantiate(BuffHPPrefab);
            obj.GetComponent<BufffHPFX>().trans = m_CritterFollowController.Critter_Follow_Element_01.transform;
            L_HpBuffPrefabs.Add(obj);
        }
        if (m_CritterFollowController.Critter_Follow_Element_02.CritterFollowType != ECharacterType.NONE && m_CritterFollowController.Critter_Follow_Element_02.gameObject.activeInHierarchy)
        {
            GameObject obj = Instantiate(BuffHPPrefab);
            obj.GetComponent<BufffHPFX>().trans = m_CritterFollowController.Critter_Follow_Element_02.transform;
            L_HpBuffPrefabs.Add(obj);
        }
        if (m_CritterFollowController.Critter_Follow_Element_03.CritterFollowType != ECharacterType.NONE && m_CritterFollowController.Critter_Follow_Element_03.gameObject.activeInHierarchy)
        {
            GameObject obj = Instantiate(BuffHPPrefab);
            obj.GetComponent<BufffHPFX>().trans = m_CritterFollowController.Critter_Follow_Element_03.transform;
            L_HpBuffPrefabs.Add(obj);
        }
        if (m_CritterFollowController.Critter_Follow_Element_04.CritterFollowType != ECharacterType.NONE && m_CritterFollowController.Critter_Follow_Element_04.gameObject.activeInHierarchy)
        {
            GameObject obj = Instantiate(BuffHPPrefab);
            obj.GetComponent<BufffHPFX>().trans = m_CritterFollowController.Critter_Follow_Element_04.transform;
            L_HpBuffPrefabs.Add(obj);
        }
        HiddenEffectBuff();
    }

    public void HiddenEffectBuff()
    {
        for (int i = 0; i < L_HpBuffPrefabs.Count; i++)
        {
            Destroy(L_HpBuffPrefabs[i], 1f);
        }
        L_HpBuffPrefabs.Clear();
    }
    private void OnDisable()
    {
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
    }
}
