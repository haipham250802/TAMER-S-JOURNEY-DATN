using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PackZoneButton : MonoBehaviour
{
    public GameObject IconNoti;
    private void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnCLickPurchaseButton);
        NotifiController.Instance.NotiPackZone += SetNoti;
    }
    void OnCLickPurchaseButton()
    {
        popUpManager.Instance.m_PopUpPackZone.gameObject.SetActive(true);
    }
    public void SetNoti(bool check)
    {
        if(IconNoti != null)
        {
            IconNoti.SetActive(check);
        }
    }
}
