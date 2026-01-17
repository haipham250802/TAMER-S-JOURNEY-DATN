using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpCommingSoon : MonoBehaviour
{
    public Button EXitButton;
    public Text COntent;

    private void Awake()
    {
        EXitButton.onClick.AddListener(OnclickExitButton);
    }
    private void OnEnable()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.Sound_Efect_MisNoti);
    }
    void OnclickExitButton()
    {
        gameObject.SetActive(false);
    }
}
