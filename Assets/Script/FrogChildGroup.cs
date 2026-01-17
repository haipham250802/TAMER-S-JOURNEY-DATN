using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
public class FrogChildGroup : MonoBehaviour
{
    public MoveEnemy m_MoveEnemy;
    public Transform target;

    public List<SkeletonAnimation> L_FrogSkins;

    public bool isCheckStateMove;
    public bool isCheckStateIdle;
    public bool IsFacingRight;

    public void Start()
    {
    }
    private void Update()
    {
        MoveFrogChildGroup();
    }
    public void MoveFrogChildGroup()
    {
        if(isCheckStateIdle && !isCheckStateMove)
        {
            isCheckStateMove = false;
            isCheckStateIdle = false;
        }
       // FlipToTarget(target.position);
        m_MoveEnemy.Go(target.position, (bool isDone) =>
        {
            if (isDone)
            {
                if(!isCheckStateIdle)
                {
                    StateIdleFrog();
                    isCheckStateMove = false;
                    isCheckStateIdle = true;
                }
            }
        });
        if (!isCheckStateMove)
        {
            StateMoveFrog();
            isCheckStateMove = true;
            isCheckStateIdle = false;
        }
    }
    private void StateMoveFrog()
    {
        for (int i = 0; i < L_FrogSkins.Count; i++)
        {
            if (L_FrogSkins[i] != null && L_FrogSkins[i].gameObject.activeInHierarchy)
            {
                L_FrogSkins[i].state.SetAnimation(0, "Move", true);
            }
        }
    }
    private void StateIdleFrog()
    {
        for (int i = 0; i < L_FrogSkins.Count; i++)
        {
            if (L_FrogSkins[i] != null && L_FrogSkins[i].gameObject.activeInHierarchy)
            {
                L_FrogSkins[i].state.SetAnimation(0, "Idle", true);
            }
        }
    }
    public void FlipToTarget(Vector2 Target)
    {
        float PointXvalue = Target.x;
        float FrogXvalue = gameObject.transform.position.x;

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
}
