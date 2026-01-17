using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PackShop : MonoBehaviour
{
    public GameObject ContentPack;
    public GameObject BannerScale;

    public int index;
    public int ID;
    public int ID2;

    public Button Next_L;
    public Button Next_R;

    public List<Button> buttonsPacks;
    public List<Button> L_PurchaseButton;
    public Vector3[] Offset;
    public PopUpPack m_PopUpPack;

    public List<GameObject> L_Bg = new List<GameObject>();
    public List<PACK_ELEMENT> L_PackElement = new List<PACK_ELEMENT>();

    public int QuantityChestNormal;
    public int QuantityChestEpic;
    public int QuantityChestLegend;

    private void Awake()
    {
        Next_L.onClick.AddListener(OnClickButtonLeft);
        Next_R.onClick.AddListener(OnCLickButtonRight);
    }
    private void OnEnable()
    {
        StartCoroutine(IE_delayOnenable());
    }
    IEnumerator IE_delayOnenable()
    {
        yield return null;
        if (L_PackElement.Count == 0)
        {
            BannerScale.SetActive(false);
            Next_L.gameObject.SetActive(false);
            Next_R.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        for (int i = 0; i < L_Bg.Count; i++)
        {
            L_Bg[i].GetComponent<Image>().color = Color.black;
        }
        L_Bg[0].GetComponent<Image>().color = Color.yellow;

        LoadPackElement();

    }
    public void OnClickButtonLeft()
    {
        if (index == 0)
        {
            return;
        }
        if (index > 0)
        {
            index--;
            NextPack(index);
            ActiveSelectedButton(index);
        }
    }
    public void OnCLickButtonRight()
    {
        if (index > L_PackElement.Count)
        {
            return;
        }
        if (index < L_PackElement.Count - 1)
        {
            index++;
            NextPack(index);
            ActiveSelectedButton(index);
        }
    }
    public void LoadPackElement()
    {
        for (int i = 0; i < L_PackElement.Count; i++)
        {
            for (int j = 0; j < DataPlayer.ListPack().Count; j++)
            {
                if (i == DataPlayer.ListPack()[j])
                {
                    Destroy(L_PackElement[i].gameObject);
                    L_PackElement.Remove(L_PackElement[i]);
                }
            }
        }

        for (int i = 0; i < L_Bg.Count; i++)
        {
            for (int j = 0; j < DataPlayer.ListPack().Count; j++)
            {
                if (i == DataPlayer.ListPack()[j])
                {
                    Destroy(L_Bg[i].gameObject);
                    L_Bg.Remove(L_Bg[i]);
                }
            }
        }

        for (int i = 0; i < buttonsPacks.Count; i++)
        {
            for (int j = 0; j < DataPlayer.ListPack().Count; j++)
            {
                if (i == DataPlayer.ListPack()[j])
                {
                    Destroy(buttonsPacks[i].gameObject);
                    buttonsPacks.Remove(buttonsPacks[i]);
                }
            }
        }

        for (int i = 0; i < L_PurchaseButton.Count; i++)
        {
            for (int j = 0; j < DataPlayer.ListPack().Count; j++)
            {
                if (i == DataPlayer.ListPack()[j])
                {
                    Destroy(L_PurchaseButton[i].gameObject);
                    L_PurchaseButton.Remove(L_PurchaseButton[i]);
                }
            }
        }

        for (int i = 0; i < L_PurchaseButton.Count; i++)
        {
            L_PurchaseButton[i].GetComponent<PurchaseButtonPack>().ID = i;
        }
        for (int i = 0; i < buttonsPacks.Count; i++)
        {
            buttonsPacks[i].GetComponent<ButtonPack>().ID = i;
        }
        for (int i = 0; i < L_Bg.Count; i++)
        {
            if (L_Bg[i] != null)
            {
                L_Bg[i].GetComponent<Image>().color = Color.yellow;
                return;
            }
        }
    }

    public void NextPack(int index)
    {
        switch (index)
        {
            case 0:
                if (L_PackElement.Count >= 1)
                    MoveBannerPack(Offset[0]);
                break;
            case 1:
                if (L_PackElement.Count >= 2)
                    MoveBannerPack(Offset[1]);
                break;
            case 2:
                if (L_PackElement.Count >= 3)
                    MoveBannerPack(Offset[2]);
                break;
            case 3:
                if (L_PackElement.Count >= 4)
                    MoveBannerPack(Offset[3]);
                break;
            case 4:
                if (L_PackElement.Count >= 5)
                    MoveBannerPack(Offset[4]);
                break;
            case 5:
                if (L_PackElement.Count >= 6)
                    MoveBannerPack(Offset[5]);
                break;
            case 6:
                if (L_PackElement.Count >= 7)
                    MoveBannerPack(Offset[6]);
                break;
        }
    }
    public void MoveBannerPack(Vector3 Pos)
    {
        ContentPack.transform.DOLocalMove(Pos, 0.3f);
    }
    public void ActiveSelectedButton(int index)
    {
        for (int i = 0; i < L_Bg.Count; i++)
        {
            if (L_Bg[i] != null)
                L_Bg[i].GetComponent<Image>().color = Color.black;
        }
        if (L_Bg[index] != null)
            L_Bg[index].GetComponent<Image>().color = Color.yellow;
    }

    public void GetPack()
    {
        switch (ID2)
        {
            case 0:
                SpawnCard(Controller.Instance.dataPack.L_PackReward1);
                break;
            case 1:
                SpawnCard(Controller.Instance.dataPack.L_PackReward2);
                break;
            case 2:
                SpawnCard(Controller.Instance.dataPack.L_PackReward3);
                break;
            case 3:
                SpawnCard(Controller.Instance.dataPack.L_PackReward4);
                break;
            case 4:
                SpawnCard(Controller.Instance.dataPack.L_PackReward5);
                break;
            case 5:
                SpawnCard(Controller.Instance.dataPack.L_PackReward6);
                break;
            case 6:
                SpawnCard(Controller.Instance.dataPack.L_PackReward7);
                break;
        }

        UI_Home.Instance.m_UIShop.chestNormal.InitView();
        UI_Home.Instance.m_UIShop.chestEpic.InitView();
        UI_Home.Instance.m_UIShop.chestLegend.InitView();
    }
    public void SpawnCard(List<PackReward> L_pack)
    {
        for (int i = 0; i < L_pack.Count; i++)
        {
            Debug.Log("Type: " + L_pack[i].typeRwPack);
            switch (L_pack[i].typeRwPack)
            {
                case TypeRewardPack.Gem:
                    var obj = Instantiate(m_PopUpPack.cardPopUpPack);
                    obj.GetComponent<CardItemPopUpPack>().IconItem.sprite = L_pack[i].SP_Item;
                    obj.GetComponent<CardItemPopUpPack>().Quantity.text = "x" + L_pack[i].Quantity.ToString();
                    obj.transform.SetParent(m_PopUpPack.content.transform);
                    obj.transform.localScale = Vector3.one;
                    m_PopUpPack.L_Item.Add(obj.gameObject);

                    UI_Home.Instance.m_UIGemManager.SetTextGem(L_pack[i].Quantity);

                    break;
                case TypeRewardPack.Coin:
                    var obj1 = Instantiate(m_PopUpPack.cardPopUpPack);
                    obj1.GetComponent<CardItemPopUpPack>().IconItem.sprite = L_pack[i].SP_Item;
                    obj1.GetComponent<CardItemPopUpPack>().Quantity.text = "x" + L_pack[i].Quantity.ToString();
                    obj1.transform.SetParent(m_PopUpPack.content.transform);
                    obj1.transform.localScale = Vector3.one;
                    m_PopUpPack.L_Item.Add(obj1.gameObject);

                    UI_Home.Instance.m_UICoinManager.SetTextCoin(L_pack[i].Quantity);
                    break;
                case TypeRewardPack.Chest_Normal:
                    var obj2 = Instantiate(m_PopUpPack.cardPopUpPack);
                    obj2.GetComponent<CardItemPopUpPack>().IconItem.sprite = L_pack[i].SP_Item;
                    obj2.GetComponent<CardItemPopUpPack>().Quantity.text = "x" + L_pack[i].Quantity.ToString();
                    obj2.transform.SetParent(m_PopUpPack.content.transform);
                    obj2.transform.localScale = Vector3.one;
                    m_PopUpPack.L_Item.Add(obj2.gameObject);
                    QuantityChestNormal = DataPlayer.GetQuantityChestNormalPack();
                    QuantityChestNormal += L_pack[i].Quantity;
                    DataPlayer.SetQuantityChestNormalPack(QuantityChestNormal);
                    break;
                case TypeRewardPack.Chest_Epic:
                    var obj3 = Instantiate(m_PopUpPack.cardPopUpPack);
                    obj3.GetComponent<CardItemPopUpPack>().IconItem.sprite = L_pack[i].SP_Item;
                    obj3.GetComponent<CardItemPopUpPack>().Quantity.text = "x" + L_pack[i].Quantity.ToString();
                    obj3.transform.SetParent(m_PopUpPack.content.transform);
                    obj3.transform.localScale = Vector3.one;
                    m_PopUpPack.L_Item.Add(obj3.gameObject);
                    QuantityChestEpic = DataPlayer.GetQuantityChestEpicPack();
                    QuantityChestEpic += L_pack[i].Quantity;
                    DataPlayer.SetQuantityChestEpicPack(QuantityChestEpic);
                    break;
                case TypeRewardPack.Chest_Legend:
                    var obj4 = Instantiate(m_PopUpPack.cardPopUpPack);
                    obj4.GetComponent<CardItemPopUpPack>().IconItem.sprite = L_pack[i].SP_Item;
                    obj4.GetComponent<CardItemPopUpPack>().Quantity.text = "x" + L_pack[i].Quantity.ToString();
                    obj4.transform.SetParent(m_PopUpPack.content.transform);
                    obj4.transform.localScale = Vector3.one;
                    m_PopUpPack.L_Item.Add(obj4.gameObject);
                    QuantityChestLegend += L_pack[i].Quantity;
                    DataPlayer.SetQuantityChestLegendPack(QuantityChestLegend);
                    break;
            }
        }
    }
}

