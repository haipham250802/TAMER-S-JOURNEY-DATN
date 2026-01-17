/*using System;
using DG.Tweening;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

#endif

public class PathEditer : MonoBehaviour
{

namespace WE.Unit.Enemy
{
    public class Enemy : GameUnit
    {
        //public Pool.PoolEnemy Pool;
        public enum PatrolType
        {
            RandomInBox,
            RandomInPath,
            Path
        }

        #region public properties

#if UNITY_EDITOR
        [GUIColor(1, 1, 0)]
        [FoldoutGroup("Test")]
        public float DamageTest = 10;

        [GUIColor(1, 1, 0)]
        [FoldoutGroup("Test")]
        public KeyCode testDamageKey = KeyCode.Space;

        [GUIColor(0, 1, 0)]
        [FoldoutGroup("Debug")]
        public bool ShowDebug = true;

#endif



        [InfoBox(
            "Game object to do transformation(move , rotate, scale) if needed\nCan be unit's transform, or weapon container")]
        public Transform Transformer = null;


        //public bool isBoss;

        private bool saveInGroup;

        #endregion

        [GUIColor(0.5f, 1, 1)] public BehaviourTreeOwner _BehaviourTreeOwner;

        [SerializeField]
        [Required]
        [GUIColor(0, 1, 0)]
        public EnemyStats stats;

        [HideInInspector] public float Damage = 0f;

        [HideInInspector] public int Level = 0;

        [HideInInspector] public int SkillLevel = 0;

        //[HideInInspector]
        //public bool CanShoot = false;


        [FoldoutGroup("Campaign")]
        public bool canScanInMapCampagin = true;
        [HideInInspector] public float MoveSpeed;

        [HideInInspector] public bool Attacked = false;

        [GUIColor(0, 1, 1)] public float RotateSpeed = 5;
        [GUIColor(0, 1, 1)] public float DetectRange = 7;
        [GUIColor(0, 1, 1)] public float AttackRange = 5;
        [GUIColor(0, 1, 1)] public float ChaseRange = 7;


        [HideInInspector] public bool IsRotating = false;

        [HideInInspector] public bool IsMoving = false;

        [HideInInspector] public bool InGroup;

        [HideInInspector] public bool IsReady = false;

        [HideInInspector] public bool AutoReady = true;



        [HideInInspector] public bool IsAttacking = false;

        [HideInInspector] public bool Hit = false;

        [GUIColor(1, 0.5f, 0.5f)] public BaseMover Mover = null;

        [FoldoutGroup("Hold Region")]
        [GUIColor(1, 0.4f, 0.4f)]
        public Vector3 StartPosition;

        [FoldoutGroup("Hold Region")]
        [GUIColor(1, 0.4f, 0.4f)]
        public float HoldRegionRadius = 5;

        [ListDrawerSettings(ShowIndexLabels = true)]
        [GUIColor(1, 1, 0)]
        [InfoBox("Unit's skills to use")]
        public BaseSkillAction[] Skills;

        //[ListDrawerSettings(ShowIndexLabels = true)]
        //public BaseSkillAction[] Actions;

        [FoldoutGroup("Events")] public UnityEvent OnStart;

        [FoldoutGroup("Events")] public UnityEvent OnDie;

        [FoldoutGroup("Events")] public UnitHpChangedEvent OnHpChanged;

        [FoldoutGroup("Events")] public UnityEvent OnNeutral;



        [FoldoutGroup("Patrol")] public PatrolType patrolType = PatrolType.RandomInBox;

        [FoldoutGroup("Patrol")]
        [HideIf("patrolType", PatrolType.RandomInBox)]
        [ListDrawerSettings(ShowIndexLabels = true)]
        public Vector3[] PatrolPath;

        [FoldoutGroup("Patrol")]
        [ShowIf("patrolType", PatrolType.RandomInBox)]
        public Rect PatrolBox = new Rect(-2, -2, 4, 4);

        [FoldoutGroup("Patrol")]
        [HideIf("patrolType", PatrolType.RandomInBox)]
        public bool IsClosedPath = true;



        protected bool _die = false;

        protected bool _needInitStats = true;

        protected Vector3 _StartPosition;

        protected Rect _PatrolBox;

        protected Vector3[] _PatrolPath;

        protected bool _SavePositions = false;


        [FoldoutGroup("Hold Region")]
        [Button("Reset Start Position")]
        [GUIColor(0, 1, 0)]
        void ResetStartPosition()
        {
            StartPosition = Vector3.zero;
        }

        [FoldoutGroup("Patrol")]
        [Button("Reset Patrol Box")]
        [GUIColor(0, 1, 0)]
        void ResetPatrolBox()
        {
            PatrolBox = PatrolBox.SetCenter(Vector2.zero);
        }

        BaseTank player;
        public bool FoundTarget = false;
        protected virtual void Start()
        {
            if (Target == null)
            {
                if (GameplayManager.Instance != null)
                    Target = GameplayManager.Instance.CurrenTank.transform;
                else
                {
                    var player = GameObject.Find("Player");
                    if (player != null)
                    {
                        Target = player.transform;
                    }
                }
            }
            //if (!IsInitialize)
            //{
            //    Init();
            //}
            if (!_SavePositions)
            {
                SavePositionsData();
            }

        }

        public virtual void CheckPayload()
        {
            // tranh ban log trong scene BRImporter va map test
            if (GameplayManager.Instance != null)
            {
                if (GameManager.Instance.CurrentModeGameplay == EModeGamePlay.ESCORT)
                {
                    if (FoundTarget) return;
                    if (GameplayManager.Instance.CurrenTank == null)
                        return;
                    if (player == null) player = GameplayManager.Instance.CurrenTank;
                    var a = player != null;
                    var b = WE.Unit.Payload.Instance != null;

                    if (a && !b) Target = player.transform;
                    else if (!a && b) Target = WE.Unit.Payload.Instance.transform;
                    else if (a && b)
                    {
                        if (ntDev.Ez.GetEdistanceSqr(transform.position, player.transform.position) > ntDev.Ez.GetEdistanceSqr(transform.position, WE.Unit.Payload.Instance.transform.position, 1))
                            Target = WE.Unit.Payload.Instance.transform;
                        else Target = player.transform;
                    }
                }
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            IsReady = false;
            transform.DOKill();
            InGroup = false;
            //if (GameplayManager.Instance != null)
            //    GameplayManager.Instance.RemoveEnemy(ID);
            StopAllCoroutines();

            OnStart.RemoveAllListeners();
            OnDie.RemoveAllListeners();
            OnHpChanged.RemoveAllListeners();
            OnNeutral.RemoveAllListeners();
            //init = false;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            //if (GameplayManager.Instance != null)
            //    GameplayManager.Instance.AddEnemy(ID, this);
            if (!IsInitialize)
            {
                Init();
            }

            saveInGroup = InGroup;
            Level = 0;
            SkillLevel = 0;
            //isAlive = true;
            OnStart.Invoke();
            _die = false;
            Attacked = false;
            _needInitStats = true;
        }

        #region public methods

        public void SetNeedInitStats(bool value)
        {
            _needInitStats = value;
        }

        public void LeftGroup()
        {
            saveInGroup = InGroup;
            InGroup = false;
        }

        public void JoinGroup()
        {
            InGroup = saveInGroup;
        }

        public virtual float GetTimeExecuteSkill(int skill)
        {
            if (skill > -1 && skill < Skills.Length)
                if (Skills[skill].TimeExecuteSkill > 0f)
                    return Skills[skill].TimeExecuteSkill;

            return 0f;
        }

        public virtual void Attack()
        {
            IsAttacking = true;
            Skills[SkillLevel].Play();
        }

        public virtual void Attack(Action<object> callback)
        {
            IsAttacking = true;
        }

        public virtual void Attack(Action<object> callback, int level)
        {
            IsAttacking = true;
        }

        public virtual void Attack(int skill)
        {
            IsAttacking = true;
            if (skill > -1)
            {
                SkillLevel = skill;
            }

            Skills[SkillLevel].Play();
        }

        public virtual void AttackWarning(bool show)
        {
            Skills[SkillLevel].Warning(show);
        }

        public virtual void AttackWarning(int skill, bool show)
        {
            Skills[skill].Warning(show);
        }

        public virtual void StopAttack()
        {
            IsAttacking = false;
            StopAttack(SkillLevel);
        }

        public virtual void StopAttack(int skill)
        {
            IsAttacking = false;
            if (skill > -1 && skill < Skills.Length)
                Skills[skill].Stop();
        }

        public override void Pause()
        {
            if (_BehaviourTreeOwner != null)
            {
                _BehaviourTreeOwner.enabled = false;
            }

            IsPausing = true;
            transform.DOPause();
            Mover?.Pause();
        }

        public override void Resume()
        {
            if (_BehaviourTreeOwner != null)
            {
                _BehaviourTreeOwner.enabled = true;
            }

            IsPausing = false;
            transform.DOTogglePause();
            Mover?.Resume();
        }

        public override void Die()
        {
            if (_die)
            {
                return;
            }

            _die = true;

            IsInitialize = false;
            base.Die();
            OnDie?.Invoke();
            Despawn();
        }

        public override void Despawn(float delay = 0)
        {
            Utils.Utils.Despawn(gameObject);
            //SimplePool.in.Despawn(gameObject);
        }

        public override void Init()
        {
            base.Init();
            FoundTarget = false;
            if (stats != null && _needInitStats)
            {
                InitWithStats(stats);
            }

            if (Target != null && GameplayManager.Instance != null)
            {
                if (GameplayManager.Instance.CurrenTank != null)
                {
                    Target = GameplayManager.Instance.CurrenTank.transform;
                }
                else
                {
                    StartCoroutine(WaitForFindTarget());
                }
            }

            //#if UNITY_EDITOR
            //            if (SceneManager.GetActiveScene().name != Constant.SCENE_GAMEPLAY)
            //            {
            //                currentHP = hp = TestHP;
            //            }

            //#endif
            //init = true;
            //#if GAME_ROCKET
            //            if(GameManager.Instance != null &&GameManager.Instance.isTesting && GameplayManager.Instance != null)
            //            {
            //                Debug.Log("[Enemy]: Wave: " + Player.Instance.CurrentWave + " | Name: " + this.name + " | Hp: " + this.HP + " | Damage: " + this.Damage);
            //            }
            //#endif

            CheckPayload();

            SetupPositions();

            IsInitialize = true;
        }

        public virtual void SavePositionsData()
        {
            _StartPosition = StartPosition;
            if (patrolType == PatrolType.RandomInBox)
            {
                _PatrolBox = PatrolBox;
            }
            else
            {
                _PatrolPath = new Vector3[PatrolPath.Length];
                Array.Copy(PatrolPath, _PatrolPath, PatrolPath.Length);
            }

            _SavePositions = true;
        }

        public void SetupPositions()
        {
            if (!_SavePositions)
            {
                SavePositionsData();
            }
            Vector3 pos = transform.position;
            StartPosition = _StartPosition + (Vector3)pos;
            if (patrolType == PatrolType.RandomInBox)
            {
                PatrolBox = _PatrolBox.SetCenter(_PatrolBox.center + (Vector2)pos);
            }
            else
            {
                for (int i = 0; i < _PatrolPath.Length; i++)
                {
                    PatrolPath[i] = _PatrolPath[i] + pos;
                }
            }
        }

        public void InitWithStats(EnemyStats stats)
        {
            var multiplier = GameMultiplier.GetInstance();

            //#if UNITY_EDITOR
            //            if (GameplayManager.Instance != null && GameManager.Instance != null && GameManager.Instance.CurrentModeGameplay == EModeGamePlay.CAMPAIGN)
            //                Helper.ShowHp_Damage_Test(this, stats.Name, stats.HP, stats.Damage, stats.Skills, multiplier);
            //#endif

            Damage = stats.Damage * multiplier.Damage;
            //MoveSpeed = stats.MoveSpeed * multiplier.MoveSpeed;
            //currentHP = hp = stats.HP * multiplier.HP;

            if (stats.Skills.Length == Skills.Length)
            {
                for (int i = 0; i < Skills.Length; i++)
                {
                    Skills[i].SetStats(stats.Skills[i], multiplier);
                }
            }

#if UNITY_EDITOR
            if (GameplayManager.Instance != null && GameManager.Instance != null && GameManager.Instance.CurrentModeGameplay == EModeGamePlay.SURVIVAL && stats != null)
            {
                var survival_enemy = DataSystem.Instance.dataSurvivalMode.GetInfoSurvivalEnemy(stats.Name);
                if (survival_enemy != null)
                    Helper.ShowHp_Damage_Test_Survival(this, stats.Name, survival_enemy.Hp, survival_enemy.Atk, stats.HP, stats.Skills[0].BulletDamage);
            }
#endif
#if UNITY_EDITOR
            if (stats != null && stats.Skills.Length == 0 && stats.name != "Power_Station")
            {
                Debug.LogError("Skills.Length is invalid! -  " + stats.Name);
            }
#endif


            MoveSpeed = stats.MoveSpeed * multiplier.MoveSpeed;
            CurrentHp = HP = stats.HP * multiplier.HP;

            _needInitStats = false;

            OnInitStats?.Invoke(this);
        }

        public virtual void Ready()
        {
            IsReady = true;
        }

        public virtual void Neutral()
        {
            OnNeutral?.Invoke();
            Skills[SkillLevel].Warning(false);
        }

        public virtual bool IsSkillAvailable(int index)
        {
            return Skills[index].CanPlay;
        }

        public virtual void Idle()
        {
            //IsMoving = false;
        }

        public override void AddCurrentHp(float addHp)
        {
            base.AddCurrentHp(addHp);
            OnHpChanged?.Invoke(CurrentHp, HP);
        }

        #endregion

        private IEnumerator WaitForFindTarget()
        {
            while (GameplayManager.Instance.CurrenTank == null)
                yield return null;
            this.Target = GameplayManager.Instance.CurrenTank.transform;
        }


        public virtual void ChangeMoveSpeed(float newMoveSpeed)
        {

        }

        public override void TakeDamage(float value, bool needCalculateReduceDamage = true)
        {
            base.TakeDamage(value);
            Attacked = true;
            OnHpChanged?.Invoke(CurrentHp, HP);
        }

        protected virtual void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(testDamageKey))
            {
                TakeDamage(DamageTest);
            }

#endif
        }
        public override void GoActive(float delay = 0)
        {
            if (_BehaviourTreeOwner == null)
            {
                try
                {
                    _BehaviourTreeOwner = GetComponent<BehaviourTreeOwner>();
                }
                catch (Exception ex)
                {
                    DebugCustom.LogError(ex);
                }
            }
            if (delay > 0)
            {
                //StartCoroutine(_GoActive(delay));
                MEC.Timing.RunCoroutine(_GoActive(delay));
            }
            else
            {
                if (_BehaviourTreeOwner != null)
                    _BehaviourTreeOwner.enabled = true;

            }

        }


        IEnumerator<float> _GoActive(float delay)
        {
            yield return MEC.Timing.WaitForSeconds(delay);//new  WaitForSeconds(delay);
            if (_BehaviourTreeOwner != null)
                _BehaviourTreeOwner.enabled = true;

        }

        public override void GoDisable(float delay = 0)
        {
            if (delay > 0)
            {
                //StartCoroutine(_GoDisable(delay));
                MEC.Timing.RunCoroutine(_GoDisable(delay));
            }
            else
            {
                if (_BehaviourTreeOwner != null)
                    _BehaviourTreeOwner.enabled = false;

            }
        }

        IEnumerator<float> _GoDisable(float delay)
        {
            yield return MEC.Timing.WaitForSeconds(delay);//new WaitForSeconds(delay);
            if (_BehaviourTreeOwner != null)
                _BehaviourTreeOwner.enabled = false;

        }




#if UNITY_EDITOR
        protected GUIStyle style = new GUIStyle();

        protected string typeName;

        protected virtual void OnDrawGizmosSelected()
        {
            Vector3 pos = Vector3.zero;
            Vector3 rootPos = transform.position;
            Handles.color = Color.red;

            if (!EditorApplication.isPlaying)
            {
                pos = rootPos;
            }

            style.normal.textColor = Color.white;
            Handles.color = new Color(1, 1, 0, 0.5f);
            Handles.DrawSolidDisc(rootPos, Vector3.forward, 0.2f);
            style.normal.textColor = Color.green;
            Handles.Label(rootPos, name, style);

            //Start Position
            Vector3 startPosition = pos + StartPosition;
            Handles.color = Color.magenta;
            Handles.DrawLine(pos, startPosition);
            Handles.Label(startPosition, $"{name} - Start Position");
            //Hold Region
            Handles.color = Color.red;
            Handles.DrawLine(startPosition, new Vector3(startPosition.x + HoldRegionRadius, startPosition.y));
            Handles.DrawWireDisc(startPosition, Vector3.forward, HoldRegionRadius);

            if (patrolType != Enemy.PatrolType.RandomInBox && PatrolPath.Length > 0)
            {
                var patrolPath = PatrolPath;
                Handles.color = Color.cyan;
                if (PatrolPath.Length > 0)
                {
                    Handles.Label(pos + patrolPath[0] + new Vector3(0, 0.3f),
                        name + " PatrolPath");
                    style.normal.textColor = Color.red;

                    if (patrolPath.Length > 1)
                    {
                        for (int i = 0; i < patrolPath.Length - 1; i++)
                        {
                            var newPos = pos + patrolPath[i];
                            Handles.Label(newPos + new Vector3(-0.1f, -0.1f), i.ToString(), style);
                            Handles.DrawLine(newPos, pos + patrolPath[i + 1]);

                        }

                        if (IsClosedPath)
                        {
                            Handles.DrawLine(pos + patrolPath[patrolPath.Length - 1], pos + patrolPath[0]);
                        }
                        Handles.Label(pos + patrolPath[patrolPath.Length - 1] + new Vector3(-0.1f, -0.1f), (patrolPath.Length - 1).ToString(), style);
                    }
                    else
                    {
                        Handles.Label(pos + patrolPath[0] + new Vector3(-0.1f, -0.1f), "0", style);
                    }
                }

            }
            else
            {
                Handles.color = Color.cyan;
                var box = PatrolBox.SetCenter(PatrolBox.center + (Vector2)pos);
                Handles.DrawWireCube(box.center, box.size);
                Handles.Label(box.min, name + " - Min PatrolBox");
                Handles.Label(box.max, name + " - Max PatrolBox");

            }

        }
#endif
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(Enemy)), CanEditMultipleObjects]
    public class EnemyEditor : OdinEditor
    {

        protected GUIStyle style = new GUIStyle();

        protected string typeName;



        // register an event handler when the class is initialized
        //static EnemyEditor()
        //{
        //    EditorApplication.playModeStateChanged += LogPlayModeState;
        //}

        //private static void LogPlayModeState(PlayModeStateChange state)
        //{

        //}
        protected override void OnEnable()
        {
            style.normal.textColor = Color.white;
            typeName = nameof(Enemy);
        }

        protected void DrawEnemyInfo(Enemy _target)
        {
            DrawButtons(_target);
            if (!Application.isPlaying)
            {
                Vector3 pos = _target.transform.position;

                _target.StartPosition = Handles.PositionHandle(_target.StartPosition + pos, Quaternion.identity) - pos;
                if (_target.patrolType == Enemy.PatrolType.RandomInBox)
                {
                    var box = _target.PatrolBox.SetCenter(_target.PatrolBox.center + (Vector2)pos);
                    var min = Handles.PositionHandle(box.min, Quaternion.identity);
                    var max = Handles.PositionHandle(box.max, Quaternion.identity);
                    if (max.x > min.x && max.y > min.y)
                    {
                        _target.PatrolBox.Set(min.x - pos.x, min.y - pos.y, max.x - min.x, max.y - min.y);
                    }
                }
                else
                {
                    for (int i = 0; i < _target.PatrolPath.Length; i++)
                    {
                        _target.PatrolPath[i] = Handles.PositionHandle(_target.PatrolPath[i] + pos, Quaternion.identity) - pos;
                    }

                }
            }

        }


        protected void DrawButtons(Enemy _target)
        {
            Handles.BeginGUI();
            GUILayout.BeginVertical(new GUILayoutOption[] { GUILayout.Width(150) });
            if (GUILayout.Button("Reset Start Position", new GUILayoutOption[] { GUILayout.Height(40) }))
            {
                _target.StartPosition = Vector3.zero;
            }

            if (_target.patrolType == Enemy.PatrolType.RandomInBox)
            {
                if (GUILayout.Button("Reset Patrol Box", new GUILayoutOption[] { GUILayout.Height(40) }))
                {
                    target.PatrolBox = target.PatrolBox.SetCenter(Vector2.zero);
                }
            }

            GUILayout.EndVertical();
            Handles.EndGUI();
        }
        protected virtual void OnSceneGUI()
        {
            var enemy = target as Enemy;
            if (enemy == null)
            {
                return;
            }

            if (!enemy.ShowDebug)
            {
                return;
            }
            string info = $"{typeName} - {enemy.name}";
            Undo.RecordObject(enemy, info);

            DrawEnemyInfo(enemy);

        }
    }

#endif
}
*/