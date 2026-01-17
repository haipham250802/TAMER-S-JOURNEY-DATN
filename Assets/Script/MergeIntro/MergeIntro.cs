using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Spine.Unity;
using Spine;
using DG.Tweening;
using UnityEngine.UI;
public class MergeIntro : MonoBehaviour
{
    private const string DOWN_NAME = "Down";
    private const string DOWN_NAME2 = "Down2";

    private const string UP_NAME = "Up";
    private const string UP_NAME2 = "Up2";
    private const string MERGE_NAME = "Merge";
    private const string IDLE_NAME = "Idle";

    public UI_Merge m_UIMerge;
    public SkeletonGraphic Merge_Machine;
    public SkeletonGraphic Critter_01;
    public SkeletonGraphic Critter_02;
    public SkeletonGraphic CritterMergeDone;
    public Vector3 Offset;

    public Transform PosCritter_1;
    public Transform PosCritter_2;
    public Transform startPos, endpos, MovePos;

    public Transform Pos_1;
    public Transform Pos_2;
    public Button TapToSKipButton;

    public float duration;
    public int JumpPower;
    public int Duration;
    public PopUpMergeIntro m_PopUPMergeIntro;
    Tweener _Tweener;

    public int Speed;
    Action EventCallbackAnimationComplete;

    public List<GameObject> L_BGClone = new List<GameObject>();
    private bool isHold = false;
    AnimationEvent Event;
    public Animator Animator;
    AnimationClip clip;
    public AnimationCurve _AnimCurve;
    public Text NameCritter;

    public GameObject NewImage;
    public GameObject Efect;

