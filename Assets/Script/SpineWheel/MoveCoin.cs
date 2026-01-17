using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MoveCoin : Singleton<MoveCoin>
{
    public float MinszY;
    public float MaxszY;

    public float MinszX;
    public float MaxszX;

    public float timMin;
    public float timMax;
    public float time;

    public int num = 0;

    public Transform PosEndCoin;

    public List<GameObject> L_obj = new List<GameObject>();
    public void Action(GameObject CoinObjectPrefabs, int numCoin, Vector3 startPos, Transform Parent, System.Action callback = null)
    {
        num = 0;
        L_obj.Clear();
        for (int i = 0; i < numCoin; i++)
        {
            if (i <= numCoin)
            {
                CoinObjectPrefabs = Instantiate(CoinObjectPrefabs, Parent);
                CoinObjectPrefabs.transform.position = startPos;
                CoinObjectPrefabs.GetComponent<Image>().SetNativeSize();

                L_obj.Add(CoinObjectPrefabs);
            }
            if (i + 1 == numCoin)
            {
                for (int j = 0; j < L_obj.Count; j++)
                {
                    float x = Random.Range(MinszX, MaxszX);
                    float y = Random.Range(MinszY, MaxszY);

                    L_obj[j].transform.DOMove(L_obj[j].transform.position + new Vector3(x, y, 0), 0.1f).OnComplete(() =>
                    {
                        num++;
                    });
                }
                StartCoroutine(IE_Delay(callback));
            }
        }
    }


    IEnumerator IE_Delay(System.Action callback)
    {
        yield return new WaitForSeconds(0.3f);

        if (num == L_obj.Count)
        {
            for (int i = 0; i < L_obj.Count; i++)
            {
                float time = Random.Range(timMin, timMax);
                L_obj[i].transform.DOMove(PosEndCoin.position, time).OnStart(() =>
                {
                    AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectGetCoin);
                }).OnComplete(() =>
                {
                    UI_Home.Instance.m_UICoin.ImageCoin.transform.DOScale(1.1f, 0.1f);
                });
                if (i + 1 == L_obj.Count)
                {
                    Debug.Log(i + "/" + L_obj.Count);

                    StartCoroutine(IE_destroy(1f, callback));
                }
            }
        }
    }
    IEnumerator IE_destroy(float time, System.Action callback = null)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < L_obj.Count; i++)
        {
            Destroy(L_obj[i].gameObject);
            if (i + 1 == L_obj.Count)
            {
                callback?.Invoke();
                L_obj.Clear();
            }
        }
    }
}
