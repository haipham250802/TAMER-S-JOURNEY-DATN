using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestI2 : MonoBehaviour
{
    private void Start()
    {
        string a = I2.Loc.LocalizationManager.GetTranslation("ALO_123");
        Debug.Log(a);
    }
}
