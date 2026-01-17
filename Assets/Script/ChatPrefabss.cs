using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChatPrefabss : MonoBehaviour
{
    public Text Story;
    [System.Obsolete]
    private void OnEnable()
    {
        int rand = Random.RandomRange(0, CritterFollowController.Instance.Stories.Length);
        Story.text = CritterFollowController.Instance.Stories[rand].ToString();
    }
}
