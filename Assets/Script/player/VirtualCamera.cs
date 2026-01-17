using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using DG.Tweening;

public class VirtualCamera : Singleton<VirtualCamera>
{
    public float ZoomOutSize;
    public float ZoomStart;
    public float Smoth;

    public CinemachineVirtualCamera virtualCamera;
    public player player;
    public GameObject effectSnow;
    public GameObject effectDesert;
    public GameObject effectSea;

    public List<GameObject> L_Effect = new List<GameObject>();

    private void Start()
    {
        ZoomStart = virtualCamera.m_Lens.OrthographicSize;

    }
    public void ResetListEffect()
    {
        for (int i = 0; i < L_Effect.Count; i++)
        {
            Destroy(L_Effect[i].gameObject);
        }
        L_Effect.Clear();
    }
    public void ZoomOut()
    {

    }
    private void Update()
    {
        if (player.IsZoomOut)
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, ZoomOutSize, Smoth);
        if (player.IsZoomIn)
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, ZoomStart, Smoth);
        if (virtualCamera.m_Lens.OrthographicSize >= ZoomOutSize - 0.05)
        {
            player.IsZoomOut = false;
        }
        if (virtualCamera.m_Lens.OrthographicSize <= ZoomStart + 0.05f)
        {
            player.IsZoomIn = false;
        }
    }
    public bool isActiveTap;
    public bool isActiveUIShowAllid;
    public void MoveToObject(Transform trans, System.Action callback = null)
    {
        if (!trans.gameObject.activeInHierarchy)
            return;
        Debug.Log("da den");
        GetComponent<CinemachineVirtualCamera>().Follow = null;
        GetComponent<CinemachineVirtualCamera>().LookAt = null;
        Vector3 Pos = player.Instance.transform.position;
        Pos.z = transform.position.z;
        Vector3 PosTarget = trans.position;
        PosTarget.z = transform.position.z;
        player.Instance.GetComponent<Collider2D>().enabled = false;
        player.Instance.startSpeed = 0;

        transform.DOMove(PosTarget, 2f).OnStart(() =>
        {
            /* UI_Home.Instance.m_UIScreen.disableButton();
             BagManager.Instance.SelectMap_Btn.interactable = false;
             BagManager.Instance.btnTeam.interactable = false;
             BagManager.Instance.btnMerge.interactable = false;
             BagManager.Instance.Monster_Btn.interactable = false;
             BagManager.Instance.Shop_Button.interactable = false;
             UI_Home.Instance.m_ShowAllid.AddCritterAllidSlotShowAllid1_Btn.interactable = false;
             UI_Home.Instance.m_ShowAllid.AddCritterAllidSlotShowAllid2_Btn.interactable = false;
             UI_Home.Instance.m_ShowAllid.AddCritterAllidSlotShowAllid3_Btn.interactable = false;
             UI_Home.Instance.m_ShowAllid.AddCritterAllidSlotShowAllid4_Btn.interactable = false;
             UI_Home.Instance.m_ShowAllid.PurchaseSlot4.interactable = false;
             isMoving = false;*/
            if (player.Instance.TabToMoveBtn.gameObject.activeInHierarchy)
            {
                player.Instance.TabToMoveBtn.gameObject.SetActive(false);
                isActiveTap = true;
            }

            player.Instance.ShadowBag.SetActive(false);
            BagManager.Instance.gameObject.SetActive(false);

            BagManager.Instance.m_RuleController.objEnemy.SetActive(false);
            UI_Home.Instance.m_UIScreen.HiddenObjScreen();
           /* if (BagManager.Instance.m_RuleController.L_boss[0].gameObject.activeInHierarchy)
            {
                BagManager.Instance.m_RuleController.L_boss[0].isCanAI = true;
                BagManager.Instance.m_RuleController.L_boss[0].m_MoveEnemy.agent.maxSpeed = 0;
            }*/
            if (UI_Home.Instance.m_ShowAllid.gameObject.activeInHierarchy)
            {
                UI_Home.Instance.m_ShowAllid.gameObject.SetActive(false);
                isActiveUIShowAllid = true;
            }
            UI_Home.Instance.m_UiCurrency.LayerMaskObj.SetActive(true);
            /* UI_Home.Instance.m_UiCurrency.Coin.SetActive(false);
             UI_Home.Instance.m_UiCurrency.Gem.SetActive(false);*/
        }).OnUpdate(() =>
        {
            trans.position = new Vector3(trans.position.x, trans.position.y, 0);

        }).OnComplete(() =>
        {
            if (callback != null)
            {
                callback?.Invoke();
            }
            trans.position = new Vector3(trans.position.x, trans.position.y, 0);
            StartCoroutine(IE_Delay(Pos));
        });
    }
    public bool isCanFollow;
    IEnumerator IE_Delay(Vector3 pos)
    {
        yield return new WaitForSeconds(2f);
        transform.DOMove(pos, 2f).OnUpdate(() =>
        {
            GetComponent<CinemachineVirtualCamera>().m_Lens.NearClipPlane = 0.3f;
        }).OnComplete(() =>
        {
            GetComponent<CinemachineVirtualCamera>().LookAt = player.Instance.transform;
            GetComponent<CinemachineVirtualCamera>().Follow = player.Instance.transform;
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            /* UI_Home.Instance.m_UIScreen.EnableButton();
             BagManager.Instance.SelectMap_Btn.interactable = true;
             BagManager.Instance.btnTeam.interactable = true;
             BagManager.Instance.btnMerge.interactable = true;
             BagManager.Instance.Monster_Btn.interactable = true;
             BagManager.Instance.Shop_Button.interactable = true;
             UI_Home.Instance.m_ShowAllid.AddCritterAllidSlotShowAllid1_Btn.interactable = true;
             UI_Home.Instance.m_ShowAllid.AddCritterAllidSlotShowAllid2_Btn.interactable = true;
             UI_Home.Instance.m_ShowAllid.AddCritterAllidSlotShowAllid3_Btn.interactable = true;
             UI_Home.Instance.m_ShowAllid.AddCritterAllidSlotShowAllid4_Btn.interactable = true;
             UI_Home.Instance.m_ShowAllid.PurchaseSlot4.interactable = true;
             isMoving = true;*/

            player.Instance.startSpeed = 5;
            player.Instance.GetComponent<Collider2D>().enabled = true;

            BagManager.Instance.m_RuleController.objEnemy.SetActive(true);
            BagManager.Instance.gameObject.SetActive(true);
            UI_Home.Instance.m_UIScreen.ActiveObjScreen();

            player.Instance.ShadowBag.SetActive(true);
            UI_Home.Instance.m_UiCurrency.LayerMaskObj.SetActive(false);

            Debug.Log(player.Instance.isCheckPoint);
            if (isActiveTap)
                player.Instance.TabToMoveBtn.gameObject.SetActive(true);
            if (DataPlayer.GetIsCheckDoneTutorial() && isActiveUIShowAllid)
                UI_Home.Instance.m_ShowAllid.gameObject.SetActive(true);

            isActiveTap = false;
            isActiveUIShowAllid = false;
/*
            BagManager.Instance.m_RuleController.L_boss[0].isCanAI = false;
            BagManager.Instance.m_RuleController.L_boss[0].m_MoveEnemy.agent.maxSpeed = 3.5f;*/
            /*UI_Home.Instance.m_UiCurrency.Coin.SetActive(true);
            UI_Home.Instance.m_UiCurrency.Gem.SetActive(true);*/

            /*   if (!DataPlayer.GetIsCheckDoneTutorial())
               {
                   DataPlayer.SetIsCheckDoneTutorial(true);
                   LoadSceneManager.Instance.LOAD_MAP_01();
                   DataPlayer.AddListLevel(1);
                   DataPlayer.SetCurLevel(1);
                   UI_Home.Instance.m_UIselectMap.CurrentLevel();
               }*/
        });
    }
}
