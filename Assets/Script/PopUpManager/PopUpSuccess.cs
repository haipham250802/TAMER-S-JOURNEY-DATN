using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpSuccess : MonoBehaviour
{
    private void OnEnable()
    {
        UI_Home.Instance.m_UIMerge.OutButton.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        UI_Home.Instance.m_UIMerge.OutButton.gameObject.SetActive(true);
    }
}
