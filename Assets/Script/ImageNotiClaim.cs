using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ImageNotiClaim : MonoBehaviour
{
    private void OnEnable()
    {
        MoveImg(gameObject.transform);
    }
    void MoveImg(Transform _tranform)
    {
        _tranform.DOLocalMove(new Vector3(_tranform.localPosition.x, _tranform.localPosition.y - 5, 0), 0.2f).OnComplete(() =>
        {
            _tranform.DOLocalMove(new Vector3(_tranform.localPosition.x, _tranform.localPosition.y + 5, 0), 0.2f).OnComplete(() =>
            {
                MoveImg(_tranform);
            });
        });
    }
    private void OnDisable()
    {
       // DOTween.KillAll();
    }
}
