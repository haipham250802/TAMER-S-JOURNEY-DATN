using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource BG_In_Game_Music;
    public GameObject Sound_Prefab;

    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectButton;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectPopUpLose;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectPopUpVictory;
    [FoldoutGroup("Sound Effect")]

    public AudioClip SoundEffectGetCoin;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectScaleCoin;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectAttack;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectLuckyWheel;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectSuction;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffecrHealing;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectRewardChest;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectRewardAllGame;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectRewardBar;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoudEffectMegreDone;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectPunch1;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectPunch2;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectMerge;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectWosh;
    [FoldoutGroup("Sound Effect")]
    public AudioClip Slash;
    [FoldoutGroup("Sound Effect")]
    public AudioClip NewItemMerge;
    [FoldoutGroup("Sound Effect")]
    public AudioClip Sound_Effect_Card_UITeam_And_Merge_FLy;
    [FoldoutGroup("Sound Effect")]
    public AudioClip Sound_Effect_Card_UITeam_And_Merge_To_Slot;
    [FoldoutGroup("Sound Effect")]
    public AudioClip Sound_Efect_MisNoti;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectFallChest;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundEffectDie;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundJump;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundVaccumFall;
    [FoldoutGroup("Sound Effect")]
    public AudioClip SoundAppearCharacter;

    [FoldoutGroup("Music")]
    public AudioClip MusicUIBattle;
    [FoldoutGroup("Music")]
    public AudioClip MusicUIHome;
    [FoldoutGroup("Music")]

    public List<GameObject> L_SoundClone = new List<GameObject>();
    public bool isMuteSound = false;
    public bool isMuteMusic = false;

    public GameObject ParentSoundClone;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AudioManager();
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        BG_In_Game_Music.mute = false;
    }

    void Start()
    {
        PlayMusic(BG_In_Game_Music, MusicUIHome);
    }
    void Update()
    {

    }
    public void PlaySound(AudioClip sound)
    {
        if (isMuteSound) return;

        var SoundClone = SimplePool.Spawn(Sound_Prefab, Vector3.zero, Quaternion.identity);
        SoundClone.GetComponent<AudioSource>().mute = false;
        SoundClone.GetComponent<AudioSource>().PlayOneShot(sound);
        SoundClone.transform.SetParent(ParentSoundClone.transform);
        L_SoundClone.Add(SoundClone);
        TurnOffSound(SoundClone, sound);
    }
    public void TurnOffSound(GameObject audio, AudioClip sound)
    {
        StartCoroutine(IE_DelayRemoveAudioShot(sound.length, audio));
    }
    public void TurnOffSound(GameObject audio, float time)
    {
        StartCoroutine(IE_DelayRemoveAudioShot(time, audio));
    }
    IEnumerator IE_DelayRemoveAudioShot(float time, GameObject audio)
    {
        yield return new WaitForSeconds(time);
        SimplePool.Despawn(audio);
    }

    public void PlayMusic(AudioSource aus, AudioClip clip)
    {
       // if (isMuteMusic) return;
      //  aus.mute = false;
        aus.clip = clip;
        aus.Play();
        aus.loop = true;
    }
    public void MuteMusic(AudioSource aus)
    {
        isMuteMusic = true;
        aus.mute = true;
    }
    public void UnMuteMusic(AudioSource aus)
    {
        isMuteMusic = false;
        aus.mute = false;
    }
    public void MuteAllSound()
    {
        isMuteSound = !isMuteSound;
        for (int i = 0; i < L_SoundClone.Count; i++)
        {
            L_SoundClone[i].GetComponent<AudioSource>().mute = true;
        }
    }
    public void UnMuteAllSound()
    {
        isMuteSound = !isMuteSound;
        for (int i = 0; i < L_SoundClone.Count; i++)
        {
            L_SoundClone[i].GetComponent<AudioSource>().mute = false;
        }
    }
    public void MuteSound()
    {
        for (int i = 0; i < L_SoundClone.Count; i++)
        {
            L_SoundClone[i].GetComponent<AudioSource>().mute = false;
        }
    }

    private void OnDisable()
    {
        Destroy(gameObject);
    }
}
