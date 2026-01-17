using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AllIn1SpriteShader;
using DG.Tweening;
public class ShaderButton : MonoBehaviour
{
    private void OnEnable()
    {
        Material mat = GetComponent<Image>().material;
        mat.EnableKeyword("SHINE_ON");
        DOTween.To(() => 0f, _ =>
        {
            mat.SetFloat("_ShineLocation", _);
        }, 1f, 1.5f).OnComplete(() =>
        {
            StartCoroutine(IE_delay());
        });
    }
    IEnumerator IE_delay()
    {
        yield return new WaitForSeconds(3);

        Material mat = GetComponent<Image>().material;
        mat.EnableKeyword("SHINE_ON");
        DOTween.To(() => 0f, _ =>
        {
            mat.SetFloat("_ShineLocation", _);
        }, 1f, 1f);
        StartCoroutine(IE_delay());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        DOTween.KillAll();
    }
}
