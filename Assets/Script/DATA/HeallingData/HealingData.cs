using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Healing", menuName = "Create Healing")]
public class HealingData : ScriptableObject
{
    [SerializeField]
    public List<Blood_Rate> E_Blood = new List<Blood_Rate>();

    public Blood_Rate EBloodRateIndex(E_Stars e_Stars)
    {
        return E_Blood[(int)e_Stars];
    }

#if UNITY_EDITOR
    [Button("Load Data")]
    private void LoadData()
    {
        E_Blood = new List<Blood_Rate>();
        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=1778957278&single=true&output=csv";
        System.Action<string> actionComplete = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            Debug.Log(n);
            for (int i = 2; i < n; i++)
            {
                Blood_Rate e_Blood_Rate = new Blood_Rate();

                e_Blood_Rate.Cost_HP_Lowest = int.Parse(data[i][1]);
                e_Blood_Rate.Cost_HP_Low = int.Parse(data[i][2]);
                e_Blood_Rate.Cost_HP_Medium = int.Parse(data[i][3]);
                e_Blood_Rate.Cost_HP_Hight = int.Parse(data[i][4]);

                E_Blood.Add(e_Blood_Rate);
            }

            Debug.Log(E_Blood[1].Cost_HP_Medium);
            UnityEditor.EditorUtility.SetDirty(this);

        });
        EditorCoroutine.start(Utils.IELoadData(url, actionComplete));
    }
#endif
}
[System.Serializable]
public class Blood_Rate
{
    public int Cost_HP_Lowest;
    public int Cost_HP_Low;
    public int Cost_HP_Medium;
    public int Cost_HP_Hight;
}
public enum E_Stars
{
    NONE = -1,
    Star_1 = 0,
    Star_2 = 1,
    Star_3 = 2,
    Star_4 = 3,
    Star_5 = 4,
    Star_6 = 5,
    Star_7 = 6,
    Star_8 = 7,
    Star_9 = 8,
    Star_10 = 9,
    Star_11 = 10,
}
public class HealingStat
{
    public E_Stars Stars;
    public List<E_Stars> L_Stars = new List<E_Stars>();
}
