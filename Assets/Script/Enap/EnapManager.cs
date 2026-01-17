using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnapManager : MonoBehaviour
{
    public Action<int, float> A_PurchaseShopBtn;
    public UI_CoinManager m_CoinManager;
    private int Coin;

    private static EnapManager instance;
    public static EnapManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EnapManager();
            }
            return instance;
        }
    }


    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        A_PurchaseShopBtn += PurchaseShopBtn;
    }
    private void PurchaseShopBtn(int _coin, float _cost)
    {
        Coin = DataPlayer.GetCoin();
        Coin += _coin;

        // m_CoinManager.SetTextCoin();
    }
    public void AnimationCoin()
    {
        TextCoinAnimation.Instance.ActionAnimationText(m_CoinManager.CoinTxt, DataPlayer.GetCoin(), Coin, 0.7f);
        DataPlayer.SetCoin(Coin);
    }
    private void OnDisable()
    {
        A_PurchaseShopBtn -= PurchaseShopBtn;
        Destroy(gameObject);
    }
}
