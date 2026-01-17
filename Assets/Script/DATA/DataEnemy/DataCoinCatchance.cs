using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Data Catchance", menuName = "Create Data Catchance")]
public class DataCoinCatchance : ScriptableObject
{
    public List<CatchChanceStat> dataCoins = new List<CatchChanceStat>();

    public CatchChanceStat CatchanceStat(int Star)
    {
        if (Star > 0)
            return dataCoins[Star - 1];
        return null;
    }
#if UNITY_EDITOR
    [Button("LOAD DATA")]
    private void LoadData()
    {
        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=545160927&single=true&output=csv";
        System.Action<string> actionComplete = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            dataCoins.Clear();
            Debug.Log(n);
            for (int i = 2; i < n; i++)
            {
                CatchChanceStat catchChanceStat = new CatchChanceStat();

                catchChanceStat.Star = int.Parse(data[i][0]);
                catchChanceStat.Coin = int.Parse(data[i][1]);
                catchChanceStat.Gem = int.Parse(data[i][2]);
                catchChanceStat.RateBonusWithCoin = int.Parse(data[i][3]);
                catchChanceStat.RateBonusWithGem = int.Parse(data[i][4]);
                catchChanceStat.RateBonusWithAds = int.Parse(data[i][5]);

                dataCoins.Add(catchChanceStat);
            }
            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, actionComplete));
    }
#endif
}
[System.Serializable]
public class CatchChanceStat
{
    public int Star;
    public int Coin;
    public int Gem;
    public int RateBonusWithCoin;
    public int RateBonusWithGem;
    public int RateBonusWithAds;
}

