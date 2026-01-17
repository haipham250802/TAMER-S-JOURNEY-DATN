using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
public class PopUp_Victory : MonoBehaviour
{
    public Button TapToSkipBtn;
    public bool IsTap;

    public UI_Battle m_UIBattle;
    public SkeletonGraphic skeleton;


    private void Awake()
    {
        TapToSkipBtn.onClick.AddListener(OnClickTapButton);
    }
    void OnClickTapButton()
    {
      
        AudioManager.instance.BG_In_Game_Music.mute = true;
        AudioManager.instance.BG_In_Game_Music.loop = true;
    }
}
