using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class X10ChestLegend : ChestParent
{
    public Button PurchaseButton;
    public Text PriceTxt;
    public int CountShowCard;
    public int Price;

    public List<ChestReward> L_chestRw;

    private void Awake()
    {
        PurchaseButton.onClick.AddListener(BuyX10Chest);
    }
    private void Start()
    {
        PriceTxt.text = (Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestLegend * 10 * 0.8f).ToString();
    }
    public override void BuyX10Chest()
    {
        m_PopUpChest.indexChestLegendX10 = 0;
        m_PopUpChest.typeChest = TypeChest.ChestLegendx10;
        L_chestRw = new List<ChestReward>();
        for (int i = 0; i < 10; i++)
        {
            ChestReward chestRw = Controller.Instance.dataChest.ChestRewardIndex(TypeChest.ChestLegend);
            L_chestRw.Add(chestRw);
        }
        base.BuyX10Chest();
    }
    public override void SubCoin()
    {
        base.SubCoin();
    }
}
