using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AnimPopUp : MonoBehaviour
{
    private void Awake()
    {
        gameObject.transform.localScale = new Vector3((transform.localScale.x / 1.5f), (transform.localScale.x / 1.5f), (transform.localScale.x / 1.5f));
    }
    private void OnEnable()
    {
        StartCoroutine(IE_delay());
    }
    IEnumerator IE_delay()
    {
        yield return null;
        Scale();
    }
    void Scale()
    {
        transform.DOScale(new Vector3((transform.localScale.x * 1.5f), (transform.localScale.x * 1.5f), (transform.localScale.x * 1.5f)), 0.2f);
    }
    private void OnDisable()
    {
        gameObject.transform.localScale = new Vector3((transform.localScale.x / 1.5f), (transform.localScale.x / 1.5f), (transform.localScale.x / 1.5f));
        DOTween.KillAll();
    }
}
