using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreenObj : MonoBehaviour
{
    public bool isHiddenChangeScreenDone = false;
    public void HiddenChangeScreen()
    {
        this.gameObject.SetActive(false);
    }
}
