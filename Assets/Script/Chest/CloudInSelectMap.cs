using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
public class CloudInSelectMap : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;

    public Vector3 offset_1;
    public Vector3 offset_2;

    public float time;

    private void Start()
    {
        startPos = transform.position + offset_1;
        endPos = transform.position + offset_2;
        Action(transform);
    }

#if UNITY_EDITOR
    [Button("Test")]
    void Test()
    {
        
    }
#endif

    void Action(Transform transfom)
    {
        float rand = Random.Range((time - 1f), (time + 1f));

        transfom.transform.DOMove(startPos, rand).SetEase(Ease.Linear).OnComplete(() =>
        {
            transform.DOMove(endPos, rand).SetEase(Ease.Linear).OnComplete(() =>
            {
                Action(transfom);
            });
        });
    }
}
