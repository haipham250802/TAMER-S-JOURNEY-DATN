using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_CoinManager : MonoBehaviour
{
    public Text CoinTxt;
    public Image coinimg;
    public Button button2;

    public void Start()
    {
        CoinTxt.text = DataPlayer.GetCoin().ToString();
    }

    private void Awake()
    {
        button2.onClick.AddListener(OnclickButtonCoin);
    }

    public void OnclickButtonCoin()
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            UI_Home.Instance.m_UIShop.gameObject.SetActive(true);
            UI_Home.Instance.m_UIShop.FollowPosCoin();
        }
    }

    public void SetTextCoin()
    {
        CoinTxt.text = DataPlayer.GetCoin().ToString();
    }

    public void SetTextCoin(int value)
    {
        int sum = DataPlayer.GetCoin() + value;
        TextCoinAnimation.Instance.ActionAnimationText(CoinTxt, DataPlayer.GetCoin(), sum, 0.3f);
        DataPlayer.SetCoin(sum);
        SetTextCoin();
    }

    public void Subcoin(int coin)
    {
        int value = DataPlayer.GetCoin();
        value -= coin;
        TextCoinAnimation.Instance.ActionAnimationText(CoinTxt, DataPlayer.GetCoin(), value, 0.3f);
        DataPlayer.SetCoin(value);
        SetTextCoin();
    }
}