    private void Awake()
    {
        TapToSKipButton.onClick.AddListener(OnClickTaptoskipButtomn);
        /* Event = new AnimationEvent();
         Event.functionName = "Spawnhand";
         clip = Animator.runtimeAnimatorController.animationClips[0];
         clip.AddEvent(Event);*/
        Efect.gameObject.SetActive(false);

    }
    private void OnEnable()
    {
        //StartCoroutine(IE_delayy());
        TapToSKipButton.gameObject.SetActive(false);
    }
    void OnClickTaptoskipButtomn()
    {
        gameObject.SetActive(false);
        UI_Home.Instance.m_UITeam.ResetAllid();
        UI_Home.Instance.m_UITeam.LoadAllied();
        UI_Home.Instance.m_UIMerge.LoadEventory(UI_Home.Instance.m_UIMerge.Eventory.gameObject);

        UI_Home.Instance.m_UITeam.AnonymousCard(UI_Home.Instance.m_UIMerge.Eventory);

        DataPlayer.SetIsChecktapSKipPopUpMerge(true);
    }
    IEnumerator IE_delayy()
    {
        yield return null;
        isMoveBGDone = false;
        BG_Parent.transform.position = StartPosBG.position;
        /*        Open();
                Merge_Machine.AnimationState.Complete += OnCompleteSpine;*/
    }
    private void Start()
    {
        CritterMergeDone.gameObject.SetActive(false);
        NewImage.SetActive(false);
    }
    /*
     private void Awake()
     {
         Merge_Machine.unscaledTime = true;
         OutButton.onClick.AddListener(OnClickOutButton);
     }
     private void Start()
     {
         CritterMergeDone.gameObject.SetActive(false);
     }
     private void OnCompleteSpine(TrackEntry trackEntry)
     {
         if (trackEntry.Animation.Name == "Action")
         {
             isHold = false;
             Open();
             return;
         }
         if (trackEntry.Animation.Name == "Open")
         {
             Debug.LogError("ishold: " + isHold);
             if (isHold)
             {
                 OpenHold();
             }
             else
             {
                 ShowPopUp();
             }
             return;
         }
         if (trackEntry.Animation.Name == "Close")
         {
             ACtionMerge();
             return;
         }
     }*/
    public void SetData()
    {
        EnemyStat enemyStat_1 = Controller.Instance.GetStatEnemy(m_UIMerge.slot1.Type);
        EnemyStat enemyStat_2 = Controller.Instance.GetStatEnemy(m_UIMerge.slot2.Type);

        EnemyStat enemyStat_3 = Controller.Instance.GetStatEnemy(m_UIMerge.SlotMerge.Type);



        Debug.Log("1: " + m_UIMerge.slot1.Type);
        Debug.Log("2: " + m_UIMerge.slot2.Type);
        Debug.Log("3: " + m_UIMerge.SlotMerge.Type);

        Critter_01.skeletonDataAsset = null;
        Critter_01.skeletonDataAsset = enemyStat_1.ICON;
        Critter_01.Initialize(true);

        Critter_02.skeletonDataAsset = null;
        Critter_02.skeletonDataAsset = enemyStat_2.ICON;
        Critter_02.Initialize(true);

        CritterMergeDone.skeletonDataAsset = null;
        CritterMergeDone.skeletonDataAsset = enemyStat_3.ICON;
        CritterMergeDone.Initialize(true);

        Critter_01.AnimationState.SetAnimation(0, "Idle", true);
        Critter_02.AnimationState.SetAnimation(0, "Idle", true);

        NameCritter.text = enemyStat_3.Type.ToString();
    }
    public void ActionMerge()
    {
        Critter_01.unscaledTime = true;
        Critter_02.unscaledTime = true;
        Merge_Machine.unscaledTime = true;
        // PlayAnimation(IDLE_NAME, true, Do_Something_When_Idle);
        //  Merge_Machine.AnimationState.SetAnimation(0, "Open", false);
        // StartCoroutine(IEdelayMerge());
        Merge_Machine.AnimationState.SetAnimation(0, "Idle", true);
        StartCoroutine(IE_DelayIdle());
    }
    IEnumerator IE_DelayIdle()
    {
        yield return new WaitForSeconds(1.2f);
        Merge_Machine.AnimationState.SetAnimation(0, "Action", false);
        StartCoroutine(IE_delayMerge());
        Merge_Machine.AnimationState.Complete += /*onComplete =>*/ CompleteDelayIdle;
        /* {
             if (onComplete.Animation.Name == "Action")
             {
                 Critter_01.gameObject.SetActive(false);
                 Critter_02.gameObject.SetActive(false);
                 Merge_Machine.AnimationState.SetAnimation(0, "Hold", true);
                 StartCoroutine(IE_delayHold());
             }
         };*/
    }
    void CompleteDelayIdle(TrackEntry _track)
    {
        if (_track.Animation.Name == "Action")
        {
            Critter_01.gameObject.SetActive(false);
            Critter_02.gameObject.SetActive(false);
            Merge_Machine.AnimationState.SetAnimation(0, "Hold", true);
            StartCoroutine(IE_delayHold());
        }
        Merge_Machine.AnimationState.Complete -= CompleteDelayIdle;
    }
    IEnumerator IE_delayMerge()
    {
        yield return null;
        AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectMerge);
    }
    IEnumerator IE_delayHold()
    {
        yield return new WaitForSeconds(1f);
        Merge_Machine.AnimationState.SetAnimation(0, "Open", false);
        StartCoroutine(IE_delayActiveCritterMergeDone());
        AudioManager.instance.PlaySound(AudioManager.instance.NewItemMerge);

        Merge_Machine.AnimationState.Complete += onComplete =>
        {
            if (onComplete.Animation.Name == "Open")
            {
                //   m_PopUPMergeIntro.gameObject.SetActive(true);
            }
        };
        StartCoroutine(IE_DelayMerge());
    }
    public void Spawnhand()
    {
        if (!DataPlayer.GetIsCheckSkipPopUpMerge() && !DataPlayer.GetIsCheckDoneTutorial())
        {
            for (int i = 0; i < TutorialManager.Instance.l_obj.Count; i++)
            {
                if (TutorialManager.Instance.l_obj[i].gameObject == null)
                {
                    TutorialManager.Instance.l_obj.RemoveAt(i);
                }
            }
            Controller.Instance.exampObj.gameObject.SetActive(true);
            string str = I2.Loc.LocalizationManager.GetTranslation("KEY_TUT_MERGE_DONE");
            TapToSKipButton.gameObject.AddComponent<Canvas>();
            TapToSKipButton.gameObject.AddComponent<GraphicRaycaster>();
            TapToSKipButton.gameObject.GetComponent<Canvas>().overrideSorting = true;
            TapToSKipButton.gameObject.GetComponent<Canvas>().sortingOrder = 10003;
            TutorialManager.Instance.SpawnHandUIHome(TapToSKipButton.transform, new Vector3(50, -60));
          /*  for (int i = 0; i < TutorialManager.Instance.l_obj.Count; i++)
            {
                if (TutorialManager.Instance.l_obj[i].activeSelf)
                    TutorialManager.Instance.l_obj[i].transform.localScale = Vector3.one;
            }*/
            ExampleStoryTut.Instance.SetText(str);

        }
    }
    IEnumerator IE_DelayMerge()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("1...");
        ShowPopUp();
    }
    IEnumerator IE_delayActiveCritterMergeDone()
    {
        yield return null;
        CritterMergeDone.AnimationState.SetAnimation(0, "Idle", true);
        CritterMergeDone.gameObject.SetActive(true);
        Efect.SetActive(true);
    }

    /*  void Hold()
      {
          Merge_Machine.AnimationState.SetAnimation(0, "Hold", false);
      }
      private void AnimationState_Complete(TrackEntry trackEntry)
      {
          throw new NotImplementedException();
      }*/

    /*   public void PlayAnimation(string _animationName, bool _loop, System.Action _animationCallback)
  {
      EventCallbackAnimationComplete = null;
      EventCallbackAnimationComplete = _animationCallback;
      Merge_Machine.AnimationState.Complete += Animation_Oncomplete;
      Merge_Machine.AnimationState.SetAnimation(0, _animationName, _loop);
  }
  private void Animation_Oncomplete(TrackEntry trackEntry)
  {
      EventCallbackAnimationComplete?.Invoke();
      Merge_Machine.AnimationState.Complete -= Animation_Oncomplete;
  }

  IEnumerator IEdelayMerge()
  {
      yield return null;
      Idle(MoveToMachineMerge);
  }
  public void Do_Something_When_Idle()
  {
      Critter_02.transform.DOScale(new Vector3(-0.5f, 0.5f, 0.5f), duration - 0.2f);
      Critter_01.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), duration - 0.2f);
      Critter_02.transform.DOJump(Pos_2.transform.position + Offset, JumpPower, 5, Duration);
      Critter_01.transform.DOJump(Pos_1.transform.position + Offset, JumpPower, 5, Duration).OnComplete(() =>
      {
          if (this.gameObject.activeInHierarchy)
              StartCoroutine(IE_PlayAnimDown());
      });
  }
  IEnumerator IE_PlayAnimDown()
  {
      yield return new WaitForSecondsRealtime(0.4f);
      PlayAnimation(DOWN_NAME, false, Do_Something_When_Down);
  }
  public void Do_Something_When_Down()
  {
      StartCoroutine(IE_ActionAnimUp());
  }
  IEnumerator IE_ActionAnimUp()
  {
      yield return new WaitForSecondsRealtime(1.2f);

      Critter_01.gameObject.SetActive(false);
      Critter_02.gameObject.SetActive(false);
      PlayAnimation(UP_NAME, false, Do_Something_When_Up);
  }
  IEnumerator IE_ActioneMerge()
  {
      yield return null;
      PlayAnimation(MERGE_NAME, false, Do_Something_When_Merge);
  }
  public void Do_Something_When_Up()
  {
      StartCoroutine(IE_ActioneMerge());
  }
  public void Do_Something_When_Merge()
  {
      PlayAnimation(DOWN_NAME2, false, null);
      Do_Something_When_Down_After_Merge();
  }

  public void Do_Something_When_Down_After_Merge()
  {
      if (this.gameObject.activeInHierarchy)
      {
          StartCoroutine(IE_Action_Anim_Up_When_After_Merge());
      }
  }

  IEnumerator IE_Action_Anim_Up_When_After_Merge()
  {
      yield return new WaitForSecondsRealtime(1.7f);
      CritterMergeDone.gameObject.SetActive(true);
      PlayAnimation(UP_NAME2, false, ShowPopUp);
  }

  // lam lai
  public void MoveToMachineMerge()
  {
      isHold = true;
      Open();
      StartCoroutine(IE_delayMove());
      *//*Critter_02.transform.DOMove(Pos_2.transform.position + Offset, 2);
      Critter_01.transform.DOMove(Pos_1.transform.position + Offset, 2).OnStart(() =>
      {
          Critter_02.transform.SetAsLastSibling();
          Critter_01.transform.SetAsLastSibling();
      });*//*
      StartCoroutine(IE_delay());
  }
  bool isPlayFade;
  IEnumerator IE_delayMove()
  {
      yield return new WaitForSeconds(1f);


      Critter_02.transform.DOMove(Pos_2.transform.position + Offset, 0.5f).SetEase(Ease.InQuad);
      Critter_01.transform.DOMove(Pos_1.transform.position + Offset, 0.5f).OnStart(() =>
      {
          Critter_02.transform.SetAsLastSibling();
          Critter_01.transform.SetAsLastSibling();
          DOTween.To(() => 1f, _ =>
          {
              Color color = Critter_01.GetComponent<SkeletonGraphic>().color;
              Color color2 = Critter_02.GetComponent<SkeletonGraphic>().color;
              color.a = _;
              color2.a = _;
              Critter_01.GetComponent<SkeletonGraphic>().color = color;
              Critter_02.GetComponent<SkeletonGraphic>().color = color;
          }, 0f, 0.3f).SetEase(Ease.Linear).SetDelay(0.7f);
      }).SetEase(Ease.InQuad).OnComplete(() =>
      {
          StartCoroutine(IE_DelaySetAsLast());
      });
  }
  IEnumerator IE_DelaySetAsLast()
  {
      yield return new WaitForSeconds(0.7f);
      Critter_02.transform.SetAsFirstSibling();
      Critter_01.transform.SetAsFirstSibling();
      CloseDoor(ACtionMerge);
  }
  IEnumerator IE_delay()
  {
      yield return new WaitForSeconds(1.5f);
      Critter_02.transform.DOScale(new Vector3(-0.4f, 0.4f, 0.4f), 1f);
      Critter_01.transform.DOScale(0.4f, 1f).OnComplete(() =>
      {

      });
  }
  public void Idle(System.Action callback = null)
  {
      callback?.Invoke();
      *//*Merge_Machine.AnimationState.SetAnimation(0, "Idle", false);
      Merge_Machine.AnimationState.Complete += _ =>
      {
          if (_.Animation.Name == "Idle")
          {
              callback?.Invoke();
          }
      };*//*
  }

  public void Open()
  {
      Merge_Machine.AnimationState.SetAnimation(0, "Open", false);
      //Merge_Machine.AnimationState.Complete += _ =>
      //{
      //    if (_.Animation.Name == "Open")
      //    {
      //        callback?.Invoke();
      //    }
      //};
  }
  public void OpenHold()
  {
      Merge_Machine.AnimationState.SetAnimation(0, "Open_hole", false);
  }
  public void CloseDoorAndActionMerge()
  {
      CloseDoor(ACtionMerge);
  }
  public void CloseDoor(System.Action callback = null)
  {
      Merge_Machine.AnimationState.SetAnimation(0, "Close", false);
      //Merge_Machine.AnimationState.Complete += _ =>
      //{
      //    if (_.Animation.Name == "Close")
      //    {
      //        callback?.Invoke();
      //    }
      //};
  }
  public void ACtionMerge()
  {
      Merge_Machine.AnimationState.SetAnimation(0, "Action", false).TimeScale = 1.4f;
      //Merge_Machine.AnimationState.Complete += _ =>
      //{
      //    if (_.Animation.Name == "Action")
      //    {
      //        Open(ShowPopUp);
      //    }
      //};
  }

*/
    public void ShowPopUp()
    {
        m_UIMerge.Merge();
        //    this.gameObject.SetActive(false);
        //ResetPosObject();
        StartCoroutine(IE_delay());
    }
    IEnumerator IE_delay()
    {
        yield return new WaitForSeconds(0.5f);

        if (m_UIMerge.isNewCritter)
        {
            NewImage.SetActive(true);
            NewImage.transform.localScale = Vector3.zero;
            DOTween.To(() => 0, _ =>
            {
                NewImage.transform.localScale = new Vector3(_, _, _);
            }, 1, 1f).SetEase(_AnimCurve);
            m_UIMerge.isNewCritter = false;
        }
        TapToSKipButton.gameObject.SetActive(true);
        StartCoroutine(IE_delaySpawnHand());
    }
    IEnumerator IE_delaySpawnHand()
    {
        yield return new WaitForSeconds(0.5f);
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            Spawnhand();
        }
    }
    public void ResetPosObject()
    {
        CritterMergeDone.gameObject.SetActive(false);
        Critter_01.gameObject.SetActive(true);
        Critter_02.gameObject.SetActive(true);

        Critter_01.transform.position = PosCritter_1.transform.position;
        Critter_02.transform.position = PosCritter_2.transform.position;
    }
    public void KillTween()
    {
        _Tweener.Kill(true);
    }

    private void OnDisable()
    {
        KillTween();
        ResetPosObject();

        Critter_01.gameObject.SetActive(true);
        Critter_02.gameObject.SetActive(true);

        Critter_01.transform.position = PosCritter_1.position;
        Critter_02.transform.position = PosCritter_2.position;

        NewImage.SetActive(false);
        Efect.gameObject.SetActive(false);

        if (DataPlayer.GetIsCheckOutUiMerge())
        {
            return;
        }
        //  Merge_Machine.AnimationState.Complete -= OnCompleteSpine;

        /*  Critter_01.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
          Critter_02.transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);*/

        /*  Color color = Critter_01.GetComponent<SkeletonGraphic>().color;
          Color color2 = Critter_02.GetComponent<SkeletonGraphic>().color;
          color.a = 1;
          color2.a = 1;
          Critter_01.GetComponent<SkeletonGraphic>().color = color;
          Critter_02.GetComponent<SkeletonGraphic>().color = color;*/
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            return;
        }
        if (!DataPlayer.GetIsCheckOutUiMerge() && !DataPlayer.GetIsCheckDoneTutorial())
        {
            UI_Home.Instance.m_UIMerge.OutButton.GetComponent<Canvas>().sortingOrder = 10002;
            TutorialManager.Instance.SpawnHandUIHome(UI_Home.Instance.m_UIMerge.PosBackButton, new Vector3(60, -110));
            for (int i = 0; i < TutorialManager.Instance.l_obj.Count; i++)
            {
                TutorialManager.Instance.l_obj[i].transform.localScale = Vector3.one;
            }
        }
    }
    public GameObject BG_Parent;
    public Transform StartPosBG;
    public Transform EndPosBG;

    public bool isMoveBGDone;
    public void MoveBackGround(Transform BG)
    {
        _Tweener = BG.DOMove(EndPosBG.position, 60).SetEase(Ease.Linear).OnComplete(() =>
        {
            BG.position = StartPosBG.position;
            isMoveBGDone = false;
        });
    }
    private void Update()
    {
        if (!isMoveBGDone)
        {
            MoveBackGround(BG_Parent.transform);
            isMoveBGDone = true;
        }
    }
    public void OnClickOutButton()
    {
        DataPlayer.SetIsChecktapSKipPopUpMerge(true);
        TutorialManager.Instance.DeSpawn();
        Destroy(TapToSKipButton.GetComponent<GraphicRaycaster>());
        Destroy(TapToSKipButton.GetComponent<Canvas>());
        ResetPosObject();
        gameObject.SetActive(false);
        KillTween();
    }
}
