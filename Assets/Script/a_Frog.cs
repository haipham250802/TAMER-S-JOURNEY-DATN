using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using DG.Tweening;
public class a_Frog : MonoBehaviour
{
    ENewEnemyState stateEnemy;
    private Vector2 startPos;
    private Vector2 PatrolPos;

    public int ID;
    public int QuantityCoinNoAds;
    public int QuantityCoinWithAds;
    public SkeletonAnimation skeleton;

    public float SpeedPatrol;
    public float SpeedChasing;
    public float RangePatrol = 2.0f;
    public float RangeChasing;
    public float StartSpeedPatrol;

    public bool isCanPatrol;
    bool isCanChasing;
    bool isPatrol;
    public bool IsFacingRight;

    public GameObject FrogChildGroup;

    Tweener tweener;
    public MoveEnemy m_MoveEnemy;

    public float RangeChase, RangeOutChase;

    private Coroutine coroutine;
    public void KillTween()
    {
        tweener.Kill();
    }
    private void Start()
    {
        isCanPatrol = false;
        StartSpeedPatrol = SpeedPatrol;
        IsFacingRight = false;
        skeleton.state.SetAnimation(0, "Move", true);
        startPos = transform.position;
        PatrolPos = startPos;
        stateEnemy = ENewEnemyState.Patrol;
        QuantityCoinNoAds *= BagManager.Instance.m_RuleController. CurLevel;
        QuantityCoinWithAds *= BagManager.Instance.m_RuleController.CurLevel;
    }

    private void Update()
    {

    }
    bool isdelay;
    private void LateUpdate()
    {
        switch (stateEnemy)
        {
            case ENewEnemyState.Idle:
                break;
            case ENewEnemyState.Patrol:
                if (!isCanPatrol)
                {
                    isCanPatrol = true;
                    StatePatrol();
                }
                if (isCanPatrol && !isdelay)
                {
                    coroutine = StartCoroutine(IE_DelayNewPos());
                    isdelay = true;
                }
                break;
                /* if (IsCheckPlayerChase(5))
                 {
                     ChasePlayer();
                 }
                 break;
             case ENewEnemyState.Chase:
                 if (IsCheckPlayerOutChase(5.1f))
                 {
                     StatePatrol();
                 }
                 break;*/
        }
    }
    /*    public bool IsCheckPlayerChase(float RangeChase)
        {
            if (Vector2.Distance(transform.position, Controller.Instance.m_Player.transform.position) <= RangeChase)
            {
                return true;
            }
            return false;
        }
        public bool IsCheckPlayerOutChase(float RangeOutChase)
        {
            if (Vector2.Distance(transform.position, Controller.Instance.m_Player.transform.position) >= RangeOutChase)
            {
                return true;
            }
            return false;
        }

        public void ChasePlayer()
        {
            stateEnemy = ENewEnemyState.Chase;
            m_MoveEnemy.StopMove();
            Vector2 Pos = player.Instance.gameObject.transform.position;
            FlipSprite(Pos);
            m_MoveEnemy.Go(Pos);
        }*/

    Vector2 LastPos;
    bool isDoneNewPos;
    public Vector2 PosPatrol;
    public void StatePatrol()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        stateEnemy = ENewEnemyState.Patrol;
        m_MoveEnemy.StopMove();
        PosPatrol = isDoneNewPos ? startPos : transform.position + (Vector3)Random.insideUnitCircle * 4;

        Vector2 CurrentPos = transform.position;
        LastPos = CurrentPos;
        m_MoveEnemy.Go(PosPatrol, (bool done) =>
        {
            if (done)
            {
                isDoneNewPos = !isDoneNewPos;
                isCanPatrol = false;
                isdelay = false;
            }
        });
    }


    IEnumerator IE_DelayNewPos()
    {
        yield return new WaitForSeconds(3f);
        isCanPatrol = false;
    }

    public void FlipSprite(Vector2 Pos)
    {
        float PointXvalue = Pos.x;
        float FrogXvalue = this.gameObject.transform.position.x;

        if (PointXvalue > FrogXvalue && !IsFacingRight)
        {
            Flip();
        }
        else if (PointXvalue < FrogXvalue && IsFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 TheScale = transform.localScale;
        TheScale.x *= -1;
        transform.localScale = TheScale;
    }
    private void OnDisable()
    {
        isPatrol = false;
    }
}
public enum ENewEnemyState
{
    Idle,
    Patrol,
    Chase,
    Attack,
}