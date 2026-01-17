using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;
using Spine;
public class AllidBase : MonoBehaviour
{
    const string AttackName = "Attack";
    const string IdleName = "Idle";
    const string Move = "Move";

    public GameObject ParentAllid;

    [FoldoutGroup("$Type")] public ECharacterType Type;
    [FoldoutGroup("$Type")] public E_RangeAttack RangeAttack;
    [FoldoutGroup("$Type")] public int Damage;
    [FoldoutGroup("$Type")] public int Speed;
    [FoldoutGroup("$Type")] public int JumpPower;
    [FoldoutGroup("$Type")] public int SpeedMoveToPos;

    [FoldoutGroup("$Type")] public int HP;
    [FoldoutGroup("$Type")] public int CurHP;
    [FoldoutGroup("$Type")] public int ID;
    [FoldoutGroup("$Type")] public SkeletonDataAsset ICON;
    [FoldoutGroup("$Type")] public float TimeEffect;
    [FoldoutGroup("$Type")] public Image DeadImg;

    public GameObject TextDamageAnim;
    public Transform TextDamagePos;

    public GameObject hitEffect;
    public GameObject EffectJump;

    [SerializeField] private AnimationCurve _animCurveJump, _animCurveMove;
    public int JumpCount;

    public int JumpPowerStart;
    public int JumpCountStart;
    public float DurationStart;

    public int JumpPowerMoveToStartPos;
    public int JumpCountMoveToStartPos;
    public float DurationMoveToStartPos;

    public float Duration;
    public float DurationDead;
    public int JumPowerDead;

    public Transform PosDead;
    public Transform PosStartToMove;
    private Vector3 StartPos;
    public Vector3 Offset;

    private bool isTurnDone = false;
    public List<AllidELement> L_AllidElement = new List<AllidELement>();
    public UI_Battle m_UIBattle;

    public bool isTurnEnemy = false;

    public SkeletonGraphic skeletonGraphic;
    private System.Action EventCallbackAnimationComplete;

