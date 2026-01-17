using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChestLegend : ChestParent
{
    public int TotalGem;
    public Button PurchaseButton;
    public Text PriceTxt;
    public TypeChest typeChest;
    public ChestReward chestRw;

    private void Awake()
    {
        PurchaseButton.onClick.AddListener(OnclickButton);
    }
    private void Start()
    {
        SetPrice();
    }
    void OnclickButton()
    {
        typeChest = TypeChest.ChestLegend;
        m_PopUpChest.typeChest = TypeChest.ChestLegend;
        BuyChestWithGem();
        SubGem();
        chestRw = Controller.Instance.dataChest.ChestRewardIndex(TypeChest.ChestLegend);
    }
    public void SubGem()
    {
        TotalGem -= Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestLegend;
    }
    void SetPrice()
    {
        PriceTxt.text = Controller.Instance.dataChest.PRICE_CHEST[0].PriceChestLegend.ToString();
    }
    public override void BuyChestWithGem()
    {
        base.BuyChestWithGem();
    }
}
