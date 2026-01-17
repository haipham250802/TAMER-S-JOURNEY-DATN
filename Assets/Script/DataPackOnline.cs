using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "New Data Pack Online", menuName = "Create DataPack Online")]
public class DataPackOnline : ScriptableObject
{
    [SerializeField]
    public List<PackOnline> L_PackOnline = new List<PackOnline>();

#if UNITY_EDITOR
    [Button("Load")]
    void LoadData()
    {
        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=1247044339&single=true&output=csv";
        System.Action<string> ActionComplete = new System.Action<string>((string str) =>
        {
            L_PackOnline = new List<PackOnline>();
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            Debug.Log(n);
            for (int i = 2; i < n; i++)
            {
                PackOnline packOnline = new PackOnline();
                packOnline.L_pack.typeLogInDay = Utils.ToEnum<E_LogInDays>(data[i][0]);
                packOnline.L_pack.GemFree = int.Parse(data[i][2]);
                packOnline.L_pack.GemNoFree = int.Parse(data[i][3]);
                L_PackOnline.Add(packOnline);
            }
            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, ActionComplete));
    }

#endif
}

[System.Serializable]
public class PackOnline
{
    public PackStat L_pack = new PackStat();
}
[System.Serializable]
public class PackStat
{
    public E_LogInDays typeLogInDay;
    public int GemNoFree;
    public int GemFree;
}
