using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Spine;
public class PopUpCatched : MonoBehaviour
{
    public SkeletonGraphic avatar;

    public Button NextToMerge_Btn;
    public Button NextToTeam_Btn;

    public Button SkipBtn;
    public Text NameCritter;

    public UI_Battle m_UiBattle;

    private void Awake()
    {
        SkipBtn.onClick.AddListener(OnClickSkipBtn);
        NextToMerge_Btn.onClick.AddListener(ActiveUIMerge);
        NextToTeam_Btn.onClick.AddListener(ActiveUITeam);
    }
    public void ActiveUITeam()
    {
        UI_Home.Instance.m_Player.GetComponent<Collider2D>().enabled = true;
        UI_Home.Instance.ActiveUIHome();
        AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music, AudioManager.instance.MusicUIHome);
        if (!UI_Home.Instance.m_UITeam.OutButton.gameObject.activeInHierarchy)
        {
            UI_Home.Instance.m_UITeam.OutButton.gameObject.SetActive(true);
        }
        if (m_UiBattle.m_character is Enemy)
        {
            this.gameObject.SetActive(false);
            UI_Home.Instance.m_UITeam.gameObject.SetActive(true);
            UI_Home.Instance.m_UICollection.ActiveUIteam();
            UI_Home.Instance.m_UITeam.ResetAllid();
            UI_Home.Instance.m_UITeam.ResetAllidInEventory();
            UI_Home.Instance.m_UITeam.Spawn(UI_Home.Instance.m_UITeam.Eventory.transform);
            UI_Home.Instance.m_UITeam.HiddenAllidEventory();
            UI_Home.Instance.m_UITeam.LoadAllied();
            m_UiBattle.DestroyCanvas();
            UI_Home.Instance.ActiveBag();

            m_UiBattle.RemoveEnemy();
        }
        else
        {
            NextToMerge_Btn.GetComponent<Image>().color = Color.gray;
            NextToTeam_Btn.GetComponent<Image>().color = Color.gray;
        }
    }
    public void ActiveUIMerge()
    {
        AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music, AudioManager.instance.MusicUIHome);
        UI_Home.Instance.ActiveUIHome();
        if (!UI_Home.Instance.m_UIMerge.OutButton.gameObject.activeInHierarchy)
        {
            UI_Home.Instance.m_UIMerge.OutButton.gameObject.SetActive(true);
        }
        UI_Home.Instance.m_Player.GetComponent<Collider2D>().enabled = true;
        if (m_UiBattle.m_character is Enemy)
        {
            this.gameObject.SetActive(false);
            UI_Home.Instance.m_UIMerge.gameObject.SetActive(true);
            UI_Home.Instance.m_UICollection.ActiveUIMerge();
            UI_Home.Instance.m_UIMerge.ResetItemMerge();
            UI_Home.Instance.m_UIMerge.ResetAllid();
            UI_Home.Instance.m_UIMerge.LoadEventory(UI_Home.Instance.m_UIMerge.Eventory.gameObject);

            m_UiBattle.DestroyCanvas();
            UI_Home.Instance.ActiveBag();
            m_UiBattle.RemoveEnemy();
        }
        else
        {
            NextToMerge_Btn.GetComponent<Image>().color = Color.gray;
            NextToTeam_Btn.GetComponent<Image>().color = Color.gray;
        }
    }
    public void SetAvatar(ECharacterType type)
    {
        EnemyStat enemy = Controller.Instance.GetStatEnemy(type);

        this.avatar.skeletonDataAsset = null;
        this.avatar.skeletonDataAsset = enemy.ICON;
        this.avatar.Initialize(true);
        this.avatar.AnimationState.SetAnimation(0, "Idle", true);
    }
    public void SetName(ECharacterType NameCritter)
    {
        this.NameCritter.text = NameCritter.ToString();
    }
    void OnClickSkipBtn()
    {
        UI_Home.Instance.ActiveUIHome();


        if (m_UiBattle.m_character is BossPatrol)
        {
            m_UiBattle.RemoveBoss();
            if (m_UiBattle.isFocusSelectMap)
            {
                UI_Home.Instance.m_UIselectMap.HiddenLockImg();
                UI_Home.Instance.m_UIselectMap.gameObject.SetActive(true);
                m_UiBattle.isFocusSelectMap = false;
            }
        }
        else
            m_UiBattle.RemoveEnemy();
    }
}
