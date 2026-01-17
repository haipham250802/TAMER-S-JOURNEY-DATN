using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Spine.Unity;
using DG.Tweening;
public class UIPopUp : MonoBehaviour
{
    public Button TabToSkipBtn;
    public GameObject UIPopUpSuccess;

    public SkeletonGraphic ICON;

    public GameObject New_Img;

    public Transform startPos;
    public Transform PosBG;
    public Transform endpos;

    public float duration;
    public Text NameTxt;
    public Text RarityTxt;
    public Text HpTxt;
    public Text DamageTxt;

    public List<GameObject> L_BGClone = new List<GameObject>();

    public void Init(MergeElement mergeElement)
    {
        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(mergeElement.Type);
        //Icon = mergeElement.ICON;
        ICON.skeletonDataAsset = null;
        ICON.skeletonDataAsset = enemyStat.ICON;
        ICON.Initialize(true);
        ICON.AnimationState.SetAnimation(1, "Idle", true);
        NameTxt.text = mergeElement.Type.ToString();
        RarityTxt.text = mergeElement.Rarity.ToString();
        /* HpTxt.text = mergeElement.HP.ToString();
         DamageTxt.text = mergeElement.Damage.ToString();*/
    }
    private void Awake()
    {
        New_Img.SetActive(false);
        TabToSkipBtn.onClick.AddListener(OnTabToSkipBtn);
    }

    private void Start()
    {
        UIPopUpSuccess.SetActive(false);
    }
    void OnTabToSkipBtn()
    {
        DataPlayer.SetIsChecktapSKipPopUpMerge(true);
        TutorialManager.Instance.DeSpawn();

        UIPopUpSuccess.SetActive(false);
        New_Img.SetActive(false);
        if (DataPlayer.GetIsCheckDoneTutorial())
            return;
        if (!DataPlayer.GetIsCheckOutUiMerge())
        {
            UI_Home.Instance.m_UIMerge.OutButton.GetComponent<Canvas>().sortingOrder = 10002;
            TutorialManager.Instance.SpawnHandUIHome(UI_Home.Instance.m_UIMerge.PosBackButton, new Vector3(60, -110));
        }

    }
    public void _ShowPopUpSuccess(MergeElement mergeElement)
    {
        // TutorialManager.Instance.DeSpawn();
        if (!DataPlayer.GetIsCheckSkipPopUpMerge())
        {
            UI_Home.Instance.m_UIMerge.OutButton.GetComponent<Canvas>().sortingOrder = 8;
            StartCoroutine(IEdelay());
        }
        //  UIPopUpSuccess.SetActive(true);
        Init(mergeElement);
    }
    IEnumerator IEdelay()
    {
        yield return new WaitForSeconds(2.5f);
       // if (!DataPlayer.GetIsCheckDoneTutorial())
        //    TutorialManager.Instance.SpawnHandUIHome(UIPopUpSuccess.transform, TabToSkipBtn.transform.localPosition + new Vector3(20, -100));
    }
    Tweener twennerr;
    void KillTween()
    {
        twennerr.Kill();
    }
    private void Update()
    {
        if (!isMoveBGDone)
        {
            MoveBackGround(BG_Parent.transform);
            isMoveBGDone = true;
        }
    }
    private void OnEnable()
    {
        isMoveBGDone = false;
        BG_Parent.transform.position = StartPosBG.position;
        if (!DataPlayer.GetIsCheckDoneTutorial())
            TabToSkipBtn.GetComponent<Canvas>().sortingOrder = 10002;
    }
    private void OnDisable()
    {
        KillTween();
        isMoveBGDone = true;
    }
    public GameObject BG_Parent;

    public Transform StartPosBG;
    public Transform EndPosBG;

    public bool isMoveBGDone = true;
    public void MoveBackGround(Transform BG)
    {
        twennerr = BG.DOMove(EndPosBG.position, 60).SetEase(Ease.Linear).OnComplete(() =>
        {
            BG.position = StartPosBG.position;
            isMoveBGDone = false;
        });
    }
}
