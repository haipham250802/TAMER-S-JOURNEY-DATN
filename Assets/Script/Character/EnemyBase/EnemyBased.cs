using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;
using Spine;
public class EnemyBased : MonoBehaviour
{
    [SerializeField] private AnimationCurve _animCurveJump, _animCurveMove;

    public GameObject parentEnemy;
    public GameObject shadow;

    const string attackName = "Attack";
    const string MoveName = "Move";
    const string IdleName = "Idle";

    public int Speed;
    public int SpeedMoveToPos;

    public float JumpPower;
    public int JumCount;
    public float Duration;
    public int HP;
    public int Damage;
    public int Rarity;
    public int ID;

    public ECharacterType Type;
    public E_RangeAttack rangeAttack;
    public UI_Battle m_UIBattle;

    public GameObject hitEffect;
    public GameObject TextDamageAnim;
    public Transform ParentCoin;

    private bool isTurnPlayer = false;

    public Transform StartPos;
    public Transform PosDead;
    public int JumpPowerDead;
    public float DurationDead;

    StateEnemyBased _StateEnemy;

    public SkeletonGraphic skeletonGraphic;
    private System.Action EventCallbackAnimationComplete;

    public float TimeEffect;
    public EditorPointSetup editorPointSetup;

    public GameObject JumpEffectStart;
    public GameObject CoinObj;
    public Transform PosJumpCoin;
    public Transform PosEndCoin;

    Tweener tweener;

    public int BeforeCoin;
    public int AfterCoin;

    public List<GameObject> L_Coin = new List<GameObject>();
    private void Start()
    {
        hitEffect.SetActive(false);
        TextDamageAnim.SetActive(false);
    }
    private void OnEnable()
    {
        StartCoroutine(IE_delayEnable());
    }
    IEnumerator IE_delayEnable()
    {
        yield return null;
        BeforeCoin = DataPlayer.GetCoin();
        if (skeletonGraphic.skeletonDataAsset)
            skeletonGraphic.AnimationState.SetAnimation(0, "Idle", false);
        /*CoinObj.AddComponent<Canvas>();
        CoinObj.GetComponent<Canvas>().overrideSorting = true;
        CoinObj.GetComponent<Canvas>().sortingLayerName = "UI";
        CoinObj.GetComponent<Canvas>().sortingOrder = 21000;*/
    }
    public bool IsMoveToPosCatched = false;
    public Transform TextDamagePos;
    public void SetTextDamage(string Damage)
    {
        TextDamageAnim.GetComponent<Text>().text = Damage;
    }
    public void TextDamageAnimJump()
    {
        TextDamageAnim.transform.DOJump(TextDamagePos.position, 0.5f, 3, 0.5f).OnComplete(() =>
        {
            TextDamageAnim.transform.position = TextDamagePos.position;
            TextDamageAnim.gameObject.SetActive(false);
        });
    }
    public bool isJumStartPosWhenDieEnemy = false;
    public void JumDead()
    {
        hitEffect.SetActive(false);
        m_UIBattle.DisableButton();
        transform.DOJump(PosDead.position, JumpPowerDead, 1, DurationDead).SetUpdate(true).OnComplete(() =>
        {
            StartCoroutine(IE_SwitchAndJumStartPos());
        });
        FallCoinWhenEnemyDie();
    }
    IEnumerator IE_SwitchAndJumStartPos()
    {
        yield return new WaitForSeconds(0.1f);
        m_UIBattle.switchEnemy();
        if (this.HP > 0)
            m_UIBattle.enemyBased.JumpStartPosWhenDead();
        isJumStartPosWhenDieEnemy = true;
    }

