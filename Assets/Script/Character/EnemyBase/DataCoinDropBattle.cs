using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Data Coin Drop Battle", menuName = "CreateDataCoin")]
public class DataCoinDropBattle : ScriptableObject
{
    [SerializeField]
    public List<CoinKilledCritter> L_critter = new List<CoinKilledCritter>();
#if UNITY_EDITOR
    [Button("Load")]
    void Load()
    {
        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=1319751420&single=true&output=csv";
        System.Action<string> ActionComplete = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            Debug.Log("Count: " + n);
            L_critter = new List<CoinKilledCritter>();
            for(int i = 2; i < n; i++)
            {
                CoinKilledCritter coinKilledCritter = new CoinKilledCritter();
                coinKilledCritter.Star = int.Parse(data[i][0]);
                coinKilledCritter.Coin = int.Parse(data[i][1]);

                L_critter.Add(coinKilledCritter);
            }

            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, ActionComplete));
    }
#endif
}
[System.Serializable]
public class CoinKilledCritter
{
    public int Star;
    public int Coin;
}
