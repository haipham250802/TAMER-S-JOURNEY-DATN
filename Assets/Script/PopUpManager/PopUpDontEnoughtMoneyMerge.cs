using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpDontEnoughtMoneyMerge : MonoBehaviour
{
    public Button Out;
    public Button GotoShop;
    // Start is called before the first frame update

    private void Awake()
    {
        Out.onClick.AddListener(onclickOutButton);
        GotoShop.onClick.AddListener(OnClickGotoShopButton);
    }
    public void onclickOutButton()
    {
        this.gameObject.SetActive(false);
    }
    public void OnClickGotoShopButton()
    {
        UI_Home.Instance.m_UIShop.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
public enum TypeCurrentcy
{
    NONE = -1,
    COIN = 0,
    GEM = 1
}