    public int StartCoin;
    bool IsPlaySoundCoin = false;
    int Coin;
    public int CoinDrop;
    public void FallCoinWhenEnemyDie()
    {
        StartCoin = DataPlayer.GetCoin();
        int Curcoin = DataPlayer.GetCoin();
        for (int i = 0; i < Controller.Instance.dataCoinDrop.L_critter.Count; i++)
        {
            if (Controller.Instance.dataCoinDrop.L_critter[i].Star == Rarity)
            {
                Coin = Controller.Instance.dataCoinDrop.L_critter[i].Coin;
                CoinDrop += Coin;
                Debug.Log("cooin: " + Coin);
                break;
            }
        }
        CoinObj.SetActive(true);
        int n = 25;
        for (int i = 0; i < n; i++)
        {
            GameObject obj = Instantiate(CoinObj);

            L_Coin.Add(obj);
            int RandX = Random.Range(-2, 2) * 50;
            int RandY = Random.Range(0, 3) * 50;

            obj.transform.SetParent(ParentCoin, false);
            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 120);
            obj.GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
            obj.GetComponent<RectTransform>().localPosition = new Vector2(200, -110);
            obj.transform.DOJump(new Vector3(PosJumpCoin.transform.position.x + RandX, PosJumpCoin.transform.position.y + RandY, 0), 10, 1, 0.5f)
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .OnComplete(() =>
            {
                if (this.gameObject.activeInHierarchy)
                {
                    StartCoroutine(IE_MoveCoinToUICoin(obj, Coin, StartCoin, Curcoin));
                }
            });
            Curcoin += Coin;
            if (i + 1 == n)
            {
                AudioManager.instance.MuteSound();
                IsPlaySoundCoin = false;
            }
        }
        AfterCoin = Curcoin;
        Debug.Log("BeforeCoin: " + BeforeCoin);
        Debug.Log("AfterCoin: " + AfterCoin);
        StartCoroutine(IE_AnimCoin(Coin, StartCoin, 0));
    }
    IEnumerator IE_MoveCoinToUICoin(GameObject obj, int Coin, int StartCoin, int CurCoin)
    {
        yield return new WaitForSeconds(0.5f);
        float duration = Random.Range(0.2f, 0.9f);
        obj.transform.DOMove(PosEndCoin.position, duration).OnStart(() =>
        {
            if (!IsPlaySoundCoin)
            {
                IsPlaySoundCoin = true;
                AudioManager.instance.PlaySound(AudioManager.instance.SoundEffectGetCoin);
            }
        }).OnComplete(() =>
        {
            Destroy(obj);
        });
    }
    IEnumerator IE_AnimCoin(int Coin, int StartCoin, int CurCoin)
    {
        yield return new WaitForSeconds(1f);
        UI_Home.Instance.m_UICoinManager.SetTextCoin(Coin);
        AfterCoin = DataPlayer.GetCoin();
    }
    public bool isTurnEnemy;
    public void JumpStartPosWhenDead()
    {
        shadow.gameObject.SetActive(true);
        transform.DOJump(StartPos.position, JumpPower, JumCount, Duration - 0.3f).OnUpdate(() =>
        {
            UpdatePosShadow();
        }).OnComplete(() =>
        {
            /*  if (AudioManager.instance.SoundJump)
                  AudioManager.instance.PlaySound(AudioManager.instance.SoundJump);*/
            CoinObj.transform.position = this.transform.position;
            JumpEffectStart.SetActive(true);
            StartCoroutine(IE_HiddenJumpEffect());
            skeletonGraphic.AnimationState.SetAnimation(0, "Idle", true);
        });
    }
    public void JumpStartPos()
    {
        Debug.Log("Jum Start");
        m_UIBattle.ShakeBackGround();
        shadow.gameObject.SetActive(true);

        if (rangeAttack == E_RangeAttack.HitFar || rangeAttack == E_RangeAttack.HitNear)
        {
            transform.DOJump(StartPos.position, JumpPower, JumCount, DurationMoveToStartPos - 0.1f).OnUpdate(() =>
            {
                UpdatePosShadow();
            }).OnComplete(() =>
            {
            //    m_UIBattle.m_Auto.isTurnAutoDone = false;

                CoinObj.transform.position = this.transform.position;
                JumpEffectStart.SetActive(true);
                if (AudioManager.instance.SoundJump)
                    AudioManager.instance.PlaySound(AudioManager.instance.SoundJump);
                /*for (int i = 0; i < m_UIBattle.L_AllidELements.Count; i++)
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
                if (this.gameObject.activeInHierarchy)
                    StartCoroutine(IE_delay());
                m_UIBattle.ActiveTutorial();
            });
        }
        else
        {
            transform.DOMove(StartPos.position + new Vector3(-1, 0, 0), 0.2f).OnUpdate(() =>
            {
                UpdatePosShadow();
            }).OnComplete(() =>
            {
                transform.DOMove(StartPos.position, 0.2f).OnUpdate(() =>
                {
                    UpdatePosShadow();
                }).OnComplete(() =>
                {
                 //   m_UIBattle.m_Auto.isTurnAutoDone = false;

                    if (AudioManager.instance.SoundJump)
                        AudioManager.instance.PlaySound(AudioManager.instance.SoundJump);
                    /* for (int i = 0; i < m_UIBattle.L_AllidELements.Count; i++)
                     {
                         if (m_UIBattle.L_AllidELements[i] != null)
                         {
                             if (m_UIBattle.L_AllidELements[i].HP > 0)
                             {
                                // m_UIBattle.EnableButton(i + 1);
                             }
                         }
                     }*/
                    m_UIBattle.EnableButton();
                    if (this.gameObject.activeInHierarchy)
                        StartCoroutine(IE_delay());
                    m_UIBattle.ActiveTutorial();
                });

            });
        }
        skeletonGraphic.AnimationState.SetAnimation(0, "Idle", true);
    }
    IEnumerator IE_delay()
    {
        yield return new WaitForSeconds(1f);
      //  m_UIBattle.m_Auto.isTurnAutoDone = false;
        JumpEffectStart.SetActive(false);
    }
    public void UpdatePosShadow()
    {
        shadow.transform.position = new Vector3(this.transform.position.x, shadow.transform.position.y, 0);
    }
    public void hiddenShadow()
    {
        shadow.gameObject.SetActive(false);
    }
    public void MoveToPosCatchedCritter()
    {
        /*  transform.DOMove(m_UIBattle.PosEnemyWhenPartEnemy.transform.position, Speed).SetSpeedBased(true).OnComplete(() =>
          {
              m_UIBattle.CatchEnemy(this);
          });*/
    }
    public void PlayAnimation(string _animationName, bool _Loop, System.Action _animationCallback)
    {
        EventCallbackAnimationComplete = null;
        skeletonGraphic.AnimationState.SetAnimation(0, _animationName, _Loop);

        if (_animationCallback == null) return;
        if (skeletonGraphic.AnimationState.GetCurrent(0).Animation.Name == attackName)
        {
            EventCallbackAnimationComplete = _animationCallback;
            skeletonGraphic.AnimationState.Complete += AnimationState_Complete;
        }
        else
        {
            skeletonGraphic.AnimationState.Complete -= AnimationState_Complete;
        }
    }
    public void AnimationState_Complete(TrackEntry _trackEntry)
    {
        if (_trackEntry.Animation.Name == attackName)
        {
            EventCallbackAnimationComplete?.Invoke();
            skeletonGraphic.AnimationState.Complete -= AnimationState_Complete;
        }
    }
    public void SetData(SkeletonDataAsset _data)
    {
        skeletonGraphic.skeletonDataAsset = null;
        skeletonGraphic.skeletonDataAsset = _data;
        skeletonGraphic.Initialize(true);
    }
    public void init(EnemyBaseElement enemyBaseElement)
    {
        Type = enemyBaseElement.Type;
        Damage = enemyBaseElement.Damage;
        HP = enemyBaseElement.HP;
        ID = enemyBaseElement.ID;
        Rarity = enemyBaseElement.Rarity;
        skeletonGraphic.skeletonDataAsset = enemyBaseElement.ICON;

        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(Type);

        rangeAttack = enemyStat.RangeAttack;
    }
    private void Update()
    {
        if (HP < 1)
        {
            HP = 0;
        }
    }
    public void SetData(EnemyBaseElement enemyBaseElement)
    {
        this.skeletonGraphic.skeletonDataAsset = null;
        this.init(enemyBaseElement);
        this.skeletonGraphic.Initialize(true);
    }

    public bool IsDead;

    private void KillTweener()
    {
        if (tweener == null)
            return;
        tweener.Kill();
        tweener = null;
    }

    IEnumerator IE_ActiveEffectWithTime(float Time)
    {
        yield return new WaitForSeconds(Time);
        if (m_UIBattle.AllidBase != null)
        {
            m_UIBattle.AllidBase.GetComponent<AllidBase>().hitEffect.transform.position = m_UIBattle.AllidBase.transform.position + new Vector3(0, 2, 0);
            m_UIBattle.AllidBase.GetComponent<AllidBase>().hitEffect.SetActive(true);
            m_UIBattle.AllidBase.GetComponent<AllidBase>().SetTextDamage("- " + Damage.ToString());
            m_UIBattle.AllidBase.GetComponent<AllidBase>().TextDamageAnimJump();
            m_UIBattle.TakeDamageAllid(Damage, m_UIBattle.AllidBase.GetComponent<AllidBase>());
            m_UIBattle.AllidBase.GetComponent<AllidBase>().JumpToPosDeadAllidBase();
            m_UIBattle.UpdateViewAllidBaseElement();

            m_UIBattle.GameOver();
            if (gameObject.activeInHierarchy)
                StartCoroutine(IE_HiddenEffectHitAllid());

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
    }
    public void DoSomthingWhenAttack()
    {
        KillTweener();
        m_UIBattle.TakeDamageAllid(Damage, m_UIBattle.AllidBase.GetComponent<AllidBase>());
        m_UIBattle.UpdateViewAllidBaseElement();

        m_UIBattle.GameOver();
        StartCoroutine(IE_MoveToStartPos());

    }
    public int JumpPowerMoveToStartPos;
    public int JumpCountMoveToStartPos;
    public float DurationMoveToStartPos;

    IEnumerator IE_MoveToStartPos()
    {
        yield return null;
        skeletonGraphic.AnimationState.SetAnimation(0, "Move", true);

        if (transform.localScale.x > 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        if (rangeAttack == E_RangeAttack.HitNear)
        {
            m_UIBattle.switchAllidbase();
            transform.DOJump(StartPos.transform.position, JumpPowerMoveToStartPos, JumpCountMoveToStartPos, DurationMoveToStartPos)
                .SetUpdate(true)
                 .OnUpdate(() =>
                 {
                     UpdatePosShadow();
                 })
                .OnComplete(() =>
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;

                skeletonGraphic.AnimationState.SetAnimation(0, IdleName, true);
                isTurnPlayer = true;
                isButton = true;
                if (m_UIBattle.enemyBased.gameObject.activeInHierarchy)
                { StartCoroutine(IE_PlayAnimationIdle()); }
                shadow.transform.position = new Vector3(this.transform.position.x, shadow.transform.position.y, 0);
                shadow.SetActive(true);
                m_UIBattle.EnableButton();
            });
        }
        else if (rangeAttack == E_RangeAttack.HitNear2)
        {
            m_UIBattle.switchAllidbase();

            transform.DOMove(StartPos.position, 0.5f).OnUpdate(() =>
            {
                UpdatePosShadow();
            }).OnComplete(() =>
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;

                skeletonGraphic.AnimationState.SetAnimation(0, IdleName, true);
                isTurnPlayer = true;
                isButton = true;
                if (m_UIBattle.enemyBased.gameObject.activeInHierarchy)
                { StartCoroutine(IE_PlayAnimationIdle()); }

                shadow.transform.position = new Vector3(this.transform.position.x, shadow.transform.position.y, 0);
                shadow.SetActive(true);
                m_UIBattle.EnableButton();
            });
        }
        else if (rangeAttack == E_RangeAttack.HitFar)
        {
            transform.position = StartPos.position;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            isTurnPlayer = true;
            skeletonGraphic.AnimationState.SetAnimation(0, IdleName, true);
            isButton = true;
            if (m_UIBattle.enemyBased.gameObject.activeInHierarchy)
            { StartCoroutine(IE_PlayAnimationIdle()); }
            m_UIBattle.switchAllidbase();
            m_UIBattle.EnableButton();
        }
    }

    IEnumerator IE_HiddenEffectHitAllid()
    {
        yield return new WaitForSeconds(0.2f);
        m_UIBattle.AllidBase.GetComponent<AllidBase>().hitEffect.SetActive(false);
    }
    IEnumerator IE_PlayAnimationIdle()
    {
        yield return null;
        if (skeletonGraphic != null)
            skeletonGraphic.AnimationState.SetAnimation(0, IdleName, true);
    }

    bool isButton = false;
    public void SetTurnPlayerBased(bool isTurnPlayer)
    {
        this.isTurnPlayer = isTurnPlayer;
    }
    public bool GetTurnPlayerBased()
    {
        return isTurnPlayer;
    }
    public void SetBtn(bool isBtn)
    {
        isButton = isBtn;
    }
    public bool IsBtn()
    {
        return isButton;
    }
    // *****************************
    IEnumerator IE_delay(float time, EnemyStat enemyStat)
    {
        yield return new WaitForSeconds(time);
        AudioManager.instance.PlaySound(enemyStat.SoundStart);
    }
    IEnumerator IE_delaySoundStart2(float time, EnemyStat enemyStat)
    {
        yield return new WaitForSeconds(time);
        AudioManager.instance.PlaySound(enemyStat.SoundStart2);
    }
    public void ATTACK_ENEMY()
    {
        Debug.Log("da damnh");
        JumpEffectStart.SetActive(false);
        if (m_UIBattle.AllidBase != null)
        {
           // m_UIBattle.m_Auto.isTurnAutoDone = true;
            if (HP > 0)
            {
                parentEnemy.transform.SetAsLastSibling();
                EnemyStat enemyStat = Controller.Instance.GetStatEnemy(Type);
                TimeEffect = enemyStat.TimeEffect;
                Vector3 Offset = enemyStat.Offset;
                if (enemyStat.SoundStart2)
                {
                    StartCoroutine(IE_delaySoundStart2(enemyStat.TimeSoundStart2, enemyStat));
                }
                if (enemyStat.SoundStart)
                {
                    StartCoroutine(IE_delay(enemyStat.TimeSoundStart, enemyStat));
                }
                if (rangeAttack == E_RangeAttack.HitNear)
                {

                    transform.DOJump(m_UIBattle.EnemyMoveToPosAllidBase.position - Offset, 6, JumCount, 0.35f)
                      .OnUpdate(() =>
                      {
                          UpdatePosShadow();
                      })
                      .SetEase(Ease.Linear)
                      .OnComplete(() =>
                      {
                          if (this.gameObject.activeInHierarchy)
                          {
                              PlayAnimation("Attack", false, DoSomethingWhenAnimationAttack);
                              StartCoroutine(IE_ActiveEffectWithTime(TimeEffect));
                          }

                      });
                }
                else if (rangeAttack == E_RangeAttack.HitNear2)
                {
                    skeletonGraphic.AnimationState.SetAnimation(0, "Move", true);
                    // EnemyStat enemyStat = Controller.Instance.GetStatEnemy(Type);
                    TimeEffect = enemyStat.TimeEffect;

                    transform.DOMove(m_UIBattle.AllidMoveToPosEnemyBase.position + new Vector3(-2, 0, 0), 0.25f).SetEase(Ease.Linear)
                        .OnUpdate(() =>
                        {
                            UpdatePosShadow();
                        })
                        .OnComplete(() =>
                        {
                            StartCoroutine(IE_MoveFlashHitNear2());
                        });
                }
                else if (rangeAttack == E_RangeAttack.HitFar)
                {
                    //  EnemyStat enemyStat = Controller.Instance.GetStatEnemy(Type);
                    TimeEffect = enemyStat.TimeEffect;
                    if (this.gameObject.activeInHierarchy)
                    {
                        StartCoroutine(IE_AttackHitFar());
                        StartCoroutine(IE_ActiveEffectWithTime(TimeEffect));
                    }

                }
            }
        }

    }
    IEnumerator IE_AttackHitFar()
    {
        yield return null;
        PlayAnimation("Attack", false, DoSomethingWhenAnimationAttack);

    }
    public void JumpDead()
    {
        SwitchEnemyAndJumpToStartPos();
    }
    public void JumpToDeadPos()
    {
        transform.DOJump(PosDead.position, 5, 1, DurationDead).SetUpdate(true).OnUpdate(() =>
        {
            UpdatePosShadow();
        });
        FallCoinWhenEnemyDie();
    }
    public void SwitchEnemyAndJumpToStartPos() // thay doi enemy
    {
        m_UIBattle.switchEnemy();
        if (this.HP > 0)
            m_UIBattle.enemyBased.JumpToStartPosWhenDead();
    }
    public void JumpToStartPosWhenDead() //con thu +1 nhay den vi tri bat dau sau khi chet
    {
        if (transform.localScale.x < 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
        transform.DOJump(StartPos.position, 5, JumCount, 0.2f).OnComplete(() =>
        {
            if (this.gameObject.activeInHierarchy)
            {
                JumpEffectStart.SetActive(true);
                StartCoroutine(IE_HiddenJumpEffect());
                StartCoroutine(IE_AttackEnemy());
            }
            UpdatePosShadow();
        });
    }
    IEnumerator IE_AttackEnemy()
    {
        yield return new WaitForSeconds(0.1f);
        ATTACK_ENEMY();
        IsDead = false;
    }
    public void DoSomethingWhenAnimationAttack()
    {
        if (rangeAttack == E_RangeAttack.HitNear)
        {
            FLIP();
            transform.DOJump(StartPos.position, 3, 3, 0.3f).OnUpdate(() =>
            {
                UpdatePosShadow();
            }).OnComplete(() =>
            {
                FLIP();
                m_UIBattle.EnableButton();
            //    m_UIBattle.m_Auto.isTurnAutoDone = false;
                GetStateIdle();
            });
        }
        else if (rangeAttack == E_RangeAttack.HitNear2)
        {
            FLIP();
            transform.DOMove(StartPos.position, 0.3f)
                .OnUpdate(() =>
                {
                    UpdatePosShadow();
                })
                .OnComplete(() =>
            {
                FLIP();
                m_UIBattle.EnableButton();
            //    m_UIBattle.m_Auto.isTurnAutoDone = false;
                GetStateIdle();
            });
        }
        else if (rangeAttack == E_RangeAttack.HitFar)
        {
            m_UIBattle.EnableButton();
          //  m_UIBattle.m_Auto.isTurnAutoDone = false;
            GetStateIdle();
        }
    }

    IEnumerator IE_MoveFlashHitNear2()
    {
        skeletonGraphic.AnimationState.SetAnimation(0, "Attack", false);
        yield return new WaitForSeconds(0.2f);
        transform.DOMove(m_UIBattle.AllidBase.GetComponent<AllidBase>().PosStartToMove.position, 0.1f).OnComplete(() =>
        {
            StartCoroutine(IE_ActiveEffectWithTime(TimeEffect));
            DoSomethingWhenAnimationAttack();
            skeletonGraphic.AnimationState.SetAnimation(0, "Idle", true);
        });
    }

    public void GetStateIdle()
    {
        skeletonGraphic.AnimationState.SetAnimation(0, "Idle", true);
    }
    IEnumerator IE_HiddenJumpEffect()
    {
        yield return new WaitForSeconds(0.2f);
        JumpEffectStart.SetActive(false);
    }
    public void FLIP()
    {
        if (transform.localScale.x < 0)
        {
            Vector3 theScalee = transform.localScale;
            theScalee.x *= -1;
            transform.localScale = theScalee;
            return;
        }
        if (transform.localScale.x > 0)
        {
            Vector3 theScalee = transform.localScale;
            theScalee.x *= -1;
            transform.localScale = theScalee;
            return;
        }
    }
    private void OnDisable()
    {
        if(transform.localScale.x < 0)
        {
            Vector3 thescale = transform.localScale;
            thescale.x *= -1;
            transform.localScale = thescale;
        }
        StopAllCoroutines();
        DOTween.KillAll();
    }
}
public enum StateEnemyBased
{
    IDLE,
    ATTACK
}
