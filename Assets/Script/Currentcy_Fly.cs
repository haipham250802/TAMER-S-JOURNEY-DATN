using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
public class Currentcy_Fly : MonoBehaviour
{
    public GameObject CurrentcyPrefabs;
    public Transform Parent;
    public Transform EndPosMove;
    public int maxQuantity;

    public float MinSizeCurrentcy;
    public float MaxSizeCurrentcy;
    public float sizeCurrentcy;
    public float Range;

    public float MinTimeMoveCoin;
    public float MaxTimeMoveCoin;
    public float TimeDelay;

    public List<GameObject> CurrentcyList;
    public AnimationCurve animCurve;

    public GameObject Object = null;
    public System.Action A_CallBack;
    public System.Action A_CallBack2;

    public void ActiveCurrency(int index)
    {
        if (index < maxQuantity)        {
            var obj = Instantiate(CurrentcyPrefabs);
            obj.transform.SetParent(Parent.transform,false  );
            if (obj.GetComponent<Canvas>())
                obj.GetComponent<Canvas>().sortingLayerName = "UI";
            Vector3 pos = (Vector3)Random.insideUnitCircle * Range;
            obj.transform.localPosition = pos;
            obj.transform.localScale = Vector3.zero;
            sizeCurrentcy = Random.Range(MinSizeCurrentcy, MaxSizeCurrentcy);
            obj.transform.DOScale(sizeCurrentcy, 0.005f).OnStart(() =>
            {
                if (index == 1)
                {
                    AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectScaleCoin);
                }
            }).SetEase(animCurve).OnComplete(() =>
            {
                CurrentcyList.Add(obj);
                ActiveCurrency(index);
                if (index == maxQuantity)
                {
                    MoveCoin(0);
                }
            });
            index++;

        }
    }
    bool isTurnOnSound;
    bool isSound;
    bool isCallBack;
    public void MoveCoin(int index, GameObject obj = null)
    {
        float duration = Random.Range(MinTimeMoveCoin, MaxTimeMoveCoin);
        CurrentcyList[index].transform.DOMove(EndPosMove.position, duration).OnStart(() =>
        {
            if (!isSound)
            {
                AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectGetCoin);
                isSound = true;
            }
        })
            .SetDelay(TimeDelay).OnComplete(() =>
            {
                if(!isCallBack)
                {
                    A_CallBack?.Invoke();
                    isCallBack = true;
                }
                if (!isTurnOnSound)
                {
                    isTurnOnSound = true;
                }
                if (index == CurrentcyList.Count)
                {
                    A_CallBack2?.Invoke();
                    for (int i = 0; i < CurrentcyList.Count; i++)
                    {
                        Destroy(CurrentcyList[i].gameObject);
                    }
                    CurrentcyList.Clear();
                    isTurnOnSound = false;
                    isSound = false;
                    if (Object != null)
                        Object.SetActive(false);
                    isCallBack = false;
                }
            });
        index++;
        if (index <= CurrentcyList.Count)
        {
            MoveCoin(index);
        }
    }
}
