using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Create Enemy")]
public class EnemyData : ScriptableObject
{
    public SkeletonDataAsset Icon;
    public Sprite Avatar;
    [SerializeField]
    public List<EnemyStat> enemies = new List<EnemyStat>();
    [SerializeField]
    public EnemyStat EnemyStatIndex(ECharacterType type)
    {
        if (type != ECharacterType.NONE)
            return enemies[(int)type];
        return null;
    }
    public int GetHPEmemy(ECharacterType type)
    {
        return EnemyStatIndex(type).HP;
    }
    public int GetCounterHP(ECharacterType type)
    {
        return EnemyStatIndex(type).EncounterHP;

    }
    public int GetHPBoss(ECharacterType type)
    {
        return EnemyStatIndex(type).EncounterHP;
    }
    public int GetDamageEnemy(ECharacterType type)
    {
        if (type != ECharacterType.NONE)
            return EnemyStatIndex(type).Damage;
        return 0;
    }
#if UNITY_EDITOR
    [Button("FAKE ICON Data")]
    void FakeICon()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].ICON == null)
            {
                enemies[i].ICON = Icon;
            }
        }
    }
    [Button("Load Num Data")]
    void LoadNumData()
    {
        // enemies = new List<EnemyStat>();

        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=0&single=true&output=csv";
        System.Action<string> actionComplete = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            Debug.Log(n);
            for (int k = 1; k < n; k++)
            {
                ECharacterType tmp = Utils.ToEnum<ECharacterType>(data[k][0]);

            }
            for (int j = 0; j < enemies.Count; j++)
            {

                enemies[j].Damage = int.Parse(data[j + 1][1]);
                enemies[j].HP = int.Parse(data[j + 1][2]);

                enemies[j].Rarity = int.Parse(data[j + 1][3]);

                enemies[j].EnocunterATK = int.Parse(data[j + 1][4]);

                enemies[j].EncounterHP = int.Parse(data[j + 1][5]);

                if (string.IsNullOrEmpty(data[j + 1][6])) enemies[j].CatchChance = 0;
                else enemies[j].CatchChance = int.Parse(data[j + 1][6]);

                if (string.IsNullOrEmpty(data[j + 1][7])) enemies[j].CombineCost = 0;
                else enemies[j].CombineCost = int.Parse(data[j + 1][7]);
            }

            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, actionComplete));
    }
    [Button("Load Data")]
    private void LoadData()
    {
        enemies = new List<EnemyStat>();

        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=0&single=true&output=csv";
        System.Action<string> actionComplete = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            Debug.Log(n);
            for (int i = 1; i < n; i++)
            {
                EnemyStat item = new EnemyStat();
                ECharacterType tmp = Utils.ToEnum<ECharacterType>(data[i][0]);
                item.Type = tmp;

                item.Damage = int.Parse(data[i][1]);

                item.HP = int.Parse(data[i][2]);

                item.Rarity = int.Parse(data[i][3]);

                item.EnocunterATK = int.Parse(data[i][4]);

                item.EncounterHP = int.Parse(data[i][5]);

                item.ICON = Icon;

                item.Avatar = Avatar;

                if (string.IsNullOrEmpty(data[i][6])) item.CatchChance = 0;
                else item.CatchChance = int.Parse(data[i][6]);

                if (string.IsNullOrEmpty(data[i][7])) item.CombineCost = 0;
                else item.CombineCost = int.Parse(data[i][7]);

                enemies.Add(item);
            }
            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, actionComplete));
    }
#endif
}
[System.Serializable]
public class EnemyStat
{
    [FoldoutGroup("$Type")]
    public Sprite Avatar;
    [FoldoutGroup("$Type")]
    public SkeletonDataAsset ICON;
    [FoldoutGroup("$Type")]
    public ECharacterType Type;
    [FoldoutGroup("$Type")]
    public E_RangeAttack RangeAttack;
    [FoldoutGroup("$Type")]
    public float TimeEffect;
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
    [FoldoutGroup("$Type")]
    public Vector3 Offset;
    [FoldoutGroup("$Type")]
    public AudioClip SoundStart;
    [FoldoutGroup("$Type")]
    public AudioClip SoundStart2;
    [FoldoutGroup("$Type")]
    public AudioClip SoundEnd;
    [FoldoutGroup("$Type")]
    public float TimeSoundStart;
    [FoldoutGroup("$Type")]
    public float TimeSoundStart2;
}
public enum ECharacterType
{
    NONE = -1,
    Tuchis,
    Snicket,
    Nibble,
    Huzzah,
    Mizzen,
    Chockablock,
    Mishmash,
    Bumpkin,
    Tuber,
    Loony,
    Footloose,
    Quagga,
    Squabble,
    Grog,
    Octothorpe,
    Fiddlesticks,
    Sprinkles,
    Quiddle,
    Klops,
    Kama,
    Bozo,
    Fez,
    Jabberwocky,
    Moue,
    Toady,
    Bogey,
    Gewgaw,
    Fizgig,
    Guffaw,
    Scuttlebutt,
    Bugaboo,
    Pizzazz,
    Quaff,
    Bafflegab,
    Switcheroo,
    Babushka,
    Topple,
    Manifesto,
    Jambeau,
    Flink,
    Quibble,
    Bonanza,
    Quokka,
    Donkeyman,
    Hoopla,
    Fiddledeedee,
    Teepee,
    Snarky,
    Lummox,
    Lingo,
    Gimpy,
    Juju,
    Noodge,
    Yahoo,
    Spork,
    Meep,
    Blimp,
    Fartsdump,
    Quinzee,
    Shabang,
    Wonky,
    Yaffle,
    Chinchilla,
    Blob,
    Fribble,
    Gonzo,
    Hoodwink,
    Juggernaut,
    Braggart,
    Burgoo,
    Waddle,
    Foofaraw,
    Lollapalooza,
    Poppysmic,
    Hurlyburly,
    Maelstrom,
    Gubbins,
    Foolscap,
    Dunderhead,
    Razzmatazz,
    Kumquat,
    Chortle,
    Feeble,
    Snirt,
    Mumbojumbo,
    Mugwump,
    Slosh,
    Periwinkle,
    Oomph,
    Wheezy,
    Jawbreaker,
    Didymous,
    Kazoo,
    Yoyo,
    Rumpa,
    Hooey,
    Beanpole,
    Bumfuzzle,
    Jargogle,
    Gobbledygook,
    Gaberlunzie,
    Loggerhead,
    Toupee,
    Wabbit,
    Didgeridoo,
    Snooty,
    Kalita,
    Bungey,
    Caterwaul,
    Spackle,
    Finicky,
    Cankle,
    Glib,
    Yaws,
    Pollywog,
    Gibbo,
    Pogo,
    Dingy,
    Gizmo,
    Giglet
}
public enum E_RangeAttack
{
    NONE = -1,
    HitNear,
    HitNear2,
    HitFar,
}
public enum E_MoveState
{
    NONE = -1,
    JUMP,
    RUN,
    IDLE
}
