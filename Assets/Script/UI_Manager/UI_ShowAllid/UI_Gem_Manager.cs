using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Gem_Manager : MonoBehaviour
{
    public int NumGem;
    public Text Gemtxt;
    public Button GemBtn;

    private void Awake()
    {
        GemBtn.onClick.AddListener(OnClickButtonGem);
    }
    private void OnEnable()
    {
        Init();
    }
    void OnClickButtonGem()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            UI_Home.Instance.m_UIShop.gameObject.SetActive(true);
            UI_Home.Instance.m_UIShop.FollowPosGem();
        }
    }
    void Init()
    {
        NumGem = DataPlayer.GetGem();
        Gemtxt.text = NumGem.ToString();
    }
    public void SetTextGem(int value)
    {
        int sum = DataPlayer.GetGem() + value;
        TextCoinAnimation.Instance.ActionAnimationText(Gemtxt, DataPlayer.GetGem(), sum, 0.3f);
        DataPlayer.SetGem(sum);
        SetTextGem();
    }
    public void SetTextGem()
    {
        Gemtxt.text = DataPlayer.GetGem().ToString();
    }
    public void SubGem(int Gem)
    {
        int value = DataPlayer.GetGem();
        value -= Gem;
        TextCoinAnimation.Instance.ActionAnimationText(Gemtxt, DataPlayer.GetGem(), value, 0.3f);
        DataPlayer.SetGem(value);
        SetTextGem();
    }
}
