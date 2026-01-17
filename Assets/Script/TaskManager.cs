using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TaskManager : MonoBehaviour
{
    public Text TaskTxt;

    public void SetTextTask(string value1, string maxValue)
    {
        TaskTxt.text = value1 + "/" + maxValue;
    }
    public void ResetTaskManager()
    {
        TaskTxt.text = 0 + "/" + 0;
    }
}
