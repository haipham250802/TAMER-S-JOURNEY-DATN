using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Spine.Unity;
using Sirenix.OdinInspector;
public class BossPatrol : Character
{
    /*public LevelBoss levelboss;

    ENewEnemyState E_StateEnemy;

    private Vector2 startPos;
    private Vector2 PatrolPos;
    StateEnemy stateEnemy;

    public float SpeedPatrol;
    public float SpeedChasing;
    public float RangePatrol = 2.0f;
    public float RangeChasing;

    public GameObject UIBattle;
    public GroupEnemyIntro m_GroupEnemyIntro;
    public RuleController m_RuleController;
    public GameObject ChangeScreen;

    player m_player;
    Tweener tweener;

    public bool isTrigger = false;
    float StartSpeedChasing = 0;

    public SkeletonAnimation skeleton;
    public MoveEnemy m_MoveEnemy;


    public Canvas canvas;
    private void Awake()
    {
        canvas.transform.SetParent(null);
        canvas.worldCamera = Camera.main;
    }
    private void OnEnable()
    {
    }
    void Start()
    {
        skeleton.AnimationState.SetAnimation(1, "Move", true);
        StartSpeedChasing = SpeedChasing;
        startPos = transform.position;
        PatrolPos = startPos;
        stateEnemy = StateEnemy.PATROL;
        E_StateEnemy = ENewEnemyState.Patrol;

        CharacterManager.Instance.AddBossToListBoss(this);
        m_RuleController.AddListBoss(this);
        this.gameObject.SetActive(false);
        IsFacingRight = false;
    }
    bool isPatrol = false;
    private void Update()
    {
    }
    private void LateUpdate()
    {
        switch (E_StateEnemy)
        {
            case ENewEnemyState.Idle:
                break;
            case ENewEnemyState.Patrol:
                if (!isCanPatrol)
                {
                    MoveAI();
                    isCanPatrol = true;
                }
                if (IsCheckPlayerChase(3))
                {
                    ChasePlayer();
                }
                break;
            case ENewEnemyState.Chase:
                if (IsCheckPlayerOutChase(3.1f))
                {
                    MoveAI();
                    ActiveCollider();
                    SetSpeed();
                }
                break;
        }
    }
    private void SetSpeed()
    {
        for (int i = 0; i < m_RuleController.L_enemy.Count; i++)
        {
            m_RuleController.L_enemy[i].GetComponent<PolyNavAgent>().maxSpeed = 3.5f;
        }
    }
    public void ChasePlayer()
    {
        E_StateEnemy = ENewEnemyState.Chase;
        m_MoveEnemy.StopMove();
        Vector2 Pos = player.Instance.gameObject.transform.position;
        FlipSprite(Pos);
        m_MoveEnemy.Go(Pos);
    }
    public bool IsCheckPlayerChase(float RangeChase)
    {
        if (Vector2.Distance(transform.position, Controller.Instance.m_Player.transform.position) <= RangeChase)
        {
            return true;
        }
        return false;
    }
    public bool IsCheckPlayerOutChase(float RangeOutChase)
    {
        if (Vector2.Distance(transform.position, Controller.Instance.m_Player.transform.position) >= RangeOutChase)
        {
            return true;
        }
        return false;
    }
    bool patrolOut;
    public bool IsFacingRight;

    public void StatePatrol()
    {
        isPatrol = true;
        patrolOut = !patrolOut;
        Vector2 PosPatrol = patrolOut ? GetPatrolPos() : startPos;
        Vector2 CurrentPos = transform.position;
        if (CurrentPos.x > PosPatrol.x && IsFacingRight)
        {
            Flip();
        }
        else if (CurrentPos.x < PosPatrol.x && !IsFacingRight)
        {
            Flip();
        }
        tweener = transform.DOMove(PosPatrol, SpeedPatrol).SetSpeedBased(true).SetEase(Ease.Linear).OnComplete(() =>
        {
            StatePatrol();
        });
    }
    Vector2 LastPos;
    bool isCanPatrol;
    bool isDoneMoveStartPos = true;

    public void MoveAI()
    {
        E_StateEnemy = ENewEnemyState.Patrol;
        m_MoveEnemy.StopMove();
        Vector2 PosPatrol = transform.position + (Vector3)Random.insideUnitCircle * 3;
        Vector2 CurrentPos = transform.position;
        FlipSprite(PosPatrol);
        LastPos = CurrentPos;
        m_MoveEnemy.Go(PosPatrol, (bool done) =>
        {
            if (done)
            {
                isCanPatrol = false;
                isDoneMoveStartPos = !isDoneMoveStartPos;
            }
        });
    }
    public void FlipSprite(Vector2 Pos)
    {
        float PointXvalue = Pos.x;
        float FrogXvalue = this.gameObject.transform.position.x;

        if (PointXvalue > FrogXvalue && !IsFacingRight)
        {
            Flip();
        }
        else if (PointXvalue < FrogXvalue && IsFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 TheScale = transform.localScale;
        TheScale.x *= -1;
        transform.localScale = TheScale;
    }

    public Vector2 GetPatrolPos()
    {
        float Rand = 0;

        if (isPatrol)
        {
            Rand = Random.Range(1f, RangePatrol);
        }
        PatrolPos = Random.insideUnitCircle * Rand + startPos;

        return PatrolPos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player Col_Player = collision.GetComponent<player>();
        UI_Battle m_Uibattle = UIBattle.GetComponent<UI_Battle>();

        if (Col_Player != null)
        {
            m_Uibattle.enemyBased.isJumStartPosWhenDieEnemy = false;
            m_Uibattle.enemyBased.IsDead = false;
            m_Uibattle.CheckAttack = false;
            DisableCollider();
            isTrigger = true;
            ActiveChangeScreen();
            ActiveUIBattle();
            ReSetUpPlayer();
            HiddenUIHome();
            DoSomeThingWithUIBattle();
            StartCoroutine(IE_LoadAllidBaseElement());
            m_Uibattle.resetPosGroupEnemyIntro();
            LoadIntroEnemyBased();
            m_Uibattle.ResetPosAllidBase_And_EnemyBase();
            m_Uibattle.DisableButton();
            SetSpeedEnemys();
        }
    }
    private void DisableCollider()
    {
        if (this.GetComponent<Collider2D>().enabled == true)
            this.GetComponent<Collider2D>().enabled = false;
    }
    private void ActiveCollider()
    {
        if (this.GetComponent<Collider2D>().enabled == false)
            this.GetComponent<Collider2D>().enabled = true;
    }
    private void HiddenUIHome()
    {
        UI_Home.Instance.TatBatUI(false);
    }
    private void LoadIntroEnemyBased()
    {
        m_GroupEnemyIntro.LoadIntroEnemyBased();
    }
    private void SetSpeedEnemys()
    {
        for (int i = 0; i < m_RuleController.L_enemy.Count; i++)
        {
            m_RuleController.L_enemy[i].GetComponent<PolyNavAgent>().maxSpeed = 0;
        }
    }
    private void ReSetUpPlayer()
    {
        Controller.Instance.m_Player.GetComponent<Collider2D>().enabled = false;
        Controller.Instance.m_Player.Speed = 0;
        Controller.Instance.m_Player.skeleton.AnimationState.SetAnimation(0, "Idle", true);
    }
    private void ActiveChangeScreen()
    {
        ChangeScreen.SetActive(true);
    }
    private void DoSomeThingWithUIBattle()
    {
        UI_Battle m_Uibattle = UIBattle.GetComponent<UI_Battle>();
        m_Uibattle.DisableButton();
        m_Uibattle.enemyBased.hiddenShadow();
        m_Uibattle.TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().ResetCritterPopUp();
        m_Uibattle.TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().isHealing = false;
        m_Uibattle.TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().isGameOver = false;
        m_Uibattle.TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().LoadEnemyBasedElement();
        m_Uibattle.ResetAllidBaseElement();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        player Col_Player = collision.GetComponent<player>();

        if (Col_Player != null)
        {
            for (int i = 0; i < m_RuleController.L_enemy.Count; i++)
            {
                m_RuleController.L_enemy[i].GetComponent<PolyNavAgent>().maxSpeed = 3.5f;
            }
        }
    }
    public void ActiveUIBattle()
    {
        UIBattle.SetActive(true);
        AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music, AudioManager.instance.MusicUIBattle);
    }
    public void ActiveUiHome()
    {
        UI_Home.Instance.TatBatUI(true);
        Controller.Instance.m_Player.gameObject.SetActive(true);
    }

    public void ActiveJpyStick()
    {
        UI_Home.Instance.ActiveBag();
    }

    IEnumerator IE_LoadAllidBaseElement()
    {
        yield return null;
        UIBattle.GetComponent<UI_Battle>().LoadAlliedBasedElement();
        UIBattle.GetComponent<UI_Battle>().LoadStartAllidBase();
        UIBattle.GetComponent<UI_Battle>().SpawnEnemyBase();
        UIBattle.GetComponent<UI_Battle>().SetHPEnemyBase();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangePatrol);
        Gizmos.color = Color.green;
        if (startPos == Vector2.zero)
        {
            Gizmos.DrawWireSphere(transform.position, RangeChasing);
        }
        else
        {
            Gizmos.DrawWireSphere(startPos, RangeChasing);
        }
    }

#endif*/

