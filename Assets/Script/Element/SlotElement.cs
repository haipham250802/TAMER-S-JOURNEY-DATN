using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SlotElement : MonoBehaviour
{
    public Transform Slot_01;
    public Transform Slot_02;

    public List<Element> ElementList_01;
    public List<Element> ElementList_02;

    private bool isEmptySlot_01;
    private bool isEmptySlot_02;

    public bool IsEmptySlot_01 { get => isEmptySlot_01; set => isEmptySlot_01 = value; }
    public bool IsEmptySlot_02 { get => isEmptySlot_02; set => isEmptySlot_02 = value; }

    public Vector2 GetPosSlot1()
    {
        return Slot_01.position;
    }
    public Vector2 GetPosSlot2()
    {
        return Slot_02.position;
    }
}
