using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITeamNew : MonoBehaviour
{
    public UITeamCellRow itemPrefabs;
    public Transform parentTransform;

    private void OnEnable()
    {
        Init();
    }
    private void Init()
    {
        var data = new List<ElementData>();
        foreach (var i in DataPlayer.GetDictionary())
        {
            for (int j = 0; j < i.Value.Count; j++)
            {
                data.Add(i.Value[j]);
            }
        }
        for (int i = 0; i < data.Count; i++)
        {
            //UITeamCellRow _item = Instantiate(itemPrefabs, parentTransform);
            //_item.Init(data[i]);
        }
    }
}
