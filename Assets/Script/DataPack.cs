using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Data Pack", menuName = "DataGame/Create DataPack")]
public class DataPack : ScriptableObject
{
    [SerializeField]
    public List<PackReward> L_PackReward1 = new List<PackReward>();
    public List<PackReward> L_PackReward2 = new List<PackReward>();
    public List<PackReward> L_PackReward3 = new List<PackReward>();
    public List<PackReward> L_PackReward4 = new List<PackReward>();
    public List<PackReward> L_PackReward5 = new List<PackReward>();
    public List<PackReward> L_PackReward6 = new List<PackReward>();
    public List<PackReward> L_PackReward7 = new List<PackReward>();

    public List<PricePack> L_PricePack = new List<PricePack>();

#if UNITY_EDITOR
    [Button("Load")]
    void LoadData()
    {
        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=776261938&single=true&output=csv";
        System.Action<string> ActionComplete = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;

            L_PackReward1 = new List<PackReward>();
            L_PackReward2 = new List<PackReward>();
            L_PackReward3 = new List<PackReward>();
            L_PackReward4 = new List<PackReward>();
            L_PackReward5 = new List<PackReward>();
            L_PackReward6 = new List<PackReward>();
            L_PackReward7 = new List<PackReward>();
            L_PricePack = new List<PricePack>();

            Debug.Log("Count: " + n);
            for (int i = 2; i < 5; i++)
            {
                PackReward Pack1 = new PackReward();
                Pack1.typeRwPack = Utils.ToEnum<TypeRewardPack>(data[i][0]);
                Pack1.Quantity = int.Parse(data[i][1]);
                L_PackReward1.Add(Pack1);

                PackReward Pack2 = new PackReward();
                Pack2.typeRwPack = Utils.ToEnum<TypeRewardPack>(data[i][3]);
                Pack2.Quantity = int.Parse(data[i][4]);
                L_PackReward2.Add(Pack2);

                PackReward Pack3 = new PackReward();
                Pack3.typeRwPack = Utils.ToEnum<TypeRewardPack>(data[i][6]);
                Pack3.Quantity = int.Parse(data[i][7]);
                L_PackReward3.Add(Pack3);

                PackReward Pack4 = new PackReward();
                Pack4.typeRwPack = Utils.ToEnum<TypeRewardPack>(data[i][9]);
                Pack4.Quantity = int.Parse(data[i][10]);
                L_PackReward4.Add(Pack4);

                PackReward Pack5 = new PackReward();
                Pack5.typeRwPack = Utils.ToEnum<TypeRewardPack>(data[i][12]);
                Pack5.Quantity = int.Parse(data[i][13]);
                L_PackReward5.Add(Pack5);

                PackReward Pack6 = new PackReward();
                Pack6.typeRwPack = Utils.ToEnum<TypeRewardPack>(data[i][15]);
                Pack6.Quantity = int.Parse(data[i][16]);
                L_PackReward6.Add(Pack6);
                
                PackReward Pack7 = new PackReward();
                Pack7.typeRwPack = Utils.ToEnum<TypeRewardPack>(data[i][18]);
                Pack7.Quantity = int.Parse(data[i][19]);
                L_PackReward7.Add(Pack7);
            }
            for (int i = 7; i < n; i++)
            {
                PricePack pricePack = new PricePack();
                pricePack.pricePack = float.Parse(data[i][1]);
                L_PricePack.Add(pricePack);
            }
            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, ActionComplete));
    }
#endif
}

[System.Serializable]
public class PackReward
{
    public TypeRewardPack typeRwPack;
    public Sprite SP_Item;
    public int Quantity;
}
[System.Serializable]
public class PricePack
{
    public float pricePack;
}
[System.Serializable]
public enum TypeRewardPack
{
    NONE = -1,
    Gem,
    Coin,
    Chest_Normal,
    Chest_Epic,
    Chest_Legend
}


