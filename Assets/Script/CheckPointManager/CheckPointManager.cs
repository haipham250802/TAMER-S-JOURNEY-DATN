using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public List<Transform> L_CheckPoint = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
      
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Debug.Log("Pause");
            DataPlayer.GetListCheckPointPos().Clear();
            for (int i = 0; i < L_CheckPoint.Count; i++)
            {
                DataPlayer.AddCheckPointPosToList(L_CheckPoint[i]);
            }
        }
    }
    private void OnApplicationQuit()
    {
        DataPlayer.GetListCheckPointPos().Clear();
        for (int i = 0; i < L_CheckPoint.Count; i++)
        {
            DataPlayer.AddCheckPointPosToList(L_CheckPoint[i]);
        }
    }
}
