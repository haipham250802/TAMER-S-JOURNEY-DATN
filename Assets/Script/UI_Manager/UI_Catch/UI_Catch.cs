using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using DG.Tweening;
using System;
using Spine;

public class UI_Catch : MonoBehaviour
{
    public SkeletonGraphic UFO;
    public SkeletonGraphic Critter;
    public Transform Pos_UFO_Catch;
    public Transform Pos_Critter_Suck;

    public Transform StartPosCatchMachine;
    public Transform StartPosCritter;
    public Transform PosOutUICatch;

    public GameObject Critter_Parent;
    public GameObject PopUpCatch;

    public GameObject Top; // giao dien top hien thi chi so %
    public GameObject Bottom; // giao dien bottom option tang % cho user

    public Button Recatch_Button_With_Coin;
    public Button Recatch_Button_With_Ads;
    public Button Recatch_Button_With_Gem;
    public Button ExitBtn;

    public int Coin;
    public int Gem;

    public Slider Chance_Bar;
    public Image Fill_Chance_Img;
    public Text Chance_Txt;
    public Color LowColor;
    public Color HightColor;

    public UI_Battle m_UIBattle;

    public int Speed;
    Action EventCallbackAnimationComplete;
    Tweener tweener;

    public int NumCatch;
    public int CountNumCatch;
    public float JumpPower;

    private void Awake()
    {
        Recatch_Button_With_Ads.onClick.AddListener(On_Recatch_Button_With_Ads);
        Recatch_Button_With_Coin.onClick.AddListener(On_Recatch_Button_With_Coin);
        Recatch_Button_With_Gem.onClick.AddListener(On_Recatch_Button_With_Gem);
        ExitBtn.onClick.AddListener(OnclickExit);
    }
    private void Start()
    {
        UFO.AnimationState.SetAnimation(0, "Idle", true);
    }

    public void OnclickExit()
    {
        UI_Home.Instance.m_Player.GetComponent<Collider2D>().enabled = true;
        AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music, AudioManager.instance.MusicUIHome);

        m_UIBattle.gameObject.SetActive(false);
        this.gameObject.SetActive(false);
        Bottom.gameObject.SetActive(false);
        Top.gameObject.SetActive(false);
        m_UIBattle.gameObject.SetActive(false);
        if (!UI_Home.Instance.uI_Battle.gameObject.activeInHierarchy)
            UI_Home.Instance.TatBatUI(true);
        UI_Home.Instance.ActiveBag();


