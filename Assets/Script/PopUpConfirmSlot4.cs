using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpConfirmSlot4 : MonoBehaviour
{
    public Button YesBtn;
    public Button NoBtn;
    private void Awake()
    {
        YesBtn.onClick.AddListener(OnclickYesBtn);
        NoBtn.onClick.AddListener(OnCLickNoBtn);
    }
    void OnclickYesBtn()
    {
        Debug.Log("dabam");
        DataPlayer.SetIsActiveSlot4(true);
        UI_Home.Instance.m_ShowAllid.LoadShowButtonUnlock();
        UI_Home.Instance.m_UITeam.LoadView();
        gameObject.SetActive(false);

        UI_Home.Instance.m_UIGemManager.SubGem(UI_Home.Instance.m_ShowAllid.GemActiveSlot4);
    }
    void OnCLickNoBtn()
    {
        Debug.Log("dabam");
        gameObject.SetActive(false);
    }
}
