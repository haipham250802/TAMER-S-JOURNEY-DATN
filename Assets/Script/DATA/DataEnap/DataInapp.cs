using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Enap", menuName = "Create Element Data")]
public class DataInapp : ScriptableObject
{
    [SerializeField]
    public List<ProductsGem> ProductGem = new List<ProductsGem>();
    public List<ProductsCoin> ProductCoin = new List<ProductsCoin>();
#if UNITY_EDITOR
    [Button("LOAD DATA")]
    void LoadData()
    {
        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=481821817&single=true&output=csv";
        System.Action<string> ActionComplete = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            Debug.Log("n: " + n + ": " + data[2][1]);

            ProductGem = new List<ProductsGem>();
            ProductCoin = new List<ProductsCoin>();
            for (int i = 2; i < n; i++)
            {
                ProductsGem proDuct = new ProductsGem();
                proDuct.TypeShopGem = Utils.ToEnum<TypeShopGem>(data[i][0]);
                proDuct.Quantity_Gem = int.Parse(data[i][1]);
                proDuct.Cost = float.Parse(data[i][2]);
                if (proDuct.TypeShopGem != TypeShopGem.NONE)
                {
                    ProductGem.Add(proDuct);
                }
            }

            for (int i = 2; i < n; i++)
            {
                ProductsCoin proDuct = new ProductsCoin();
                proDuct.typeShopCoin = Utils.ToEnum<TypeShopCoin>(data[i][4]);
                proDuct.Quantity_Coin = int.Parse(data[i][5]);
                proDuct.Cost = float.Parse(data[i][6]);
                if (proDuct.typeShopCoin != TypeShopCoin.NONE)
                {
                    ProductCoin.Add(proDuct);
                }
            }
            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, ActionComplete));
    }
#endif
}

[System.Serializable]
public class ProductsGem
{
    [FoldoutGroup("ProductGem")]
    public TypeShopGem TypeShopGem;
    [FoldoutGroup("ProductGem")]
    public float Cost;
    [FoldoutGroup("ProductGem")]
    public int Quantity_Gem;
}
[System.Serializable]

public class ProductsCoin
{
    [FoldoutGroup("ProductCoin")]
    public TypeShopCoin typeShopCoin;
    [FoldoutGroup("ProductCoin")]
    public float Cost;
    [FoldoutGroup("ProductCoin")]
    public int Quantity_Coin;
}
public enum TypeShopGem
{
    NONE,
    Gem_free,
    Gem_1usd,
    Gem_5usd,
    Gem_10usd,
    Gem_20usd,
    Gem_50usd,
    Gem_100usd
}
public enum TypeShopCoin
{
    NONE,
    Coin_1,
    Coin_2,
    Coin_3
}
