using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
public class POP_UP_CHEST : MonoBehaviour
{
    public E_TypeBuy EstatBuy;
    public TypeChest EtypeChest;

    public CHEST_NORMAL chestNormal;
    public CHEST_EPIC chestEpic;
    public CHEST_LEGEND chestLegend;

    public SkeletonGraphic ChestSpine;

    public Button Continue_Button;
    public Button Skip_Button;

    public Image CurrencyImage;
    public Text continue_Text;
    public GameObject NotiObj;
    public Text notiObjTxt;

    public Sprite SP_Coin_btn;
    public Sprite SP_Coin_Card;
    public Sprite SP_Gem;

    public GameObject Content;
    public GameObject[] PosActiveFlyCards;

    public List<GameObject> ContentList;
    public List<GameObject> ItemCloneList;

    public bool isShowItemDone;

    Spine.EventData eventData;

    private void Awake()
    {
        Skip_Button.onClick.AddListener(OnclickSkipButton);
        Continue_Button.onClick.AddListener(OnClickButtonContinue);
    }
    private void OnEnable()
    {
        isShowItemDone = false;
        StartCoroutine(IE_Delay());
    }
    IEnumerator IE_Delay()
    {
        yield return null;
        Action();
    }
    void Action()
    {
        HiddenButton();
        ChangeSkinChest();
        FallChest();
    }
    void Action2()
    {
        HiddenButton();
        ChangeSkinChest();
        OpenChest();
        Continue_Button.gameObject.SetActive(false);
    }
    public void FallChest()
    {
        StartCoroutine(IE_delayPlaySoundFallChest());
        ChestSpine.AnimationState.SetAnimation(0, "Appear", false);
        ChestSpine.AnimationState.Complete += onComplete =>
        {
            if (onComplete.Animation.Name == "Appear")
            {
                OpenChest();
            }
        };
    }
    IEnumerator IE_delayPlaySoundFallChest()
    {
        yield return new WaitForSeconds(0.05f);
        AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectFallChest);
    }
    public void OpenChest(System.Action callback = null)
    {
        ChestSpine.AnimationState.SetAnimation(0, "Open", false);
        eventData = ChestSpine.Skeleton.Data.FindEvent("Open");
        ChestSpine.AnimationState.Event -= AnimationState_Event;
        ChestSpine.AnimationState.Event += AnimationState_Event;
    }
    private void AnimationState_Event(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        bool Event_Match = (eventData == e.Data);
        if (Event_Match)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectRewardChest);
            StartCoroutine(IEdelayActiveButton());
        }
    }
    IEnumerator IEdelayActiveButton()
    {
        yield return new WaitForSeconds(0.1f);
        isShowItemDone = true;
     //   ActiveButton();
        ShowItem();
    }
    public void ChangeSkinChest()
    {
        switch (EtypeChest)
        {
            case TypeChest.ChestNormal:
                ChestSpine.Skeleton.SetSkin("Skin_blue");
                ChestSpine.LateUpdate();
                break;
            case TypeChest.ChestEpic:
                ChestSpine.Skeleton.SetSkin("Skin_Violet");
                ChestSpine.LateUpdate();
                break;
            case TypeChest.ChestLegend:
                ChestSpine.Skeleton.SetSkin("Skin_gold");
                ChestSpine.LateUpdate();
                break;
        }
    }
    public void ActiveButton()
    {
        Skip_Button.gameObject.SetActive(true);
        Continue_Button.gameObject.SetActive(true);
    }
    public void HiddenButton()
    {
        Skip_Button.gameObject.SetActive(false);
        Continue_Button.gameObject.SetActive(false);
    }
    public void ShowItem()
    {
        switch (EtypeChest)
        {
            case TypeChest.ChestNormal:
                chestNormal.LoadItem();
                Update_After_Show_Item_Done();
                break;
            case TypeChest.ChestEpic:
                chestEpic.LoadItem();
                Update_After_Show_Item_Done();
                break;
            case TypeChest.ChestLegend:
                chestLegend.LoadItem();
                Update_After_Show_Item_Done();
                break;
        }
    }
    void Update_After_Show_Item_Done()
    {
        if (isShowItemDone)
        {
            Color color = CurrencyImage.GetComponent<Image>().color;
            color.a = 1;
            CurrencyImage.GetComponent<Image>().color = color;
            NotiObj.gameObject.SetActive(false);
            switch (EstatBuy)
            {
                case E_TypeBuy.ADS:
                    switch (EtypeChest)
                    {
                        case TypeChest.ChestNormal:
                            continue_Text.text = Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestNormal.ToString();
                            CurrencyImage.sprite = SP_Coin_btn;
                            break;
                        case TypeChest.ChestEpic:
                            continue_Text.text = Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestEpic.ToString();
                            CurrencyImage.sprite = SP_Gem;
                            break;
                        case TypeChest.ChestLegend:
                            continue_Text.text = Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestLegend.ToString();
                            CurrencyImage.sprite = SP_Gem;
                            break;
                    }
                    break;
                case E_TypeBuy.COIN:
                    switch (EtypeChest)
                    {
                        case TypeChest.ChestNormal:
                            if (chestNormal.QuantityBuy == E_QuantityBuy.x1)
                            {
                                continue_Text.text = chestNormal.Coin_x1.ToString();
                                CurrencyImage.sprite = SP_Coin_btn;
                            }
                            else if (chestNormal.QuantityBuy == E_QuantityBuy.x10)
                            {
                                continue_Text.text = chestNormal.Coin_x10.ToString();
                                CurrencyImage.sprite = SP_Coin_btn;
                            }
                            break;
                    }
                    break;
                case E_TypeBuy.GEM:
                    switch (EtypeChest)
                    {
                        case TypeChest.ChestEpic:
                            if (chestEpic.QuantityBuy == E_QuantityBuy.x1)
                            {
                                continue_Text.text = chestEpic.Gem_x1.ToString();
                                CurrencyImage.sprite = SP_Gem;
                            }
                            else if (chestEpic.QuantityBuy == E_QuantityBuy.x10)
                            {
                                continue_Text.text = chestEpic.Gem_10.ToString();
                                CurrencyImage.sprite = SP_Gem;
                            }

                            break;
                        case TypeChest.ChestLegend:
                            if (chestLegend.QuantityBuy == E_QuantityBuy.x1)
                            {
                                continue_Text.text = chestLegend.Gem_x1.ToString();
                                CurrencyImage.sprite = SP_Gem;
                            }
                            else if (chestLegend.QuantityBuy == E_QuantityBuy.x10)
                            {
                                continue_Text.text = chestLegend.Gem_10.ToString();
                                CurrencyImage.sprite = SP_Gem;
                            }
                            break;
                    }
                    break;
                case E_TypeBuy.PACK:
                    switch (EtypeChest)
                    {
                        case TypeChest.ChestNormal:
                            if (chestNormal.QuantityNormalChest <= 0)
                            {
                                continue_Text.text = Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestNormal.ToString();
                                CurrencyImage.sprite = SP_Coin_btn;
                            }
                            else
                            {
                                continue_Text.text = I2.Loc.LocalizationManager.GetTranslation("KEY_OPEN");
                                Color a = CurrencyImage.GetComponent<Image>().color;
                                a.a = 0;
                                CurrencyImage.GetComponent<Image>().color = a;
                                NotiObj.gameObject.SetActive(true);
                                notiObjTxt.text = "x" + DataPlayer.GetQuantityChestNormalPack().ToString();
                            }
                            break;
                        case TypeChest.ChestEpic:
                            if (chestEpic.QuantityEpicChest <= 0)
                            {
                                continue_Text.text = Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestEpic.ToString();
                                CurrencyImage.sprite = SP_Gem;
                            }
                            else
                            {
                                continue_Text.text = I2.Loc.LocalizationManager.GetTranslation("KEY_OPEN");

                                Color a = CurrencyImage.GetComponent<Image>().color;
                                a.a = 0;
                                CurrencyImage.GetComponent<Image>().color = a;
                                NotiObj.gameObject.SetActive(true);
                                notiObjTxt.text = "x" + DataPlayer.GetQuantityChestEpicPack().ToString();
                            }
                            break;
                        case TypeChest.ChestLegend:
                            if (chestLegend.QuantityLegendChest <= 0)
                            {
                                continue_Text.text = Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestLegend.ToString();
                                CurrencyImage.sprite = SP_Gem;
                            }
                            else
                            {
                                continue_Text.text = I2.Loc.LocalizationManager.GetTranslation("KEY_OPEN");

                                Color a = CurrencyImage.GetComponent<Image>().color;
                                a.a = 0;
                                CurrencyImage.GetComponent<Image>().color = a;
                                NotiObj.gameObject.SetActive(true);
                                notiObjTxt.text = "x" + DataPlayer.GetQuantityChestLegendPack().ToString();
                            }
                            break;
                    }
                    break;
            }
        }
    }
    
    void OnClickButtonContinue()
    {
        ResetContent();
        
        switch (EstatBuy)
        {
            case E_TypeBuy.ADS:
                switch (EtypeChest)
                {
                    case TypeChest.ChestNormal:
                        if (chestNormal.QuantityBuy == E_QuantityBuy.x1)
                        {
                            if (DataPlayer.GetCoin() >= chestNormal.Coin_x1)
                            {
                                chestNormal.BUY_CHEST_X1();
                                Action2();
                            }
                            else
                            {
                                Debug.LogError("Khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.COIN;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }
                       /* else if (chestNormal.QuantityBuy == E_QuantityBuy.x10)
                        {
                            if (DataPlayer.GetCoin() >= chestNormal.Coin_x10)
                            {
                                chestNormal.BUY_CHEST_X10();
                                Action2();
                            }
                            else
                            {
                                Debug.Log("khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }*/
                        break;
                    case TypeChest.ChestEpic:
                        if (chestEpic.QuantityBuy == E_QuantityBuy.x1)
                        {
                            if (DataPlayer.GetGem() > chestEpic.Gem_x1)
                            {
                                chestEpic.BUY_CHEST_X1();
                                Action2();
                            }
                            else
                            {
                                Debug.Log("khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.GEM;
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }
                      /*  else if (chestEpic.QuantityBuy == E_QuantityBuy.x10)
                        {
                            if(DataPlayer.GetGem() > chestEpic.Gem_10)
                            {
                                chestEpic.BUY_CHEST_X10();
                                Action2();
                            }
                            else
                            {
                                Debug.Log("khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }*/
                        break;
                    case TypeChest.ChestLegend:
                        if (chestLegend.QuantityBuy == E_QuantityBuy.x1)
                        {
                            if (DataPlayer.GetGem() > chestLegend.Gem_10)
                            {
                                chestLegend.BUY_CHEST_X1();
                                Action2();
                            }
                            else
                            {
                                Debug.Log("khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.GEM;
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }
                      /*  else if (chestLegend.QuantityBuy == E_QuantityBuy.x10)
                        {
                            if (DataPlayer.GetGem() > chestLegend.Gem_10)
                            {
                                chestLegend.BUY_CHEST_X10();
                                Action2();
                            }
                            else
                            {
                                Debug.Log("khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }*/
                        break;
                }
                break;
            case E_TypeBuy.COIN:
                switch (EtypeChest)
                {
                    case TypeChest.ChestNormal:
                        if (chestNormal.QuantityBuy == E_QuantityBuy.x1)
                        {
                            if (DataPlayer.GetCoin() >= chestNormal.Coin_x1)
                            {
                                chestNormal.BUY_CHEST_X1();
                                Action2();
                            }
                            else
                            {
                                Debug.LogError("Khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.COIN;
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }
                        else if (chestNormal.QuantityBuy == E_QuantityBuy.x10)
                        {
                            if (DataPlayer.GetCoin() > chestNormal.Coin_x10)
                            {
                                chestNormal.BUY_CHEST_X10();
                                Action2();
                            }
                            else
                            {
                                Debug.LogError("Khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.COIN;
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }
                        break;
                    case TypeChest.ChestEpic:
                        Debug.LogError("a");
                        if (chestEpic.QuantityBuy == E_QuantityBuy.x1)
                        {
                            if (DataPlayer.GetGem() > chestEpic.Gem_x1)
                            {
                                chestEpic.BUY_CHEST_X1();
                                Action2();
                            }
                            else
                            {
                                Debug.LogError("Khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.GEM;
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }
                      /*  else if (chestEpic.QuantityBuy == E_QuantityBuy.x10)
                        {

                            if (DataPlayer.GetGem() > chestEpic.Gem_10)
                            {
                                chestEpic.BUY_CHEST_X10();
                                Action2();
                            }
                            else
                            {
                                Debug.LogError("Khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }*/
                        break;
                    case TypeChest.ChestLegend:
                        if (chestLegend.QuantityBuy == E_QuantityBuy.x1)
                        {
                            if (DataPlayer.GetGem() > chestLegend.Gem_x1)
                            {
                                chestLegend.BUY_CHEST_X1();
                                Action2();
                            }
                            else
                            {
                                Debug.LogError("Khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.GEM;
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }
                       /* else if (chestLegend.QuantityBuy == E_QuantityBuy.x10)
                        {
                            if (DataPlayer.GetGem() > chestLegend.Gem_10)
                            {
                                chestLegend.BUY_CHEST_X10();
                                Action2();
                            }
                            else
                            {
                                Debug.LogError("Khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }*/
                        break;
                }
                break;
            case E_TypeBuy.GEM:
                switch (EtypeChest)
                {
                   /* case TypeChest.ChestNormal:
                        if (chestNormal.QuantityBuy == E_QuantityBuy.x1)
                        {
                            chestNormal.BUY_CHEST_X1();
                            Action2();
                        }
                        else if (chestNormal.QuantityBuy == E_QuantityBuy.x10)
                        {
                            chestNormal.BUY_CHEST_X10();
                            Action2();
                        }
                        break;*/
                    case TypeChest.ChestEpic:
                        if (chestEpic.QuantityBuy == E_QuantityBuy.x1)
                        {
                            if (DataPlayer.GetGem() >= chestEpic.Gem_x1)
                            {
                                chestEpic.BUY_CHEST_X1();
                                Action2();
                            }
                            else
                            {
                                Debug.LogError("Khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.GEM;
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }
                        else if (chestEpic.QuantityBuy == E_QuantityBuy.x10)
                        {
                            if (DataPlayer.GetGem() >= chestEpic.Gem_10)
                            {
                                chestEpic.BUY_CHEST_X10();
                                Action2();
                            }
                            else
                            {
                                Debug.LogError("Khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.GEM;
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }
                        break;
                    case TypeChest.ChestLegend:
                        if (chestLegend.QuantityBuy == E_QuantityBuy.x1)
                        {
                            if (DataPlayer.GetGem() >= chestLegend.Gem_x1)
                            {
                                chestLegend.BUY_CHEST_X1();
                                Action2();
                            }
                            else
                            {
                                Debug.LogError("Khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.GEM;
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }
                        else if (chestLegend.QuantityBuy == E_QuantityBuy.x10)
                        {
                            if (DataPlayer.GetGem() >= chestLegend.Gem_10)
                            {
                                chestLegend.BUY_CHEST_X10();
                                Action2();
                            }
                            else
                            {
                                Debug.LogError("Khong du tien");
                                popUpManager.Instance.m_PopUpNotmoney.type_Currentcy = TypeCurrentcy.GEM;
                                popUpManager.Instance.m_PopUpNotmoney.obj = gameObject;
                                popUpManager.Instance.m_PopUpNotmoney.gameObject.SetActive(true);
                            }
                        }
                        break;
                }
                break;
            case E_TypeBuy.PACK:
                switch (EtypeChest)
                {
                    case TypeChest.ChestNormal:
                        if (chestNormal.QuantityBuy == E_QuantityBuy.x1)
                        {
                            chestNormal.BUY_CHEST_X1();
                            Action2();
                        }
                        else if (chestNormal.QuantityBuy == E_QuantityBuy.x10)
                        {
                            chestNormal.BUY_CHEST_X10();
                            OpenChest();
                        }
                        break;
                    case TypeChest.ChestEpic:
                        if (chestEpic.QuantityBuy == E_QuantityBuy.x1)
                        {
                            chestEpic.BUY_CHEST_X1();
                            Action2();
                        }
                        else if (chestEpic.QuantityBuy == E_QuantityBuy.x10)
                        {
                            chestEpic.BUY_CHEST_X10();
                            Action2();
                        }
                        break;
                    case TypeChest.ChestLegend:
                        if (chestLegend.QuantityBuy == E_QuantityBuy.x1)
                        {
                            chestLegend.BUY_CHEST_X1();
                            Action2();
                        }
                        else if (chestLegend.QuantityBuy == E_QuantityBuy.x10)
                        {
                            chestLegend.BUY_CHEST_X10();
                            Action2();
                        }
                        break;
                }
                break;
        }
    }
    void OnclickSkipButton()
    {
        gameObject.SetActive(false);
    }
    public void ResetContent()
    {
        for (int i = 0; i < ContentList.Count; i++)
        {
            Destroy(ContentList[i]);
          
        }
        ContentList.Clear();
        for (int i = 0; i < ItemCloneList.Count; i++)
        {
            Destroy(ItemCloneList[i].gameObject);
         
        }
        ContentList.Clear();
    }

    public void ActiveWithSpin()
    {

    }
    private void OnDisable()
    {
        ResetContent();
    }
}
