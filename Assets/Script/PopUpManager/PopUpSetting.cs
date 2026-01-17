using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
public class PopUpSetting : MonoBehaviour
{
    [FoldoutGroup("SETTING_MUSIC")]
    public Button ChangeStateMusicButton;
    [FoldoutGroup("SETTING_MUSIC")]
    public GameObject StateOnMusic;
    [FoldoutGroup("SETTING_MUSIC")]
    public GameObject StateOffMusic;
    [FoldoutGroup("SETTING_MUSIC")]
    public Text StateTxtOfMusic;

    [FoldoutGroup("SETTING_SOUND")]
    public Button ChangeStateSoundButton;
    [FoldoutGroup("SETTING_SOUND")]
    public GameObject StateOnSound;
    [FoldoutGroup("SETTING_SOUND")]
    public GameObject StateOffSound;
    [FoldoutGroup("SETTING_SOUND")]
    public Text StateTxtOfSound;

    public Button ChangeLanguageButton;
    public Text LanguageTxt;
    public PopUPChangeLanguage m_PopUpChangeLanguage;


    private void Awake()
    {
        ChangeStateSoundButton.onClick.AddListener(OnclickButtonChangeStateOfSound);
        ChangeStateMusicButton.onClick.AddListener(OnclickButtonChangeStateOfMusic);
        ChangeLanguageButton.onClick.AddListener(OncLickChangeLanguageButton);
        SetTxtCurrentLanguage(DataPlayer.GetCurrentLanguage());

    }
    bool isOnewba;
    private void OnEnable()
    {
        StartCoroutine(IE_delay());
        /* if (StateOnSound.activeInHierarchy && !StateOffSound.activeInHierarchy)
         {
             StateOnSound.SetActive(false);
             StateOffSound.SetActive(true);
             StateTxtOfSound.text = I2.Loc.LocalizationManager.GetTranslation("KEY_OFF");
             AudioManager.instance.MuteAllSound();
         }
         if (StateOffSound.activeInHierarchy && !StateOnSound.activeInHierarchy)
         {
             StateOffSound.SetActive(false);
             StateOnSound.SetActive(true);
             StateTxtOfSound.text = I2.Loc.LocalizationManager.GetTranslation("KEY_ON");
             AudioManager.instance.UnMuteAllSound();
         }*/
    }
    IEnumerator IE_delay()
    {
        yield return null;
        if (!isOnewba)
        {
            isOnewba = true;
            if (StateTxtOfMusic.gameObject.activeInHierarchy)
                StateTxtOfMusic.text = I2.Loc.LocalizationManager.GetTranslation("KEY_ON");
            else if(!StateTxtOfMusic.gameObject.activeInHierarchy)
                StateTxtOfMusic.text = I2.Loc.LocalizationManager.GetTranslation("KEY_OFF");
            if (StateTxtOfSound.gameObject.activeInHierarchy)
                StateTxtOfSound.text = I2.Loc.LocalizationManager.GetTranslation("KEY_ON");
            else if(!StateTxtOfMusic.gameObject.activeInHierarchy)
                StateTxtOfSound.text = I2.Loc.LocalizationManager.GetTranslation("KEY_OFF");
        }
    }
    void SetTxtCurrentLanguage(string str)
    {
        switch (str)
        {
            case "vi":
                LanguageTxt.text = "Vietnamese";
                break;
            case "en":
                LanguageTxt.text = "English";
                break;
            case "fr":
                LanguageTxt.text = "French (France)";
                break;
            case "ita":
                LanguageTxt.text = "Italian";
                break;
            case "ger":
                LanguageTxt.text = "German (Germany)";
                break;
            case "por":
                LanguageTxt.text = "Portuguese (Portugal)";
                break;
            case "ru":
                LanguageTxt.text = "Russian";
                break;
            case "ja":
                LanguageTxt.text = "Japanese";
                break;
            case "kore":
                LanguageTxt.text = "Korean";
                break;
            case "es":
                LanguageTxt.text = "Español";
                break;
            default:
                break;
        }
    }
    public void OncLickChangeLanguageButton()
    {
        m_PopUpChangeLanguage.gameObject.SetActive(true);
    }
    public void OnclickButtonChangeStateOfSound()
    {
        if (StateOnSound.activeInHierarchy && !StateOffSound.activeInHierarchy)
        {
            StateOnSound.SetActive(false);
            StateOffSound.SetActive(true);
            StateTxtOfSound.text = I2.Loc.LocalizationManager.GetTranslation("KEY_OFF");
            AudioManager.instance.MuteAllSound();
            return;
        }
        if (StateOffSound.activeInHierarchy && !StateOnSound.activeInHierarchy)
        {
            StateOffSound.SetActive(false);
            StateOnSound.SetActive(true);
            StateTxtOfSound.text = I2.Loc.LocalizationManager.GetTranslation("KEY_ON");
            AudioManager.instance.UnMuteAllSound();
            return;
        }
    }
    public void OnclickButtonChangeStateOfMusic()
    {
        if (StateOnMusic.activeInHierarchy && !StateOffMusic.activeInHierarchy)
        {
            StateOnMusic.SetActive(false);
            StateOffMusic.SetActive(true);
            StateTxtOfMusic.text = I2.Loc.LocalizationManager.GetTranslation("KEY_OFF");
            if (AudioManager.instance.BG_In_Game_Music.mute == false)
            {
                AudioManager.instance.MuteMusic(AudioManager.instance.BG_In_Game_Music);
            }
            return;
        }
        if (StateOffMusic.activeInHierarchy && !StateOnMusic.activeInHierarchy)
        {
            StateOffMusic.SetActive(false);
            StateOnMusic.SetActive(true);
            StateTxtOfMusic.text = StateTxtOfMusic.text = I2.Loc.LocalizationManager.GetTranslation("KEY_ON");

            if (AudioManager.instance.BG_In_Game_Music.mute == true)
            {
                AudioManager.instance.UnMuteMusic(AudioManager.instance.BG_In_Game_Music);
            }
            return;
        }
    }
    private void OnDisable()
    {
        isOnewba = !isOnewba;
    }
}
