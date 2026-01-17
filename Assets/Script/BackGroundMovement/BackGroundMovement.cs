using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BackGroundMovement : MonoBehaviour
{
    bool IsCanMove;

    public Transform StartPos;
    public Transform PosInstantiate;
    public Transform EndPos;
    public float Duration;

    private void Update()
    {
        if (!IsCanMove)
        {
            transform.DOMove(EndPos.position, Duration).OnComplete(() =>
            {

            });
        }
    }
    private void Start()
    {
        
    }
}
