using Dragon.SDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PACK_ELEMENT : MonoBehaviour
{
    public typePack type_Pack;
    public List<PackReward> L_Packrw = new List<PackReward>();

    public Image item1;
    public Image item2;
    public Image item3;

    public Text quantityItem1Txt;
    public Text quantityItem2Txt;
    public Text quantityItem3Txt;
    public Text PriceTxt;

    public Button Purchasebutton;
    public PackShop m_PackShop;
    public PopUpPack m_PopUpPack;

    private void Awake()
    {
        Purchasebutton.onClick.AddListener(OnClickPurchase);
    }

    private void Start()
    {
        UpdateView();

    }

    void UpdateView()
    {
        switch (type_Pack)
        {
            case typePack.PACK_1:
                L_Packrw = Controller.Instance.dataPack.L_PackReward1;
                PriceTxt.text = SDKDGManager.Instance.IAPManager.GetPrice(ProduckID.PACK_01_SHOP).ToString();
                break;
            case typePack.PACK_2:
                L_Packrw = Controller.Instance.dataPack.L_PackReward2;
                PriceTxt.text = SDKDGManager.Instance.IAPManager.GetPrice(ProduckID.PACK_02_SHOP).ToString();
                break;
            case typePack.PACK_3:
                L_Packrw = Controller.Instance.dataPack.L_PackReward3;
                PriceTxt.text = SDKDGManager.Instance.IAPManager.GetPrice(ProduckID.PACK_03_SHOP).ToString();
                break;
            case typePack.PACK_4:
                L_Packrw = Controller.Instance.dataPack.L_PackReward4;
                PriceTxt.text = SDKDGManager.Instance.IAPManager.GetPrice(ProduckID.PACK_04_SHOP).ToString();
                break;
            case typePack.PACK_5:
                L_Packrw = Controller.Instance.dataPack.L_PackReward5;
                PriceTxt.text = SDKDGManager.Instance.IAPManager.GetPrice(ProduckID.PACK_05_SHOP).ToString();
                break;
            case typePack.PACK_6:
                L_Packrw = Controller.Instance.dataPack.L_PackReward6;
                PriceTxt.text = SDKDGManager.Instance.IAPManager.GetPrice(ProduckID.PACK_06_SHOP).ToString();
                break;
            case typePack.PACK_7:
                L_Packrw = Controller.Instance.dataPack.L_PackReward7;
                PriceTxt.text = SDKDGManager.Instance.IAPManager.GetPrice(ProduckID.PACK_07_SHOP).ToString();
                break;
        }

        item1.sprite = L_Packrw[0].SP_Item;
        item2.sprite = L_Packrw[1].SP_Item;
        item3.sprite = L_Packrw[2].SP_Item;

        quantityItem1Txt.text = "x" + L_Packrw[0].Quantity.ToString();
        quantityItem2Txt.text = "x" + L_Packrw[1].Quantity.ToString();
        quantityItem3Txt.text = "x" + L_Packrw[2].Quantity.ToString();
    }
    public void OnClickPurchase()
    {
        switch (type_Pack)
        {
            case typePack.PACK_1:
                Purchase(ProduckID.PACK_01_SHOP);
                break;
            case typePack.PACK_2:
                Purchase(ProduckID.PACK_02_SHOP);
                break;
            case typePack.PACK_3:
                Purchase(ProduckID.PACK_03_SHOP);
                break;
            case typePack.PACK_4:
                Purchase(ProduckID.PACK_04_SHOP);
                break;
            case typePack.PACK_5:
                Purchase(ProduckID.PACK_05_SHOP);
                break;
            case typePack.PACK_6:
                Purchase(ProduckID.PACK_06_SHOP);
                break;
            case typePack.PACK_7:
                Purchase(ProduckID.PACK_07_SHOP);
                break;
        }
    }
    public void Purchase(string id)
    {
        SDKDGManager.Instance.IAPManager.Purchase(id, () =>
        {
            OnclickPurchaseButton();
            Debug.Log(id + "$");
            Debug.Log("Pruchase Success Update UI");
        });
    }

    public void OnclickPurchaseButton()
    {
        m_PopUpPack.gameObject.SetActive(true);
        Debug.Log("da bam");
        Destroy(gameObject);

        PurchaseButtonPack m_Button = Purchasebutton.GetComponent<PurchaseButtonPack>();

        m_PackShop.ID = m_Button.ID;
        m_Button.m_pack.ID = m_Button.ID;
        m_Button.m_pack.ID2 = m_Button.ID_2;
        m_PackShop.GetPack();
        m_Button.m_pack.NextPack(0);

        Debug.Log("Button: " + m_Button.m_pack.ID);
        DataPlayer.addlistIDPack(m_Button.ID);


        Destroy(m_Button.m_pack.buttonsPacks[m_Button.ID].gameObject);
        m_Button.m_pack.buttonsPacks.Remove(m_Button.m_pack.buttonsPacks[m_Button.ID]);

        m_PackShop.L_PackElement.Remove(this);
        m_PackShop.index = 0;
        for (int i = 0; i < m_Button.m_pack.buttonsPacks.Count; i++)
        {
            m_Button.m_pack.buttonsPacks[i].GetComponent<ButtonPack>().ID = i;
        }

        Destroy(m_Button.m_pack.L_PurchaseButton[m_Button.ID].gameObject);
        m_Button.m_pack.L_PurchaseButton.Remove(m_Button.m_pack.L_PurchaseButton[m_Button.ID]);

        for (int i = 0; i < m_Button.m_pack.L_PurchaseButton.Count; i++)
        {
            m_Button.m_pack.L_PurchaseButton[i].GetComponent<PurchaseButtonPack>().ID = i;
        }

        for (int i = 0; i < m_Button.m_pack.L_Bg.Count; i++)
        {
            if (i == m_Button.ID)
            {
                Destroy(m_Button.m_pack.L_Bg[i].gameObject);
                m_Button.m_pack.L_Bg.Remove(m_Button.m_pack.L_Bg[i]);
            }
        }
        if (m_Button.m_pack.L_PurchaseButton.Count == 0)
        {
            m_Button.m_pack.BannerScale.gameObject.SetActive(false);
            m_Button.m_pack.Next_L.gameObject.SetActive(false);
            m_Button.m_pack.Next_R.gameObject.SetActive(false);
        }

        for (int i = 0; i < m_Button.m_pack.L_Bg.Count; i++)
        {
            if (m_Button.m_pack.L_Bg[i] != null)
            {
                m_Button.m_pack.L_Bg[i].GetComponent<Image>().color = Color.yellow;
                return;
            }
        }

    }
}
public enum typePack
{
    PACK_1,
    PACK_2,
    PACK_3,
    PACK_4,
    PACK_5,
    PACK_6,
    PACK_7
}

