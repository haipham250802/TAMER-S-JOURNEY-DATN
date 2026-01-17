using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpNotMoney : MonoBehaviour
{
    public GameObject obj;
    public Button gotoshopBtn;
    public Button ExitBtn;
    public Text contentTxt;

    public bool isCoin;
    public bool isGem;

    public TypeCurrentcy type_Currentcy;
    public string KEY;

    private void Awake()
    {
        gotoshopBtn.onClick.AddListener(GotoshoopButton);
        ExitBtn.onClick.AddListener(onClickExitButton);
    }
    private void OnEnable()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.Sound_Efect_MisNoti);
        LoadTextCurrentcy();
    }
    void LoadTextCurrentcy()
    {
        if(type_Currentcy == TypeCurrentcy.COIN)
        {
            KEY = "KEY_COIN_LACK";
        }
        else
        {
            KEY = "KEY_GEM_LACK";
        }
        contentTxt.text = I2.Loc.LocalizationManager.GetTranslation(KEY);
    }
    void GotoshoopButton()
    {
        if(obj)
        {
            obj.gameObject.SetActive(false);
        }
        if(isCoin)
        {
            isCoin = false;
            isGem = false;
            gameObject.SetActive(false);
            UI_Home.Instance.m_UIShop.gameObject.SetActive(true);
            UI_Home.Instance.m_UIShop.FollowPosCoin();
        }
        if(isGem)
        {
            isGem = false;
            isCoin = false;
            gameObject.SetActive(false);
            UI_Home.Instance.m_UIShop.gameObject.SetActive(true);
            UI_Home.Instance.m_UIShop.FollowPosGem();
        }
        else
        {
            gameObject.SetActive(false);
            UI_Home.Instance.m_UIShop.gameObject.SetActive(true);
        }    
    }
    void onClickExitButton()
    {
        gameObject.SetActive(false);
    }
}
