using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class ListCritterManager : MonoBehaviour
{
    public List<ECharacterType> characterTypesLV1 ;
    public List<ECharacterType> characterTypesLV2;
    public List<ECharacterType> characterTypesLV3;
    public List<ECharacterType> characterTypesLV4;
    public List<ECharacterType> characterTypesLV5;
    public List<ECharacterType> characterTypesLV6;
    public List<ECharacterType> characterTypesLV7;
    public List<ECharacterType> characterTypesLV8;
    public List<ECharacterType> characterTypesLV9;
    public List<ECharacterType> characterTypesLV10;
    public List<ECharacterType> characterTypesLV11;


#if UNITY_EDITOR
    [Button("Load data")]
    private void LoadData()
    {
        foreach (var i in DataPlayer.GetDictionary())
        {
            for (int j = 0; j < i.Value.Count; j++)
            {
                if (i.Value[j].Rarity == 4)
                {
                    characterTypesLV4.Add(i.Value[j].Type);

                }
            }
        }
      /*  for (int i = 0; i < Controller.Instance.enemyData.enemies.Count; i++)
        {
            if(Controller.Instance.enemyData.enemies[i].Rarity == 4)
            {
                characterTypesLV4.Add(Controller.Instance.enemyData.enemies[i].Type)
            }
        }*/
    }
#endif
}
