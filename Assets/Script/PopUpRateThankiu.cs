using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpRateThankiu : MonoBehaviour
{
    public Button ExitButton;


    private void Awake()
    {
        ExitButton.onClick.AddListener(OnclickExitbutton);
    }
    private void OnEnable()
    {
        AudioManager.Instance.PlaySound(AudioManager.instance.SoundEffectWosh);
    }
    void OnclickExitbutton()
    {
        gameObject.SetActive(false);
    }
}
