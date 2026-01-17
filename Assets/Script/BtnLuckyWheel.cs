using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnLuckyWheel : MonoBehaviour
{
    public GameObject notiIcon;
    // Start is called before the first frame update
    private void Start()
    {
        NotifiController.Instance.NotiWheel += SetNoti;
    }
   public void SetNoti(bool check)
    {
        if(notiIcon != null)
            notiIcon.SetActive(check);
    }
}
