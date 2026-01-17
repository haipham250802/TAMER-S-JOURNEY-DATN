using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "New Data Pack Zone", menuName = "Create DataPack Zone")]

public class DataPackZone : ScriptableObject
{
    [SerializeField]
    public List<PackZoneStat> L_PackZoneStat = new List<PackZoneStat>();
    public Sprite SP_Coin;
    public Sprite SP_Gem;
    public Sprite SP_ChestNormal;
    public Sprite SP_ChestEpic;
    public Sprite SP_ChestLegend;

#if UNITY_EDITOR
    [Button("Load")]
    void LoadData()
    {
        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=1111231652&single=true&output=csv";
        System.Action<string> ActionComplete = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            L_PackZoneStat = new List<PackZoneStat>();

            for (int i = 2; i < n; i++)
            {
                PackZoneStat PackZone = new PackZoneStat();
                PackZone.typeZone = Utils.ToEnum<TypeZone>(data[i][0]);
                PackZone.TypeRewardsNofree1 = Utils.ToEnum<TypeRewardPack>(data[i][4]);
                PackZone.TypeRewardsNofree2 = Utils.ToEnum<TypeRewardPack>(data[i][6]);
                switch (PackZone.TypeRewardsNofree1)
                {
                    case TypeRewardPack.Gem:
                        PackZone.SP_NoFreeIcon1 = SP_Gem;
                        break;
                    case TypeRewardPack.Coin:
                        PackZone.SP_NoFreeIcon1 = SP_Coin;
                        break;
                }
                switch (PackZone.TypeRewardsNofree2)
                {
                    case TypeRewardPack.Coin:
                        PackZone.SP_NoFreeIcon2 = SP_Coin;
                        break;
                    case TypeRewardPack.Gem:
                        PackZone.SP_NoFreeIcon2 = SP_Gem;
                        break;
                    case TypeRewardPack.Chest_Normal:
                        PackZone.SP_NoFreeIcon2 = SP_ChestNormal;
                        break;
                    case TypeRewardPack.Chest_Epic:
                        PackZone.SP_NoFreeIcon2 = SP_ChestEpic;
                        break;
                    case TypeRewardPack.Chest_Legend:
                        PackZone.SP_NoFreeIcon2 = SP_ChestLegend;
                        break;
                }
                PackZone.QuantityRewarsFree = int.Parse(data[i][1]);
                PackZone.QuantityRewarsNoFree1 = int.Parse(data[i][3]);
                PackZone.QuantityRewarsNoFree2 = int.Parse(data[i][5]);

                L_PackZoneStat.Add(PackZone);
            }
            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, ActionComplete));
    }
#endif
}

[System.Serializable]
public class PackZoneStat
{
    [FoldoutGroup("PackZone")]
    public TypeZone typeZone;
    [FoldoutGroup("PackZone")]
    public TypeRewardPack TypeRewardsNofree1;
    [FoldoutGroup("PackZone")]
    public TypeRewardPack TypeRewardsNofree2;
    [FoldoutGroup("PackZone")]
    public Sprite SP_NoFreeIcon1;
    [FoldoutGroup("PackZone")]
    public Sprite SP_NoFreeIcon2;
    [FoldoutGroup("PackZone")]
    public int QuantityRewarsFree;
    [FoldoutGroup("PackZone")]
    public int QuantityRewarsNoFree1;
    [FoldoutGroup("PackZone")]
    public int QuantityRewarsNoFree2;
}
public enum TypeZone
{
    Zone_01,
    Zone_02,
    Zone_03,
    Zone_04,
    Zone_05,
    Zone_06,
    Zone_07,
    Zone_08,
    Zone_09,
    Zone_10
}
