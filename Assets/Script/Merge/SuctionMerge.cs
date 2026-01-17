using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SuctionMerge : MonoBehaviour
{
    public Transform NormalPos;
    public Transform PosMerge01;
    public Transform PosMerge02;
    public Transform PosMerge03;
    public GameObject Shadow;

    public CritterIntroMerge m_CritterIntroMerge_01;
    public CritterIntroMerge m_CritterIntroMerge_02;

    public UI_Merge m_UIMerge;

    public int JumPower;
    public int JumpCount;
    public float Duration;
    public int Speed;

    public bool IsMoveDone = false;
 
    public void SuctionMoveToPosMerge()
    {
        Debug.Log("da bay");
        transform.DOMove(PosMerge01.position, Speed).SetSpeedBased(true).OnStart(() =>
        {
            Shadow.gameObject.SetActive(true);
            Vector3 theScale = new Vector3(3, 4, 0);
            Shadow.transform.DOScale(theScale, Duration + 1.5f).SetUpdate(true);
        }).SetUpdate(true).OnComplete(() =>
        {
            transform.DOMove(PosMerge02.position, Speed * 5).SetSpeedBased(true).SetUpdate(true).OnComplete(() =>
            {
                transform.DOJump(PosMerge03.position, JumPower, JumpCount, Duration).SetUpdate(true).OnComplete(() =>
                {
                    m_CritterIntroMerge_01.transform.DOMove(PosMerge03.position, Speed).SetUpdate(true).SetSpeedBased(true);
                    m_CritterIntroMerge_02.transform.DOMove(PosMerge03.position, Speed).SetUpdate(true).SetSpeedBased(true).OnComplete(() =>
                    {
                        m_UIMerge.SubCoin();
                      //  m_UIMerge.Merge();
                    });
                });
            });
        });
        Debug.Log("da ha xuong");
    }
    public void ResetPos()
    {
        transform.position = NormalPos.position;
        m_CritterIntroMerge_01.ResetPos();
        m_CritterIntroMerge_02.ResetPos();
        Shadow.GetComponent<RectTransform>().localScale = new Vector3(2, 3 , 0);
    }
    public void CritterMoveToPos()
    {
        m_CritterIntroMerge_01.MoveToPosMerge();
        m_CritterIntroMerge_02.MoveToPosMerge();
    }
}
