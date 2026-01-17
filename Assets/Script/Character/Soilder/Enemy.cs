using DG.Tweening;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
public class Enemy : Character
{
    ENewEnemyState E_StateEnemy;
    private Vector2 startPos;

    public float RangePatrol = 2.0f;
    public float RangeChasing;
    public bool isTrigger = false;

    public RuleController m_RuleController;
    public SkeletonAnimation skeleton;
    public MoveEnemy m_MoveEnemy;
    public GroupEnemyBasedElement childEnemyGroup;
    public bool isCanAI;

    private void OnEnable()
    {

    }
    void Start()
    {
        skeleton.AnimationState.SetAnimation(1, "Move", true);
        startPos = transform.position;
        isCanPatrol = false;
        E_StateEnemy = ENewEnemyState.Patrol;
        CharacterManager.Instance.AddEnemyToListEnemy(this);
        //   m_RuleController.AddListEnemy(this);
        m_RuleController.AddListEnemy2(this);
        IsFacingRight = false;
    }
    private void Update()
    {
    }
    public bool isdelay;
    private Coroutine coroutine;
    private void FixedUpdate()
    {
        if (!UI_Home.Instance.CanShowUIBattle())
        {
            return;
        }
        else
        {
            isCanAI = false;
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
                            ActiveCollider();
                            SetSpeed();
                        }
                        break;
                }
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
        PosPatrol = isDoneMoveStartPos ? transform.position + (Vector3)Random.insideUnitCircle * 4 : startPos;
        Vector2 CurrentPos = transform.position;
        m_MoveEnemy.Go(PosPatrol, (bool done) =>
        {
            if (done)
            {
                isDoneMoveStartPos = !isDoneMoveStartPos;
                isCanPatrol = false;
                isdelay = false;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!UI_Home.Instance.CanShowUIBattle())
        {
            return;
        }
        player Col_Player = collision.GetComponent<player>();
        UI_Battle m_Uibattle = UI_Home.Instance.uI_Battle;
        if (popUpManager.Instance.m_popUpFrog.gameObject.activeInHierarchy /*|| !UI_Home.Instance.m_UIScreen.gameObject.activeInHierarchy
            || popUpManager.Instance.m_PickerWheel.gameObject.activeInHierarchy*/ /*|| !VirtualCamera.Instance.isMoving*/)
        {
            return;
        }

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
    IEnumerator IE_delayLoadBattle(UI_Battle uibatle)
    {
        yield return new WaitForSeconds(0.8f);
        if (UI_Home.Instance.CanShowUIBattle())
        {
            isTrigger = true;
            UI_Home.Instance.m_Player.cinemachineCam.transform.position = UI_Home.Instance.m_Player.transform.position;
            //   ActiveChangeScreen();
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        player Col_Player = collision.GetComponent<player>();
        if (Col_Player != null)
        {
            isdelay = false;
            SetSpeed();
            player.Instance.Speed = 5;
            UI_Home.Instance.m_UIScreen.EnableButton();
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
        if(DataPlayer.GetCurrentLanguage() == "vi")
        {
            UI_Home.Instance.m_UITeam.m_ToastManager.SetText("Bạn không thể chiến đấu vì sinh vật đã cạn sinh lực");
        }
        else if(DataPlayer.GetCurrentLanguage() == "en")
        {
            UI_Home.Instance.m_UITeam.m_ToastManager.SetText("Can Fight Because Critter Not Enought Blood");
        }
        UI_Home.Instance.m_UITeam.m_ToastManager.MoveToastDontRepeat();
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
    private void SetSpeed()
    {
        for (int i = 0; i < m_RuleController.L_enemy.Count; i++)
        {
            m_RuleController.L_enemy[i].GetComponent<PolyNavAgent>().maxSpeed = 3.5f;
            if (UI_Home.Instance.CanShowUIBattle())
                m_RuleController.L_enemy[i].isCanAI = false;
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
    private void SetSpeedEnemys(int speed)
    {
        for (int i = 0; i < m_RuleController.L_enemy.Count; i++)
        {
            m_RuleController.L_enemy[i].GetComponent<PolyNavAgent>().maxSpeed = speed;
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
        // UI_Home.Instance.ChangeScreenObj.SetActive(true);
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
    //    AudioManager.instance.isMuteMusic = false;
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
        UI_Home.Instance.uI_Battle.LoadAlliedBasedElement();
        UI_Home.Instance.uI_Battle.LoadStartAllidBase();
        UI_Home.Instance.uI_Battle.SpawnEnemyBase();
        UI_Home.Instance.uI_Battle.SetHPEnemyBase();
        UI_Home.Instance.uI_Battle.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        isCanPatrol = false;
        if (m_RuleController.storyTutorial)
            m_RuleController.storyTutorial.gameObject.SetActive(false);
        DataPlayer.SetIsTriggerEnemy(true);
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