    ENewEnemyState E_StateEnemy;
    public LevelBoss levelboss;
    private Vector2 startPos;

    public float RangePatrol = 2.0f;
    public float RangeChasing;

    public RuleController m_RuleController;

    public bool isTrigger = false;
    public SkeletonAnimation skeleton;
    public MoveEnemy m_MoveEnemy;
    public GroupEnemyBasedElement childEnemyGroup;
    private Coroutine coroutine;
    private bool isdelay;
    public bool isCanAI;
    private void OnEnable()
    {
        isCanPatrol = false;
        isCanAI = false;
        m_MoveEnemy.agent.maxSpeed = 3.5f;
        m_RuleController.StoryBoss.gameObject.SetActive(true);
    }
    void Start()
    {
        skeleton.AnimationState.SetAnimation(1, "Move", true);
        startPos = transform.position;
        E_StateEnemy = ENewEnemyState.Patrol;
        CharacterManager.Instance.AddBossToListBoss(this);
        m_RuleController.AddListBoss(this);
        IsFacingRight = false;
        this.gameObject.SetActive(false);
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        if (!isCanAI)
        {
            switch (E_StateEnemy)
            {
                case ENewEnemyState.Idle:
                    break;
                case ENewEnemyState.Patrol:
                    if (!isCanPatrol)
                    {
                        MoveAI();
                        isCanPatrol = true;
                    }
                    if (IsCheckPlayerChase(3))
                    {
                        ChasePlayer();
                    }
                    if (isCanPatrol && !isdelay)
                    {
                        coroutine = StartCoroutine(IE_DelayNewPos());
                        isdelay = true;
                    }
                    break;
                case ENewEnemyState.Chase:
                    if (IsCheckPlayerOutChase(3.1f))
                    {
                        MoveAI();
                        SetSpeed();
                        ActiveCollider();
                    }
                    break;
            }
            if (UI_Home.Instance.m_Player)
            {
                if (Vector2.Distance(transform.position, UI_Home.Instance.m_Player.transform.position) < 4 && !isCanPatrol)
                {
                    SetSpeed();
                }
            }

        }
    }

