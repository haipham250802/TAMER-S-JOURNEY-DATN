using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GroupEnemyIntro : MonoBehaviour
{
    public UI_Battle m_UIBattle;

    public EnemyIntroELement enemyIntroELement_01;
    public EnemyIntroELement enemyIntroELement_02;
    public EnemyIntroELement enemyIntroELement_03;
    public EnemyIntroELement enemyIntroELement_04;

    public int JumpPower;
    public int JumpCount;
    public float Duration;

    public int jumpPowerFlight;
    public int JumpCountFlight;
    public float DurationFlight;

    public Transform PosFlight;
    public Transform StartPos;


    public List<EnemyIntroELement> L_enemyIntroELement = new List<EnemyIntroELement>();
    public List<EnemyIntroELement> L_enemyIntroELementCheck = new List<EnemyIntroELement>();

    Vector3 PosEnd = new Vector3(800, -46);

    private void OnEnable()
    {
        enemyIntroELement_01.transform.localPosition = new Vector3(-67, -150,0);
        enemyIntroELement_02.transform.localPosition = new Vector3(82, -150,0);
        enemyIntroELement_03.transform.localPosition = new Vector3(231, -150,0);
        enemyIntroELement_04.transform.localPosition = new Vector3(372, -150,0);
    }
    public void ResetGroupIntroEnemy()
    {
        enemyIntroELement_01.Type = ECharacterType.NONE;
        enemyIntroELement_02.Type = ECharacterType.NONE;
        enemyIntroELement_03.Type = ECharacterType.NONE;
        enemyIntroELement_04.Type = ECharacterType.NONE;
    }
    public void ActiveIntroEnemy()
    {
        for (int i = 0; i < L_enemyIntroELement.Count; i++)
        {
            if (L_enemyIntroELement[i].Type != ECharacterType.NONE)
            {
                L_enemyIntroELement[i].gameObject.SetActive(true);
            }
        }
    }
    public void SetMoveCritter()
    {
        enemyIntroELement_01.Skeleton.AnimationState.SetAnimation(0, "Move", true);
        enemyIntroELement_02.Skeleton.AnimationState.SetAnimation(0, "Move", true);
        enemyIntroELement_03.Skeleton.AnimationState.SetAnimation(0, "Move", true);
        enemyIntroELement_04.Skeleton.AnimationState.SetAnimation(0, "Move", true);
    }
    public void LoadIntroEnemyBased()
    {
        ResetGroupIntroEnemy();
        Debug.Log("da load intro enemy based element");
        for (int i = 0; i < m_UIBattle.L_EnemyBaseElement.Count; i++)
        {
            switch (i)
            {
                case 0:
                    if (enemyIntroELement_01.Type == ECharacterType.NONE)
                    {
                        enemyIntroELement_01.Type = m_UIBattle.L_EnemyBaseElement[i].Type;
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(enemyIntroELement_01.Type);
                        enemyIntroELement_01.Skeleton.skeletonDataAsset = null;
                        enemyIntroELement_01.Skeleton.skeletonDataAsset = enemyStat.ICON;
                        enemyIntroELement_01.Skeleton.Initialize(true);
                        enemyIntroELement_01.Skeleton.AnimationState.SetAnimation(0, "Idle", true);
                    }
                    break;
                case 1:
                    if (enemyIntroELement_02.Type == ECharacterType.NONE)
                    {
                        enemyIntroELement_02.Type = m_UIBattle.L_EnemyBaseElement[i].Type;
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(enemyIntroELement_02.Type);
                        enemyIntroELement_02.Skeleton.skeletonDataAsset = null;
                        enemyIntroELement_02.Skeleton.skeletonDataAsset = enemyStat.ICON;
                        enemyIntroELement_02.Skeleton.Initialize(true);
                        enemyIntroELement_02.Skeleton.AnimationState.SetAnimation(0, "Idle", true);
                    }
                    break;
                case 2:
                    if (enemyIntroELement_03.Type == ECharacterType.NONE)
                    {
                        enemyIntroELement_03.Type = m_UIBattle.L_EnemyBaseElement[i].Type;
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(enemyIntroELement_03.Type);
                        enemyIntroELement_03.Skeleton.skeletonDataAsset = null;
                        enemyIntroELement_03.Skeleton.skeletonDataAsset = enemyStat.ICON;
                        enemyIntroELement_03.Skeleton.Initialize(true);
                        enemyIntroELement_03.Skeleton.AnimationState.SetAnimation(0, "Idle", true);
                    }
                    break;
                case 3:
                    if (enemyIntroELement_04.Type == ECharacterType.NONE)
                    {
                        enemyIntroELement_04.Type = m_UIBattle.L_EnemyBaseElement[i].Type;
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(enemyIntroELement_04.Type);
                        enemyIntroELement_04.Skeleton.skeletonDataAsset = null;
                        enemyIntroELement_04.Skeleton.skeletonDataAsset = enemyStat.ICON;
                        enemyIntroELement_04.Skeleton.Initialize(true);
                        enemyIntroELement_04.Skeleton.AnimationState.SetAnimation(0, "Idle", true);
                    }
                    break;
            }
        }
        HiddenEnemyIntro();
        ActiveIntroEnemy();
        JumpIntro();
    }

    public void HiddenEnemyIntro()
    {
        for (int i = 0; i < L_enemyIntroELement.Count; i++)
        {
            if (L_enemyIntroELement[i].Type == ECharacterType.NONE)
            {
                L_enemyIntroELement[i].gameObject.SetActive(false);
            }
        }
    }

    public void JumpIntro()
    {
        PosGroupToStart();
        
        if (enemyIntroELement_01.Type != ECharacterType.NONE)
        {
            enemyIntroELement_01.transform.DOJump(enemyIntroELement_01.PosJump.position, JumpPower, JumpCount, Duration).OnComplete(() =>
            {
                L_enemyIntroELementCheck.Add(enemyIntroELement_01);
                if (enemyIntroELement_02.Type != ECharacterType.NONE)
                {
                    enemyIntroELement_02.transform.DOJump(enemyIntroELement_02.PosJump.position, JumpPower, JumpCount, Duration).OnComplete(() =>
                    {
                        L_enemyIntroELementCheck.Add(enemyIntroELement_02);
                        if (enemyIntroELement_03.Type != ECharacterType.NONE)
                        {
                            enemyIntroELement_03.transform.DOJump(enemyIntroELement_03.PosJump.position, JumpPower, JumpCount, Duration).OnComplete(() =>
                            {
                                L_enemyIntroELementCheck.Add(enemyIntroELement_03);
                                if (enemyIntroELement_04.Type != ECharacterType.NONE)
                                {
                                    enemyIntroELement_04.transform.DOJump(enemyIntroELement_04.PosJump.position, JumpPower, JumpCount, Duration).OnComplete(() =>
                                    {
                                        L_enemyIntroELementCheck.Add(enemyIntroELement_04);
                                        Flip();
                                        transform.DOLocalJump(PosEnd, 120, 4, DurationFlight - 0.2f).OnComplete(() =>
                                        {
                                            Flip();
                                            m_UIBattle.enemyBased.GetComponent<EnemyBased>().JumpStartPos();
                                        });
                                    });
                                }
                                else
                                {
                                    Flip();
                                    transform.DOLocalJump(PosEnd, 120,4, DurationFlight - 0.2f).OnComplete(() =>
                                    {
                                        Flip();
                                        m_UIBattle.enemyBased.GetComponent<EnemyBased>().JumpStartPos();
                                    });
                                }
                            });
                        }
                        else
                        {
                            Flip();
                            transform.DOLocalJump(PosEnd, 120, 4, DurationFlight - 0.2f).OnComplete(() =>
                            {
                                Flip();
                                m_UIBattle.enemyBased.GetComponent<EnemyBased>().JumpStartPos();
                            });
                        }
                    });
                }
                else
                {
                    Flip();
                    transform.DOLocalJump(PosEnd, 120, 4, DurationFlight - 0.2f).OnComplete(() =>
                    {
                        Flip();
                        m_UIBattle.enemyBased.GetComponent<EnemyBased>().JumpStartPos();
                    });
                }
            });
        }

    }
    void PosGroupToStart()
    {
        transform.localPosition = StartPos.localPosition;
    }
    void Flip()
    {
        for (int i = 0; i < L_enemyIntroELement.Count; i++)
        {
            if (L_enemyIntroELement[i].Type != ECharacterType.NONE)
            {
                Vector3 theScale = L_enemyIntroELement[i].transform.localScale;
                theScale.x *= -1;
                L_enemyIntroELement[i].transform.localScale = theScale;
            }
        }
    }
}
