using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class AdsButton : MonoBehaviour
{
    public Button PurchaseBtn;
    public PopUpPackOnline m_PopUp;
    private void Awake()
    {
        PurchaseBtn.onClick.AddListener(onClickPurchaseButton);
    }
    void onClickPurchaseButton()
    {
        m_PopUp.gameObject.SetActive(true);
    }
}