    IEnumerator IE_DelayNewPos()
    {
        yield return new WaitForSeconds(3f);
        isCanPatrol = false;
        m_MoveEnemy.agent.maxSpeed = 3.5f;

    }
    public void ChasePlayer()
    {
        E_StateEnemy = ENewEnemyState.Chase;
        m_MoveEnemy.StopMove();
        Vector2 Pos = player.Instance.gameObject.transform.position;
        m_MoveEnemy.Go(Pos);
    }
    public bool IsCheckPlayerChase(float RangeChase)
    {
        if (Controller.Instance.m_Player)
        {
            if (Vector2.Distance(transform.position, Controller.Instance.m_Player.transform.position) <= RangeChase)
            {
                return true;
            }
        }
        return false;
    }
    public bool IsCheckPlayerOutChase(float RangeOutChase)
    {
        if (Controller.Instance.m_Player)
        {
            float x2 = Controller.Instance.m_Player.transform.position.x;
            float x1 = transform.position.x;
            float y1 = Controller.Instance.m_Player.transform.position.y;
            float y2 = transform.position.y;
            float Range = Mathf.Sqrt(((x2 - x1) * (x2 - x1)) +
                ((y2 - y1) * (y2 - y1)));
            if (Range >= RangeOutChase)
            {
                return true;
            }
        }
        return false;
    }
    public bool IsFacingRight;
    Vector2 PosPatrol;
    bool isCanPatrol;

