using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewMergeElement", menuName = "CreateMergeElement")]
public class MergeElementData : ScriptableObject
{
    [SerializeField]
    public List<MergeElementStat> enemies = new List<MergeElementStat>();

    public List<MergeElementStat> enemiesHasPower = new List<MergeElementStat>();

    public List<string[]> test = new List<string[]>();
    public ECharacterType Child(ECharacterType parent, ECharacterType mother)
    {
        int parentPower = GetPowerOfType(parent);
        Debug.Log("parentPower: " + parentPower);
        int motherPower = GetPowerOfType(mother);
        Debug.Log("motherPower: " + motherPower);

        int rarityPar = GetRarityOfType(parent);
        int rarityMo = GetRarityOfType(mother);

        int count = enemiesHasPower.Count;

        int maxPower = 0;
        int minPower = 0;
        int maxRarity = 0;
        int minRarity = 0;

        if (parentPower > motherPower)
        {
            maxPower = parentPower;
            minPower = motherPower;
        }
        else
        {
            minPower = parentPower;
            maxPower = motherPower;
        }

        if (rarityPar > rarityMo)
        {
            maxRarity = rarityPar;
            minRarity = rarityMo;
        }
        else
        {
            minRarity = rarityPar;
            maxRarity = rarityMo;
        }
        int sumPower = (int)MergePowerCalculator(maxPower, minPower, maxRarity, minRarity);
        Debug.Log("SumPower: " + sumPower);

        for (int i = count - 1; i >= 0; i--)
        {
            if (sumPower >= enemiesHasPower[i].Power)
            {
                Debug.Log("type result: " + enemiesHasPower[i].Type);
                Debug.Log("SumPower result : " + enemiesHasPower[i].Power);

                return enemiesHasPower[i].Type;
            }
        }

        Debug.Log("not type: " + sumPower);

        return ECharacterType.NONE;
    }
   private int MergePowerCalculator(
    int power1, int power2,
    int rarity1, int rarity2)
{
    float bias = 1f + 0.15f * Mathf.Min(rarity1, rarity2);

    float mergedPower =
        Mathf.Sqrt(power1 * power2) * bias;

    return Mathf.RoundToInt(mergedPower);
}

    public int GetPowerOfType(ECharacterType type)
    {
        foreach (var item in enemiesHasPower)
        {
            if (item.Type == type)
                return item.Power;
        }
        return 0;
    }
    public int GetRarityOfType(ECharacterType type)
    {
        foreach (var item in enemiesHasPower)
        {
            if (item.Type == type)
                return item.Rarity;
        }
        return 0;
    }
#if UNITY_EDITOR
    [Button("Load Data")]
    private void LoadData()
    {
        #region NotPower
        //    enemies = new List<MergeElementStat>();

        // string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=636966808&single=true&output=csv";
        // System.Action<string> actionComplete = new System.Action<string>((string str) =>
        // {
        //     var data = CSVReader.ReadCSV(str);
        //     int n = data.Count;
        //     int m = data[0].Length;


        //     Debug.Log("N = " + n + " M = " + m);
        //     for (int i = 1; i < n; i++)
        //     {
        //         MergeElementStat item = new MergeElementStat();
        //         item.Type = Utils.ToEnum<ECharacterType>(data[i][0]);
        //         Debug.Log("data: " + item.Type);

        //         item.TypeChild = new List<ECharacterType>();
        //         for (int j = 1; j < m; j++)
        //         {
        //             ECharacterType tmp = ECharacterType.NONE;
        //             if (string.IsNullOrEmpty(data[i][j]))
        //             {
        //                 if (!string.IsNullOrEmpty(data[j][i]))
        //                 {
        //                     tmp = Utils.ToEnum<ECharacterType>(data[j][i]);
        //                 }
        //             }
        //             else
        //             {
        //                 tmp = Utils.ToEnum<ECharacterType>(data[i][j]);
        //             }
        //             item.TypeChild.Add(tmp);
        //         }
        //         enemies.Add(item);
        //     }

        //     UnityEditor.EditorUtility.SetDirty(this);
        // });
        // EditorCoroutine.start(Utils.IELoadData(url, actionComplete));

        #endregion
        enemiesHasPower = new();
        string urlHasPower = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRXk4sVXx1mJfg6hhufhHeGovZM-TdjRqKxcXrPkjznzfzSlzSsyFz6igyOHo8B35IhG1-b0nlTLRba/pub?gid=0&single=true&output=csv";
        System.Action<string> getDataComplete = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);

            for (int i = 1; i < data.Count; i++)
            {
                MergeElementStat mergeElementStatCache = new();
                mergeElementStatCache.Type = Utils.ToEnum<ECharacterType>(data[i][0]);
                mergeElementStatCache.Power = int.Parse(data[i][4]);
                mergeElementStatCache.Rarity = int.Parse(data[i][3]);
                enemiesHasPower.Add(mergeElementStatCache);
            }

            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(urlHasPower, getDataComplete));

    }
#endif

}

[System.Serializable]
public class MergeElementStat
{
    [FoldoutGroup("$Type")]
    public int Power;
    [FoldoutGroup("$Type")]
    public ECharacterType Type;
    [FoldoutGroup("$Type")]
    public int Rarity;
    [FoldoutGroup("$Type")]
    public List<ECharacterType> TypeChild;
}
