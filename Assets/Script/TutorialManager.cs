using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class TutorialManager : Singleton<TutorialManager>
{
    public player m_Player;

    public GameObject CursorInUIHome;
    public GameObject CursorInUIBattle;

    public Image ShadowOnBackUiTeam;
    public Image Shadow;
    public Image arrow;

    public Transform PosAdd_Slot_To_Team_In_UIHome;
    public Transform PosSlotAllid;
    public Transform PosExitUiTeam;
    public Transform PosTapToMove;
    public Transform PosStartSlide;
    public Transform PosEndSlide;
    public Transform Slot1Allid;

    public GameObject SlideBarTutorial;

    public bool isSlideDone;

    public Vector3 Offset;

    public bool IsTapToAddSlot = false;
    public bool IsTapToAllidOnSlotTeam = false;
    public bool IsTapToBackUITeam = false;
    public bool IstapToMove = false;

    public bool IsCheckTriggerEnemy = false;
    public List<GameObject> l_obj = new List<GameObject>();

    GameObject obj;
    GameObject obj2;
    public void SpawnHandUIHome(Transform parent, Vector3 Offset)
    {
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            return;
        }
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {
            obj = SimplePool.Spawn(CursorInUIHome, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(parent);
            obj.transform.localPosition = Vector3.zero + Offset;
            if (DataPlayer.GetIsTapGotIt())
                obj.transform.localScale = Vector3.zero;
            else
                obj.transform.localScale = Vector3.one;

            l_obj.Add(obj);
        }
    }
    public void SpawnHandUIHomee(Transform parent, Vector3 Offset)
    {
        obj = SimplePool.Spawn(CursorInUIHome, Vector3.zero, Quaternion.identity);
        obj.transform.SetParent(parent);
        obj.transform.position = Vector3.zero;
        obj.transform.position = Offset;
        obj.transform.localScale = Vector3.one;
        l_obj.Add(obj);
    }
    public void SpawnHandUIBattle(Transform parent, Vector3 Offset)
    {
        obj = SimplePool.Spawn(CursorInUIBattle, Vector3.zero, Quaternion.identity);
        obj.GetComponent<Canvas>().sortingLayerName = "UI";
        obj.GetComponent<Canvas>().sortingOrder = 999;
        obj.transform.SetParent(parent);
        obj.transform.localPosition = Vector3.zero + Offset;
        obj.transform.localScale = Vector3.one;
        l_obj.Add(obj);
    }
    public void DeSpawn()
    {
        if (obj != null)
        {
            SimplePool.Despawn(obj);
            Debug.LogWarning("da dess");
         
        }

        /* for(int i = 0; i< l_obj.Count;i++)
         {
             Destroy(l_obj[i].gameObject);
         }*/
    }
    public void DestroyAllObjHandClone()
    {
        for (int i = 0; i < l_obj.Count; i++)
        {
            Destroy(l_obj[i]);
        }
        l_obj.Clear();
    }

    private void Awake()
    {

    }

    private void OnApplicationQuit()
    {
        DataPlayer.SetPlayerPosWhenExit(transform.position);
        if (!DataPlayer.GetIsCheckDoneTutorial())
        {

            PlayerPrefs.DeleteAll();
        }
    }
}