    bool isDoneMoveStartPos = true;
    public void MoveAI()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        E_StateEnemy = ENewEnemyState.Patrol;
        m_MoveEnemy.StopMove();
        PosPatrol = isDoneMoveStartPos ? transform.position + (Vector3)Random.insideUnitCircle * 3 : startPos;
        Vector2 CurrentPos = transform.position;
        m_MoveEnemy.Go(PosPatrol, (bool done) =>
        {
            if (done)
            {
                isCanPatrol = false;
                isdelay = false;
                isDoneMoveStartPos = !isDoneMoveStartPos;
            }
        });
    }
    public void FlipSprite(Vector2 Pos)
    {
        float PointXvalue = Pos.x;
        float FrogXvalue = this.gameObject.transform.position.x;

        if (PointXvalue > FrogXvalue && !IsFacingRight)
        {
            Flip();
        }
        else if (PointXvalue < FrogXvalue && IsFacingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 TheScale = transform.localScale;
        TheScale.x *= -1;
        transform.localScale = TheScale;
    }
    private void OnTriggerExit(Collider other)
    {
       /* player Col_Player = collision.GetComponent<player>();
        if(co)*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player Col_Player = collision.GetComponent<player>();
        UI_Battle m_Uibattle = UI_Home.Instance.uI_Battle;
        if (Col_Player != null)
        {
            if (CheckIsCanFight())
            {
                m_RuleController.StopEnemy();
                m_MoveEnemy.agent.Stop();
                UI_Home.Instance.m_UIScreen.disableButton();
                m_Uibattle.enemyBased.isJumStartPosWhenDieEnemy = false;
                m_Uibattle.enemyBased.IsDead = false;
                m_Uibattle.CheckAttack = false;
                DisableCollider();
                player.Instance.Speed = 0;
                player.Instance.GetComponent<Collider2D>().enabled = false;
                Controller.Instance.skeleton.gameObject.SetActive(true);
                Controller.Instance.skeleton.AnimationState.SetAnimation(0, "Transition", false);
                StartCoroutine(IE_delayLoadBattle(m_Uibattle));
                Controller.Instance.skeleton.AnimationState.Complete += complete =>
                {
                    if (complete.Animation.Name == "Transition")
                    {
                        Controller.Instance.skeleton.gameObject.SetActive(false);
                    }
                };
            }
            else if (!CheckIsCanFight())
            {
                Debug.Log("Alo");
                MoveToast();
            }
        }
    }
    public void SetSpeedEnemys(int speed)
    {
        for (int i = 0; i < m_RuleController.L_boss.Count; i++)
        {
            m_RuleController.L_boss[i].GetComponent<PolyNavAgent>().maxSpeed = speed;
        }
    }
    IEnumerator IE_delayLoadBattle(UI_Battle uibatle)
    {
        yield return new WaitForSeconds(0.8f);
        if (UI_Home.Instance.CanShowUIBattle())
        {
            isTrigger = true;
            UI_Home.Instance.m_Player.cinemachineCam.transform.position = UI_Home.Instance.m_Player.transform.position;
            //    ActiveChangeScreen();
            ActiveUIBattle();
            ReSetUpPlayer();
            HiddenUIHome();
            DoSomeThingWithUIBattle();
            StartCoroutine(IE_LoadAllidBaseElement());
            uibatle.resetPosGroupEnemyIntro();
            LoadIntroEnemyBased();
            uibatle.ResetPosAllidBase_And_EnemyBase();
            uibatle.DisableButton();
            SetSpeedEnemys(0);
            UI_Home.Instance.uI_Battle.m_character = this;
            UI_Home.Instance.uI_Battle.m_RuleController = m_RuleController;
        }
        else
        {
            UI_Home.Instance.uI_Battle.OutBattle();
        }
    }
    private bool CheckIsCanFight()
    {
        int CountAllidBaseDie = 0;
        int CountAllidBaseLive = 0;
        player.Instance.m_CritterFollowController.LoadCritterFollow();
        for (int i = 0; i < player.Instance.m_CritterFollowController.L_CritterFollowElement.Count; i++)
        {
            if (player.Instance.m_CritterFollowController.L_CritterFollowElement[i].HP < 1 && player.Instance.m_CritterFollowController.L_CritterFollowElement[i].gameObject.activeInHierarchy)
            {
                CountAllidBaseDie++;
            }
        }
        for (int i = 0; i < player.Instance.m_CritterFollowController.L_CritterFollowElement.Count; i++)
        {
            if (player.Instance.m_CritterFollowController.L_CritterFollowElement[i].gameObject.activeInHierarchy)
            {
                CountAllidBaseLive++;
            }
        }
        Debug.Log(CountAllidBaseDie + ": " + CountAllidBaseLive);
        if (CountAllidBaseDie == CountAllidBaseLive || CountAllidBaseLive == 0)
        {
            return false;
        }
        return true;
    }
    private void MoveToast()
    {
        UI_Home.Instance.m_UITeam.m_ToastManager.ResetToast();
        UI_Home.Instance.m_UITeam.m_ToastManager.SetText("Can Fight Because Critter Not Enought Blood");
        UI_Home.Instance.m_UITeam.m_ToastManager.MoveToastDontRepeat();
    }
    public void DisableCollider()
    {
        if (this.GetComponent<Collider2D>().enabled == true)
            this.GetComponent<Collider2D>().enabled = false;
    }
    public void ActiveCollider()
    {
        if (this.GetComponent<Collider2D>().enabled == false)
            this.GetComponent<Collider2D>().enabled = true;
    }
    public void SetSpeed()
    {
        for (int i = 0; i < m_RuleController.L_boss.Count; i++)
        {
            m_RuleController.L_boss[i].GetComponent<PolyNavAgent>().maxSpeed = 3.5f;
        }
    }
    private void HiddenUIHome()
    {
        UI_Home.Instance.TatBatUI(false);
    }
    private void LoadIntroEnemyBased()
    {
        UI_Home.Instance.m_GroupEnemyIntro.LoadIntroEnemyBased();
    }
    private void SetSpeedEnemys()
    {
        for (int i = 0; i < m_RuleController.L_enemy.Count; i++)
        {
            m_RuleController.L_enemy[i].GetComponent<PolyNavAgent>().maxSpeed = 0;
        }
    }
    private void ReSetUpPlayer()
    {
        Controller.Instance.m_Player.GetComponent<Collider2D>().enabled = false;
        Controller.Instance.m_Player.Speed = 0;
        Controller.Instance.m_Player.skeleton.AnimationState.SetAnimation(0, "Idle", true);
    }
    private void ActiveChangeScreen()
    {
        UI_Home.Instance.ChangeScreenObj.SetActive(true);
    }
    private void DoSomeThingWithUIBattle()
    {
        UI_Battle m_Uibattle = UI_Home.Instance.uI_Battle;
        m_Uibattle.DisableButton();
        m_Uibattle.enemyBased.hiddenShadow();
        m_Uibattle.TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().ResetCritterPopUp();
        m_Uibattle.TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().isHealing = false;
        m_Uibattle.TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().isGameOver = false;
        m_Uibattle.TimeCoolDownBeforeLose.GetComponent<TimeCoolDownBeforeLose>().LoadEnemyBasedElement();
        m_Uibattle.ResetAllidBaseElement();
    }

    public void ActiveUIBattle()
    {
        UI_Battle battle = UI_Home.Instance.uI_Battle;
        battle.InitEnemy(childEnemyGroup.L_enemyBaseElement);
        battle.gameObject.SetActive(true);

       // AudioManager.instance.isMuteMusic = false;
        AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music, AudioManager.instance.MusicUIBattle);
    }
    public void ActiveUiHome()
    {
        if (UI_Home.Instance.uI_Battle.gameObject.activeInHierarchy)
            UI_Home.Instance.TatBatUI(true);
        Controller.Instance.m_Player.gameObject.SetActive(true);
    }
    public void ActiveJpyStick()
    {
        UI_Home.Instance.ActiveBag();
    }

    IEnumerator IE_LoadAllidBaseElement()
    {
        yield return null;
        UI_Home.Instance.uI_Battle.LoadAlliedBasedElement();
        UI_Home.Instance.uI_Battle.LoadStartAllidBase();
        UI_Home.Instance.uI_Battle.SpawnEnemyBase();
        UI_Home.Instance.uI_Battle.SetHPEnemyBase();
    }
    private void OnDisable()
    {
        /*if*//* (UI_Home.Instance.UI_HomeObj.activeInHierarchy)
            UI_Home.Instance.uI_Battle.UnlockLevel();*/
        isCanPatrol = false;

    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RangePatrol);
        Gizmos.color = Color.green;
        if (startPos == Vector2.zero)
        {
            Gizmos.DrawWireSphere(transform.position, RangeChasing);
        }
        else
        {
            Gizmos.DrawWireSphere(startPos, RangeChasing);
        }
    }
#endif
}
public enum StateEnemy
{
    PATROL,
    CHASING
}
public enum LevelBoss
{
    LV1,
    LV2,
    LV3,
    LV4,
    LV5,
    LV6,
    LV7,
    LV8,
    LV9,
    LV10,
    LV11,
}