    public EditorPointSetup editorPointSetup;
    public GameObject Shadow;
    private void Awake()
    {

    }
    private void OnEnable()
    {
        StartCoroutine(IEdelay());
    }
    IEnumerator IEdelay()
    {
        yield return null;
        transform.localPosition = PosDead.position;
        TextDamageAnim.SetActive(false);
    }
    private void Start()
    {
        hitEffect.SetActive(false);
        TextDamageAnim.SetActive(false);
        StartPos = transform.position;
    }
    public void SetTextDamage(string Damage)
    {
        TextDamageAnim.GetComponent<Text>().text = Damage;
    }
    public void JumDead()
    {
        transform.DOJump(PosDead.position, JumPowerDead, 1, DurationDead).SetUpdate(true);
    }
    public void TextDamageAnimJump()
    {
        TextDamageAnim.gameObject.SetActive(true);
        if (TextDamageAnim.transform.localScale.x > 0)
        {
            Vector3 theScale = TextDamageAnim.transform.localScale;
            theScale.x *= -1;
            TextDamageAnim.transform.localScale = theScale;
        }
        TextDamageAnim.transform.DOJump(TextDamagePos.position, 0.5f, 3, 0.5f).OnComplete(() =>
        {
            TextDamageAnim.transform.position = TextDamagePos.position;
            TextDamageAnim.gameObject.SetActive(false);

        });
    }
    public void JumpToStartPosWhenJumpDead()
    {
        m_UIBattle.SLOT1_BTN.enabled = false;
        m_UIBattle.SLOT2_BTN.enabled = false;
        m_UIBattle.SLOT3_BTN.enabled = false;
        m_UIBattle.SLOT4_BTN.enabled = false;
        EffectJump.SetActive(false);

        if (HP > 0)
        {
            transform.DOJump(PosStartToMove.position, JumpPowerStart, JumpCountStart, DurationStart).SetEase(Ease.Linear).OnUpdate(() =>
            {
                UpdateViewShadow();
            }).OnComplete(() =>
            {
               /* if (AudioManager.instance.SoundJump)
                    AudioManager.instance.PlaySound(AudioManager.instance.SoundJump);*/
                EffectJump.SetActive(true);
                /* for (int i = 0; i < m_UIBattle.L_AllidELements.Count; i++)
                 {
                     if (m_UIBattle.L_AllidELements[i] != null)
                     {
                         if (m_UIBattle.L_AllidELements[i].HP > 0)
                         {
                             m_UIBattle.EnableButton(i + 1);
                         }
                     }
                 }*/
                m_UIBattle.EnableSlotButton();
                /*if(m_UIBattle.L_AllidELements.Count > 0)
                {
                    if (m_UIBattle.L_AllidELements[0] != null)
                    {
                        if (m_UIBattle.L_AllidELements[0].HP > 0)
                        {
                            m_UIBattle.SLOT1_BTN.enabled = true;
                        }
                    }
                }
                if (m_UIBattle.L_AllidELements.Count > 0)
                {
                    if (m_UIBattle.L_AllidELements[1] != null)
                    {
                        if (m_UIBattle.L_AllidELements[1].HP > 0)
                        {
                            m_UIBattle.SLOT2_BTN.enabled = true;
                        }
                    }
                }
                if (m_UIBattle.L_AllidELements.Count > 0)
                {
                    if (m_UIBattle.L_AllidELements[2] != null)
                    {
                        if (m_UIBattle.L_AllidELements[2].HP > 0)
                        {
                            m_UIBattle.SLOT3_BTN.enabled = true;
                        }
                    }
                }
                if (m_UIBattle.L_AllidELements.Count > 0)
                {
                    if (m_UIBattle.L_AllidELements[3] != null)
                    {
                        if (m_UIBattle.L_AllidELements[3].HP > 0)
                        {
                            m_UIBattle.SLOT4_BTN.enabled = true;
                        }
                    }
                }*/
            });
        }
    }
    public void MoveToStartPos()
    {
        EffectJump.SetActive(false);
       
        if (Type != ECharacterType.NONE)
        {
            Debug.Log(RangeAttack);

            if (RangeAttack == E_RangeAttack.HitFar || RangeAttack == E_RangeAttack.HitNear)
            {
                if (HP > 0)
                {
                    Debug.Log("da den 1");
                    transform.DOJump(PosStartToMove.position, JumpPowerStart, JumpCountStart, DurationStart).SetEase(Ease.Linear).OnUpdate(() =>
                    {
                        UpdateViewShadow();
                    }).OnComplete(() =>
                    {
                        if (AudioManager.instance.SoundJump)
                            AudioManager.instance.PlaySound(AudioManager.instance.SoundJump);
                        EffectJump.SetActive(true);
                    });
                }
                return;
            }
            else
            {
                if (HP > 0)
                {
                    Debug.Log("da den 2");

                    transform.DOMove(PosStartToMove.position + new Vector3(1, 0, 0), 0.2f).OnUpdate(() =>
                    {
                        UpdateViewShadow();
                    }).OnComplete(() =>
                    {
                        if (AudioManager.instance.SoundJump)
                            AudioManager.instance.PlaySound(AudioManager.instance.SoundJump);
                        transform.DOMove(PosStartToMove.position, 0.2f).OnUpdate(() =>
                        {
                            UpdateViewShadow();
                        });
                    });
                }
                return;
            }
        }
    }
    public void init(AllidELement allidELement)
    {
        Type = allidELement.Type;
        Damage = allidELement.Damage;
        HP = allidELement.HP;
        ID = allidELement.ID;
        skeletonGraphic.skeletonDataAsset = null;
        skeletonGraphic.skeletonDataAsset = allidELement.ICON;
        skeletonGraphic.Initialize(true);
    }
    public void PlayAnimation(string _animationName, bool _Loop, System.Action _animationCallback)
    {
        /* EventCallbackAnimationComplete = null;
         EventCallbackAnimationComplete = _animationCallback;
         skeletonGraphic.AnimationState.Complete += Animation_Oncomplete;
         skeletonGraphic.AnimationState.SetAnimation(1, _animationName, _loop);*/
        EventCallbackAnimationComplete = null;
        skeletonGraphic.AnimationState.SetAnimation(0, _animationName, _Loop);

        if (_animationCallback == null) return;
        if (skeletonGraphic.AnimationState.GetCurrent(0).Animation.Name == "Attack")
        {
            EventCallbackAnimationComplete = _animationCallback;
            skeletonGraphic.AnimationState.Complete += Animation_Oncomplete;
        }
        else
        {
            skeletonGraphic.AnimationState.Complete -= Animation_Oncomplete;
        }
    }
    private void Animation_Oncomplete(TrackEntry _trackEntry)
    {
        /*skeletonGraphic.AnimationState.Complete -= Animation_Oncomplete;
        EventCallbackAnimationComplete?.Invoke();*/
        if (_trackEntry.Animation.Name == "Attack")
        {
            skeletonGraphic.AnimationState.Complete -= Animation_Oncomplete;
            EventCallbackAnimationComplete?.Invoke();
        }
    }

