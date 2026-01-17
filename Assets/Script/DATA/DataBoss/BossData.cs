using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "New Boss", menuName = "Create Boss")]
public class BossData : ScriptableObject
{

    [SerializeField]
    public List<BossStat> Boss = new List<BossStat>();
    public Sprite Icon;

    public BossStat BossStatIndex(EBossType type)
    {
        return Boss[(int)type];
    }
#if UNITY_EDITOR
    [Button("Load Data")]
    private void LoadData()
    {
        Boss = new List<BossStat>();
        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=0&single=true&output=csv";
        System.Action<string> actionComplete = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            Debug.Log(n);
            for (int i = 1; i < n; i++)
            {
                
                if (int.Parse(data[i][8]) == 1)
                {
                    BossStat item = new BossStat();
                    EBossType tmp = Utils.ToEnum<EBossType>(data[i][0]);
                    Debug.Log("a: " + i);
                    item.Type = tmp;

                    item.Damage = int.Parse(data[i][1]);

                    item.HP = int.Parse(data[i][2]);

                    item.Rarity = int.Parse(data[i][3]);

                    item.EnocunterATK = int.Parse(data[i][4]);

                    item.EncounterHP = int.Parse(data[i][5]);
                    item.ICON = Icon;

                    if (string.IsNullOrEmpty(data[i][6])) item.CatchChance = 0;
                    else item.CatchChance = int.Parse(data[i][6]);

                    if (string.IsNullOrEmpty(data[i][7])) item.CombineCost = 0;
                    else item.CombineCost = int.Parse(data[i][7]);

                    Boss.Add(item);
                }
            }
            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, actionComplete));
    }
#endif
}
[System.Serializable]
public class BossStat
{
    [FoldoutGroup("$Type")]
    public Sprite ICON;
    [FoldoutGroup("$Type")]
    public EBossType Type;
    [FoldoutGroup("$Type")]
    public int ID;
    [FoldoutGroup("$Type")]
    public int Damage;
    [FoldoutGroup("$Type")]
    public int HP;
    [FoldoutGroup("$Type")]
    public int Rarity;
    [FoldoutGroup("$Type")]
    public int EnocunterATK;
    [FoldoutGroup("$Type")]
    public int EncounterHP;
    [FoldoutGroup("$Type")]
    public int CatchChance;
    [FoldoutGroup("$Type")]
    public int CombineCost;
}
public enum EBossType
{
    NONE = -1,
    Octoking,
    Captaincarrot,
    Cowcho,
    Schytherret,
    Cluckix,
    Gingyking,
    Shreekin,
    Magamlor,
    Mecharaijin,
}