using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CritterIntroMerge : MonoBehaviour
{
    public Transform NormalPos;
    public Transform PosMerge;
    public int Speed;

    public void MoveToPosMerge()
    {
        transform.DOMove(PosMerge.position, Speed).SetUpdate(true).SetSpeedBased(true);
    }
    public void ResetPos()
    {
        transform.position = NormalPos.position;
    }
}
