using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "New Data Picker Wheel", menuName = "Create Data Picker Wheel")]
public class DataPickerWheel : ScriptableObject
{
    [SerializeField]
    public List<WheelPiece> wheelPieces = new List<WheelPiece>();

    public Sprite SP_Coin;
    public Sprite SP_Gem;
    public Sprite SP_ChestEpic;

#if UNITY_EDITOR
    [Button("Load")]
    void Load()
    {
        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=282640740&single=true&output=csv";
        System.Action<string> ActionComplete = new System.Action<string>((string str) =>
        {
            wheelPieces = new List<WheelPiece>();

            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            for (int i = 2; i < n; i++)
            {
                WheelPiece wheelPiece = new WheelPiece();
                wheelPiece.typeSlotPickerWheel = Utils.ToEnum<TypeSlotPickerWheel>(data[i][0]);
                wheelPiece.typeRewardsPack = Utils.ToEnum<TypeRewardPickerWheel>(data[i][1]);
                wheelPiece.Quantity = int.Parse(data[i][2]);
                wheelPiece.RateChance = int.Parse(data[i][3]);

                switch (wheelPiece.typeRewardsPack)
                {
                    case TypeRewardPickerWheel.Coin:
                        wheelPiece.Icon = SP_Coin;
                        break;
                    case TypeRewardPickerWheel.Gem:
                        wheelPiece.Icon = SP_Gem;
                        break;
                    case TypeRewardPickerWheel.Chest_Epic:
                        wheelPiece.Icon = SP_ChestEpic;
                        break;
                }

                wheelPieces.Add(wheelPiece);
            }
            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, ActionComplete));
    }
#endif
}


[System.Serializable]
public class WheelPiece
{
    [FoldoutGroup("WheelPieceELement")]
    public TypeSlotPickerWheel typeSlotPickerWheel;
    [FoldoutGroup("WheelPieceELement")]
    public TypeRewardPickerWheel typeRewardsPack;
    [FoldoutGroup("WheelPieceELement")]
    public int RateChance;
    [FoldoutGroup("WheelPieceELement")]
    public int Quantity;
    [FoldoutGroup("WheelPieceELement")]
    public Sprite Icon;
}
[System.Serializable]

public enum TypeSlotPickerWheel
{
    SL_1,
    SL_2,
    SL_3,
    SL_4,
    SL_5,
    SL_6,
    SL_7,
    SL_8
}
[System.Serializable]

public enum TypeRewardPickerWheel
{
    Coin,
    Gem,
    Chest_Epic
}
