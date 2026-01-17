using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterFollow : MonoBehaviour
{
    public Transform GroupCritterFollow;
    public List<CritterFollowElement> L_CritterFollowElement = new List<CritterFollowElement>();

    public void Start()
    {
        gameObject.SetActive(false);
        var arr = GetComponentsInChildren<CritterFollowElement>();

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != null)
            {
                L_CritterFollowElement.Add(arr[i].GetComponent<CritterFollowElement>());
            }
        }
    }
}
