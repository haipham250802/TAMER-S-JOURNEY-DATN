using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadSceneManager : MonoBehaviour
{
    public Cinemachine.CinemachineConfiner CameraConfiner;
    public CritterFollowController CritterFollowController;
    public BagManager m_bag;
    public string STR_LEVEL;
    public PopUpHealingWhenTriggerLake m_pop;

    private static LoadSceneManager instance;
    public List<GameObject> L_Level_Object = new List<GameObject>();

    public player m_PLayer;
    public GameObject UI_Load;

    public static LoadSceneManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
    }

    public IEnumerator Start()
    {
        int level = DataPlayer.getCurLevel();
        STR_LEVEL = level.ToString();

        var Level_Prefab = Resources.LoadAsync<GameObject>($"LEVEL/{"LEVEL" + STR_LEVEL}");
        while (!Level_Prefab.isDone)
        {
            yield return null;
        }
        if (level == 1)
        {
            VirtualCamera.Instance.ResetListEffect();
            var objectClone = Instantiate(VirtualCamera.Instance.effectDesert);
            objectClone.transform.SetParent(VirtualCamera.Instance.gameObject.transform);
            objectClone.transform.localPosition = new Vector3(-6, 1, 0);
            VirtualCamera.Instance.L_Effect.Add(objectClone);
        }
        else if (level == 4)
        {
            VirtualCamera.Instance.ResetListEffect();
            var objectClone = Instantiate(VirtualCamera.Instance.effectSnow);
            objectClone.transform.SetParent(VirtualCamera.Instance.gameObject.transform);
            objectClone.transform.localPosition = new Vector3(-1.5f, 15.5f, 0);
            VirtualCamera.Instance.L_Effect.Add(objectClone);
        }
        else if (level == 3)
        {
            VirtualCamera.Instance.ResetListEffect();
            var objectClone = Instantiate(VirtualCamera.Instance.effectSea);
            objectClone.transform.SetParent(VirtualCamera.Instance.gameObject.transform);
            objectClone.transform.localPosition = new Vector3(-6, 1, 0);
            VirtualCamera.Instance.L_Effect.Add(objectClone);
        }
        var LevelPrefab = Instantiate((GameObject)Level_Prefab.asset);
        CameraConfiner.m_BoundingShape2D = LevelPrefab.GetComponent<LEVEL>().Bounding_Shape_Collider2D;
        CritterFollowController.polyNav2d = LevelPrefab.GetComponent<LEVEL>().polyNav2D;
        CritterFollowController.Critter_Follow_Element_01.agent.map = LevelPrefab.GetComponent<LEVEL>().polyNav2D;
        CritterFollowController.Critter_Follow_Element_02.agent.map = LevelPrefab.GetComponent<LEVEL>().polyNav2D;
        CritterFollowController.Critter_Follow_Element_03.agent.map = LevelPrefab.GetComponent<LEVEL>().polyNav2D;
        CritterFollowController.Critter_Follow_Element_04.agent.map = LevelPrefab.GetComponent<LEVEL>().polyNav2D;
        m_bag.m_RuleController = LevelPrefab.GetComponent<LEVEL>().ruleController;
        if (L_Level_Object.Count > 0)
        {
            for (int i = 0; i < L_Level_Object.Count; i++)
            {
                Destroy(L_Level_Object[i]);
                L_Level_Object.RemoveAt(i);
            }
        }
        L_Level_Object.Clear();
        StartCoroutine(IE_Remove(LevelPrefab));

    }
    public IEnumerator IE_Remove(GameObject obj)
    {
        yield return null;
        L_Level_Object.Clear();
        L_Level_Object.Add(obj);
        for (int i = 0; i < m_pop.L_objectCloneLake.Count; i++)
        {
            Destroy(m_pop.L_objectCloneLake[i].gameObject);
        }
        m_pop.L_objectCloneLake.Clear();

        popUpManager.Instance.m_PopUpFrog.ClearFrogCry();
    }
    public void LOAD_LEVEL()
    {
        StartCoroutine(IE_LOAD_LEVEL());
    }
    IEnumerator IE_LOAD_LEVEL()
    {
        UI_Home.Instance.m_UIScreen.NotificationBoss.gameObject.SetActive(false);
        var Level_Prefab = Resources.LoadAsync<GameObject>($"LEVEL/{"LEVEL" + STR_LEVEL}");

        while (!Level_Prefab.isDone)
        {
            yield return null;
            if (UI_Loading.Instance.gameObject.activeInHierarchy == true)
                UI_Loading.Instance.Loading();
        }

        var LevelPrefab = Instantiate((GameObject)Level_Prefab.asset);
        CameraConfiner.m_BoundingShape2D = LevelPrefab.GetComponent<LEVEL>().Bounding_Shape_Collider2D;
        CritterFollowController.polyNav2d = LevelPrefab.GetComponent<LEVEL>().polyNav2D;
        // m_PLayer.GetPos();
        m_PLayer.transform.position = CheckPointManager.Instance.L_CheckPoint[0].transform.position;
        CritterFollowController.Critter_Follow_Element_01.agent.map = LevelPrefab.GetComponent<LEVEL>().polyNav2D;
        CritterFollowController.Critter_Follow_Element_02.agent.map = LevelPrefab.GetComponent<LEVEL>().polyNav2D;
        CritterFollowController.Critter_Follow_Element_03.agent.map = LevelPrefab.GetComponent<LEVEL>().polyNav2D;
        CritterFollowController.Critter_Follow_Element_04.agent.map = LevelPrefab.GetComponent<LEVEL>().polyNav2D;
        m_bag.m_RuleController = LevelPrefab.GetComponent<LEVEL>().ruleController;

        
        //   m_PLayer.transform.position = DataPlayer.GetListCheckPointPos()[0];
        m_PLayer.cinemachineCam.transform.position = m_PLayer.transform.position;
        UI_Home.Instance.m_UIScreen.EnableButton();
        if (DataPlayer.GetIsCheckDoneTutorial())
        {
            Destroy(BagManager.Instance.gameObject.GetComponent<GraphicRaycaster>());
            Destroy(BagManager.Instance.gameObject.GetComponent<Canvas>());
        }
        StartCoroutine(IE_Remove(LevelPrefab));
    }

    public void LOAD_MAP_01()
    {
        DataPlayer.SetIsCheckDoneTutorial(true);
        if(DataPlayer.GetIsCheckDoneTutorial())
        {
            UI_Home.Instance.m_UIScreen.EnableButton();
        }
        if (L_Level_Object.Count > 0)
        {
            for (int i = 0; i < L_Level_Object.Count; i++)
            {
                Destroy(L_Level_Object[i].gameObject);
                L_Level_Object.RemoveAt(i);
            }
        }

        VirtualCamera.Instance.ResetListEffect();
        var objectClone = Instantiate(VirtualCamera.Instance.effectDesert);
        objectClone.transform.SetParent(VirtualCamera.Instance.gameObject.transform);
        objectClone.transform.localPosition = new Vector3(-6, 0, 0);
        VirtualCamera.Instance.L_Effect.Add(objectClone);

        UI_Load.SetActive(true);
        STR_LEVEL = "1";

        StartCoroutine(IE_LOAD_LEVEL());
        //   m_PLayer.gameObject.transform.position = m_PLayer.CheckPoint.transform.position;
        m_PLayer.Option.SetActive(false);
        m_PLayer.CloseBag();

        if (DataPlayer.GetIsCheckDoneTutorial())
        {
        }
    }
    public void LOAD_MAP_02()
    {
        VirtualCamera.Instance.ResetListEffect();
        if (L_Level_Object.Count > 0)
        {
            for (int i = 0; i < L_Level_Object.Count; i++)
            {
                Destroy(L_Level_Object[i].gameObject);
                L_Level_Object.RemoveAt(i);
            }
        }

        UI_Load.SetActive(true);
        STR_LEVEL = "2";
        StartCoroutine(IE_LOAD_LEVEL());
        //  m_PLayer.gameObject.transform.position = m_PLayer.CheckPoint.transform.position;
        m_PLayer.Option.SetActive(false);
        m_PLayer.CloseBag();
 
    }
    public void LOAD_MAP_03()
    {
        VirtualCamera.Instance.ResetListEffect();

        if (L_Level_Object.Count > 0)
        {
            for (int i = 0; i < L_Level_Object.Count; i++)
            {
                Destroy(L_Level_Object[i].gameObject);
                L_Level_Object.RemoveAt(i);
            }
        }
        UI_Load.SetActive(true);
        STR_LEVEL = "3";
        StartCoroutine(IE_LOAD_LEVEL());

        // m_PLayer.gameObject.transform.position = m_PLayer.CheckPoint.transform.position;
        m_PLayer.Option.SetActive(false);
        m_PLayer.CloseBag();
    }
    public void LOAD_MAP_04()
    {

        if (L_Level_Object.Count > 0)
        {
            for (int i = 0; i < L_Level_Object.Count; i++)
            {
                Destroy(L_Level_Object[i].gameObject);
                L_Level_Object.RemoveAt(i);
            }
        }

        VirtualCamera.Instance.ResetListEffect();
        GameObject objectClone = Instantiate(VirtualCamera.Instance.effectSnow);
        objectClone.transform.SetParent(VirtualCamera.Instance.gameObject.transform);
        objectClone.transform.localPosition = new Vector3(-1.5f, 15.5f, 0);
        VirtualCamera.Instance.L_Effect.Add(objectClone);
        UI_Load.SetActive(true);
        STR_LEVEL = "4";
        StartCoroutine(IE_LOAD_LEVEL());

        m_PLayer.Option.SetActive(false);
        m_PLayer.CloseBag();
    }
    public void LOAD_MAP_05()
    {
        VirtualCamera.Instance.ResetListEffect();

        if (L_Level_Object.Count > 0)
        {
            for (int i = 0; i < L_Level_Object.Count; i++)
            {
                Destroy(L_Level_Object[i].gameObject);
                L_Level_Object.RemoveAt(i);
            }
        }
        UI_Load.SetActive(true);
        STR_LEVEL = "5";
        StartCoroutine(IE_LOAD_LEVEL());

        // m_PLayer.gameObject.transform.position = m_PLayer.CheckPoint.transform.position;
        m_PLayer.Option.SetActive(false);
        m_PLayer.CloseBag();
    }
    public void LOAD_MAP_06()
    {
        VirtualCamera.Instance.ResetListEffect();

        if (L_Level_Object.Count > 0)
        {
            for (int i = 0; i < L_Level_Object.Count; i++)
            {
                Destroy(L_Level_Object[i].gameObject);
                L_Level_Object.RemoveAt(i);
            }
        }
        UI_Load.SetActive(true);
        STR_LEVEL = "6";
        StartCoroutine(IE_LOAD_LEVEL());

        // m_PLayer.gameObject.transform.position = m_PLayer.CheckPoint.transform.position;
        m_PLayer.Option.SetActive(false);
        m_PLayer.CloseBag();
    }
    public void LOAD_MAP_07()
    {
        VirtualCamera.Instance.ResetListEffect();

        if (L_Level_Object.Count > 0)
        {
            for (int i = 0; i < L_Level_Object.Count; i++)
            {
                Destroy(L_Level_Object[i].gameObject);
                L_Level_Object.RemoveAt(i);
            }
        }
        UI_Load.SetActive(true);
        STR_LEVEL = "7";
        StartCoroutine(IE_LOAD_LEVEL());

        // m_PLayer.gameObject.transform.position = m_PLayer.CheckPoint.transform.position;
        m_PLayer.Option.SetActive(false);
        m_PLayer.CloseBag();
    }
    public void LOAD_MAP_08()
    {
        VirtualCamera.Instance.ResetListEffect();

        if (L_Level_Object.Count > 0)
        {
            for (int i = 0; i < L_Level_Object.Count; i++)
            {
                Destroy(L_Level_Object[i].gameObject);
                L_Level_Object.RemoveAt(i);
            }
        }
        UI_Load.SetActive(true);
        STR_LEVEL = "8";
        StartCoroutine(IE_LOAD_LEVEL());

        // m_PLayer.gameObject.transform.position = m_PLayer.CheckPoint.transform.position;
        m_PLayer.Option.SetActive(false);
        m_PLayer.CloseBag();
    }
    public void LOAD_MAP_09()
    {
        VirtualCamera.Instance.ResetListEffect();

        if (L_Level_Object.Count > 0)
        {
            for (int i = 0; i < L_Level_Object.Count; i++)
            {
                Destroy(L_Level_Object[i].gameObject);
                L_Level_Object.RemoveAt(i);
            }
        }
        UI_Load.SetActive(true);
        STR_LEVEL = "9";
        StartCoroutine(IE_LOAD_LEVEL());

        // m_PLayer.gameObject.transform.position = m_PLayer.CheckPoint.transform.position;
        m_PLayer.Option.SetActive(false);
        m_PLayer.CloseBag();
    }
    public void LOAD_MAP_10()
    {
        VirtualCamera.Instance.ResetListEffect();

        if (L_Level_Object.Count > 0)
        {
            for (int i = 0; i < L_Level_Object.Count; i++)
            {
                Destroy(L_Level_Object[i].gameObject);
                L_Level_Object.RemoveAt(i);
            }
        }
        UI_Load.SetActive(true);
        STR_LEVEL = "10";
        StartCoroutine(IE_LOAD_LEVEL());

        // m_PLayer.gameObject.transform.position = m_PLayer.CheckPoint.transform.position;
        m_PLayer.Option.SetActive(false);
        m_PLayer.CloseBag();
    }
    public void LOAD_MAP_11()
    {
        VirtualCamera.Instance.ResetListEffect();

        if (L_Level_Object.Count > 0)
        {
            for (int i = 0; i < L_Level_Object.Count; i++)
            {
                Destroy(L_Level_Object[i].gameObject);
                L_Level_Object.RemoveAt(i);
            }
        }
        UI_Load.SetActive(true);
        STR_LEVEL = "11";
        StartCoroutine(IE_LOAD_LEVEL());

        // m_PLayer.gameObject.transform.position = m_PLayer.CheckPoint.transform.position;
        m_PLayer.Option.SetActive(false);
        m_PLayer.CloseBag();
    }
}