    public void SetData(AllidELement allidELement)
    {
        this.skeletonGraphic.skeletonDataAsset = null;
        this.init(allidELement);
        this.skeletonGraphic.Initialize(true);
    }
    // **************** tam dung *****************************


    /*
        private void AnimationState_Event(TrackEntry trackEntry, Spine.Event e)
        {

            skeletonGraphic.AnimationState.Event -= AnimationState_Event;
        }*/

    public EnemyBased m_EnemyBased;
    IEnumerator IE_HiddenEffectHitEnemy()
    {
        yield return new WaitForSeconds(0.3f);
        m_UIBattle.enemyBased.hitEffect.SetActive(false);
    }

    public void ShowPopUpUICatch()
    {
        m_UIBattle.PopUpVictory.GetComponent<PopUp_Victory>().IsTap = true;
        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().ShowUICatch(m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>());
    }
    bool isAttackDone = false;
    public void ATTACK_ALLID()
    {
        m_UIBattle.curtime = 0;
        EffectJump.SetActive(false);
        m_UIBattle.DisableButton();

        if (m_UIBattle.enemyBased != null)
        {
            ParentAllid.transform.SetAsLastSibling();
            EnemyStat enemyStat = Controller.Instance.GetStatEnemy(Type);
            if(enemyStat.SoundStart2)
            {
                StartCoroutine(IE_delaySoundStart2(enemyStat.TimeSoundStart2, enemyStat));
            }
            if (enemyStat.SoundStart)
            {
                StartCoroutine(IE_delaySoundStart(enemyStat.TimeSoundStart, enemyStat));
            }
            TimeEffect = enemyStat.TimeEffect;
            Vector3 Offset = enemyStat.Offset;
            if (enemyStat.RangeAttack == E_RangeAttack.HitNear)
            {
                transform.DOJump(m_UIBattle.AllidMoveToPosEnemyBase.position + Offset, 6, 1, 0.35f)
                    .SetUpdate(true)
                    .SetEase(Ease.Linear)
                    .OnUpdate(() =>
                    {
                        UpdateViewShadow();
                    })
                    .OnComplete(() =>
                    {
                        if (!isAttackDone)
                        {
                            isAttackDone = true;
                            StartCoroutine(IE_ActiveHitEffectEnemyaBaseWithTime(TimeEffect));
                            PlayAnimation("Attack", false, DoSomethingWhenAnimationAttack);
                        }
                    });
                return;
            }
            else if (enemyStat.RangeAttack == E_RangeAttack.HitNear2)
            {
                Debug.Log("Hit near2");

                transform.DOMove(m_UIBattle.AllidMoveToPosEnemyBase.position + new Vector3(-2, 0, 0), 0.25f).SetEase(Ease.Linear).SetUpdate(true)
                    .OnUpdate(() =>
                    {
                        UpdateViewShadow();
                    })
                    .OnComplete(() =>
                    {
                        if (!isAttackDone)
                        {
                            isAttackDone = true;
                            StartCoroutine(IE_MoveFlashHitNear2());
                        }
                    });
                return;
            }
            else if (enemyStat.RangeAttack == E_RangeAttack.HitFar)
            {
                Debug.Log("Hit fa");
                if (!isAttackDone)
                {
                    isAttackDone = true;
                    StartCoroutine(IE_ActiveHitEffectEnemyaBaseWithTime(TimeEffect));
                    PlayAnimation("Attack", false, DoSomethingWhenAnimationAttack);
                }
                return;
            }
        }
    }
    IEnumerator IE_delaySoundStart(float time, EnemyStat enemyStat)
    {
        yield return new WaitForSeconds(time);
        AudioManager.instance.PlaySound(enemyStat.SoundStart);
    } 
    IEnumerator IE_delaySoundStart2(float time, EnemyStat enemyStat)
    {
        yield return new WaitForSeconds(time);
        AudioManager.instance.PlaySound(enemyStat.SoundStart2);
    }
    public void DoSomethingWhenAnimationAttack()
    {
        StartCoroutine(IE_HiddenEffectHitEnemy());

        EnemyStat ENEMYSTAT = Controller.Instance.GetStatEnemy(Type);
        if (ENEMYSTAT.RangeAttack == E_RangeAttack.HitNear)
        {
            transform.DOJump(PosStartToMove.position, 3, 3, 0.3f).OnStart(() =>
            {
                FLIP();
            }).OnUpdate(() =>
            {
                UpdateViewShadow();
            }).OnComplete(() =>
            {
                FLIP();
                skeletonGraphic.AnimationState.SetAnimation(0, "Idle", true);
                AttackEnemyWhenAllidBaseMoveToPos();
                if (m_UIBattle.enemyBased.Type == m_UIBattle.L_EnemyBaseElement[m_UIBattle.L_EnemyBaseElement.Count - 1].Type
                      && m_UIBattle.enemyBased.ID == m_UIBattle.L_EnemyBaseElement[m_UIBattle.L_EnemyBaseElement.Count - 1].ID)
                {
                    if (m_UIBattle.enemyBased.HP < 1)
                    {
                        m_EnemyBased = m_UIBattle.enemyBased;
                        m_UIBattle.enemyBased.JumpPower = 0;
                        m_UIBattle.GameWin();
                        if (m_UIBattle.isPartEnemy)// da giet chet con cuoi
                        {
                            m_UIBattle.LoadCatchChance(m_UIBattle.enemyBased);
                            EnemyStat enemyStat = Controller.Instance.GetStatEnemy(m_UIBattle.enemyBased.Type);

                            m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetTextChance(m_UIBattle.Sum_Catch.ToString() + "%"); // set text ti le

                            Debug.Log("Type: " + m_UIBattle.enemyBased.Type);
                            Debug.Log("ti le: " + m_UIBattle.Catch_Chance);
                            m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetValueBar(100, m_UIBattle.Sum_Catch); // set gia tri slider
                            Debug.Log("da den !");
                            m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetCritterSkeleton(enemyStat.ICON);
                            m_UIBattle.m_Auto.AutoOn = false;
                            m_UIBattle.m_Auto.ButtonAuto_Txt.text = "Auto: Off";
                            //  m_UIBattle.PopUpVictory.SetActive(true); // hien thi popUp victory
                            // tap skip trong event button
                            // m_UIBattle.PopUpVictory.GetComponent<PopUp_Victory>().skeleton.AnimationState.SetAnimation(0, "Appear", false);
                            // StartCoroutine(IE_ChangeAnimPopUpWin());
                            //  StartCoroutine(IE_delayActivePopUpVictory());

                            if (m_UIBattle.AllidBase.transform.localScale.x > 0)
                            {
                                Vector3 theScalee = transform.localScale;
                                theScalee.x *= -1;
                                transform.localScale = theScalee;
                            }
                        }
                    }
                }
            });
        }
        else if (ENEMYSTAT.RangeAttack == E_RangeAttack.HitNear2)
        {
            transform.DOMove(PosStartToMove.position, 0.3f).OnStart(() =>
            {
                FLIP();
            }).OnUpdate(() =>
            {
                UpdateViewShadow();
            }).OnComplete(() =>
            {
                FLIP();
                skeletonGraphic.AnimationState.SetAnimation(0, "Idle", true);
                AttackEnemyWhenAllidBaseMoveToPos();
                if (m_UIBattle.enemyBased.Type == m_UIBattle.L_EnemyBaseElement[m_UIBattle.L_EnemyBaseElement.Count - 1].Type
                      && m_UIBattle.enemyBased.ID == m_UIBattle.L_EnemyBaseElement[m_UIBattle.L_EnemyBaseElement.Count - 1].ID)
                {
                    if (m_UIBattle.enemyBased.HP < 1)
                    {
                        m_EnemyBased = m_UIBattle.enemyBased;
                        m_UIBattle.enemyBased.JumpPower = 0;
                        m_UIBattle.GameWin();
                        if (m_UIBattle.isPartEnemy)// da giet chet con cuoi
                        {
                            m_UIBattle.LoadCatchChance(m_UIBattle.enemyBased);
                            EnemyStat enemyStat = Controller.Instance.GetStatEnemy(m_UIBattle.enemyBased.Type);

                            m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetTextChance(m_UIBattle.Sum_Catch.ToString() + "%"); // set text ti le

                            Debug.Log("Type: " + m_UIBattle.enemyBased.Type);
                            Debug.Log("ti le: " + m_UIBattle.Catch_Chance);
                            m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetValueBar(100, m_UIBattle.Sum_Catch); // set gia tri slider
                            Debug.Log("da den !");
                            m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetCritterSkeleton(enemyStat.ICON);
                            //  m_UIBattle.PopUpVictory.SetActive(true); // hien thi popUp victory
                            // tap skip trong event button
                            m_UIBattle.m_Auto.AutoOn = false;
                            m_UIBattle.m_Auto.ButtonAuto_Txt.text = "Auto: Off";
                            //  m_UIBattle.PopUpVictory.GetComponent<PopUp_Victory>().skeleton.AnimationState.SetAnimation(0, "Appear", false);
                            //   StartCoroutine(IE_delayActivePopUpVictory());
                            //  StartCoroutine(IE_ChangeAnimPopUpWin());


                        }
                    }
                }
            });
        }
        else if (ENEMYSTAT.RangeAttack == E_RangeAttack.HitFar)
        {
            skeletonGraphic.AnimationState.SetAnimation(0, "Idle", true);
            AttackEnemyWhenAllidBaseMoveToPos();
            if (m_UIBattle.enemyBased.Type == m_UIBattle.L_EnemyBaseElement[m_UIBattle.L_EnemyBaseElement.Count - 1].Type
                      && m_UIBattle.enemyBased.ID == m_UIBattle.L_EnemyBaseElement[m_UIBattle.L_EnemyBaseElement.Count - 1].ID)
            {
                if (m_UIBattle.enemyBased.HP < 1)
                {
                    m_EnemyBased = m_UIBattle.enemyBased;
                    m_UIBattle.enemyBased.JumpPower = 0;
                    m_UIBattle.GameWin();
                    if (m_UIBattle.isPartEnemy)// da giet chet con cuoi
                    {
                        m_UIBattle.LoadCatchChance(m_UIBattle.enemyBased);
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(m_UIBattle.enemyBased.Type);

                        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetTextChance(m_UIBattle.Sum_Catch.ToString() + "%"); // set text ti le

                        Debug.Log("Type: " + m_UIBattle.enemyBased.Type);
                        Debug.Log("ti le: " + m_UIBattle.Catch_Chance);
                        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetValueBar(100, m_UIBattle.Sum_Catch); // set gia tri slider
                        Debug.Log("da den !");
                        m_UIBattle.UI_Catch_Pending.GetComponent<UI_Catch_Chance>().SetCritterSkeleton(enemyStat.ICON);
                        // hien thi popUp victory
                        // tap skip trong event button
                        //   StartCoroutine(IE_delayActivePopUpVictory());
                        m_UIBattle.m_Auto.AutoOn = false;
                        m_UIBattle.m_Auto.ButtonAuto_Txt.text = "Auto: Off";
                        if (m_UIBattle.AllidBase.transform.localScale.x > 0)
                        {
                            Vector3 theScalee = transform.localScale;
                            theScalee.x *= -1;
                            transform.localScale = theScalee;
                        }
                    }
                }
            }
        }
        isAttackDone = false;
    }
    IEnumerator IE_delayActivePopUpVictory()
    {
        yield return new WaitForSeconds(0.5f);
        m_UIBattle.PopUpVictory.SetActive(true);
        m_UIBattle.PopUpVictory.GetComponent<PopUp_Victory>().skeleton.AnimationState.SetAnimation(0, "Appear", false);
        // AudioManager.instance.PlayMusic(AudioManager.instance.BG_In_Game_Music , AudioManager.instance.SoundEffectPopUpVictory);
        AudioManager.instance.BG_In_Game_Music.loop = false;
        StartCoroutine(IE_ChangeAnimPopUpWin());
    }
    IEnumerator IE_ChangeAnimPopUpWin()
    {
        yield return new WaitForSeconds(0.4f);
        m_UIBattle.PopUpVictory.GetComponent<PopUp_Victory>().skeleton.AnimationState.SetAnimation(1, "Idle", true);
    }
    public void AttackEnemyWhenAllidBaseMoveToPos()
    {
        if (!m_UIBattle.enemyBased.IsDead)
        {
            m_UIBattle.enemyBased.ATTACK_ENEMY();
        }
        else if (m_UIBattle.enemyBased.IsDead)
        {
            m_UIBattle.enemyBased.JumpDead();
        }
    }

