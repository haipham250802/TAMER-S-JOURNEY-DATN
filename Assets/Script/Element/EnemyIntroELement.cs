using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;
public class EnemyIntroELement : MonoBehaviour
{
    public ECharacterType Type;
    public SkeletonGraphic Skeleton;
    public Transform PosJump;
    public Transform PosDown;

    private void OnEnable()
    {
        StartCoroutine(IE_DelayActive());
    }
    IEnumerator IE_DelayActive()
    {
        yield return null;
        transform.localScale = new Vector3(0.75f, 0.75f, 0);
    }
}
