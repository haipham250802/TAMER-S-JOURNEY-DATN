using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "New Data Rewards", menuName = "Create Data Rewards")]
public class DataRewardsFree : ScriptableObject
{
    [SerializeField]
    public List<RewardsStat> L_rewards = new List<RewardsStat>();
#if UNITY_EDITOR
    [Button("Load")]
    void LoadData()
    {
        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=1738029607&single=true&output=csv";
        System.Action<string> ActionComplete = new System.Action<string>((string str) =>
        {
            L_rewards = new List<RewardsStat>();
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            Debug.Log(n);
            for (int i = 2; i < n; i++)
            {
                RewardsStat rew = new RewardsStat();
                rew.CurMap = int.Parse(data[i][0]);
                rew.Coin = int.Parse(data[i][1]);
                rew.Gem = int.Parse(data[i][2]);

                L_rewards.Add(rew);
            }
            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, ActionComplete));
    }

#endif
}
[System.Serializable]
public class RewardsStat
{
    public int CurMap;
    public int Coin;
    public int Gem;
}
