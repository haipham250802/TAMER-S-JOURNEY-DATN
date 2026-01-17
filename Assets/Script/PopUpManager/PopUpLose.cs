using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
public class PopUpLose : MonoBehaviour
{
    public Button SkipBtn;
    public UI_Battle m_UIBattle;

    public SkeletonGraphic skeleton;

    private void Awake()
    {
        SkipBtn.onClick.AddListener(OnclickSkipBtn);
    }
    void OnclickSkipBtn()
    {
        HiddenPopUpLose();
        AudioManager.instance.BG_In_Game_Music.mute = true;
        AudioManager.instance.BG_In_Game_Music.loop = true;
        m_UIBattle.SpinWheel.SetActive(true);
        m_UIBattle.ResetAllidBaseElement();
        m_UIBattle.TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().LoadEnemyBasedElement();
    }
    void HiddenPopUpLose()
    {
        this.gameObject.SetActive(false);
    }
    void HiddenUiBattle()
    {
        m_UIBattle.gameObject.SetActive(false);
    }
    void ActiveUiHome()
    {
        UI_Home.Instance.UI_HomeObj.SetActive(true);
        Controller.Instance.m_Player.gameObject.SetActive(true);
    }
    void ActiveJoyStick()
    {
        UI_Home.Instance.ActiveBag();
    }
}