        if (m_UIBattle.m_character is BossPatrol)
        {
            m_UIBattle.RemoveBoss();
            if (m_UIBattle.isFocusSelectMap)
            {
                UI_Home.Instance.m_UIselectMap.HiddenLockImg();
                UI_Home.Instance.m_UIselectMap.gameObject.SetActive(true);
                m_UIBattle.isFocusSelectMap = false;
            }
        }
    }

    public void PlayAnimation(string _animationName, bool _loop, System.Action _animationCallback)
    {
        EventCallbackAnimationComplete = null;
        EventCallbackAnimationComplete = _animationCallback;
        UFO.AnimationState.Complete += Animation_Oncomplete;
        UFO.AnimationState.SetAnimation(0, _animationName, _loop);
    }

    private void Animation_Oncomplete(TrackEntry trackEntry)
    {
        UFO.AnimationState.Complete -= Animation_Oncomplete;
        EventCallbackAnimationComplete?.Invoke();
    }
    public void FallPosCritter()
    {
        Debug.Log("da false");
        UFO.unscaledTime = true;
        Critter.unscaledTime = true;

        Critter_Parent.transform.DOMove(StartPosCritter.position, 1.5f).SetUpdate(true).OnStart(() =>
        {
            Critter_Parent.transform.DOScale(0.7f, 1.5f);
        }).OnComplete(() =>
        {
            MoveToStartCatchMachine();
            MoveOutUICatch();
        });
    }
    public void MoveOutUICatch()
    {
        if (CountNumCatch == NumCatch)
        {
            ResetUI();
            Critter_Parent.transform.DORotate(new Vector3(0, 0, -90), 0.1f).SetUpdate(true).OnComplete(() =>
            {
                FlipCritter();
                Critter.AnimationState.SetAnimation(0, "Move", true);
            });
            Critter_Parent.transform.DOJump(PosOutUICatch.position, 1.5f, 7, 1.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                this.gameObject.SetActive(false);
                m_UIBattle.gameObject.SetActive(false);
                UI_Home.Instance.ActiveBag();
                UI_Home.Instance.UI_HomeObj.SetActive(true);
                Critter_Parent.transform.position = StartPosCritter.position;
                CountNumCatch = 0;
                Critter_Parent.transform.DORotate(new Vector3(0, 0, 0), 0.1f).SetUpdate(true);
                UI_Home.Instance.m_Player.GetComponent<Collider2D>().enabled = true;
                m_UIBattle.A_unlockLevel?.Invoke();
                m_UIBattle.RemoveBoss();
                UI_Home.Instance.ActiveUIHome();
            });
            if (m_UIBattle.m_character is BossPatrol)
            {
                m_UIBattle.RemoveBoss();
                if (m_UIBattle.isFocusSelectMap)
                {
                    UI_Home.Instance.m_UIselectMap.HiddenLockImg();
                    UI_Home.Instance.m_UIselectMap.gameObject.SetActive(true);
                    m_UIBattle.isFocusSelectMap = false;
                }
            }
        }
    }

    void FlipCritter()
    {
        if (Critter.transform.localScale.x > 0)
        {
            Vector3 TheScale = Critter.transform.localScale;
            TheScale.x *= -1;
            Critter.transform.localScale = TheScale;
        }
    }
    public void SetTextChance(string ChanceTxt)
    {
        Chance_Txt.text = ChanceTxt;
    }
    public void SetValueBar(int MaxValue, int Value)
    {
        Chance_Bar.maxValue = MaxValue;
        Chance_Bar.value = Value;

        Image img = Fill_Chance_Img.GetComponent<Image>();
        img.color = Color.Lerp(LowColor, HightColor, Chance_Bar.normalizedValue);
        var tempColor = img.color;
        tempColor.a = 1f;
        img.color = tempColor;
    }
    public void MovePosCritterSuck()
    {
        UFO.transform.DOMove(Pos_UFO_Catch.position, 1.5f).OnComplete(() =>
        {
            UFO.AnimationState.SetAnimation(0, "Idle_light", false);
            StartCoroutine(IE_DisappearLight());
        });
    }

    public void Do_Something_When_Idle_LightSappear()
    {
        UFO.AnimationState.SetAnimation(0, "Idle", true);
    }

    public CritterOfCatch m_CritterOfCatch;

    IEnumerator IE_DisappearLight()
    {
        yield return new WaitForSecondsRealtime(2f);

        tweener = Critter_Parent.transform.DOMove(Pos_Critter_Suck.position, 4).OnStart(() =>
        {
            Critter_Parent.transform.DOScale(0.45f, 3);
            m_CritterOfCatch.animator.SetBool("isDance", true);
        }).SetEase(Ease.Linear)
        .OnComplete(() =>
        {
            m_CritterOfCatch.animator.SetBool("isDance", false);
            Debug.Log("da move to CritterSuck");
            tweener.Kill();
            PlayAnimation("Idle_light_disappear", false, Do_Something_When_Idle_LightSappear);
            Catch();
        });
    }

    public void BtnPlusChanceAds()
    {

    }
    public void BtnPlusChanceCoin()
    {
        int CurCoin = DataPlayer.GetCoin();
        CurCoin -= Coin;
        DataPlayer.SetCoin(CurCoin);
        m_UIBattle.Catch_Chance_Plus += 10;
        if (m_UIBattle.Catch_Chance_Plus > 100)
        {
            m_UIBattle.Catch_Chance_Plus = 100;
        }
        //  m_UIBattle.LoadCatchChance(m_UIBattle.AllidBase.GetComponent<AllidBase>().m_EnemyBased);
        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetTextChance(m_UIBattle.Sum_Catch.ToString() + "%");
        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetValueBar(100, m_UIBattle.Sum_Catch);
        UI_Home.Instance.m_UICoinManager.SetTextCoin();
    }
    public void BtnPlusChanceGem()
    {
        m_UIBattle.Catch_Chance_Plus += 30;
        if (m_UIBattle.Catch_Chance_Plus > 100)
        {
            m_UIBattle.Catch_Chance_Plus = 100;
        }
        //  m_UIBattle.LoadCatchChance(m_UIBattle.AllidBase.GetComponent<AllidBase>().m_EnemyBased);
        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetTextChance(m_UIBattle.Sum_Catch.ToString() + "%");
        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetValueBar(100, m_UIBattle.Sum_Catch);
    }
    public void Catch()
    {
        //  m_UIBattle.CatchEnemy(m_UIBattle.AllidBase.GetComponent<AllidBase>().m_EnemyBased);
    }

    public void SetSkeletonCritter()
    {
        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(m_UIBattle.AllidBase.GetComponent<AllidBase>().m_EnemyBased.Type);
        Critter.skeletonDataAsset = null;
        this.Critter.skeletonDataAsset = enemyStat.ICON;
        Critter.Initialize(true);
    }
    public void MoveToStartCatchMachine()
    {
        UFO.transform.DOMove(StartPosCatchMachine.position, 1.5f).SetUpdate(true).OnComplete(() =>
        {
            SetValueBar(100, m_UIBattle.Sum_Catch);
            SetTextChance(m_UIBattle.Sum_Catch.ToString() + "%");
            if (CountNumCatch < NumCatch)
            {
                Bottom.SetActive(true);
                Top.SetActive(true);
            }
        });
    }
    private void OnEnable()
    {
        Bottom.SetActive(false);
        Top.SetActive(false);
    }

    public void On_Recatch_Button_With_Coin()
    {
        CountNumCatch++;
        int SumCoin = DataPlayer.GetCoin();
        int SubCoin = SumCoin - Coin;
        DataPlayer.SetCoin(SubCoin);
        UI_Home.Instance.m_UICoinManager.SetTextCoin();

        m_UIBattle.Catch_Chance_Plus += 10;
        //  m_UIBattle.LoadCatchChance(m_UIBattle.AllidBase.GetComponent<AllidBase>().m_EnemyBased);
        Debug.Log("alo: " + m_UIBattle.Sum_Catch);

        SetValueBar(100, m_UIBattle.Sum_Catch);
        SetTextChance(m_UIBattle.Sum_Catch.ToString() + "%");
        MovePosCritterSuck();

        StartCoroutine(IE_DelayHiddenObject());
    }
    public void On_Recatch_Button_With_Gem()
    {

    }
    public void On_Recatch_Button_With_Ads()
    {
        m_UIBattle.Catch_Chance_Plus += 5;
        //   m_UIBattle.LoadCatchChance(m_UIBattle.AllidBase.GetComponent<AllidBase>().m_EnemyBased);
        Debug.Log("alo: " + m_UIBattle.Sum_Catch);

        SetValueBar(100, m_UIBattle.Sum_Catch);
        SetTextChance(m_UIBattle.Sum_Catch.ToString() + "%");
        MovePosCritterSuck();

        StartCoroutine(IE_DelayHiddenObject());
    }
    IEnumerator IE_DelayHiddenObject() // an ui coin ....
    {
        yield return new WaitForSeconds(0.5f);
        Bottom.SetActive(false);
        Top.SetActive(false);
    }
    public void ResetUI()
    {
        UFO.transform.position = StartPosCatchMachine.position;
        Top.gameObject.SetActive(false);
        Bottom.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        Critter_Parent.transform.localPosition = new Vector3(0, -73.6f, StartPosCritter.position.z);
        UFO.transform.position = StartPosCatchMachine.position;
        Critter_Parent.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);

    }
}
