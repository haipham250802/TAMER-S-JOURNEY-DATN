using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpComingSoon : MonoBehaviour
{
    public Button ButtonExit;
    private void OnEnable()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.Sound_Efect_MisNoti);
    }
    private void Awake()
    {
        ButtonExit.onClick.AddListener(OnclickButtonExit);
    }
    void OnclickButtonExit()
    {
        gameObject.SetActive(false);
    }
}
