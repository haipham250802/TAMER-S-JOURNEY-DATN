using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CritterFollowController : Singleton<CritterFollowController>
{
    public CritterFollowElement Critter_Follow_Element_01;
    public CritterFollowElement Critter_Follow_Element_02;
    public CritterFollowElement Critter_Follow_Element_03;
    public CritterFollowElement Critter_Follow_Element_04;

    public List<CritterFollowElement> L_CritterFollowElement = new List<CritterFollowElement>();

    public player m_PLayer;
    public Transform PosFish;
    public Transform PosCritterElement_1;
    public Transform PosCritterElement_2;
    public Transform PosCritterElement_3;

    public Vector3 Offset_CritterElement01;
    public Vector3 Offset_CritterElement02;
    public Vector3 Offset_CritterElement03;
    public Vector3 Offset_CritterElement04;

    public Vector3 Offset;
    public int Index = 1;

    public PolyNav2D polyNav2d;

    public GameObject ChatPrefabs;
    public string[] Stories = new string[4];
    public bool isChat;

    private void OnEnable()
    {
        Stories[0] = "I'm hungry, I want to eat meat !";
        Stories[1] = "Let's go fight, BOSS !";
        Stories[2] = "Let's go hunting Monsters !";
        Stories[3] = "Let me Fight !";
        Stories[4] = "Let\'s Go";
    }

    private void AddCritterElement()
    {
        StartCoroutine(IE_AddCritterElement());
    }
    IEnumerator IE_AddCritterElement()
    {
        var obj = GetComponentsInChildren<CritterFollowElement>();
        for (int i = 0; i < obj.Length; i++)
        {
            if (obj[i].GetComponent<CritterFollowElement>().CritterFollowType != ECharacterType.NONE)
            {
                L_CritterFollowElement.Add(obj[i].GetComponent<CritterFollowElement>());
            }
        }
        yield return null;
    }

    private void Start()
    {
        LoadCritterFollow();
    }
    private void Awake()
    {
        //  MoveCritter();
    }
    bool isFollow01;
    private void Update()
    {
        if (polyNav2d != null)
            AI_MoveCritterFollow();
    }

    public void LoadCritterFollow()
    {
        ResetState();
        for (int i = 0; i < DataPlayer.GetListAllid().Count; i++)
        {
            switch (i)
            {
                case 0:
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        Critter_Follow_Element_01.CritterFollowType = DataPlayer.GetListAllid()[i].Type;
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(DataPlayer.GetListAllid()[i].Type);
                        Critter_Follow_Element_01.ICON.skeletonDataAsset = null;
                        Critter_Follow_Element_01.ICON.skeletonDataAsset = enemyStat.ICON;
                        Critter_Follow_Element_01.ICON.Initialize(true);
                        Critter_Follow_Element_01.HP = DataPlayer.GetListAllid()[i].HP;
                        Critter_Follow_Element_01.gameObject.SetActive(true);
                        Critter_Follow_Element_01.shadow.SetActive(true);
                    }
                    else
                    {
                        Critter_Follow_Element_01.gameObject.SetActive(false);
                        Critter_Follow_Element_01.shadow.SetActive(false);

                    }
                    break;
                case 1:
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        Critter_Follow_Element_02.CritterFollowType = DataPlayer.GetListAllid()[i].Type;
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(DataPlayer.GetListAllid()[i].Type);
                        Critter_Follow_Element_02.ICON.skeletonDataAsset = null;
                        Critter_Follow_Element_02.ICON.skeletonDataAsset = enemyStat.ICON;
                        Critter_Follow_Element_02.HP = DataPlayer.GetListAllid()[i].HP;
                        Critter_Follow_Element_02.ICON.Initialize(true);

                        Critter_Follow_Element_02.gameObject.SetActive(true);
                        Critter_Follow_Element_02.shadow.SetActive(true);
                    }
                    else
                    {
                        Critter_Follow_Element_02.gameObject.SetActive(false);
                        Critter_Follow_Element_02.shadow.SetActive(false);
                    }
                    break;
                case 2:
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        Critter_Follow_Element_03.CritterFollowType = DataPlayer.GetListAllid()[i].Type;
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(DataPlayer.GetListAllid()[i].Type);
                        Critter_Follow_Element_03.ICON.skeletonDataAsset = null;
                        Critter_Follow_Element_03.ICON.skeletonDataAsset = enemyStat.ICON;
                        Critter_Follow_Element_03.ICON.Initialize(true);
                        Critter_Follow_Element_03.HP = DataPlayer.GetListAllid()[i].HP;
                        Critter_Follow_Element_03.gameObject.SetActive(true);
                        Critter_Follow_Element_03.shadow.SetActive(true);
                    }
                    else
                    {
                        Critter_Follow_Element_03.gameObject.SetActive(false);
                        Critter_Follow_Element_03.shadow.SetActive(false);
                    }
                    break;
                case 3:
                    if (DataPlayer.GetListAllid()[i].Type != ECharacterType.NONE)
                    {
                        Critter_Follow_Element_04.CritterFollowType = DataPlayer.GetListAllid()[i].Type;
                        EnemyStat enemyStat = Controller.Instance.GetStatEnemy(DataPlayer.GetListAllid()[i].Type);
                        Critter_Follow_Element_04.ICON.skeletonDataAsset = null;
                        Critter_Follow_Element_04.ICON.skeletonDataAsset = enemyStat.ICON;
                        Critter_Follow_Element_04.HP = DataPlayer.GetListAllid()[i].HP;
                        Critter_Follow_Element_04.ICON.Initialize(true);
                        Critter_Follow_Element_04.gameObject.SetActive(true);
                        Critter_Follow_Element_04.shadow.SetActive(true);
                    }
                    else
                    {
                        Critter_Follow_Element_04.gameObject.SetActive(false);
                        Critter_Follow_Element_04.shadow.SetActive(false);
                    }
                    break;
            }
        }
    }

    bool IsFlipCritter1;
    bool IsFlipCritter2;
    bool IsFlipCritter3;
    bool IsFlipCritter4;

    public bool AnimIdleDoneCritter01 = false;
    public bool AnimMoveDoneCritter01 = false;

    public bool AnimIdleDoneCritter02 = false;
    public bool AnimMoveDoneCritter02 = false;

    public bool AnimIdleDoneCritter03 = false;
    public bool AnimMoveDoneCritter03 = false;

    public bool AnimIdleDoneCritter04 = false;
    public bool AnimMoveDoneCritter04 = false;


    public bool isCanFollowCritter01;
    public void ResetState()
    {
        AnimIdleDoneCritter01 = false;
        AnimMoveDoneCritter01 = false;

        AnimIdleDoneCritter02 = false;
        AnimMoveDoneCritter02 = false;

        AnimIdleDoneCritter03 = false;
        AnimMoveDoneCritter03 = false;

        AnimIdleDoneCritter04 = false;
        AnimMoveDoneCritter04 = false;
    }
    public void MoveCritter()
    {
        if (Critter_Follow_Element_01 != null)
        {
            Critter_Follow_Element_01.Go(player.Instance.transform.position, (bool Done) =>
            {
                if (Done)
                {
                    Debug.Log("da den");
                    MoveCritter();
                }
            });
        }
    }
    bool isIdle4 = false;

    public void AI_MoveCritterFollow()
    {
        if (Critter_Follow_Element_01.gameObject.activeInHierarchy)
        {
            Critter_Follow_Element_01.UpdatePosShadow();
            Critter_Follow_Element_01.agent.SetDestination(PosFish.position + Offset_CritterElement01, (bool Done) =>
            {
                if (Done)
                {
                    if (!AnimIdleDoneCritter01)
                    {
                        Critter_Follow_Element_01.ICON.AnimationState.SetAnimation(2, "Idle", true);
                        AnimIdleDoneCritter01 = true;
                        AnimMoveDoneCritter01 = false;
                    }
                }
            });
            /*
                        if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter1 == false)
                        {
                            Vector3 theScale = Critter_Follow_Element_01.transform.localScale;
                            theScale.x *= -1;
                            Critter_Follow_Element_01.transform.localScale = theScale;
                            IsFlipCritter1 = true;
                        }
                        else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter1 == true)
                        {
                            Vector3 theScale = Critter_Follow_Element_01.transform.localScale;
                            theScale.x *= -1;
                            Critter_Follow_Element_01.transform.localScale = theScale;
                            IsFlipCritter1 = false;
                        }*/
            float x2 = PosFish.position.x + Offset_CritterElement01.x;
            float y2 = PosFish.position.y + Offset_CritterElement01.y;

            float x1 = PosCritterElement_1.position.x;
            float y1 = PosCritterElement_1.position.y;

            float Range = Mathf.Sqrt(((x2 - x1) * (x2 - x1)) +
                ((y2 - y1) * (y2 - y1)));
            if (Mathf.Abs(Range) <= 3f)
            {
                if (!AnimIdleDoneCritter01 && Critter_Follow_Element_01.gameObject.activeInHierarchy)
                {
                    Critter_Follow_Element_01.ICON.AnimationState.TimeScale = 1;
                    Critter_Follow_Element_01.agent.maxSpeed = 4;
                }
            }
            //**********************
            else if (Mathf.Abs(Range) > 0)
            {
                if (!AnimMoveDoneCritter01 && Critter_Follow_Element_01.gameObject.activeInHierarchy)
                {
                    Critter_Follow_Element_01.ICON.AnimationState.SetAnimation(2, "Move", true);
                    AnimMoveDoneCritter01 = true;
                    AnimIdleDoneCritter01 = false;
                }
            }
            if (Mathf.Abs(Range) > 3f &&
                Mathf.Abs(Range) < 6f)
            {
                Critter_Follow_Element_01.ICON.AnimationState.TimeScale = 1.5f;
                Critter_Follow_Element_01.agent.maxSpeed = 6;
            }
            else if (Mathf.Abs(Range) > 6f)
            {
                Critter_Follow_Element_01.ICON.AnimationState.TimeScale = 2f;
                Critter_Follow_Element_01.agent.maxSpeed = 8;
            }
            if (AnimIdleDoneCritter01 && AnimMoveDoneCritter01)
            {
                AnimIdleDoneCritter01 = false;
                AnimMoveDoneCritter01 = false;
            }

            // ************ neu co 1 va co 2
            if (Critter_Follow_Element_02.gameObject.activeInHierarchy)
            {
                Critter_Follow_Element_02.UpdatePosShadow();

                Critter_Follow_Element_02.agent.SetDestination(PosCritterElement_1.position, (bool Done) =>
                {
                    if (Done)
                    {
                        if (!AnimIdleDoneCritter02)
                        {
                            Critter_Follow_Element_02.ICON.AnimationState.SetAnimation(2, "Idle", true);
                            Critter_Follow_Element_02.ICON.AnimationState.TimeScale = 1;
                            AnimIdleDoneCritter02 = true;
                        }
                    }
                });
                if (AnimIdleDoneCritter02 && AnimMoveDoneCritter02)
                {
                    AnimIdleDoneCritter02 = false;
                    AnimMoveDoneCritter02 = false;
                }

                /* if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter2 == false)
                 {
                     Vector3 theScale = Critter_Follow_Element_02.transform.localScale;
                     theScale.x *= -1;
                     Critter_Follow_Element_02.transform.localScale = theScale;
                     IsFlipCritter2 = true;
                 }
                 else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter2 == true)
                 {
                     Vector3 theScale = Critter_Follow_Element_02.transform.localScale;
                     theScale.x *= -1;
                     Critter_Follow_Element_02.transform.localScale = theScale;
                     IsFlipCritter2 = false;
                 }*/

                float x21 = Critter_Follow_Element_02.transform.position.x;
                float y21 = Critter_Follow_Element_02.transform.position.y;

                float x11 = PosCritterElement_1.position.x;
                float y11 = PosCritterElement_1.position.y;

                float Range1 = Mathf.Sqrt(((x2 - x1) * (x2 - x1)) +
                    ((y2 - y1) * (y2 - y1)));

                if (Mathf.Abs(Range1) <= 3f)
                {
                    if (!AnimIdleDoneCritter02 && Critter_Follow_Element_02.gameObject.activeInHierarchy)
                    {
                        Critter_Follow_Element_02.ICON.AnimationState.TimeScale = 1;
                        Critter_Follow_Element_02.agent.maxSpeed = 4;
                    }
                }
                else if (Mathf.Abs(Range1) > 1f)
                {
                    if (!AnimMoveDoneCritter02 && Critter_Follow_Element_02.gameObject.activeInHierarchy)
                    {
                        Critter_Follow_Element_02.ICON.AnimationState.SetAnimation(2, "Move", true);
                        AnimMoveDoneCritter02 = true;
                        AnimIdleDoneCritter02 = false;
                    }
                }
                if (Mathf.Abs(Range1) > 3f &&
                                  Mathf.Abs(Range1) < 6f)
                {
                    Critter_Follow_Element_02.ICON.AnimationState.TimeScale = 1.5f;
                    Critter_Follow_Element_02.agent.maxSpeed = 6;
                }
                else if (Mathf.Abs(Range1) > 6f)
                {
                    Critter_Follow_Element_02.ICON.AnimationState.TimeScale = 2f;
                    Critter_Follow_Element_02.agent.maxSpeed = 8;
                }
                //******

                if (Critter_Follow_Element_02.gameObject.activeInHierarchy && Critter_Follow_Element_03.gameObject.activeInHierarchy)
                {
                    Critter_Follow_Element_03.UpdatePosShadow();
                    if (AnimIdleDoneCritter03 && AnimMoveDoneCritter03)
                    {
                        AnimIdleDoneCritter03 = false;
                        AnimMoveDoneCritter03 = false;
                    }
                    Critter_Follow_Element_03.agent.SetDestination(PosCritterElement_2.position, (bool Done) =>
                    {
                        if (Done)
                        {
                            if (!AnimIdleDoneCritter03)
                            {
                                Critter_Follow_Element_03.ICON.AnimationState.SetAnimation(2, "Idle", true);
                                Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 1;
                                AnimIdleDoneCritter03 = true;
                            }
                        }
                    });

                    /* if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter3 == false)
                     {
                         Vector3 theScale = Critter_Follow_Element_03.transform.localScale;
                         theScale.x *= -1;
                         Critter_Follow_Element_03.transform.localScale = theScale;
                         IsFlipCritter3 = true;
                     }
                     else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter3 == true)
                     {
                         Vector3 theScale = Critter_Follow_Element_03.transform.localScale;
                         theScale.x *= -1;
                         Critter_Follow_Element_03.transform.localScale = theScale;
                         IsFlipCritter3 = false;
                     }
 */
                    float x22 = Critter_Follow_Element_03.transform.position.x;
                    float y22 = Critter_Follow_Element_03.transform.position.y;

                    float x12 = PosCritterElement_2.position.x;
                    float y12 = PosCritterElement_2.position.y;

                    float Range2 = Mathf.Sqrt(((x22 - x12) * (x22 - x12)) +
                        ((y22 - y12) * (y22 - y12)));
                    if (Mathf.Abs(Range2) > 0.5f)
                    {
                        if (!AnimMoveDoneCritter03 && Critter_Follow_Element_03.gameObject.activeInHierarchy)
                        {
                            Critter_Follow_Element_03.ICON.AnimationState.SetAnimation(2, "Move", true);
                            AnimMoveDoneCritter03 = true;
                            AnimIdleDoneCritter03 = false;
                        }
                    }
                    if (Mathf.Abs(Range2) <= 3f)
                    {
                        if (!AnimIdleDoneCritter03 && Critter_Follow_Element_03.gameObject.activeInHierarchy)
                        {
                            Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 1;
                            Critter_Follow_Element_03.agent.maxSpeed = 4;
                        }
                    }

                    if (Mathf.Abs(Range2) > 3f &&
                                      Mathf.Abs(Range2) < 6f)
                    {
                        Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 1.5f;
                        Critter_Follow_Element_03.agent.maxSpeed = 6;
                    }
                    else if (Mathf.Abs(Range2) > 6f)
                    {
                        Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 2f;
                        Critter_Follow_Element_03.agent.maxSpeed = 8;
                    }
                }
            }
            //****** khong co 2 nhung co 1
            else if (!Critter_Follow_Element_02.gameObject.activeInHierarchy && Critter_Follow_Element_03.gameObject.activeInHierarchy)
            {

                Critter_Follow_Element_03.UpdatePosShadow();
                if (AnimIdleDoneCritter03 && AnimMoveDoneCritter03)
                {
                    AnimIdleDoneCritter03 = false;
                    AnimMoveDoneCritter03 = false;
                }
                Critter_Follow_Element_03.agent.SetDestination(PosCritterElement_1.position, (bool Done) =>
                {
                    if (Done)
                    {
                        if (!AnimIdleDoneCritter03)
                        {
                            Critter_Follow_Element_03.ICON.AnimationState.SetAnimation(2, "Idle", true);
                            Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 1;
                            AnimIdleDoneCritter03 = true;
                        }
                    }
                });

                /*  if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter3 == false)
                  {
                      Vector3 theScale = Critter_Follow_Element_03.transform.localScale;
                      theScale.x *= -1;
                      Critter_Follow_Element_03.transform.localScale = theScale;
                      IsFlipCritter3 = true;
                  }
                  else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter3 == true)
                  {
                      Vector3 theScale = Critter_Follow_Element_03.transform.localScale;
                      theScale.x *= -1;
                      Critter_Follow_Element_03.transform.localScale = theScale;
                      IsFlipCritter3 = false;
                  }*/

                float x22 = Critter_Follow_Element_03.transform.position.x;
                float y22 = Critter_Follow_Element_03.transform.position.y;

                float x12 = PosCritterElement_1.position.x;
                float y12 = PosCritterElement_1.position.y;

                float Range2 = Mathf.Sqrt(((x22 - x12) * (x22 - x12)) +
                    ((y22 - y12) * (y22 - y12)));

                if (Mathf.Abs(Range2) <= 3f)
                {
                    if (!AnimIdleDoneCritter03 && Critter_Follow_Element_03.gameObject.activeInHierarchy)
                    {
                        Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 1;
                        Critter_Follow_Element_03.agent.maxSpeed = 4;
                    }
                }
                else if (Mathf.Abs(Range2) > 0)
                {
                    if (!AnimMoveDoneCritter03 && Critter_Follow_Element_03.gameObject.activeInHierarchy)
                    {
                        Critter_Follow_Element_03.ICON.AnimationState.SetAnimation(2, "Move", true);
                        AnimMoveDoneCritter03 = true;
                        AnimIdleDoneCritter03 = false;
                    }
                }
                if (Mathf.Abs(Range2) > 3f &&
                                  Mathf.Abs(Range2) < 6f)
                {
                    Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 1.5f;
                    Critter_Follow_Element_03.agent.maxSpeed = 6;
                }
                else if (Mathf.Abs(Range2) > 6f)
                {
                    Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 2f;
                    Critter_Follow_Element_03.agent.maxSpeed = 8;
                }
            }

            if (Critter_Follow_Element_03.gameObject.activeInHierarchy)
            {
                if (Critter_Follow_Element_04.gameObject.activeInHierarchy)
                {

                    Critter_Follow_Element_04.UpdatePosShadow();
                    /* if (AnimIdleDoneCritter04 && AnimMoveDoneCritter04)
                     {
                         AnimIdleDoneCritter04 = false;
                         AnimMoveDoneCritter04 = false;
                     }*/
                    Critter_Follow_Element_04.agent.SetDestination(PosCritterElement_3.position, (bool Done) =>
                    {
                        if (Done)
                        {
                            if(!isIdle4)
                            {
                                Debug.LogWarning("Done 4");
                                /* if (!AnimIdleDoneCritter04)
                                 {*/
                                Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Idle", true);
                                Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1;
                                AnimIdleDoneCritter04 = true;
                                AnimMoveDoneCritter04 = false;
                                isIdle4 = true;
                                /*}*/
                            }

                        }
                    });

                    /* if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter4 == false)
                     {
                         Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                         theScale.x *= -1;
                         Critter_Follow_Element_04.transform.localScale = theScale;
                         IsFlipCritter4 = true;
                     }
                     else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter4 == true)
                     {
                         Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                         theScale.x *= -1;
                         Critter_Follow_Element_04.transform.localScale = theScale;
                         IsFlipCritter4 = false;
                     }*/

                    float x22 = Critter_Follow_Element_04.transform.position.x;
                    float y22 = Critter_Follow_Element_04.transform.position.y;

                    float x12 = PosCritterElement_1.position.x;
                    float y12 = PosCritterElement_1.position.y;

                    float Range2 = Mathf.Sqrt(((x22 - x12) * (x22 - x12)) +
                        ((y22 - y12) * (y22 - y12)));
                    if(Mathf.Abs(Range2) < 1)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Idle", true);
                        AnimMoveDoneCritter04 = false;
                        AnimIdleDoneCritter04 = true;
                    }
                    if (Mathf.Abs(Range2) > 3)
                    {
                        if (!AnimMoveDoneCritter04 && Critter_Follow_Element_04.gameObject.activeInHierarchy)
                        {
                            Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Move", true);
                            AnimMoveDoneCritter04 = true;
                            AnimIdleDoneCritter04 = false;
                            isIdle4 = false;

                        }
                    }
                    if (Mathf.Abs(Range2) <= 3f)
                    {
                        if (!AnimIdleDoneCritter04 && Critter_Follow_Element_04.gameObject.activeInHierarchy)
                        {
                            Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1;
                            Critter_Follow_Element_04.agent.maxSpeed = 4;
                        }
                    }

                    if (Mathf.Abs(Range2) > 3f &&
                                      Mathf.Abs(Range2) < 6f)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1.5f;
                        Critter_Follow_Element_04.agent.maxSpeed = 6;
                    }
                    else if (Mathf.Abs(Range2) > 6f)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 2f;
                        Critter_Follow_Element_04.agent.maxSpeed = 8;
                    }
                }

            }
            else if (!Critter_Follow_Element_03.gameObject.activeInHierarchy && Critter_Follow_Element_02.gameObject.activeInHierarchy)
            {
                if (Critter_Follow_Element_04.gameObject.activeInHierarchy)
                {
                    Critter_Follow_Element_04.UpdatePosShadow();
                    if (AnimIdleDoneCritter04 && AnimMoveDoneCritter04)
                    {
                        AnimIdleDoneCritter04 = false;
                        AnimMoveDoneCritter04 = false;
                    }
                    Critter_Follow_Element_04.agent.SetDestination(PosCritterElement_2.position, (bool Done) =>
                    {
                        if (Done)
                        {
                            if (!AnimIdleDoneCritter04)
                            {
                                Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Idle", true);
                                Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1;
                                AnimIdleDoneCritter04 = true;
                            }
                        }
                    });

                    /* if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter4 == false)
                     {
                         Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                         theScale.x *= -1;
                         Critter_Follow_Element_04.transform.localScale = theScale;
                         IsFlipCritter4 = true;
                     }
                     else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter4 == true)
                     {
                         Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                         theScale.x *= -1;
                         Critter_Follow_Element_04.transform.localScale = theScale;
                         IsFlipCritter4 = false;
                     }*/

                    float x22 = Critter_Follow_Element_04.transform.position.x;
                    float y22 = Critter_Follow_Element_04.transform.position.y;

                    float x12 = PosCritterElement_2.position.x;
                    float y12 = PosCritterElement_2.position.y;

                    float Range2 = Mathf.Sqrt(((x22 - x12) * (x22 - x12)) +
                        ((y22 - y12) * (y22 - y12)));
                    if (Mathf.Abs(Range2) > 0.5f)
                    {
                        if (!AnimMoveDoneCritter04 && Critter_Follow_Element_04.gameObject.activeInHierarchy)
                        {
                            Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Move", true);
                            AnimMoveDoneCritter04 = true;
                            AnimIdleDoneCritter04 = false;
                        }
                    }
                    if (Mathf.Abs(Range2) <= 3f)
                    {
                        if (!AnimIdleDoneCritter04 && Critter_Follow_Element_04.gameObject.activeInHierarchy)
                        {
                            Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1;
                            Critter_Follow_Element_04.agent.maxSpeed = 4;
                        }
                    }

                    if (Mathf.Abs(Range2) > 3f &&
                                      Mathf.Abs(Range2) < 6f)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1.5f;
                        Critter_Follow_Element_04.agent.maxSpeed = 6;
                    }
                    else if (Mathf.Abs(Range2) > 6f)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 2f;
                        Critter_Follow_Element_04.agent.maxSpeed = 8;
                    }
                }
            }
            else if (!Critter_Follow_Element_03.gameObject.activeInHierarchy && !Critter_Follow_Element_02.gameObject.activeInHierarchy)
            {
                if (Critter_Follow_Element_04.gameObject.activeInHierarchy)
                {
                    Critter_Follow_Element_04.UpdatePosShadow();

                    if (Critter_Follow_Element_04.gameObject.activeInHierarchy)
                    {
                        Critter_Follow_Element_04.agent.SetDestination(PosCritterElement_1.position, (bool Done) =>
                        {
                            if (Done)
                            {
                                if (!AnimIdleDoneCritter04)
                                {
                                    Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Idle", true);
                                    Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1;
                                    AnimIdleDoneCritter04 = true;
                                }
                            }
                        });


                        /*  if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter4 == false)
                          {
                              Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                              theScale.x *= -1;
                              Critter_Follow_Element_04.transform.localScale = theScale;
                              IsFlipCritter4 = true;
                          }
                          else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter4 == true)
                          {
                              Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                              theScale.x *= -1;
                              Critter_Follow_Element_04.transform.localScale = theScale;
                              IsFlipCritter4 = false;
                          }*/

                        float x22 = Critter_Follow_Element_04.transform.position.x;
                        float y22 = Critter_Follow_Element_04.transform.position.y;

                        float x12 = PosCritterElement_1.position.x;
                        float y12 = PosCritterElement_1.position.y;

                        float Range2 = Mathf.Sqrt(((x22 - x12) * (x22 - x12)) +
                            ((y22 - y12) * (y22 - y12)));
                        if (Mathf.Abs(Range2) > 0.5f)
                        {
                            if (!AnimMoveDoneCritter04 && Critter_Follow_Element_04.gameObject.activeInHierarchy)
                            {
                                Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Move", true);
                                AnimMoveDoneCritter04 = true;
                                AnimIdleDoneCritter04 = false;
                            }
                        }
                        else if (Mathf.Abs(Range2) <= 3f)
                        {
                            if (!AnimIdleDoneCritter04 && Critter_Follow_Element_04.gameObject.activeInHierarchy)
                            {
                                Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1;
                                Critter_Follow_Element_04.agent.maxSpeed = 4;
                            }
                        }

                        else if (Mathf.Abs(Range2) > 3f &&
                                          Mathf.Abs(Range2) < 6f)
                        {
                            Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1.5f;
                            Critter_Follow_Element_04.agent.maxSpeed = 6;
                        }
                        else if (Mathf.Abs(Range2) > 6f)
                        {
                            Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 2f;
                            Critter_Follow_Element_04.agent.maxSpeed = 8;
                        }
                    }
                }
            }
        }



        else if (!Critter_Follow_Element_01.gameObject.activeInHierarchy && Critter_Follow_Element_02.gameObject.activeInHierarchy)
        {
            Critter_Follow_Element_02.UpdatePosShadow();
            Critter_Follow_Element_02.agent.SetDestination(PosFish.position + Offset_CritterElement02, (bool Done) =>
            {
                if (Done)
                {
                    if (!AnimIdleDoneCritter02)
                    {
                        Critter_Follow_Element_02.ICON.AnimationState.SetAnimation(2, "Idle", true);
                        AnimIdleDoneCritter02 = true;
                        AnimMoveDoneCritter02 = false;
                    }
                }
            });

            /* if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter2 == false)
             {
                 Vector3 theScale = Critter_Follow_Element_02.transform.localScale;
                 theScale.x *= -1;
                 Critter_Follow_Element_02.transform.localScale = theScale;
                 IsFlipCritter2 = true;
             }
             else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter2 == true)
             {
                 Vector3 theScale = Critter_Follow_Element_02.transform.localScale;
                 theScale.x *= -1;
                 Critter_Follow_Element_02.transform.localScale = theScale;
                 IsFlipCritter2 = false;
             }*/
            if (Mathf.Abs(Vector2.Distance(PosCritterElement_2.position,
                PosFish.position + Offset_CritterElement02)) <= 3f)
            {
                if (!AnimIdleDoneCritter02 && Critter_Follow_Element_02.gameObject.activeInHierarchy)
                {
                    Critter_Follow_Element_02.ICON.AnimationState.TimeScale = 1;
                    Critter_Follow_Element_02.agent.maxSpeed = 4;
                }
            }
            else if (Mathf.Abs(Vector2.Distance(PosCritterElement_2.position,
                PosFish.position + Offset_CritterElement02)) > 0.5f)
            {
                if (!AnimMoveDoneCritter02 && Critter_Follow_Element_02.gameObject.activeInHierarchy)
                {
                    Critter_Follow_Element_02.ICON.AnimationState.SetAnimation(2, "Move", true);
                    AnimMoveDoneCritter02 = true;
                    AnimIdleDoneCritter02 = false;
                }
            }
            if (Mathf.Abs(Vector2.Distance(PosCritterElement_2.position, PosFish.position + Offset_CritterElement02)) > 3f &&
                Mathf.Abs(Vector2.Distance(PosCritterElement_2.position, PosFish.position + Offset_CritterElement02)) < 6f)
            {
                Critter_Follow_Element_02.ICON.AnimationState.TimeScale = 1.5f;
                Critter_Follow_Element_02.agent.maxSpeed = 6;
            }
            else if (Mathf.Abs(Vector2.Distance(PosCritterElement_2.position, PosFish.position + Offset_CritterElement02)) > 6f)
            {
                Critter_Follow_Element_02.ICON.AnimationState.TimeScale = 2f;
                Critter_Follow_Element_02.agent.maxSpeed = 8;
            }
            if (AnimIdleDoneCritter02 && AnimMoveDoneCritter02)
            {
                AnimIdleDoneCritter02 = false;
                AnimMoveDoneCritter02 = false;
            }

            if (Critter_Follow_Element_02.gameObject.activeInHierarchy && Critter_Follow_Element_03.gameObject.activeInHierarchy)
            {
                Critter_Follow_Element_03.UpdatePosShadow();
                if (AnimIdleDoneCritter03 && AnimMoveDoneCritter03)
                {
                    AnimIdleDoneCritter03 = false;
                    AnimMoveDoneCritter03 = false;
                }
                Critter_Follow_Element_03.agent.SetDestination(PosCritterElement_2.position, (bool Done) =>
                {
                    if (Done)
                    {
                        if (!AnimIdleDoneCritter03)
                        {
                            Critter_Follow_Element_03.ICON.AnimationState.SetAnimation(2, "Idle", true);
                            Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 1;
                            AnimIdleDoneCritter03 = true;
                        }
                    }
                });

                /*   if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter3 == false)
                   {
                       Vector3 theScale = Critter_Follow_Element_03.transform.localScale;
                       theScale.x *= -1;
                       Critter_Follow_Element_03.transform.localScale = theScale;
                       IsFlipCritter3 = true;
                   }
                   else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter3 == true)
                   {
                       Vector3 theScale = Critter_Follow_Element_03.transform.localScale;
                       theScale.x *= -1;
                       Critter_Follow_Element_03.transform.localScale = theScale;
                       IsFlipCritter3 = false;
                   }*/

                float x22 = Critter_Follow_Element_03.transform.position.x;
                float y22 = Critter_Follow_Element_03.transform.position.y;

                float x12 = PosCritterElement_2.position.x;
                float y12 = PosCritterElement_2.position.y;

                float Range2 = Mathf.Sqrt(((x22 - x12) * (x22 - x12)) +
                    ((y22 - y12) * (y22 - y12)));
                if (Mathf.Abs(Range2) > 0.5f)
                {
                    if (!AnimMoveDoneCritter03 && Critter_Follow_Element_03.gameObject.activeInHierarchy)
                    {
                        Critter_Follow_Element_03.ICON.AnimationState.SetAnimation(2, "Move", true);
                        AnimMoveDoneCritter03 = true;
                        AnimIdleDoneCritter03 = false;
                    }
                }
                if (Mathf.Abs(Range2) <= 3f)
                {
                    if (!AnimIdleDoneCritter04 && Critter_Follow_Element_03.gameObject.activeInHierarchy)
                    {
                        Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 1;
                        Critter_Follow_Element_03.agent.maxSpeed = 4;
                    }
                }

                if (Mathf.Abs(Range2) > 3f &&
                                  Mathf.Abs(Range2) < 6f)
                {
                    Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 1.5f;
                    Critter_Follow_Element_03.agent.maxSpeed = 6;
                }
                else if (Mathf.Abs(Range2) > 6f)
                {
                    Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 2f;
                    Critter_Follow_Element_03.agent.maxSpeed = 8;
                }
            }
            if (Critter_Follow_Element_03.gameObject.activeInHierarchy && Critter_Follow_Element_04.gameObject.activeInHierarchy)
            {
                /*Critter_Follow_Element_04.MoveCritterElement(PosCritterElement_3.position, Vector3.zero);
                if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter4 == false)
                {
                    Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                    theScale.x *= -1;
                    Critter_Follow_Element_04.transform.localScale = theScale;
                    IsFlipCritter4 = true;
                }
                else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter4 == true)
                {
                    Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                    theScale.x *= -1;
                    Critter_Follow_Element_04.transform.localScale = theScale;
                    IsFlipCritter4 = false;
                }
                if (Mathf.Abs(Vector3.Distance(PosCritterElement_3.position, Critter_Follow_Element_04.transform.position)) < 1)
                {
                    if (!AnimIdleDoneCritter04)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Idle", true);
                        AnimIdleDoneCritter04 = true;
                        AnimMoveDoneCritter04 = false;
                    }
                    Debug.LogWarning("Idle");
                }
                else if (Mathf.Abs(Vector3.Distance(PosCritterElement_3.position, Critter_Follow_Element_04.transform.position)) > 2)
                {
                    Debug.LogWarning("Move");
                    if (!AnimMoveDoneCritter04)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Move", true);
                        AnimMoveDoneCritter04 = true;
                        AnimIdleDoneCritter04 = false;
                    }
                }*/
                Critter_Follow_Element_04.UpdatePosShadow();
                if (AnimIdleDoneCritter04 && AnimMoveDoneCritter04)
                {
                    AnimIdleDoneCritter04 = false;
                    AnimMoveDoneCritter04 = false;
                }
                if (Critter_Follow_Element_04.gameObject.activeInHierarchy)
                {
                    Critter_Follow_Element_04.agent.SetDestination(PosCritterElement_3.position, (bool Done) =>
                    {
                        if (Done)
                        {
                            if (!AnimIdleDoneCritter04)
                            {
                                Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Idle", true);
                                Debug.Log("da den");
                                Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1;
                                AnimIdleDoneCritter04 = true;
                            }
                        }
                    });


                    /*  if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter4 == false)
                      {
                          Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                          theScale.x *= -1;
                          Critter_Follow_Element_04.transform.localScale = theScale;
                          IsFlipCritter4 = true;
                      }
                      else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter4 == true)
                      {
                          Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                          theScale.x *= -1;
                          Critter_Follow_Element_04.transform.localScale = theScale;
                          IsFlipCritter4 = false;
                      }*/

                    float x22 = Critter_Follow_Element_04.transform.position.x;
                    float y22 = Critter_Follow_Element_04.transform.position.y;

                    float x12 = PosCritterElement_3.position.x;
                    float y12 = PosCritterElement_3.position.y;

                    float Range2 = Mathf.Sqrt(((x22 - x12) * (x22 - x12)) +
                        ((y22 - y12) * (y22 - y12)));
                    if (Mathf.Abs(Range2) > 2f)
                    {
                        if (!AnimMoveDoneCritter04 && Critter_Follow_Element_04.gameObject.activeInHierarchy)
                        {
                            Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Move", true);
                            AnimMoveDoneCritter04 = true;
                            AnimIdleDoneCritter04 = false;
                        }
                    }
                    if (Mathf.Abs(Range2) <= 0.5f)
                    {
                        if (!AnimIdleDoneCritter04 && Critter_Follow_Element_04.gameObject.activeInHierarchy)
                        {
                            Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1;
                            Critter_Follow_Element_04.agent.maxSpeed = 4;
                            AnimMoveDoneCritter04 = false;
                            AnimIdleDoneCritter04 = true;
                        }
                    }

                    if (Mathf.Abs(Range2) > 3f &&
                                      Mathf.Abs(Range2) < 6f)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1.5f;
                        Critter_Follow_Element_04.agent.maxSpeed = 6;
                    }
                    else if (Mathf.Abs(Range2) > 6f)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 2f;
                        Critter_Follow_Element_04.agent.maxSpeed = 8;
                    }
                }
            }
            else if (!Critter_Follow_Element_03.gameObject.activeInHierarchy && Critter_Follow_Element_04.gameObject.activeInHierarchy)
            {
                /*Critter_Follow_Element_04.MoveCritterElement(PosCritterElement_2.position, Vector3.zero);
                if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter4 == false)
                {
                    Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                    theScale.x *= -1;
                    Critter_Follow_Element_04.transform.localScale = theScale;
                    IsFlipCritter4 = true;
                }
                else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter4 == true)
                {
                    Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                    theScale.x *= -1;
                    Critter_Follow_Element_04.transform.localScale = theScale;
                    IsFlipCritter4 = false;
                }
                if (Mathf.Abs(Vector3.Distance(PosCritterElement_2.position, Critter_Follow_Element_04.transform.position)) < 1)
                {
                    if (!AnimIdleDoneCritter04)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Idle", true);
                        AnimIdleDoneCritter04 = true;
                        AnimMoveDoneCritter04 = false;
                    }
                    Debug.LogWarning("Idle");
                }
                else if (Mathf.Abs(Vector3.Distance(PosCritterElement_2.position, Critter_Follow_Element_04.transform.position)) > 2)
                {
                    Debug.LogWarning("Move");
                    if (!AnimMoveDoneCritter04)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Move", true);
                        AnimMoveDoneCritter04 = true;
                        AnimIdleDoneCritter04 = false;
                    }
                }*/
                Critter_Follow_Element_04.UpdatePosShadow();
                if (AnimIdleDoneCritter04 && AnimMoveDoneCritter04)
                {
                    AnimIdleDoneCritter04 = false;
                    AnimMoveDoneCritter04 = false;
                }
                Critter_Follow_Element_04.agent.SetDestination(PosCritterElement_2.position, (bool Done) =>
                {
                    if (Done)
                    {
                        if (!AnimIdleDoneCritter04)
                        {
                            Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Idle", true);
                            Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1;
                            AnimIdleDoneCritter04 = true;
                        }
                    }
                });

                /*  if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter4 == false)
                  {
                      Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                      theScale.x *= -1;
                      Critter_Follow_Element_04.transform.localScale = theScale;
                      IsFlipCritter4 = true;
                  }
                  else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter4 == true)
                  {
                      Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                      theScale.x *= -1;
                      Critter_Follow_Element_04.transform.localScale = theScale;
                      IsFlipCritter4 = false;
                  }*/

                float x22 = Critter_Follow_Element_04.transform.position.x;
                float y22 = Critter_Follow_Element_04.transform.position.y;

                float x12 = PosCritterElement_2.position.x;
                float y12 = PosCritterElement_2.position.y;

                float Range2 = Mathf.Sqrt(((x22 - x12) * (x22 - x12)) +
                    ((y22 - y12) * (y22 - y12)));
                if (Mathf.Abs(Range2) > 0.5f)
                {
                    if (!AnimMoveDoneCritter04 && Critter_Follow_Element_04.gameObject.activeInHierarchy)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Move", true);
                        AnimMoveDoneCritter04 = true;
                        AnimIdleDoneCritter04 = false;
                    }
                }
                if (Mathf.Abs(Range2) <= 3f)
                {
                    if (!AnimIdleDoneCritter04 && Critter_Follow_Element_04.gameObject.activeInHierarchy)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1;
                        Critter_Follow_Element_04.agent.maxSpeed = 4;
                    }
                }

                if (Mathf.Abs(Range2) > 3f &&
                                  Mathf.Abs(Range2) < 6f)
                {
                    Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1.5f;
                    Critter_Follow_Element_04.agent.maxSpeed = 6;
                }
                else if (Mathf.Abs(Range2) > 6f)
                {
                    Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 2f;
                    Critter_Follow_Element_04.agent.maxSpeed = 8;
                }
            }
        }

        // *******
        else if (!Critter_Follow_Element_01.gameObject.activeInHierarchy && !Critter_Follow_Element_02.gameObject.activeInHierarchy && Critter_Follow_Element_03.gameObject.activeInHierarchy)
        {
            Critter_Follow_Element_03.UpdatePosShadow();
            Critter_Follow_Element_03.agent.SetDestination(PosFish.position + Offset_CritterElement03, (bool Done) =>
            {
                if (Done)
                {
                    if (!AnimIdleDoneCritter03)
                    {
                        Critter_Follow_Element_03.ICON.AnimationState.SetAnimation(2, "Idle", true);
                        AnimIdleDoneCritter03 = true;
                        AnimMoveDoneCritter03 = false;
                    }
                }
            });

            if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter3 == false)
            {
                Vector3 theScale = Critter_Follow_Element_03.transform.localScale;
                theScale.x *= -1;
                Critter_Follow_Element_03.transform.localScale = theScale;
                IsFlipCritter3 = true;
            }
            else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter3 == true)
            {
                Vector3 theScale = Critter_Follow_Element_03.transform.localScale;
                theScale.x *= -1;
                Critter_Follow_Element_03.transform.localScale = theScale;
                IsFlipCritter3 = false;
            }
            if (Mathf.Abs(Vector2.Distance(PosCritterElement_3.position,
                PosFish.position + Offset_CritterElement03)) <= 3f)
            {
                if (!AnimIdleDoneCritter03 && Critter_Follow_Element_03.gameObject.activeInHierarchy)
                {
                    Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 1;
                    Critter_Follow_Element_03.agent.maxSpeed = 4;
                }
            }
            else if (Mathf.Abs(Vector2.Distance(PosCritterElement_3.position,
                PosFish.position + Offset_CritterElement03)) > 0.5f)
            {
                if (!AnimMoveDoneCritter03 && Critter_Follow_Element_03.gameObject.activeInHierarchy)
                {
                    Critter_Follow_Element_03.ICON.AnimationState.SetAnimation(2, "Move", true);
                    AnimMoveDoneCritter03 = true;
                    AnimIdleDoneCritter03 = false;
                }
            }
            if (Mathf.Abs(Vector2.Distance(PosCritterElement_3.position, PosFish.position + Offset_CritterElement03)) > 3f &&
                Mathf.Abs(Vector2.Distance(PosCritterElement_3.position, PosFish.position + Offset_CritterElement03)) < 6f)
            {
                if (Critter_Follow_Element_03 != null)
                {
                    Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 1.5f;
                    Critter_Follow_Element_03.agent.maxSpeed = 6;
                }
            }
            else if (Mathf.Abs(Vector2.Distance(PosCritterElement_3.position, PosFish.position + Offset_CritterElement03)) > 6f)
            {
                Critter_Follow_Element_03.ICON.AnimationState.TimeScale = 2f;
                Critter_Follow_Element_03.agent.maxSpeed = 8;
            }
            if (AnimIdleDoneCritter03 && AnimMoveDoneCritter03)
            {
                AnimIdleDoneCritter03 = false;
                AnimMoveDoneCritter03 = false;
            }

            if (Critter_Follow_Element_04.gameObject.activeInHierarchy)
            {

                Critter_Follow_Element_04.UpdatePosShadow();
                if (AnimIdleDoneCritter04 && AnimMoveDoneCritter04)
                {
                    AnimIdleDoneCritter04 = false;
                    AnimMoveDoneCritter04 = false;
                }
                Critter_Follow_Element_04.agent.SetDestination(PosCritterElement_3.position, (bool Done) =>
                {
                    if (Done)
                    {
                        if (!AnimIdleDoneCritter04)
                        {
                            Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Idle", true);
                            Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1;
                            AnimIdleDoneCritter04 = true;
                        }
                    }
                });

                /* if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter4 == false)
                 {
                     Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                     theScale.x *= -1;
                     Critter_Follow_Element_04.transform.localScale = theScale;
                     IsFlipCritter4 = true;
                 }
                 else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter4 == true)
                 {
                     Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                     theScale.x *= -1;
                     Critter_Follow_Element_04.transform.localScale = theScale;
                     IsFlipCritter4 = false;
                 }*/

                float x22 = Critter_Follow_Element_04.transform.position.x;
                float y22 = Critter_Follow_Element_04.transform.position.y;

                float x12 = PosCritterElement_3.position.x;
                float y12 = PosCritterElement_3.position.y;

                float Range2 = Mathf.Sqrt(((x22 - x12) * (x22 - x12)) +
                    ((y22 - y12) * (y22 - y12)));
                if (Mathf.Abs(Range2) > 0.5f)
                {
                    if (!AnimMoveDoneCritter04 && Critter_Follow_Element_04.gameObject.activeInHierarchy)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Move", true);
                        AnimMoveDoneCritter04 = true;
                        AnimIdleDoneCritter04 = false;
                    }
                }
                if (Mathf.Abs(Range2) <= 3f)
                {
                    if (!AnimIdleDoneCritter04 && Critter_Follow_Element_04.gameObject.activeInHierarchy)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1;
                        Critter_Follow_Element_04.agent.maxSpeed = 4;
                    }
                }

                if (Mathf.Abs(Range2) > 3f &&
                                  Mathf.Abs(Range2) < 6f)
                {
                    Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1.5f;
                    Critter_Follow_Element_04.agent.maxSpeed = 6;
                }
                else if (Mathf.Abs(Range2) > 6f)
                {
                    Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 2f;
                    Critter_Follow_Element_04.agent.maxSpeed = 8;
                }
            }
        }
        else if (!Critter_Follow_Element_01.gameObject.activeInHierarchy &&
            !Critter_Follow_Element_02.gameObject.activeInHierarchy && !Critter_Follow_Element_03.gameObject.activeInHierarchy &&
            Critter_Follow_Element_04.gameObject.activeInHierarchy)
        {
            if (Critter_Follow_Element_04.gameObject.activeInHierarchy)
            {

                Critter_Follow_Element_04.UpdatePosShadow();
                Critter_Follow_Element_04.agent.SetDestination(PosFish.position + Offset_CritterElement04, (bool Done) =>
                {
                    if (Done)
                    {
                        if (!AnimIdleDoneCritter04)
                        {
                            Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Idle", true);
                            AnimIdleDoneCritter04 = true;
                            AnimMoveDoneCritter04 = false;
                        }
                    }
                });

                /*  if (m_PLayer.transform.localScale.x < 0 && IsFlipCritter4 == false)
                  {
                      Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                      theScale.x *= -1;
                      Critter_Follow_Element_04.transform.localScale = theScale;
                      IsFlipCritter4 = true;
                  }
                  else if (m_PLayer.transform.localScale.x > 0 && IsFlipCritter4 == true)
                  {
                      Vector3 theScale = Critter_Follow_Element_04.transform.localScale;
                      theScale.x *= -1;
                      Critter_Follow_Element_04.transform.localScale = theScale;
                      IsFlipCritter4 = false;
                  }*/
                if (Mathf.Abs(Vector2.Distance(Critter_Follow_Element_04.transform.position,
                    PosFish.position + Offset_CritterElement04)) <= 3f)
                {
                    if (!AnimIdleDoneCritter04 && Critter_Follow_Element_04.gameObject.activeInHierarchy)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1;
                        Critter_Follow_Element_04.agent.maxSpeed = 4;
                    }
                }
                else if (Mathf.Abs(Vector2.Distance(Critter_Follow_Element_04.transform.position,
                    PosFish.position + Offset_CritterElement04)) > 0.3f)
                {
                    if (!AnimMoveDoneCritter04 && Critter_Follow_Element_04.gameObject.activeInHierarchy)
                    {
                        Critter_Follow_Element_04.ICON.AnimationState.SetAnimation(2, "Move", true);
                        AnimMoveDoneCritter04 = true;
                        AnimIdleDoneCritter04 = false;
                    }
                }
                if (Mathf.Abs(Vector2.Distance(Critter_Follow_Element_04.transform.position, PosFish.position + Offset_CritterElement04)) > 3f &&
                    Mathf.Abs(Vector2.Distance(Critter_Follow_Element_04.transform.position, PosFish.position + Offset_CritterElement04)) < 6f)
                {
                    Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 1.5f;
                    Critter_Follow_Element_04.agent.maxSpeed = 6;
                }
                else if (Mathf.Abs(Vector2.Distance(Critter_Follow_Element_04.transform.position, PosFish.position + Offset_CritterElement04)) > 6f)
                {
                    Critter_Follow_Element_04.ICON.AnimationState.TimeScale = 2f;
                    Critter_Follow_Element_04.agent.maxSpeed = 8;
                }
                if (AnimIdleDoneCritter04 && AnimMoveDoneCritter04)
                {
                    AnimIdleDoneCritter04 = false;
                    AnimMoveDoneCritter04 = false;
                }
            }
        }
    }
    public void FlipCritter(CritterFollowElement _Critter, bool Value)
    {
        if (!Value) Value = true;
        else if (Value) Value = false;
        Vector3 theScale = _Critter.transform.localScale;
        theScale.x *= -1;
        _Critter.transform.localScale = theScale;
    }
}
