using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ToastManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Image BG;
    public Text CantFightTxt;

    public GameObject Toast;

    public Transform ToatsPosStart;
    public Transform PosMoveUpToast;
    Tweener Tween;

    public int Speed;
    public float Duration;


    public void Kill()
    {
        Tween.Kill();
    }

    public void MoveUpToast()
    {
        Toast.transform.position = ToatsPosStart.position;
        Tween = Toast.transform.DOMove(PosMoveUpToast.position, 0.5f).OnStart(() =>
        {
            BG.canvasRenderer.SetAlpha(1f);
            BG.CrossFadeAlpha(0, Duration, true);

            CantFightTxt.canvasRenderer.SetAlpha(1f);
            CantFightTxt.CrossFadeAlpha(0f, Duration, true);
        }).OnComplete(() =>
        {
            MoveUpToast();
        });
    }
    public void MoveToastDontRepeat()
    {
        Toast.SetActive(true);
        Toast.transform.position = ToatsPosStart.position;
        Tween = Toast.transform.DOMove(PosMoveUpToast.position, Speed + 30).SetSpeedBased(true).OnStart(() =>
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
    public void ResetToast()
    {
        Kill();
        Toast.transform.position = ToatsPosStart.position;
    }
    public void SetText(string txt)
    {
        CantFightTxt.text = txt;
    }
}
