using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;
public class NewCoinFly : Singleton<NewCoinFly>
{
    public GameObject CoinPrefabs;
    public GameObject Parent;

    public Transform StartPos;
    public Transform PosEndCoin;
    public int numCoin;

    public int MaxY;
    public int MinY;

    public int MaxX;
    public int MinX;

    public float MinSize;
    public float MaxSize;

    public float MinTime;
    public float MaxTime;

    public AnimationCurve AnimCurve;
    public AnimationCurve AnimCurveMoveCoin;
    public List<GameObject> L_coin = new List<GameObject>();
    public int index;

#if UNITY_EDITOR
    [Button("Test")]
    void Test()
    {
        L_coin.Clear();
        ACtionCoin(0);
    }
#endif
    public void ACtionCoin(int index ,System.Action callback = null)
    {
        if (index < numCoin)
        {
            int RandX = Random.Range(MinX, MaxX);
            int RandY = Random.Range(MinY, MaxY);
            Vector3 randpos = new Vector3(RandX, RandY, 0);

            var obj = Instantiate(CoinPrefabs);
            obj.transform.position = StartPos.position;
            obj.transform.SetParent(Parent.transform);
            obj.transform.position = new Vector3(RandX, RandY, 0);
            obj.transform.localScale = new Vector3(MinSize, MinSize, MinSize);
            obj.GetComponent<Image>().SetNativeSize();
            obj.transform.DOScale(MaxSize, 0.05f).SetEase(AnimCurve).SetDelay(0.0003f).OnComplete(() =>
            {
                L_coin.Add(obj);
                ACtionCoin(index);
                if (index == numCoin)
                {
                    MoveCoin(0,callback);
                }
            });
            index++;
        }
    }
    public void MoveCoin(int index , System.Action callback = null)
    {
        float duration = Random.Range(MinTime, MaxTime);
        L_coin[index].transform.DOMove(PosEndCoin.position, duration).SetDelay(0.005f).SetEase(AnimCurveMoveCoin).OnComplete(() =>
        {
            if (index == L_coin.Count)
            {
                callback?.Invoke();
                for (int i = 0; i < L_coin.Count; i++)
                {
                    Destroy(L_coin[i].gameObject);
                }
            }
        });
        index++;
        Debug.Log(index);
        if (index  <= L_coin.Count)
        {
            MoveCoin(index);
        }
    }

}
