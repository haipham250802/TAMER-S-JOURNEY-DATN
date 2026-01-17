using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class MoveUpToastSprite : MonoBehaviour
{
    public Transform ToatsPosStart;
    public Transform PosMoveUpToast;

    public Image BG;
    public Text CantFightTxt;

    public float Duration;
    public GameObject Toast;

    Vector2 StartPos;
    Tweener Tween;

    private void Start()
    {
        StartPos = transform.position;
    }
    public void Kill()
    {
        Tween.Kill();
    }
    public void MoveToastDontRepeat()
    {
        Toast.SetActive(true);
        Toast.transform.position = ToatsPosStart.position;
        Tween = Toast.transform.DOMove(PosMoveUpToast.position, Duration).OnStart(() =>
        {
            BG.canvasRenderer.SetAlpha(0f);
            BG.CrossFadeAlpha(1, Duration, true);

            CantFightTxt.canvasRenderer.SetAlpha(0f);
            CantFightTxt.CrossFadeAlpha(1f, Duration, true);
        }).OnComplete(() =>
        {
            if (this.gameObject.activeInHierarchy)
                StartCoroutine(IE_HiddenToast());
        });
    }
    IEnumerator IE_HiddenToast()
    {
        yield return new WaitForSeconds(2f);
        Toast.SetActive(false);
    }
}
