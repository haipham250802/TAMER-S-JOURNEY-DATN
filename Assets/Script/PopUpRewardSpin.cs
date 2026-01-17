using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PopUpRewardSpin : MonoBehaviour
{
    public Image Icon;
    public Text QuantityTxt;
    public Button TapToSkip;


    private void Awake()
    {
        TapToSkip.onClick.AddListener(OnCLickButtonTapToSkip);
    }
    private void OnEnable()
    {
        AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectRewardAllGame);
    }
    void OnCLickButtonTapToSkip()
    {
        gameObject.SetActive(false);
    }
}
