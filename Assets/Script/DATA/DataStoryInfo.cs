using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(fileName = "New Story Info", menuName = "DataGame/Create Story Info")]
public class DataStoryInfo : ScriptableObject
{
    [SerializeField]
    public List<StoryInfo> L_StoryInfor = new List<StoryInfo>();
    public StoryInfo GetStoryInfo(ECharacterType type)
    {
        if (type != ECharacterType.NONE)
            return L_StoryInfor[(int)type];
        return null;
    }
#if UNITY_EDITOR    
    [Button("LOAD DATA STORY")]
    void LoadDataStory()
    {
        L_StoryInfor = new List<StoryInfo>();
        string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vRqNXw_muJBODuvPIYMIHKXa8-cTgBf7kAlXv0cItp8CLbzIHL_K4y5uAcVdOAZF3P6qLlnP-fHPIe4/pub?gid=641255881&single=true&output=csv";
        System.Action<string> actionComplete = new System.Action<string>((string str) =>
        {
            var data = CSVReader.ReadCSV(str);
            int n = data.Count;
            Debug.Log(n);
            for (int i = 2; i < n; i++)
            {
                StoryInfo info = new StoryInfo();
                info.characterType = Utils.ToEnum<ECharacterType>(data[i][2]);
                info.KEY = data[i][2];
                info.GetStory();
                L_StoryInfor.Add(info);
            }
            UnityEditor.EditorUtility.SetDirty(this);
        });
        EditorCoroutine.start(Utils.IELoadData(url, actionComplete));
    }
#endif
}
[System.Serializable]
public class StoryInfo
{
    public ECharacterType characterType;
    public string KEY;
    public string Story;
    //  [SerializeField] public string _story;
    public string GetStory()
    {
        Story = I2.Loc.LocalizationManager.GetTranslation(KEY);
        return Story;
    }
    /*  public string Story
      {
          get { return I2.Loc.LocalizationManager.GetTranslation(Story); }
          set { _story = value; }
      }*/
}


