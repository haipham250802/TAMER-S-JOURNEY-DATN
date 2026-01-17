using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dragon.SDK;

public class UIConsentIS : MonoBehaviour
{
    private const string LinkPrivate = "https://www.privacypolicies.com/privacy/view/7J0DNy";

    public void OnClickLink()
    {
        Application.OpenURL(LinkPrivate);
    }
    public void OnClickYes()
    {
        SDKDGManager.Instance.SetShownUIConsent(true);
        SDKDGManager.Instance.SetConsent(true);
        SDKDGManager.Instance.AdsManager.InitInfo();
        gameObject.SetActive(false);
    }
    public void OnClickNo()
    {
        SDKDGManager.Instance.SetShownUIConsent(true);
        SDKDGManager.Instance.SetConsent(false);
        SDKDGManager.Instance.AdsManager.InitInfo();
        gameObject.SetActive(false);
    }
}
