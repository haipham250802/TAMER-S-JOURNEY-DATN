using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New DataChest", menuName = "Create DataChest")]
public class DataChest : ScriptableObject
{
    [SerializeField]
    public List<ChestReward> CHEST_NORMAL = new List<ChestReward>();
    public List<ChestReward> CHEST_EPIC = new List<ChestReward>();
    public List<ChestReward> CHEST_LEGEND = new List<ChestReward>();
    public List<PriceChest> PRICE_CHEST = new List<PriceChest>();

    public ChestReward ChestRewardIndex(TypeChest type)
    {
        switch (type)
        {
            case TypeChest.ChestNormal:
                return GetChestRandom(CHEST_NORMAL);
            case TypeChest.ChestEpic:
                return GetChestRandom(CHEST_EPIC);
            case TypeChest.ChestLegend:
                return GetChestRandom(CHEST_LEGEND);
            default: 
                return null;
        }
    }
    ChestReward GetChestRandom(List<ChestReward> listRandom)
    {
        Dictionary<int, float> dictRandom = new Dictionary<int, float>();
        for(int i = 0; i < listRandom.Count; i++)
        {
            if (!dictRandom.ContainsKey(listRandom[i].id))
            {
                dictRandom.Add(listRandom[i].id, listRandom[i].Rate);
            }
        }
        int IDRANDOM = Catch_Chance_Controller.GetRandomByPercent<int>(dictRandom);
        ChestReward tmp = new ChestReward();
       
        for (int i = 0; i < listRandom.Count; i++)
        {
            if (listRandom[i].id == IDRANDOM)
            {
                tmp.id = listRandom[i].id;
                tmp.typeReward = listRandom[i].typeReward;
                tmp.QuantityOrStar = listRandom[i].QuantityOrStar;
                tmp.Rate = listRandom[i].Rate;
            }
        }
        return tmp;
    }
    public TypeChest typeTest = TypeChest.ChestEpic;
    public int quantityRandom = 1000;
#if UNITY_EDITOR
    [Button("LOAD TESST")]
    void Test()
    {
        for(int i = 0; i < quantityRandom; i++)
        {
            ChestReward tmp = ChestRewardIndex(typeTest);
            Debug.LogError("ID: " + tmp.id + "-Type: " + tmp.typeReward + "-quantity: " + tmp.QuantityOrStar + "-rate: " + tmp.Rate);

        }
    }
    [Button("LOAD DATA")]
    void LoadData()
    {
        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=123957616&single=true&output=csv";
        System.Action<string> ActionComplete = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            Debug.Log(n);
            CHEST_NORMAL = new List<ChestReward>();
            CHEST_EPIC = new List<ChestReward>();
            CHEST_LEGEND = new List<ChestReward>();
            PRICE_CHEST = new List<PriceChest>();

            for (int i = 1; i < n; i++)
            {
                if (i + 2 < n)
                {
                    ChestReward ChestNormal = new ChestReward();
                    ChestNormal.id = int.Parse(data[i + 2][0]);
                    ChestNormal.typeReward = Utils.ToEnum<TypeReward>(data[i + 2][1]);
                    ChestNormal.QuantityOrStar = int.Parse(data[i + 2][2]);
                    ChestNormal.Rate = float.Parse(data[i + 2][3]);
                    CHEST_NORMAL.Add(ChestNormal);


                    ChestReward ChestEpic = new ChestReward();
                    ChestEpic.id = int.Parse(data[i + 2][5]);
                    ChestEpic.typeReward = Utils.ToEnum<TypeReward>(data[i + 2][6]);
                    ChestEpic.QuantityOrStar = int.Parse(data[i + 2][7]);
                    ChestEpic.Rate = float.Parse(data[i + 2][8]);
                    CHEST_EPIC.Add(ChestEpic);


                    ChestReward ChestLegend = new ChestReward();
                    ChestLegend.id = int.Parse(data[i + 2][10]);
                    ChestLegend.typeReward = Utils.ToEnum<TypeReward>(data[i + 2][11]);
                    ChestLegend.QuantityOrStar = int.Parse(data[i + 2][12]);
                    ChestLegend.Rate = float.Parse(data[i + 2][13]);
                    CHEST_LEGEND.Add(ChestLegend);

                }

            }

            PriceChest priceChest = new PriceChest();
            priceChest.PriceChestNormal = int.Parse(data[2][15]);
            priceChest.PriceChestEpic = int.Parse(data[2][16]);
            priceChest.PriceChestLegend = int.Parse(data[2][17]);
            PRICE_CHEST.Add(priceChest);

            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, ActionComplete));
    }
#endif
}
[System.Serializable]
public class ChestReward
{
    public int id;
    public TypeReward typeReward;
    public int QuantityOrStar;
    public float Rate;
}
[System.Serializable]
public class PriceChest
{
    public int PriceChestNormal;
    public int PriceChestEpic;
    public int PriceChestLegend;
}
public enum TypeChest
{
    NONE = -1,
    ChestNormal = 0,
    ChestEpic = 1,
    ChestLegend = 2,
    ChestNormalx10,
    ChestEpicx10,
    ChestLegendx10
}
public enum TypeReward
{
    NONE = -1,
    Coin = 0,
    Gem = 1,
    Enemy = 2
}
