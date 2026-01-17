using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class PlayerBased : MonoBehaviour
{
    public float HP;
    public float Damage;
    public float Speed;

    public List<EnemyBased> L_Based;
    public Button PurChaseBtn;

    private EnemyBased m_EnemyBased;
    private Vector2 StartPos;
    private bool isTurn = false;
    private bool isTurnEnemy;


    private void Awake()
    {
        if (PurChaseBtn != null)
            PurChaseBtn.onClick.AddListener(OnPurChaseBtn);
    }
    private void Start()
    {
        StartPos = transform.position;
    }
    public void Attack()
    {
        m_EnemyBased = GetNearTarget(L_Based);
        if (!isTurn)
        {
            if (m_EnemyBased != null)
            {
                isTurn = true;
                transform.DOMove(m_EnemyBased.transform.position, Speed).SetSpeedBased(true).OnComplete(() =>
                {
                    transform.DOMove(StartPos, Speed).SetSpeedBased(true).OnComplete(() =>
                    {
                        isTurn = false;
                        isTurnEnemy = true;
                        PurChaseBtn.enabled = false;
                    });
                });
            }
        }
    }
    public bool GetTurnEnemyBased()
    {
        return isTurnEnemy;
    }
    public void SetTurnEnemuBased(bool isTurnEnemy)
    {
        this.isTurnEnemy = isTurnEnemy;
    }
    private EnemyBased GetNearTarget(List<EnemyBased> L_Based)
    {
        int flag = 0;
        if (L_Based.Count > 1)
        {
            float MinDis = Vector2.Distance(transform.position, L_Based[0].transform.position);
            for (int i = 0; i < L_Based.Count; i++)
            {
                float TempDis = Vector2.Distance(transform.position, L_Based[i].transform.position);
                if (MinDis > TempDis)
                {
                    MinDis = TempDis;
                    flag = i;
                }
            }
            return L_Based[flag];
        }
        else if (L_Based.Count == 1)
        {
            return L_Based[0];
        }
        return null;
    }
    void OnPurChaseBtn()
    {
        gameObject.SetActive(true);
    }
}
