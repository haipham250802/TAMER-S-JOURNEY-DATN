using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using Cinemachine;
using DG.Tweening;
public class player : Character
{
    const string IdleName = "Idle";
    const string MoveName = "Move";

    public CinemachineVirtualCamera cinemachineCam;
    public FloatingJoystick joystick;

    public bool isFacingRight;
    public bool isPurchaseBagBtn;

    public Button TabToMoveBtn;

    public GameObject CritterFollow;
    public GameObject UI_ShowALlid;
    public GameObject BagBtn;
    public Image imgBag;
    public GameObject Option;
    public GameObject BackGroundJoyStick;
    public GameObject CheckPoint;

    public GameObject PopUpHealingWhenTriggerLake;
    public GameObject PopUpFrog;
    public GameObject Menu;

    public List<GameObject> L_enemy = new List<GameObject>();

    public SkeletonAnimation skeleton;
    public CritterFollowController m_CritterFollowController;

    public int startSpeed;

    private static player instance;

    public Image bagOpen;
    public Image bagClose;

    public Transform StartTransformPlayer;

    public bool isCheckPoint;

    public GameObject HandTut;
    public GameObject ApppearFx;
    public static player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new player();
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        BagBtn.GetComponent<Button>().onClick.AddListener(OnPurchaseBagButton);

    }
    bool isActiveExampleStoryTut;
    private void Update()
    {
        if (UI_Loading.Instance.isActivePlayer)
        {
            StartCoroutine(IE_DelayActivePlayer());
        }
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            if(BagManager.Instance.m_RuleController)
            {
                if (Vector2.Distance(BagManager.Instance.m_RuleController.L_enemy[0].transform.position, transform.position) < 5
                && !isActiveExampleStoryTut)
                {
                    Controller.Instance.exampObj.gameObject.SetActive(true);
                    string str = I2.Loc.LocalizationManager.GetTranslation("KEY_TUT_MOVE_NEAR");
                    ExampleStoryTut.Instance.SetText(str);
                    isActiveExampleStoryTut = true;
                    player.Instance.startSpeed = 0;
                    player.Instance.Speed = 0;
                }
            }
            
        }
    }
    IEnumerator IE_DelayActivePlayer()
    {
        yield return new WaitForSeconds(0.3f);
        player.Instance.transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
        player.Instance.isFacingRight = true;
        UI_Loading.Instance.isActivePlayer = false;
    }
    public void CloseBag()
    {
        imgBag.sprite = bagClose.sprite;
    }
    public void SetSpeed()
    {
        Speed = startSpeed;
        skeleton.AnimationState.SetAnimation(0, "Idle", true);
        Debug.Log("da set speed");
    }
    public void ActiveJoystick()
    {
        joystick.gameObject.SetActive(true);
        BackGroundJoyStick.SetActive(false);

    }
    Vector3 EndPos;
    Vector3 StartPos;
    public bool IsStart;
    private void Start()
    {
        skeleton.AnimationState.SetAnimation(0, IdleName, true);
        Option.SetActive(false);
        joystick.gameObject.SetActive(false);

        isFacingRight = true;
        startSpeed = Speed;
        if (!DataPlayer.GetIsNewUser())
        {
            transform.position = DataPlayer.GetStartPosNewUser();
        }
       /* if (DataPlayer.GetListCheckPointPos().Count > 0)
        {
            StartPos = GetCheckPointPosNearest(DataPlayer.GetListCheckPointPos());
            transform.position = StartPos;
        }*/

        StartCoroutine(IE_delay());
        /* else
         {

         }*/
    }
    public void EnableCollider()
    {
        StartCoroutine(IE_delayEnableCollider());
    }
    IEnumerator IE_delayEnableCollider()
    {
        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator IE_delay()
    {
        yield return new WaitForSeconds(0.7f);
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            DataPlayer.ReloaddataTutorial();
            TabToMove();
        }
       /* if(BagManager.Instance.m_RuleController.CurLevel == 6)
        {*/
            transform.position = CheckPointManager.Instance.L_CheckPoint[0].gameObject.transform.localPosition;
       /* }*/
    }
    public Vector3 GetCheckPointPosNearest(List<Vector3> L_CheckPointPos)
    {
        int drag = 0;
        EndPos = DataPlayer.GetPlayerPosWhenExit();
        if (L_CheckPointPos.Count > 0)
        {
            float Mindis = Vector3.Distance(EndPos, L_CheckPointPos[0]);
            for (int i = 0; i < L_CheckPointPos.Count; i++)
            {
                float TempDis = Vector3.Distance(EndPos, L_CheckPointPos[i]);
                if (Mindis > TempDis)
                {
                    Mindis = TempDis;
                    drag = i;
                }
            }
        }
        return L_CheckPointPos[drag];
    }
    public void GetPos()
    {
        if (DataPlayer.GetListCheckPointPos().Count > 0)
        {
            StartPos = GetCheckPointPosNearest(DataPlayer.GetListCheckPointPos());
            transform.position = StartPos;
        }
    }
    private void FixedUpdate()
    {
        MoveCharacter();
    }
    bool isMove = false;
    bool changeAnimIdle = false;
    bool changeAnimMove = false;
    public override void MoveCharacter()
    {
        float MoveX = joystick.Horizontal;

        isMove = (MoveX != 0);

        if (isMove)
        {
            changeAnimIdle = false;
            if (!changeAnimMove)
            {
                changeAnimMove = true;
            }

            transform.position += new Vector3(joystick.Horizontal, joystick.Vertical, 0).normalized * Speed * Time.fixedDeltaTime;

            if (MoveX < 0 && isFacingRight)
            {
                Flip();
            }
            if (MoveX > 0 && !isFacingRight)
            {
                Flip();
            }
        }
        else
        {
            changeAnimMove = false;

            if (!changeAnimIdle)
            {
                changeAnimIdle = true;
                skeleton.AnimationState.SetAnimation(0, IdleName, true);
            }
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        m_CritterFollowController.Index *= -1;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public bool IsZoomOut = false;
    public bool IsZoomIn = false;
    public void TabToMove()
    {
        joystick.gameObject.SetActive(true);
        TabToMoveBtn.gameObject.SetActive(false);
        UI_ShowALlid.SetActive(false);
        IsZoomOut = true;
        /* DataPlayer.SetIsTapToMove(true);
         TutorialManager.Instance.DeSpawn();
         if (!DataPlayer.GetIsSlideToMove())
         {
             UI_Home.Instance.UI_HomeObj.GetComponent<UI_Screen>().slideHand.gameObject.SetActive(true);
             joystick.GetComponent<Canvas>().sortingOrder = 1000;
         }*/
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            CritterFollow.SetActive(false);
            isCheckPoint = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            if (UI_ShowALlid != null)
            {
                UI_ShowALlid.SetActive(false);
                if (CritterFollow != null)
                    CritterFollow.SetActive(true);
                IsZoomOut = true;
                IsZoomIn = false;
                isCheckPoint = false;

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            if (DataPlayer.GetIsCheckDoneTutorial())
            {
                Option.SetActive(false);
                imgBag.sprite = player.Instance.bagClose.sprite;
                ShadowBag.SetActive(true);
                UI_ShowALlid.SetActive(true);
                isPurchaseBagBtn = false;
                IsZoomIn = true;
                IsZoomOut = false;
                ResetTransformCritter();
            }
            else
            {
                IsZoomIn = true;
                IsZoomOut = false;
                ResetTransformCritter();
            }
        }
        a_Frog col_Frog = collision.GetComponent<a_Frog>();

        if (col_Frog != null)
        {
            Debug.LogWarning("A");
            if (UI_Home.Instance.uI_Battle.gameObject.activeInHierarchy || popUpManager.Instance.m_PopHealing.gameObject.activeInHierarchy)
            {
                return;
            }
            GetComponent<Collider2D>().enabled = false;
            PopUpFrog.GetComponent<PopUpFrog>().FrogList.Clear();
            PopUpFrog.SetActive(true);
            PopUpFrog.GetComponent<PopUpFrog>().SetTextConTent(col_Frog.QuantityCoinWithAds.ToString());
            PopUpFrog.GetComponent<PopUpFrog>().QuantityCoinNoAdsTxt.text = col_Frog.QuantityCoinNoAds.ToString();
            PopUpFrog.GetComponent<PopUpFrog>().QuantityCoinWithAdsTxt.text = col_Frog.QuantityCoinWithAds.ToString();
            PopUpFrog.GetComponent<PopUpFrog>().ID = col_Frog.ID;
            PopUpFrog.GetComponent<PopUpFrog>().FrogList.Add(col_Frog);
            col_Frog.m_MoveEnemy.agent.maxSpeed = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Lake col_Lake = collision.gameObject.GetComponent<Lake>();
        if (col_Lake != null)
        {
            if (UI_Home.Instance.uI_Battle.gameObject.activeInHierarchy || popUpManager.Instance.m_popUpFrog.gameObject.activeInHierarchy)
            {
                return;
            }
            PopUpHealingWhenTriggerLake.SetActive(true);
            PopUpHealingWhenTriggerLake m_pop = PopUpHealingWhenTriggerLake.GetComponent<PopUpHealingWhenTriggerLake>();
            m_pop.QuantityHpHealingNoAdsTxt.text = "+" + col_Lake.QuantityHpHealingNoAds.ToString() + "HP";
            //  m_pop.QuantityHpHealingWithAdsTxt.text = "+" + col_Lake.QuantityHpHealingWithAds.ToString() + "HP";
            m_pop.ID = col_Lake.ID;
            m_pop.HpTxt.text = col_Lake.QuantityHpHealingWithAds.ToString() + " HP";
            m_pop.contentxt.text = " <color=\"#FF0000\">" + m_pop.HpTxt.text + "</color>";
            m_pop.LakeList.Add(col_Lake);
        }
    }

    public void ResetTransformCritter()
    {
        m_CritterFollowController.GetComponent<CritterFollowController>().Critter_Follow_Element_01.gameObject.transform.position = transform.position;
        m_CritterFollowController.GetComponent<CritterFollowController>().Critter_Follow_Element_02.gameObject.transform.position = transform.position;
        m_CritterFollowController.GetComponent<CritterFollowController>().Critter_Follow_Element_03.gameObject.transform.position = transform.position;
        m_CritterFollowController.GetComponent<CritterFollowController>().Critter_Follow_Element_04.gameObject.transform.position = transform.position;
    }
    public GameObject ShadowBag;

    public void OnPurchaseBagButton()
    {
        TutorialManager.Instance.DeSpawn();
        DataPlayer.SetIsCheckTapBag(true);
        DataPlayer.SetIsSlideToMove(true);
        /* if (DataPlayer.GetIsCheckTapBag() && !DataPlayer.GetIsCheckTapCloseBag())
         {
             UI_Home.Instance.m_Bag.GetComponent<Canvas>().sortingOrder = 300;
         }*/
        if (!isPurchaseBagBtn)
        {
            isPurchaseBagBtn = true;
            Option.SetActive(true);
            imgBag.sprite = bagOpen.sprite;
            ShadowBag.gameObject.SetActive(false);
            if (!DataPlayer.GetIsTapToBackUITeam())
            {
                BagBtn.GetComponent<Button>().interactable = false;
            }
        }
        else if (isPurchaseBagBtn && DataPlayer.GetIsTapToBackUITeam())
        {
            isPurchaseBagBtn = false;
            Option.SetActive(false);
            imgBag.sprite = bagClose.sprite;
            ShadowBag.gameObject.SetActive(true);
        }
        if (!DataPlayer.GetisCheckTapMergeBtn() && !DataPlayer.GetIsCheckDoneTutorial())
        {
            joystick.GetComponent<Canvas>().sortingOrder = 0;
            TutorialManager.Instance.SpawnHandUIHome(UI_Home.Instance.m_Player.Menu.transform, new Vector3(210, -175));
            UI_Home.Instance.m_Bag.UIMergeBtn.GetComponent<Canvas>().sortingOrder = 10002;
            for (int i = 0; i < TutorialManager.Instance.l_obj.Count; i++)
            {
                TutorialManager.Instance.l_obj[i].transform.localScale = Vector3.one;
            }
        }
        if (DataPlayer.GetIsCheckTapBag() && DataPlayer.GetIsCheckTapCloseBag())
        {
            Debug.LogWarning("da dong");
            if (!DataPlayer.GetIsCheckDoneTutorial())
            {
                BagManager.Instance.m_RuleController.MoveToBoss();
                BagBtn.GetComponent<Button>().interactable = false;
            }

            if (!DataPlayer.GetIsCheckActivetext())
            {
                UI_Home.Instance.m_UIScreen.NotificationBoss.gameObject.SetActive(true);
                DataPlayer.SetIsCheckActivetext(true);

                Destroy(UI_Home.Instance.m_Bag.GetComponent<GraphicRaycaster>());
                Destroy(UI_Home.Instance.m_Bag.GetComponent<Canvas>());
            }
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
            DataPlayer.SetPlayerPosWhenExit(transform.position);
    }

    private void OnApplicationQuit()
    {
        DataPlayer.SetPlayerPosWhenExit(transform.position);

    }
}
