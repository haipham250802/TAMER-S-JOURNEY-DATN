using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class PieceElement : MonoBehaviour
{
    public TypeSlotPickerWheel typeSlot;
    public TypeRewardPickerWheel typeRewards;
    public int ID;
    public Image Icon;
    public Text QuantityTxt;
    public int Quantity;
    public int ratechance;

    private void Awake()
    {

    }
    private void Start()
    {
        InitPieceElement();

    }
#if UNITY_EDITOR
    [Button("Load")]
    void Load()
    {
        Debug.Log(Controller.Instance.dataPickerWheel.wheelPieces.Count);
    }
#endif

    void InitPieceElement()
    {
        for (int i = 0; i < Controller.Instance.dataPickerWheel.wheelPieces.Count; i++)
        {
            if (typeSlot == Controller.Instance.dataPickerWheel.wheelPieces[i].typeSlotPickerWheel)
            {
                Icon.sprite = Controller.Instance.dataPickerWheel.wheelPieces[i].Icon;
                if (typeSlot == TypeSlotPickerWheel.SL_6 || typeSlot == TypeSlotPickerWheel.SL_7)
                {
                    Icon.GetComponent<RectTransform>().sizeDelta = new Vector2(80, 80);
                }
               else
                {
                    Icon.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
                }
                QuantityTxt.text = Controller.Instance.dataPickerWheel.wheelPieces[i].Quantity.ToString();
                Quantity = Controller.Instance.dataPickerWheel.wheelPieces[i].Quantity;
                typeRewards = Controller.Instance.dataPickerWheel.wheelPieces[i].typeRewardsPack;
                ratechance = Controller.Instance.dataPickerWheel.wheelPieces[i].RateChance;
            }
        }
    }
}
