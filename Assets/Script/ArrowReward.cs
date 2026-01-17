using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ArrowReward : MonoBehaviour
{
    public GameObject ArrowRed;

    public Transform StartPos;
    public Transform EndPos;

    public Text CoinInButtonClaimTxt;
    public float time;

    public PopUpCatchDone popUpcatchDone;
    public MachineReward machineReward;

    public int CoinClaim;
    Tween tweener;

    public AnimationCurve animCurve;
    private void OnEnable()
    {
        ActiveArrowRed();
    }
    private void Update()
    {
    }
    bool ischeck;
    public void ArrowMove(Transform transform)
    {
        if (!ischeck)
        {

        }
        tweener = transform.DOMove(EndPos.position, time)
            .OnStart(() =>
            {
                // AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music, AudioManager.instance.SoundEffectRewardBar); ;
            })
            .OnUpdate(() =>
        {
            CalculatorCoin(CoinInButtonClaimTxt, popUpcatchDone.CoinEarn);
        }).SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            tweener = transform.DOMove(StartPos.position, time)
            .OnUpdate(() =>
            {
                CalculatorCoin(CoinInButtonClaimTxt, popUpcatchDone.CoinEarn);
            }).SetEase(Ease.Linear).OnComplete(() =>
            {
                ArrowMove(transform);
            });
        });
    }
    public void StopArrow()
    {
        Debug.Log("da bam");
        tweener.Pause();
    }
    public void CalculatorCoin(Text txt, int value)
    {
        float x = this.transform.localPosition.x;
        if (x > -300 && x < -165)
        {
            CoinClaim = (value * 2);
            txt.text = CoinClaim.ToString();
        }
        if (x > -165 && x < -45)
        {
            CoinClaim = (value * 3);
            txt.text = CoinClaim.ToString();
        }
        if (x > -45 && x < 34)
        {
            CoinClaim = (value * 5);
            txt.text = CoinClaim.ToString();
        }
        if (x > 34 && x < 160)
        {
            CoinClaim = (value * 3);
            txt.text = CoinClaim.ToString();
        }
        if (x > 160 && x < 300)
        {
            CoinClaim = (value * 2);
            txt.text = CoinClaim.ToString();
        }

    }
    public void KillTween()
    {
        tweener.Kill(true);
    }
    public void ActiveArrowRed()
    {
        transform.localPosition = new Vector3(-300, transform.localPosition.y, 0);
        ArrowMove(transform);
    }
}
