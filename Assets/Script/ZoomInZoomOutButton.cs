using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ZoomInZoomOutButton : MonoBehaviour
{
    public float MinValue;
    public float MaxValue;
    public float Duration;
    public Button btn;

    Tweener tweener1;
    Tweener tweener2;
    private void Awake()
    {
        btn.onClick.AddListener(OnClickButton);

    }
    private void OnEnable()
    {
        StartCoroutine(IE_delay());
    }
    IEnumerator IE_delay()
    {
        yield return null;

        tweener1.Play();
       
        ScaleButton(transform);

    }
    void killTween()
    {
        tweener1.Kill(true);
        tweener2.Kill(true);
    }   
    public void ScaleButton(Transform _transform)
    {
        tweener1 = _transform.DOScale(MaxValue, Duration).OnComplete(() =>
        {
            tweener1 = _transform.DOScale(MinValue, Duration).OnComplete(() =>
            {
                ScaleButton(_transform);
            });
        });
    }
    private void OnClickButton()
    {
        tweener1.Pause();
    }
  /*  private void OnDisable()
    {
        tweener1.Pause();
        tweener2.Pause();
    }*/
}