    IEnumerator IE_MoveFlashHitNear2()
    {
        skeletonGraphic.AnimationState.SetAnimation(0, "Attack", false);
        yield return new WaitForSeconds(0.2f);
        transform.DOMove(m_UIBattle.enemyBased.StartPos.position, 0.1f).OnComplete(() =>
        {
            StartCoroutine(IE_ActiveHitEffectEnemyaBaseWithTime(TimeEffect));
            DoSomethingWhenAnimationAttack();
            skeletonGraphic.AnimationState.SetAnimation(0, "Idle", true);

        });
    }

    public void UpdateViewShadow()
    {
        Shadow.transform.position = new Vector3(this.transform.position.x, Shadow.transform.position.y, 0);
    }
    public void AddDamageAndUpdateViewEnemybaseElement()
    {
        m_UIBattle.TakeDamageEnemy(Damage, m_UIBattle.enemyBased.GetComponent<EnemyBased>());
        m_UIBattle.UpdateViewEnemyBaseElement();

        if (m_UIBattle.enemyBased.HP < 1)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectDie);
            m_UIBattle.ShakeBackGround();
            m_UIBattle.enemyBased.JumpToDeadPos();
            m_UIBattle.enemyBased.IsDead = true;
        }
    }
    IEnumerator IE_ActiveHitEffectEnemyaBaseWithTime(float Time)
    {
        yield return new WaitForSeconds(Time);
        AddDamageAndUpdateViewEnemybaseElement();
        m_UIBattle.enemyBased.SetTextDamage("- " + Damage.ToString());
        m_UIBattle.enemyBased.TextDamageAnim.SetActive(true);
        m_UIBattle.enemyBased.TextDamageAnimJump();
        m_UIBattle.enemyBased.hitEffect.transform.position = m_UIBattle.enemyBased.transform.position + new Vector3(0, 2, 0);
        m_UIBattle.enemyBased.hitEffect.SetActive(true);

        EnemyStat ENemy = Controller.Instance.GetStatEnemy(Type);
        if (ENemy.SoundEnd)
        {
            AudioManager.instance.PlaySound(ENemy.SoundEnd);
        }
        else
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectPunch1);
            }
            else
            {
                AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectPunch2);
            }
        }
    }
    public void JumpToPosDeadAllidBase()
    {
        if (HP < 1)
        {
            AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectDie);
          //  m_UIBattle.m_Auto.isTurnAutoDone = true;
            m_UIBattle.ShakeBackGround();
            m_UIBattle.DisableButton();
            transform.DOJump(PosDead.position, 300, 1, 0.3f).OnUpdate(() =>
            {
                UpdateViewShadow();
            }).OnComplete(() =>
            {
                m_UIBattle.switchAllidbase();
                if (HP > 1)
                {
                    transform.DOJump(PosStartToMove.position, 300, 1, 0.3f).OnUpdate(() =>
                    {
                        UpdateViewShadow();
                    }).OnComplete(() =>
                    {
                        StartCoroutine(IE_HiddenEffectJump());
                        m_UIBattle.EnableButton();
                        StartCoroutine(IE_delayAutoDead());
                    });
                }
            });
        }
    }
    IEnumerator IE_delayAutoDead()
    {
        yield return new WaitForSeconds(0.5f);
    //    m_UIBattle.m_Auto.isTurnAutoDone = false;
    }
    IEnumerator IE_HiddenEffectJump()
    {
        yield return new WaitForSeconds(0.2f);
        EffectJump.SetActive(false);
    }
    public void FLIP()
    {
        if (m_UIBattle.AllidBase.transform.localScale.x > 0)
        {
            Vector3 theScalee = transform.localScale;
            theScalee.x *= -1;
            transform.localScale = theScalee;
            return;
        }
        if (m_UIBattle.AllidBase.transform.localScale.x < 0)
        {
            Vector3 theScalee = transform.localScale;
            theScalee.x *= -1;
            transform.localScale = theScalee;
            return;
        }

    }
    private void OnDisable()
    {
        isAttackDone = false;
        hitEffect.gameObject.SetActive(false);
    }
}
